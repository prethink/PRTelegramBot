using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Configs;
using PRTelegramBot.Converters.Inline;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.CommandHandlers;
using PRTelegramBot.Core.Events;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Interfaces.Managers;
using PRTelegramBot.Managers;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.Logger;
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
        /// Обработчик фоновых задач.
        /// </summary>
        public IPRBackgroundTaskRunner BackgroundTaskRunner { get; protected set; }

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
                GetLogger<PRBotBase>().LogErrorInternal(ex);
                return false;
            }
        }

        /// <summary>
        /// Создать Scope для serviceProvider.
        /// </summary>
        /// <returns>Disposable объект, который хранит в себе serviceProvider.</returns>
        public DisposableScope CreateServiceScope()
        {
           var scope = Options?.ServiceProvider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
           return new DisposableScope(scope);
        }

        /// <summary>
        /// Получить serviceProvider.
        /// </summary>
        /// <returns>IServiceProvider или null.</returns>
        public IServiceProvider? GetServiceProvider()
        {
            return Options?.ServiceProvider;
        }

        /// <summary>
        /// Признак, того что есть сервис провайдер в боте.
        /// </summary>
        public bool HasServiceProvider => Options?.ServiceProvider != null;

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
                GetLogger<PRBotBase>().LogErrorInternal(ex);
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
                GetLogger<PRBotBase>().LogErrorInternal(ex);
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
                GetLogger<PRBotBase>().LogErrorInternal(ex);
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
                GetLogger<PRBotBase>().LogWarning($"\nДля генерации Inline-меню используется конвертер по умолчанию, который ограничен длиной callback_data до 64 байт (ограничение Telegram). \nЧтобы обойти это ограничение при создании бота через билдер, используйте:\n.SetInlineMenuConverter(new FileInlineConverter())\nПодробнее про работу конвертеров смотрите в справке {PRConstants.DOCUMENTATION_URL}");

            Options?.InitializeAction?.Invoke();
            BackgroundTaskRunner.Initialize(Options.BackgroundTaskMetadata, Options.BackgroundTasks);
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
                GetLogger<PRBotBase>().LogErrorInternal(ex);
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
        /// Метод выполняемый после запуска бота.
        /// </summary>
        protected virtual Task OnPostStart()
        {
            _ = BackgroundTaskRunner.StartAsync();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Получить текущий сериализатор для бота.
        /// </summary>
        /// <returns>Сериализатор.</returns>
        public IPRSerializer GetSerializer()
        {
            return this.PriorityResolve(Options.PRSerializer, () => new JsonSerializerWrapper());
        }

        /// <summary>
        /// Получить текущий inline конвертер для бота.
        /// </summary>
        /// <returns>Inline конвертер.</returns>
        public IInlineMenuConverter GetInlineConverter()
        {
            return this.PriorityResolve(Options.InlineConverter, () => new TelegramInlineConverter());
        }

        /// <summary>
        /// Получить текущий админ менеджер для бота.
        /// </summary>
        /// <returns>Админ менеджер.</returns>
        public IAdminManager GetAdminManager()
        {
            return this.PriorityResolve(Options.AdminManager, () => this.localAdminManager);
        }

        /// <summary>
        /// Получить логер.
        /// </summary>
        /// <typeparam name="T">Тип логера.</typeparam>
        /// <returns>Логер.</returns>
        public ILogger<T> GetLogger<T>()
        {
            return this.GetLoggerFactory().CreateLogger<T>();
        }

        /// <summary>
        /// Получить логер по Type.
        /// </summary>
        public ILogger GetLogger(Type type)
        {
            if (type == null) 
                throw new ArgumentNullException(nameof(type));

            return this.GetLoggerFactory().CreateLogger(type);
        }

        /// <summary>
        /// Получить фабрику создания логеров.
        /// </summary>
        /// <returns>Фабрика создания логеров.</returns>
        public ILoggerFactory GetLoggerFactory()
        {
            return this.PriorityResolve(Options.LoggerFactory, () => new PRLoggerEventsFactory(this));
        }

        /// <summary>
        /// Получить текущий менеджер белого списка для бота.
        /// </summary>
        /// <returns>Менеджер белого списка.</returns>
        public IWhiteListManager GetWhiteListManager()
        {
            return this.PriorityResolve(Options.WhiteListManager, () => this.localWhiteListManager);
        }

        /// <summary>
        /// Разрешает зависимость с учетом приоритета источников.
        /// </summary>
        /// <typeparam name="T">Тип сервиса.</typeparam>
        /// <param name="optionValue">
        /// Значение, заданное напрямую в настройках бота (имеет наивысший приоритет).
        /// </param>
        /// <param name="fallback">
        /// Фабрика создания значения по умолчанию, используемая если сервис
        /// не найден ни в настройках, ни в DI-контейнере.
        /// </param>
        /// <returns>
        /// Экземпляр сервиса, полученный по следующему приоритету:
        /// <list type="number">
        /// <item><description>Значение из <paramref name="optionValue"/>.</description></item>
        /// <item><description>Сервис из DI-контейнера.</description></item>
        /// <item><description>Результат вызова <paramref name="fallback"/>.</description></item>
        /// </list>
        /// </returns>
        private T PriorityResolve<T>(T? optionValue, Func<T> fallback)
            where T : class
        {
            return optionValue 
                ?? CurrentScope.Services?.GetService<T>()
                ?? fallback();
        }

        /// <summary>
        /// Остановка бота.
        /// </summary>
        public virtual async Task StopAsync(CancellationToken cancellationToken = default)
        {
            isInitialized = false;
            await BackgroundTaskRunner.StopAsync();
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

            BackgroundTaskRunner = new PRBackgroundTaskRunner(this);

            BotClient = Options.Client ?? new TelegramBotClient(Options.Token);
            Events = new TEvents(this);
            InlineClassRegistrar.Register(this);
            InitializeHandlers();
        }

        #endregion
    }
}
