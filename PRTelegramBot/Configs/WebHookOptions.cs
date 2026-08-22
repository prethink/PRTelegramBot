using Telegram.Bot.Types;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Telegram bot options for working over a webhook.
    /// </summary>
    public class WebHookOptions
    {
        #region Fields and properties

        /// <summary>
        /// The webhook URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Certificate for HTTPS connections.
        /// </summary>
        public InputFileStream? Certificate { get; set; }

        /// <summary>
        /// The IP address to listen for incoming connections on.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Maximum number of simultaneous connections.
        /// </summary>
        public int? MaxConnections { get; set; }

        /// <summary>
        /// Flag that drops pending updates at startup.
        /// </summary>
        public bool DropPendingUpdates { get; set; }

        /// <summary>
        /// Secret token used to verify requests coming from Telegram.
        /// </summary>
        public string? SecretToken { get; set; }

        #endregion
    }
}
