using PRTelegramBot.Configs;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Core
{
    public sealed class PRBot : IPRBot
    {
        #region Поля и свойства

        /// <summary>
        /// Имя бота.
        /// </summary>
        public string BotName { get; private set; }

        /// <summary>
        /// Клиент для telegram бота.
        /// </summary>
        public ITelegramBotClient botClient { get; private set; }

        /// <summary>
        /// Идетификатор бота в telegram.
        /// </summary>
        public long? TelegramId { get { return botClient.BotId; } }

        /// <summary>
        /// Обработчик для telegram бота
        /// </summary>
        public Handler Handler {  get; private set; }

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; private set; }

        /// <summary>
        /// Параметры бота.
        /// </summary>
        public TelegramOptions Options { get; init; } = new TelegramOptions();

        /// <summary>
        /// Идентификатор бота.
        /// </summary>
        public long BotId => Options.BotId;

        /// <summary>
        /// События.
        /// </summary>
        public TEvents Events { get; private set; }

        /// <summary>
        /// Регистрация команд.
        /// </summary>
        public RegisterCommands Register { get ; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Запуск бота
        /// </summary>
        public async Task Start()
        {
            try
            {
                if (Options.ClearUpdatesOnStart)
                    await ClearUpdates();

                botClient.StartReceiving(Handler, Options.ReceiverOptions);

                var client = await botClient.GetMeAsync();
                BotName = client?.Username;
                this.Events.OnCommonLogInvoke($"Bot {BotName} is running.", "Initialization", ConsoleColor.Yellow);
                IsWork = true;
            }
            catch (Exception ex)
            {
                IsWork = false;
                this.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Остановка бота
        /// </summary>
        public async Task Stop()
        {
            try
            {
                Options. cts.Cancel();

                await Task.Delay(3000);
                IsWork = false;
            }
            catch (Exception ex)
            {
                this.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Очистка очереди команд перед запуском
        /// </summary>
        private async Task ClearUpdates()
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

        #endregion

        #region Конструкторы

        public PRBot(Action<TelegramOptions> options)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), null) { }

        public PRBot(Action<TelegramOptions> options, IServiceProvider serviceProvider)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions)
            : this(options, null, receiverOptions, new CancellationTokenSource(), null) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions, IServiceProvider serviceProvider)
            : this(options, null, receiverOptions, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(Action<TelegramOptions> options, CancellationTokenSource cancellationToken)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, null) { }

        public PRBot(Action<TelegramOptions> options, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, serviceProvider) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken)
            : this(options, null, receiverOptions, cancellationToken, null) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(options, null, receiverOptions, cancellationToken, serviceProvider) { }

        public PRBot(TelegramOptions options)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), null) { }

        public PRBot(TelegramOptions options, IServiceProvider serviceProvider)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions)
            : this(null, options, receiverOptions, new CancellationTokenSource(), null) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions, IServiceProvider serviceProvider)
            : this(null, options, receiverOptions, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(TelegramOptions options, CancellationTokenSource cancellationToken)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, null) { }

        public PRBot(TelegramOptions options, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, serviceProvider) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken)
            : this(null, options, receiverOptions, cancellationToken, null) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(null, options, receiverOptions, cancellationToken, serviceProvider) { }

        private PRBot(Action<TelegramOptions> optionsBuilder, TelegramOptions options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
        {
            if (optionsBuilder != null)
                optionsBuilder.Invoke(Options);
            else
                Options = options;

            if (string.IsNullOrEmpty(Options.Token))
                throw new Exception("Bot token is empty");

            if (Options.BotId < 0)
                throw new Exception("Bot ID cannot be less than zero");

            botClient = new TelegramBotClient(Options.Token);
            BotCollection.Instance.AddBot(this);
            Events = new TEvents(this);
            Handler = new Handler(this);
            Register = new RegisterCommands(Handler);
        }

        #endregion
    }
}