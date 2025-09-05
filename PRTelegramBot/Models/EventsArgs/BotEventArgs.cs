using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Базовые аргументы события для ботов.
    /// </summary>
    public class BotEventArgs : EventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Бот.
        /// </summary>
        public PRBotBase Bot { get; private set; }

        /// <summary>
        /// Клиент бота.
        /// </summary>
        public ITelegramBotClient BotClient { get; private set; }

        /// <summary>
        /// Обновление.
        /// </summary>
        public Update Update { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Обновление.</param>
        public BotEventArgs(PRBotBase bot, Update update)
        {
            Bot = bot;
            BotClient = bot.BotClient;
            Update = update;
        }

        #endregion
    }
}
