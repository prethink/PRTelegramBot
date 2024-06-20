using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core
{
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
                var webHookOptions = Options as WebHookTelegramOptions;
                if(string.IsNullOrEmpty(webHookOptions.SecretToken))
                    webHookOptions.SecretToken = Generator.RandomSymbols(Generator.Chars.Alphabet, 10);

                await botClient.SetWebhookAsync(
                    url: webHookOptions.Url,
                    certificate: webHookOptions.Certificate,
                    ipAddress: webHookOptions.IpAddress,
                    maxConnections: webHookOptions.MaxConnections,
                    allowedUpdates: Array.Empty<UpdateType>(),
                    dropPendingUpdates: webHookOptions.DropPendingUpdates,
                    secretToken: webHookOptions.SecretToken,
                    cancellationToken: webHookOptions.CancellationToken.Token);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override async Task Stop()
        {
            await botClient.DeleteWebhookAsync(cancellationToken: Options.CancellationToken.Token);
        }

        public async Task<WebhookInfo> GetWebHookInfo()
        {
            return await botClient.GetWebhookInfoAsync();
        }

        #endregion

        #region Конструкторы

        //public PBBotWebHook(Action<WebHookTelegramOptions> options)
        //    : base(options, null) { }

        public PRBotWebHook(TelegramOptions options)
            : base(null, options) { }

        #endregion
    }
}
