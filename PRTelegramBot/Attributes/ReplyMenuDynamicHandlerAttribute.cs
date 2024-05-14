using PRTelegramBot.Core;

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
            : this(0, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="comparison">Тип сравнения команды.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(StringComparison comparison, params string[] commands)
            : this(0, comparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, params string[] commands)
            : this(botId, StringComparison.OrdinalIgnoreCase, commands) { }


        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, StringComparison comparison, params string[] commands) : base(botId)
        {
            this.commands.AddRange(commands);
            var bot = BotCollection.Instance.GetBotOrNull(botId);
            if (bot == null)
                return;

            var dynamicCommand = bot.Options.ReplyDynamicCommands;
            foreach (var command in commands)
            {
                if (!dynamicCommand.ContainsKey(command))
                {
                    //bot.InvokeErrorLog();
                    continue;
                }
                this.commands.Add(dynamicCommand[command]);
                this.CompareCommands.Add(dynamicCommand[command], comparison);
            }
        }

        #endregion
    }
}
