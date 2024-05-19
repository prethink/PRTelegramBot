using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using System;

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
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commands">Команды.</param>
        public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands) 
            : base(botId, commandComparison, stringComparison)
        {
            var bots = BotCollection.Instance.GetBots();
            if(botId != -1)
                bots = bots.Where(bot => bot.BotId == botId).ToList();

            foreach(var bot in bots)
            {
                var dynamicCommand = bot.Options.ReplyDynamicCommands;
                foreach (var command in commands)
                {
                    if (!dynamicCommand.ContainsKey(command))
                    {
                        //TODO: bot.InvokeErrorLog();
                        continue;
                    }
                    this.commands.Add(dynamicCommand[command]);
                }
            }    
        }

        #endregion
    }
}
