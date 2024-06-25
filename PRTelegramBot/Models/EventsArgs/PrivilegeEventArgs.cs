using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы при проверки привелегий.
    /// </summary>
    public class PrivilegeEventArgs : CommandEventsArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Маска доступа.
        /// </summary>
        public int? Mask { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="executeMethod">Метод для выполнения.</param>
        /// <param name="mask">Маска доступа.</param>
        public PrivilegeEventArgs(PRBotBase bot, Update update, Func<ITelegramBotClient, Update, Task> executeMethod, int? mask)
            : base(bot, update, executeMethod)
        {
            Mask = mask;
        }

        #endregion
    }
}
