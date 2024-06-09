using Telegram.Bot.Types;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Параметры telegram бота для работы с WebHook.
    /// </summary>
    public class WebHookTelegramOptions : TelegramOptions
    {
        #region Поля и свойства

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InputFileStream? Certificate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MaxConnections { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? DropPendingUpdates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? SecretToken { get; set; }

        #endregion
    }
}
