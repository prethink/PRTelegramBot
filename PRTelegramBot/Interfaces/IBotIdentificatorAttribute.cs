namespace PRTelegramBot.Interfaces
{
    public interface IBotIdentificatorAttribute
    {
        /// <summary>
        /// Bot identifiers.
        /// </summary>
        public List<long> BotIds { get; }
    }
}
