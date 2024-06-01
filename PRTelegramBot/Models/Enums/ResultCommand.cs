namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Результат выполнения команды.
    /// </summary>
    public enum ResultCommand
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
        /// Проверка привилегий.
        /// </summary>
        PrivilegeCheck,
        /// <summary>
        /// Не верный тип сообщения.
        /// </summary>
        WrongMessageType,
        /// <summary>
        /// Не верный тип чата.
        /// </summary>
        WrongChatType,
    }
}
