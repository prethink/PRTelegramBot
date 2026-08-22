namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Internal check performed on commands before they run.
    /// </summary>
    public enum InternalCheckResult
    {
        /// <summary>
        /// The check passed.
        /// </summary>
        Passed = 0,
        /// <summary>
        /// Privilege check.
        /// </summary>
        PrivilegeCheck,
        /// <summary>
        /// Invalid message type.
        /// </summary>
        WrongMessageType,
        /// <summary>
        /// Invalid chat type.
        /// </summary>
        WrongChatType,
        /// <summary>
        /// The user is not on the white list.
        /// </summary>
        NotInWhiteList,
        /// <summary>
        /// A custom response.
        /// </summary>
        Custom,
    }
}
