namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Внутрення проверка в командах перед их выполнением.
    /// </summary>
    public enum InternalCheckResult
    {
        /// <summary>
        /// Проверка пройдена.
        /// </summary>
        Passed = 0,
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
        /// <summary>
        /// Пользователь не в белом списке.
        /// </summary>
        NotInWhiteList,
        /// <summary>
        /// Кастомный ответ.
        /// </summary>
        Custom,
    }
}
