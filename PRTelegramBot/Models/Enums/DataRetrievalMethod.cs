namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Тип обработки данных.
    /// </summary>
    public enum DataRetrievalMethod
    {
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
