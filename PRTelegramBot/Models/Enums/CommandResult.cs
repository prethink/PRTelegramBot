namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Результат выполнения команды.
    /// </summary>
    public enum CommandResult
    {
        /// <summary>
        /// Продолжить выполенение.
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Выполнено.
        /// </summary>
        Executed,
        /// <summary>
        /// Ошибка.
        /// </summary>
        Error,
        /// <summary>
        /// Внутрення проверка.
        /// </summary>
        InternalCheck
    }
}
