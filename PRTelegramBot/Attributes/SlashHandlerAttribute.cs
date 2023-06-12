namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы слэш (/) команд
    /// </summary>
    public class SlashHandlerAttribute : Attribute
    {
        /// <summary>
        /// Коллекция слеш команд
        /// </summary>
        public List<string> Commands { get; private set; }

        public SlashHandlerAttribute(params string[] commands)
        {
            Commands = commands.ToList();
        }
    }
}
