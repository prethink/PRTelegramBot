namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Тип обработки данных.
    /// </summary>
    public enum DataRetrievalMethod
    {
        /// <summary>
        /// Классическая обработка из telegram.bot.
        /// </summary>
        Classic,
        /// <summary>
        /// Обработка данных polling.
        /// </summary>
        Polling,
        /// <summary>
        /// Обработка данных webhook.
        /// </summary>
        WebHook
    }
}
