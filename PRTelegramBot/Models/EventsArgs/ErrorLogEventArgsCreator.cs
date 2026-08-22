using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Arguments used when logging errors.
    /// </summary>
    public class ErrorLogEventArgsCreator : EventArgs
    {
        #region Fields and properties

        /// <summary>
        /// Exception.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Update.
        /// </summary>
        public Update Update { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception.</param>
        public ErrorLogEventArgsCreator(Exception exception)
            : this(exception, new Update()) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <param name="update">Update.</param>
        public ErrorLogEventArgsCreator(Exception exception, Update update)
        {
            Exception = exception;
            Update = update;
        }

        #endregion
    }
}
