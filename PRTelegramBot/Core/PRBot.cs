using PRTelegramBot.Configs;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    public sealed class PRBot : PRBotBase
    {
        #region Базовый класс

        public override DataRetrievalMethod DataRetrieval { 
            get 
            { 
                return DataRetrievalMethod.Polling; 
            } 
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        public override async Task Start()
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
        public override async Task Stop()
        {
            try
            {
                Options.CancellationToken.Cancel();

                await Task.Delay(3000);
                IsWork = false;
            }
            catch (Exception ex)
            {
                this.Events.OnErrorLogInvoke(ex);
            }
        }

        #endregion

        #region Конструкторы

        public PRBot(Action<TelegramOptions> options)
            : this(options, null) { }

        public PRBot(TelegramOptions options)
            : this(null, options) { }

        private PRBot(Action<TelegramOptions> optionsBuilder, TelegramOptions options) 
            : base()
        {
            if (optionsBuilder != null)
                optionsBuilder.Invoke(Options);
            else
                Options = options;

            if (string.IsNullOrEmpty(Options.Token))
                throw new Exception("Bot token is empty");

            if (Options.BotId < 0)
                throw new Exception("Bot ID cannot be less than zero");

            BotCollection.Instance.AddBot(this);

            botClient   = new TelegramBotClient(Options.Token);
            Events      = new TEvents(this);
            Handler     = new Handler(this);
            Register    = new RegisterCommands(Handler);
        }

        #endregion
    }
}