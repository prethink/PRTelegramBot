using PRTelegramBot.Configs;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Классический обработчик telegram.bot.
    /// </summary>
    public sealed class PRBot : PRBotBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override DataRetrievalMethod DataRetrieval => DataRetrievalMethod.Classic;

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

                    BotClient.StartReceiving(Handler, Options.ReceiverOptions, Options.CancellationTokenSource.Token);

                    var client = await BotClient.GetMe(Options.CancellationTokenSource.Token);
                    BotName = client?.Username;
                    Events.OnCommonLogInvoke($"Bot {BotName} is running.", "Initialization", ConsoleColor.Yellow);
                    IsWork = true;
                }
                catch (Exception ex)
                {
                    IsWork = false;
                    Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex));
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
                    Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(this, ex));
                }
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