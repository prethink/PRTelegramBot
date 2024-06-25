using PRTelegramBot.Core;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы событий логирования ошибок.
    /// </summary>
    public class ErrorLogEventArgs : BotEventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Исключение.
        /// </summary>
        public Exception Exception { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="e">Аргументы события.</param>
        public ErrorLogEventArgs(PRBotBase bot, ErrorLogEventArgsCreator e)
            : base(bot, e.Update)
        {
            this.Exception = e.Exception;
        }

        #endregion
    }
}
