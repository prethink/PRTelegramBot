namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Результат выполнения update.
    /// </summary>
    public enum UpdateResult
    {
        /// <summary>
        /// Продолжить выполнение.
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Не найдено.
        /// </summary>
        NotFound = 1,
        /// <summary>
        /// Обработан.
        /// </summary>
        Handled = 2,
        /// <summary>
        /// Остановить обработку.
        /// </summary>
        Stop = 3,
        /// <summary>
        /// Ошибка при обработке.
        /// </summary>
        Error = 4,
    }
}
