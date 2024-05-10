namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы слэш (/) команд.
    /// </summary>
    public class SlashHandlerAttribute : BaseQueryAttribute<string>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(params string[] commands)
            : this(0, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            foreach (var command in commands)
            {
                var formatedCommand = command.StartsWith("/") ? command : "/" + command;
                this.commands.Add(formatedCommand);
            }
        }

        #endregion
    }
}
