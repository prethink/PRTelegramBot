namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// The data handling type.
    /// </summary>
    public enum DataRetrievalMethod
    {
        /// <summary>
        /// The classic handling from telegram.bot.
        /// </summary>
        Classic,
        /// <summary>
        /// Polling data handling.
        /// </summary>
        Polling,
        /// <summary>
        /// Webhook data handling.
        /// </summary>
        WebHook,
        /// <summary>
        /// Stub.
        /// </summary>
        Dummy,
    }
}
