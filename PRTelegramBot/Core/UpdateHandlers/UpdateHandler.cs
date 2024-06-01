using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    public abstract class UpdateHandler
    {
        #region Поля и свойства

        /// <summary>
        /// Telegram bot.
        /// </summary>
        protected PRBot bot;

        /// <summary>
        /// Тип обновления.
        /// </summary>
        public abstract UpdateType TypeUpdate { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка update.
        /// </summary>
        /// <param name="update">Update telegram.</param>
        public abstract Task<ResultUpdate> Handle(Update update);

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        public UpdateHandler(PRBot bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
