namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Contract of an attribute that limits a handler to specific bots.
    /// </summary>
    public interface IBotIdentificatorAttribute
    {
        /// <summary>
        /// Bot identifiers.
        /// </summary>
        public List<long> BotIds { get; }
    }
}
