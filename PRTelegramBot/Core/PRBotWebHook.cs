using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Бот для работы через webhook.
    /// </summary>
    public sealed class PRBotWebHook : PRBotBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override DataRetrievalMethod DataRetrieval => DataRetrievalMethod.WebHook;

        /// <inheritdoc />
        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await base.StartAsync(Options.CancellationTokenSource.Token);
                if(string.IsNullOrEmpty(Options.WebHookOptions.SecretToken))
                    Options.WebHookOptions.SecretToken = Generator.RandomSymbols(Generator.Chars.Alphabet, 10);

                await BotClient.SetWebhook(
                    url: Options.WebHookOptions.Url,
                    certificate: Options.WebHookOptions.Certificate,
                    ipAddress: Options.WebHookOptions.IpAddress,
                    maxConnections: Options.WebHookOptions.MaxConnections,
                    allowedUpdates: Array.Empty<UpdateType>(),
                    dropPendingUpdates: Options.WebHookOptions.DropPendingUpdates,
                    secretToken: Options.WebHookOptions.SecretToken,
                    cancellationToken: Options.CancellationTokenSource.Token);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <inheritdoc />
        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await BotClient.DeleteWebhook(cancellationToken: Options.CancellationTokenSource.Token);
        }

        /// <inheritdoc cref="TelegramBotClientExtensions.GetWebhookInfo"/>
        public async Task<WebhookInfo> GetWebHookInfoAsync(CancellationToken cancellationToken = default)
        {
            return await BotClient.GetWebhookInfo(cancellationToken);
        }

        #endregion

        #region Конструкторы

        internal PRBotWebHook(TelegramOptions options)
            : base(null, options) { }

        #endregion
    }
}
