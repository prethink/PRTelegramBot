using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    public abstract class PRBotBase
    {
        #region Поля и свойства

        /// <summary>
        /// Имя бота.
        /// </summary>
        public string BotName { get; protected set; }

        /// <summary>
        /// Клиент для telegram бота.
        /// </summary>
        public ITelegramBotClient botClient { get; protected set; }

        /// <summary>
        /// Идетификатор бота в telegram.
        /// </summary>
        public long? TelegramId { get { return botClient.BotId; } }

        /// <summary>
        /// Обработчик для telegram бота
        /// </summary>
        public Handler Handler { get; protected set; }

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
        public RegisterCommands Register { get; protected set; }

        /// <summary>
        /// Тип получения обновления.
        /// </summary>
        public abstract DataRetrievalMethod DataRetrieval { get; }

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
                Handler = new Handler(this);
                Register = new RegisterCommands(Handler);
                return true;
            }
            catch(Exception ex)
            {
                this.Events.OnErrorLogInvoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Очистка очереди команд перед запуском.
        /// </summary>
        protected async Task ClearUpdates()
        {
            try
            {
                var update = await botClient.GetUpdatesAsync();
                foreach (var item in update)
                {
                    var offset = item.Id + 1;
                    await botClient.GetUpdatesAsync(offset);
                }
            }
            catch (Exception ex)
            {
                this.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Запустить бота.
        /// </summary>
        public abstract Task Start();


        /// <summary>
        /// Остановка бота
        /// </summary>
        public abstract Task Stop();

        #endregion

        #region Конструкторы

        protected PRBotBase(Action<TelegramOptions> optionsBuilder, TelegramOptions options)
            : base()
        {
            Options = new TelegramOptions();
            if (optionsBuilder != null)
                optionsBuilder.Invoke(Options);
            else
                Options = options;

            if (string.IsNullOrEmpty(Options.Token))
                throw new Exception("Bot token is empty");

            if (Options.BotId < 0)
                throw new Exception("Bot ID cannot be less than zero");

            BotCollection.Instance.AddBot(this);

            botClient = Options.Client ?? new TelegramBotClient(Options.Token);
            Events = new TEvents(this);
            Handler = new Handler(this);
            Register = new RegisterCommands(Handler);
        }

        #endregion
    }
}
