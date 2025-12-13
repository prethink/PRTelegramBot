using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRTelegramBot.Configs;
using PRTelegramBot.Converters.Inline;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.CommandHandlers;
using PRTelegramBot.Core.Events;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Interfaces.Managers;
using PRTelegramBot.Managers;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Registrars;
using PRTelegramBot.Wrappers;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Базовый класс экземпляра бота.
    /// </summary>
    public abstract class PRBotBase : IHostedService
    {
        #region Поля и свойства

        /// <summary>
        /// Имя бота.
        /// </summary>
        public string BotName { get; protected set; }

        /// <summary>
        /// Клиент для telegram бота.
        /// </summary>
        public ITelegramBotClient BotClient { get; protected set; }

        /// <summary>
        /// Идентификатор бота в telegram.
        /// </summary>
        public long? TelegramId => BotClient.BotId;

        /// <summary>
        /// Обработчик для telegram бота
        /// </summary>
        public IPRUpdateHandler Handler { get; protected set; }

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; protected set; }

        /// <summary>
        /// Параметры бота.
        /// </summary>
        public TelegramOptions Options { get; protected set; }

        /// <summary>
        /// Идентификатор бота.
        /// </summary>
        public long BotId => Options.BotId;

        /// <summary>
        /// События.
        /// </summary>
        public TEvents Events { get; protected set; }

        /// <summary>
        /// Регистрация команд.
        /// </summary>
        public IRegisterCommand Register { get; protected set; }

        /// <summary>
        /// Созданные экземпляры классов для Inline команд.
        /// </summary>
        public Dictionary<Enum, ICallbackQueryCommandHandler> InlineClassHandlerInstances { get; protected set; } = new();

        /// <summary>
        /// Тип получения обновления.
        /// </summary>
        public abstract DataRetrievalMethod DataRetrieval { get; }

        /// <summary>
        /// Добавлять ли бота в коллекцию при создании.
        /// </summary>
        protected abstract bool addBotToCollection { get; }

        /// <summary>
        /// Локальный менеджер администраторов.
        /// </summary>
        protected readonly IAdminManager localAdminManager = new AdminListManager();

        /// <summary>
        /// Локальный менеджер белого списка.
        /// </summary>
        protected readonly IWhiteListManager localWhiteListManager = new WhiteListManager();

        /// <summary>
        /// Проинициализирован ли бот.
        /// </summary>
        private bool isInitialized;

        #endregion

        #region Методы

        /// <summary>
        /// Перезагрузить обработчики.
        /// </summary>
        /// <returns>True - удачно, False - не удачно.</returns>
        public bool ReloadHandlers()
        {
            try
            {
                InitializeHandlers();
                Handler.HotReload();
                return true;
            }
            catch(Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex));
                return false;
            }
        }

        /// <summary>
        /// Инициализация обработчиков.
        /// </summary>
        /// <returns>True - инициализация прошла, False - не прошла.</returns>
        private bool InitializeHandlers()
        {
            try
            {
                if (Handler is null)
                {
                    Handler = Options.UpdateHandler ?? new Handler(this);
                    if(Handler is Handler baseHandler)
                    {
                        Options.MessageHandlers.Add(new SlashCommandHandler());
                        Options.MessageHandlers.Add(new ReplyCommandHandler());
                        Options.MessageHandlers.Add(new ReplyDynamicCommandHandler());

                        Options.CallbackQueryHandlers.Add(new InlineClassInstanceHandler());
                        Options.CallbackQueryHandlers.Add(new InlineCommandHandler());
                    }
                }
                if (Register is null)
                {
                    Register = Options.CommandOptions.RegisterCommand ?? new RegisterCommand();
                    Register.Init(this);
                }

                return true;
            }
            catch (Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(ex));
                return false;
            }
        }

        /// <summary>
        /// Инициализация менеджера администраторов.
        /// </summary>
        private async Task InitializeAdminManager()
        {
            try
            {
                var adminManager = GetAdminManager();
                await adminManager.AddUsers(Options.AdminIds.ToArray());
                await adminManager.Initialize();
            }
            catch(Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(ex));
            }
        }

        /// <summary>
        /// Инициализация менеджера белого списка.
        /// </summary>
        private async Task InitializeWhiteListManager()
        {
            try
            {
                var whiteList = GetWhiteListManager();
                await whiteList.AddUsers(Options.WhiteListIds.ToArray());
                whiteList.SetSettings(Options.WhiteListSettings);
                await whiteList.Initialize();
            }
            catch (Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(ex));
            }
        }

        /// <summary>
        /// Инициализация бота.
        /// </summary>
        public async Task Initialize()
        {
            if(isInitialized)
                return;

            InitializeHandlers();
            await InitializeAdminManager();
            await InitializeWhiteListManager();

            if (GetInlineConverter().GetType() == typeof(TelegramInlineConverter))
                Events.OnCommonLogInvoke($"\nДля генерации Inline-меню используется конвертер по умолчанию, который ограничен длиной callback_data до 64 байт (ограничение Telegram). \nЧтобы обойти это ограничение при создании бота через билдер, используйте:\n.SetInlineMenuConverter(new FileInlineConverter())\nПодробнее про работу конвертеров смотрите в справке {PRConstants.DOCUMENTATION_URL}", "Warning", ConsoleColor.Green);

            Options?.InitializeAction?.Invoke();
            isInitialized = true;
        }

        /// <summary>
        /// Очистка очереди команд перед запуском.
        /// </summary>
        protected async Task ClearUpdatesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var updates = await BotClient.GetUpdates(cancellationToken: cancellationToken);
                foreach (var item in updates)
                {
                    var offset = item.Id + 1;
                    await BotClient.GetUpdates(offset, cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex));
            }
        }

        /// <summary>
        /// Запустить бота.
        /// </summary>
        public virtual async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await Initialize();
        }

        /// <summary>
        /// Получить текущий сериализатор для бота.
        /// </summary>
        /// <returns>Сериализатор.</returns>
        public IPRSerializer GetSerializer()
        {
            return Options.PRSerializer
                ?? CurrentScope.Services?.GetService<IPRSerializer>()
                ?? new JsonSerializerWrapper();
        }

        /// <summary>
        /// Получить текущий inline конвертер для бота.
        /// </summary>
        /// <returns>Inline конвертер.</returns>
        public IInlineMenuConverter GetInlineConverter()
        {
            return Options.InlineConverter
                ?? CurrentScope.Services?.GetService<IInlineMenuConverter>()
                ?? new TelegramInlineConverter();
        }

        /// <summary>
        /// Получить текущий админ менеджер для бота.
        /// </summary>
        /// <returns>Админ менеджер.</returns>
        public IAdminManager GetAdminManager()
        {
            return Options.AdminManager
                ?? CurrentScope.Services?.GetService<IAdminManager>()
                ?? this.localAdminManager;
        }

        /// <summary>
        /// Получить текущий менеджер белого списка для бота.
        /// </summary>
        /// <returns>Менеджер белого списка.</returns>
        public IWhiteListManager GetWhiteListManager()
        {
            return Options.WhiteListManager
                ?? CurrentScope.Services?.GetService<IWhiteListManager>()
                ?? this.localWhiteListManager;
        }

        /// <summary>
        /// Остановка бота.
        /// </summary>
        public virtual Task StopAsync(CancellationToken cancellationToken = default)
        {
            isInitialized = false;
            return Task.CompletedTask;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="optionsBuilder">
        /// Делегат конфигурации, позволяющий программно настроить параметры бота.
        /// Может быть <c>null</c>.  
        /// Если указан, выполняется перед применением объекта <paramref name="options"/>.
        /// </param>
        /// <param name="options">
        /// Объект параметров <see cref="TelegramOptions"/>, содержащий настройки бота.  
        /// Может быть <c>null</c>.  
        /// Если одновременно переданы и <paramref name="optionsBuilder"/>, и <paramref name="options"/>,
        /// применяется комбинация обоих: сначала вызывается <paramref name="optionsBuilder"/>,
        /// затем дополняются или переопределяются параметры из <paramref name="options"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если после выполнения делегата и объединения параметров
        /// не удалось сформировать валидный экземпляр <see cref="TelegramOptions"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если параметры конфигурации содержат некорректные значения
        /// (например, отсутствует токен бота или заданы несовместимые опции).
        /// </exception>
        protected PRBotBase(Action<TelegramOptions>? optionsBuilder, TelegramOptions? options)
        {
            Options = new TelegramOptions();
            if (optionsBuilder is not null)
                optionsBuilder.Invoke(Options);
            else
                Options = options ?? throw new ArgumentNullException($"The arguments to the designer are incorrectly transferred, both arguments ({nameof(options)} and {nameof(optionsBuilder)}) cannot be null.");

            if (string.IsNullOrEmpty(Options.Token))
                throw new ArgumentException("Bot token is empty");

            if (Options.BotId < 0)
                throw new ArgumentException("Bot ID cannot be less than zero");

            if(addBotToCollection)
                BotCollection.Instance.AddBot(this);

            BotClient = Options.Client ?? new TelegramBotClient(Options.Token);
            Events = new TEvents(this);
            InlineClassRegistrar.Register(this);
            InitializeHandlers();
        }

        #endregion
    }
}
