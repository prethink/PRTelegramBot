namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для обработки reply методов.
    /// </summary>
    public class ReplyMenuHandlerAttribute : BaseQueryAttribute<string>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(params string[] commands)
            : this(0, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            this.commands.AddRange(commands);
        }

        #endregion
    }
}
