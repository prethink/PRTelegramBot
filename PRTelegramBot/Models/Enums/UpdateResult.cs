namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// The result of handling the update.
    /// </summary>
    public enum UpdateResult
    {
        /// <summary>
        /// Continue execution.
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Not found.
        /// </summary>
        NotFound = 1,
        /// <summary>
        /// Handled.
        /// </summary>
        Handled = 2,
        /// <summary>
        /// Stop processing.
        /// </summary>
        Stop = 3,
        /// <summary>
        /// An error occurred while processing.
        /// </summary>
        Error = 4,
    }
}
