namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс хранилища команд.
    /// </summary>
    /// <typeparam name="T">Тип команд.</typeparam>
    internal interface ICommandStore<T>
    {
        /// <summary>
        /// Команды.
        /// </summary>
        IEnumerable<T> Commands { get; }
    }
}
