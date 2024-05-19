using PRTelegramBot.Models.Enums;
using System;

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
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

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
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(StringComparison stringComparison, params string[] commands)
            : this(0, CommandComparison.Contains, stringComparison, commands) { }

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
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : base(botId, commandComparison, stringComparison)
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
