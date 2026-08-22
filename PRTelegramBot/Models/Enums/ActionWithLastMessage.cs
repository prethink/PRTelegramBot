namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Action to perform on the last message, for inline buttons.
    /// </summary>
    public enum ActionWithLastMessage
    {
        /// <summary>
        /// Do nothing.
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// Edit.
        /// </summary>
        Edit,
        /// <summary>
        /// Delete.
        /// </summary>
        Delete
    }
}
