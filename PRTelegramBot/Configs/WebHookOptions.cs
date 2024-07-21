using Telegram.Bot.Types;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Параметры telegram бота для работы с WebHook.
    /// </summary>
    public class WebHookOptions
    {
        #region Поля и свойства

        /// <summary>
        /// URL для WebHook.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Сертификат для HTTPS соединений.
        /// </summary>
        public InputFileStream? Certificate { get; set; }

        /// <summary>
        /// IP-адрес для прослушивания входящих соединений.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Максимальное количество одновременных соединений.
        /// </summary>
        public int? MaxConnections { get; set; }

        /// <summary>
        /// Флаг для сброса ожидающих обновлений при запуске.
        /// </summary>
        public bool DropPendingUpdates { get; set; }

        /// <summary>
        /// Секретный токен для верификации запросов от Telegram.
        /// </summary>
        public string? SecretToken { get; set; }

        #endregion
    }
}
