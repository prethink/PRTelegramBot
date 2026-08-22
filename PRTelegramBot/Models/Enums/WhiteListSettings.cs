namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Controls at which point the white list is checked.
    /// </summary>
    public enum WhiteListSettings
    {
        /// <summary>
        /// The check performed before the update.
        /// </summary>
        OnPreUpdate = 0,

        /// <summary>
        /// Only reply, slash and inline commands are checked.
        /// </summary>
        OnlyCommands = 1,
    }
}
