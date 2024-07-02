using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Бот для работы через polling.
    /// </summary>
    public sealed class PRBotPolling : PRBotBase
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

                _ = UpdatePolling();

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

        #region Методы

        /// <summary>
        /// Обработка update через polling.
        /// </summary>
        public async Task UpdatePolling()
        {
            int? offset = Options.ReceiverOptions.Offset;
            while (!Options.CancellationToken.IsCancellationRequested)
            {
                var updates = await botClient.GetUpdatesAsync(offset, Options.ReceiverOptions.Limit, Options.Timeout, Options.ReceiverOptions.AllowedUpdates, Options.CancellationToken.Token);
                foreach (var update in updates)
                {
                    offset = update.Id + 1;
                    try
                    {
                        await Handler.HandleUpdateAsync(botClient, update, Options.CancellationToken.Token);
                    }
                    catch (Exception ex)
                    {
                        this.Events.OnErrorLogInvoke(ex);
                    }
                    if (Options.CancellationToken.IsCancellationRequested) break;
                }
            }
            IsWork = false;
        }

        #endregion

        #region Конструкторы

        internal PRBotPolling(Action<TelegramOptions> options)
            : base(options, null) { }

        internal PRBotPolling(TelegramOptions options)
            : base(null, options) { }

        #endregion
    }
}
