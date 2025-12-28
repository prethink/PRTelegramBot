using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для обработки dynamic reply методов.
    /// </summary>
    public sealed class ReplyMenuDynamicHandlerAttribute : StringQueryAttribute
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(params string[] commands)
            : this(0, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, params string[] commands)
            : this(botId, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long[] botIds, params string[] commands)
            : this(botIds, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(StringComparison stringComparison, params string[] commands)
            : this(0, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
            : this(botId, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
            : this(botIds, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands) 
            : this([botId], commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : base(botIds, commandComparison, stringComparison)
        {
            var bots = BotCollection.Instance.GetBots();
            if (!botIds.Any(x => x == -1))
                bots = bots.Where(bot => botIds.Contains(bot.BotId)).ToList();

            foreach (var bot in bots)
            {
                var dynamicCommand = bot.Options.ReplyDynamicCommands;
                foreach (var command in commands)
                {
                    if (!dynamicCommand.ContainsKey(command))
                    {
                        var exception = new ArgumentException($"Bot with id {bot.BotId} not contains dynamic command {command}");
                        bot.GetLogger<ReplyMenuDynamicHandlerAttribute>().LogErrorInternal(exception);
                        continue;
                    }
                    this.commands.Add(dynamicCommand[command]);
                }
            }
        }

        #endregion
    }
}
