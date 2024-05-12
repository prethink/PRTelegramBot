namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для обработки reply методов.
    /// </summary>
    public class ReplyMenuHandlerAttribute : BaseQueryAttribute<string>
    {
        #region Поля и свойства

        /// <summary>
        /// Тип сравнения команд.
        /// </summary>
        public Dictionary<string, StringComparison> CompareCommands { get; private set; } = new Dictionary<string, StringComparison>();

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(params string[] commands)
            : this(0, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="comparison">Тип сравнения команды.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(StringComparison comparison, params string[] commands)
            : this(0, comparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, params string[] commands)
            : this(botId, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, StringComparison comparison, params string[] commands) : base(botId)
        {
            this.commands.AddRange(commands);
            foreach (string command in commands)
            {
                this.CompareCommands.Add(command, comparison);
            }
        }

        #endregion
    }
}
