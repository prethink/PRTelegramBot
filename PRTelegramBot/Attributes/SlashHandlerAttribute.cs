namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы слэш (/) команд.
    /// </summary>
    public sealed class SlashHandlerAttribute : StringQueryAttribute
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(params string[] commands)
            : this(0, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="comparison">Тип сравнения команды.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(StringComparison comparison, params string[] commands)
            : this(0, comparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, params string[] commands)
            : this(botId, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="comparison">Тип сравнения команды.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, StringComparison comparison, params string[] commands) : base(botId)
        {
            foreach (var command in commands)
            {
                var formatedCommand = command.StartsWith("/") ? command : "/" + command;
                this.commands.Add(formatedCommand);
                this.CompareCommands.Add(command, comparison);
            }
        }

        #endregion
    }
}
