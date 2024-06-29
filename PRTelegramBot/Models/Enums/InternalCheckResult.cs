namespace PRTelegramBot.Models.Enums
{
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

    }
}
