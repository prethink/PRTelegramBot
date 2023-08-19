namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы слэш (/) команд
    /// </summary>
    public class SlashHandlerAttribute : BaseQueryAttribute
    {
        /// <summary>
        /// Коллекция слеш команд
        /// </summary>
        public List<string> Commands { get; private set; }

        public SlashHandlerAttribute(params string[] commands) : base(0)
        {
            Commands = commands.ToList();
        }

        public SlashHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            Commands = commands.ToList();
        }
    }
}
