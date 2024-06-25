using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы при логирование ошибок.
    /// </summary>
    public class ErrorLogEventArgsCreator : EventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Исключение.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Обновление.
        /// </summary>
        public Update Update { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        public ErrorLogEventArgsCreator(Exception exception)
            : this(exception, new Update()) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        /// <param name="update">Обновление.</param>
        public ErrorLogEventArgsCreator(Exception exception, Update update)
        {
            Exception = exception;
            Update = update;
        }

        #endregion
    }
}
