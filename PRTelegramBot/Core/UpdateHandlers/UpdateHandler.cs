using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Базовый обработчик update.
    /// </summary>
    public abstract class UpdateHandler
    {
        #region Поля и свойства

        /// <summary>
        /// Telegram bot.
        /// </summary>
        protected PRBotBase bot;

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
        public abstract Task<UpdateResult> Handle(Update update);

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        public UpdateHandler(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
