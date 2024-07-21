namespace PRTelegramBot.Interfaces
{
    public interface IBotIdentificatorAttribute
    {
        /// <summary>
        /// Идентификаторы ботов.
        /// </summary>
        public List<long> BotIds { get; }
    }
}
