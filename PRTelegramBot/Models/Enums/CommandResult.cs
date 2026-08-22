namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// The result of executing the command.
    /// </summary>
    public enum CommandResult
    {
        /// <summary>
        /// Continue execution.
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Done.
        /// </summary>
        Executed,
        /// <summary>
        /// Error.
        /// </summary>
        Error,
        /// <summary>
        /// Internal check.
        /// </summary>
        InternalCheck
    }
}
