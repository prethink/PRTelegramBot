using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    public sealed class PRBot : PRBotBase
    {
        #region Базовый класс

        public override DataRetrievalMethod DataRetrieval 
        { 
            get 
            { 
                return DataRetrievalMethod.Polling; 
            } 
        }

        public override async Task Start()
        {
            try
            {
                await base.Start();
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

        internal PRBot(Action<TelegramOptions> options)
            : base(options, null) { }

        internal PRBot(TelegramOptions options)
            : base(null, options) { }

        #endregion
    }
}