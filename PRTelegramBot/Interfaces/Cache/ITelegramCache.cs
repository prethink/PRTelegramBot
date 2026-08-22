namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Cache for the data.
    /// </summary>
    public interface ITelegramCache
    {
        /// <summary>
        /// Clears the data
        /// </summary>
        /// <returns>True if the data was cleared; False otherwise.</returns>
        public bool ClearData();
    }
}
