using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Обработчик выполнения обновление.
    /// </summary>
    public abstract class ExecuteHandler : UpdateHandler
    {
        #region Поля и свойства

        public override UpdateType TypeUpdate => UpdateType.Message;

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить метод.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения команды.</returns>
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
        /// Внутрення проверка для <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения проверки.</returns>
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
