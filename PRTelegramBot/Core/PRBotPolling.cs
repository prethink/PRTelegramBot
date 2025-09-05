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

        /// <inheritdoc />
        public override DataRetrievalMethod DataRetrieval => DataRetrievalMethod.Polling;

        /// <inheritdoc />
        public override async Task Start(CancellationToken cancellationToken = default)
        {
            try
            {
                await base.Start(Options.CancellationTokenSource.Token);
                if (Options.ClearUpdatesOnStart)
                    await ClearUpdatesAsync(Options.CancellationTokenSource.Token);

                _ = UpdatePolling(Options.CancellationTokenSource.Token);

                var client = await BotClient.GetMe(Options.CancellationTokenSource.Token);
                BotName = client?.Username;
                Events.OnCommonLogInvoke($"Bot {BotName} is running.", "Initialization", ConsoleColor.Yellow);
                IsWork = true;
            }
            catch (Exception ex)
            {
                IsWork = false;
                Events.OnErrorLogInvoke(ex);
            }
        }

        /// <inheritdoc />
        public override async Task Stop(CancellationToken cancellationToken = default)
        {
            try
            {
                Options.CancellationTokenSource.Cancel();
                await Task.Delay(3000, CancellationToken.None);
                IsWork = false;
            }
            catch (Exception ex)
            {
                Events.OnErrorLogInvoke(ex);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка update через polling.
        /// </summary>
        public async Task UpdatePolling(CancellationToken cancellationToken = default)  // TODO: добавить постфикс Async. P.S. метод больше похож на приватный. Если приватный - убрать default для cT.
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
                        Events.OnErrorLogInvoke(ex);
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
