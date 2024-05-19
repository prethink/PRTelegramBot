namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Как сравнивать команду.
    /// </summary>
    public enum CommandComparison
    {
        /// <summary>
        /// Точное совпадение команды.
        /// </summary>
        Equals = 0,
        /// <summary>
        /// Содержит команду.
        /// </summary>
        Contains
    }
}
