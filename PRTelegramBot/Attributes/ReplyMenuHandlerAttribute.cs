using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для обработки reply методов.
    /// </summary>
    public sealed class ReplyMenuHandlerAttribute 
        : StringQueryAttribute, IBaseQueryAttribute
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(params string[] commands)
            : this(0, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, params string[] commands)
            : this(botId, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, params string[] commands)
            : this(botIds, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(CommandComparison commandComparison, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(StringComparison stringComparison, params string[] commands)
            : this(0, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
            : this(botId, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
            : this(botIds, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this([botId], commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : base(botIds, commandComparison, stringComparison)
        {
            this.commands.AddRange(commands);
        }

        #endregion
    }
}
