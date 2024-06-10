using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    public sealed class PBBotWebHook : PRBotBase
    {
        #region Базовый класс

        public new TelegramOptions Options { get; protected set; } = new WebHookTelegramOptions();

        public override DataRetrievalMethod DataRetrieval
        {
            get
            {
                return DataRetrievalMethod.WebHook;
            }
        }

        public override async Task Start()
        {
            var webHookOptions = Options as WebHookTelegramOptions;
            await botClient.SetWebhookAsync(
                url: webHookOptions.Url,
                certificate: webHookOptions.Certificate,
                ipAddress: webHookOptions.IpAddress,
                maxConnections: webHookOptions.MaxConnections,
                allowedUpdates: webHookOptions.ReceiverOptions.AllowedUpdates,
                dropPendingUpdates: webHookOptions.DropPendingUpdates,
                secretToken: webHookOptions.SecretToken,
                cancellationToken: webHookOptions.CancellationToken.Token);
        }

        public override async Task Stop()
        {
            await botClient.DeleteWebhookAsync(cancellationToken: Options.CancellationToken.Token);
        }

        #endregion

        #region Конструкторы

        //public PBBotWebHook(Action<WebHookTelegramOptions> options)
        //    : base(options, null) { }

        public PBBotWebHook(WebHookTelegramOptions options)
            : base(null, options) { }

        #endregion
    }
}
