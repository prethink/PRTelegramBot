using PRTelegramBot.Configs;
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
        public override async Task StartAsync(CancellationToken cancellationToken = default)
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
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex, cancellationToken));
            }
        }

        /// <inheritdoc />
        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Options.CancellationTokenSource.Cancel();
                await Task.Delay(3000, CancellationToken.None);
                IsWork = false;
            }
            catch (Exception ex)
            {
                Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex, cancellationToken));
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка update через polling.
        /// </summary>
        private async Task UpdatePollingAsync(CancellationToken cancellationToken)
        {
            int? offset = Options.ReceiverOptions.Offset;
            while (!Options.CancellationTokenSource.IsCancellationRequested)
            {
                var updates = await BotClient.GetUpdates(offset, Options.ReceiverOptions.Limit, Options.Timeout, Options.ReceiverOptions.AllowedUpdates, Options.CancellationTokenSource.Token);
                foreach (var update in updates)
                {
                    offset = update.Id + 1;
                    try
                    {
                        await Handler.HandleUpdateAsync(BotClient, update, Options.CancellationTokenSource.Token);
                    }
                    catch (Exception ex)
                    {
                        Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex, cancellationToken));
                    }
                    if (Options.CancellationTokenSource.IsCancellationRequested) break;
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
