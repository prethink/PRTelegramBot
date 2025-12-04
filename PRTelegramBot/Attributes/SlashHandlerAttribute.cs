using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы слэш (/) команд.
    /// </summary>
    public sealed class SlashHandlerAttribute : StringQueryAttribute
    {

        #region Поля и свойства

        /// <summary>
        /// Символ разделителя.
        /// </summary>
        public char SplitChar { get; protected set; } = default;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(char splitChar, params string[] commands)
            : this(0, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, splitChar, commands) {  }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(params string[] commands)
            : this(0, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, params string[] commands)
            : this(botId, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, char splitChar, params string[] commands)
            : this(botId, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, params string[] commands)
            : this(botIds, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, default, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, char splitChar, params string[] commands)
            : this(botIds, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, char splitChar, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, char splitChar, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, default, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, CommandComparison commandComparison, char splitChar, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(StringComparison stringComparison, params string[] commands)
            : this(0, CommandComparison.Contains, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(StringComparison stringComparison, char splitChar, params string[] commands)
            : this(0, CommandComparison.Contains, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
            : this(botId, CommandComparison.Contains, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, StringComparison stringComparison, char splitChar, params string[] commands)
            : this(botId, CommandComparison.Contains, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
            : this(botIds, CommandComparison.Contains, stringComparison, default, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, StringComparison stringComparison, char splitChar, params string[] commands)
            : this(botIds, CommandComparison.Contains, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, char splitChar, params string[] commands)
            : this(0, commandComparison, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this([botId], commandComparison, stringComparison, default, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, char splitChar, params string[] commands)
            : this([botId], commandComparison, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="splitChar">Символ разделителя.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, char splitChar, params string[] commands)
            : base(botIds, commandComparison, stringComparison)
        {
            this.SplitChar = splitChar;

            foreach (var command in commands)
            {
                var formatedCommand = command.StartsWith('/') 
                    ? command 
                    : "/" + command;

                this.commands.Add(formatedCommand);
            }
        }

        #endregion
    }
}
