namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the command store.
    /// </summary>
    /// <typeparam name="T">Command type.</typeparam>
    internal interface ICommandStore<T>
    {
        /// <summary>
        /// Commands.
        /// </summary>
        IEnumerable<T> Commands { get; }
    }
}
