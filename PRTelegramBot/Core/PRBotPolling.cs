using PRTelegramBot.Configs;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Бот для работы через polling.
    /// </summary>
    public sealed class PRBotPolling : PRBotBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override DataRetrievalMethod DataRetrieval => DataRetrievalMethod.Polling;

        /// <inheritdoc />
        protected override bool addBotToCollection => true;

        /// <inheritdoc />
        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            using (var scope = new BotDataScope(this))
            {
                try
                {
                    await base.StartAsync(Options.CancellationTokenSource.Token);
                    if (Options.ClearUpdatesOnStart)
                        await ClearUpdatesAsync(Options.CancellationTokenSource.Token);

                    _ = UpdatePollingAsync(Options.CancellationTokenSource.Token);

                    var client = await BotClient.GetMe(Options.CancellationTokenSource.Token);
                    BotName = client?.Username;
                    Events.OnCommonLogInvoke($"Bot {BotName} is running.", "Initialization", ConsoleColor.Yellow);
                    IsWork = true;
                }
                catch (Exception ex)
                {
                    IsWork = false;
                    Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(ex));
                }
            }
        }

        /// <inheritdoc />
        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            using (var scope = new BotDataScope(this))
            {
                try
                {
                    await base.StopAsync(cancellationToken);
                    Options.CancellationTokenSource.Cancel();
                    await Task.Delay(3000, CancellationToken.None);
                    IsWork = false;
                }
                catch (Exception ex)
                {
                    Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(ex));
                }
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка update через polling.
        /// </summary>
        private async Task UpdatePollingAsync(CancellationToken cancellationToken)
        {
            try
            {
                int? offset = Options.ReceiverOptions.Offset;
                while (!Options.CancellationTokenSource.IsCancellationRequested)
                {
                    var updates = await BotClient.GetUpdates(offset, Options.ReceiverOptions.Limit, Options.Timeout, Options.ReceiverOptions.AllowedUpdates, Options.CancellationTokenSource.Token);
                    foreach (var update in updates)
                    {
                        offset = update.Id + 1;
                        await Handler.HandleUpdateAsync(BotClient, update, Options.CancellationTokenSource.Token);

                        if (Options.CancellationTokenSource.IsCancellationRequested) break;
                    }
                }
                IsWork = false;
            }
            catch (Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex, cancellationToken));
            }
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
