namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Результат выполнения команды.
    /// </summary>
    public enum ResultCommand
    {
        /// <summary>
        /// Не найдено.
        /// </summary>
        NotFound = 0,
        /// <summary>
        /// Выполнено.
        /// </summary>
        Executed,
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
