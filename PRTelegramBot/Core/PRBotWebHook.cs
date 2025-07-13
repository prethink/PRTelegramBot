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

        public override DataRetrievalMethod DataRetrieval
        {
            get
            {
                return DataRetrievalMethod.WebHook;
            }
        }

        public override async Task Start()
        {
            try
            {
                await base.Start();
                if(string.IsNullOrEmpty(Options.WebHookOptions.SecretToken))
                    Options.WebHookOptions.SecretToken = Generator.RandomSymbols(Generator.Chars.Alphabet, 10);

                await botClient.SetWebhook(
                    url: Options.WebHookOptions.Url,
                    certificate: Options.WebHookOptions.Certificate,
                    ipAddress: Options.WebHookOptions.IpAddress,
                    maxConnections: Options.WebHookOptions.MaxConnections,
                    allowedUpdates: Array.Empty<UpdateType>(),
                    dropPendingUpdates: Options.WebHookOptions.DropPendingUpdates,
                    secretToken: Options.WebHookOptions.SecretToken,
                    cancellationToken: Options.CancellationToken.Token);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override async Task Stop()
        {
            await botClient.DeleteWebhook(cancellationToken: Options.CancellationToken.Token);
        }

        public async Task<WebhookInfo> GetWebHookInfo()
        {
            return await botClient.GetWebhookInfo();
        }

        #endregion

        #region Конструкторы

        internal PRBotWebHook(TelegramOptions options)
            : base(null, options) { }

        #endregion
    }
}
