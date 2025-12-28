using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Базовые аргументы события для ботов.
    /// </summary>
    public class BotEventArgs : EventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Контекст бота.
        /// </summary>
        public IBotContext Context { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Создать аргументы событий для бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Базовые аргументы события для ботов</returns>
        public static BotEventArgs CreateEventArgs(IBotContext context)
        {
            return new BotEventArgs(context);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public BotEventArgs(IBotContext context)
        {
            Context = context;
        }

        #endregion
    }
}
