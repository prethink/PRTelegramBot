namespace PRTelegramBot.Core
{
    /// <summary>
    /// PRTelegramBot constants.
    /// </summary>
    public class PRConstants
    {
        /// <summary>
        /// Documentation address.
        /// </summary>
        public const string DOCUMENTATION_URL = "https://prethink.gitbook.io/prtelegrambot/";

        /// <summary>
        /// Maximum data size that can be processed.
        /// </summary>
        public const int MAX_SIZE_CALLBACK_DATA = 64;

        /// <summary>
        /// Maximum text size of an outgoing message.
        /// </summary>
        public const int MAX_MESSAGE_LENGTH = 4000;

        /// <summary>
        /// The identifier used to target all bots.
        /// </summary>
        public const long ALL_BOTS_ID = -1;

        /// <summary>
        /// The value that stands for infinity.
        /// </summary>
        public const int INFINITY = -1;
    }
}
