namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Результат выполнение шага.
    /// </summary>
    public enum ExecuteStepResult
    {
        /// <summary>
        /// Шаг выполнен удачно.
        /// </summary>
        Success,
        /// <summary>
        /// Не удалось выполнить шаг.
        /// </summary>
        Failure,
        /// <summary>
        /// Истекло время возможного выполнения шага.
        /// </summary>
        ExpiredTime
    }
}
