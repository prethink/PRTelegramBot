namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Результат выполнения update.
    /// </summary>
    public enum ResultUpdate
    {
        /// <summary>
        /// Продолжить выполнение.
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Обработан.
        /// </summary>
        Handled = 1,
        /// <summary>
        /// Остановить обработку.
        /// </summary>
        Stop = 2,
    }
}
