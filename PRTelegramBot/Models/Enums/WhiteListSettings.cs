namespace PRTelegramBot.Models.Enums
{
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
