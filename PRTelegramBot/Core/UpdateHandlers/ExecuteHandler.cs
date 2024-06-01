using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    public abstract class ExecuteHandler<TKey> : UpdateHandler
    {

        #region Поля и свойства

        public override UpdateType TypeUpdate => UpdateType.Message;

        #endregion

        #region Методы

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected virtual async Task<ResultCommand> ExecuteMethod(Update update, CommandHandler handler)
        {
            var @delegate = handler.Command;

            var result = InternalCheck(update, handler);
            if (result != ResultCommand.Continue)
                return result;

            await @delegate(bot.botClient, update);
            return ResultCommand.Executed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected abstract ResultCommand InternalCheck(Update update, CommandHandler handler);

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        protected ExecuteHandler(PRBot bot)
            : base(bot) { }

        #endregion
    }
}
