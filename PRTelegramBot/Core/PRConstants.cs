namespace PRTelegramBot.Core
{
    /// <summary>
    /// Константы PRTelegramBot.
    /// </summary>
    public class PRConstants
    {
        /// <summary>
        /// Адрес документации.
        /// </summary>
        public const string DOCUMENTATION_URL = "https://prethink.gitbook.io/prtelegrambot/";

        /// <summary>
        /// Максимальный допустимый размер данных для обработки.
        /// </summary>
        public const int MAX_SIZE_CALLBACK_DATA = 64;

        /// <summary>
        /// Максимальный размер текста отправляемого сообщения.
        /// </summary>
        public const int MAX_MESSAGE_LENGTH = 4000;

        /// <summary>
        /// Идентификатор, который указывается для использования для всех ботов.
        /// </summary>
        public const long ALL_BOTS_ID = -1;

        /// <summary>
        /// Значение означающее бесконечность.
        /// </summary>
        public const int INFINITY = -1;
    }
}
