namespace PRTelegramBot.Models.Enums
{
    public enum WhiteListSettings
    {
        /// <summary>
        /// Проверка перед update.
        /// </summary>
        OnPreUpdate = 0,

        /// <summary>
        /// Проверка только Reply, Slash, Inline команд.
        /// </summary>
        OnlyCommands = 1,
    }
}
