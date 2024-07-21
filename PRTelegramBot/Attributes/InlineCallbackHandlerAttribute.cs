using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для inline команд.
    /// </summary>
    public sealed class InlineCallbackHandlerAttribute<T> : BaseQueryAttribute<Enum>
        where T : Enum
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public InlineCallbackHandlerAttribute(params T[] commands)
            : this(0, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public InlineCallbackHandlerAttribute(long botId, params T[] commands) 
            : this([botId], commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commands">Команды.</param>
        public InlineCallbackHandlerAttribute(long[] botIds, params T[] commands)
            : base(botIds, CommandComparison.Equals)
        {
            foreach (var command in commands)
                this.commands.Add((Enum)command);
        }

        #endregion
    }
}
