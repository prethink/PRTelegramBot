using PRTelegramBot.Core;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы события при написание пользователем /start_data.
    /// </summary>
    public class StartEventArgs : BotEventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Данные.
        /// </summary>
        public string Data;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="data">Данные.</param>
        public StartEventArgs(PRBotBase bot, Update update, string data)
            : base(bot, update)
        {
            Data = data;
        }

        #endregion
    }
}
