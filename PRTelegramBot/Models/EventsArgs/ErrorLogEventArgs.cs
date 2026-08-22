using PRTelegramBot.Core;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Error logging event arguments.
    /// </summary>
    public class ErrorLogEventArgs : BotEventArgs
    {
        #region Fields and properties

        /// <summary>
        /// Exception.
        /// </summary>
        public Exception Exception { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates event arguments carrying an error.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <returns>Error logging event arguments.</returns>
        public static ErrorLogEventArgs Create(Exception exception)
        {
            return new ErrorLogEventArgs(CurrentScope.Context, exception);
        }

        /// <summary>
        /// Creates event arguments carrying an error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Error logging event arguments.</returns>
        public static ErrorLogEventArgs Create(string message)
        {
            return new ErrorLogEventArgs(CurrentScope.Context, new Exception(message));
        }

        /// <summary>
        /// Creates event arguments carrying an error.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <param name="exception">Exception.</param>
        /// <returns>Error logging event arguments.</returns>
        public static ErrorLogEventArgs Create(PRBotBase bot, Exception exception)
        {
            return new ErrorLogEventArgs(new BotContext(bot), exception);
        }

        /// <summary>
        /// Creates event arguments carrying an error.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <param name="message">Message.</param>
        /// <returns>Error logging event arguments.</returns>
        public static ErrorLogEventArgs Create(PRBotBase bot, string message)
        {
            return new ErrorLogEventArgs(new BotContext(bot), new Exception(message));
        }

        /// <summary>
        /// Creates event arguments carrying an error.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Error logging event arguments.</returns>
        public static ErrorLogEventArgs Create(PRBotBase bot, Exception exception, CancellationToken cancellationToken)
        {
            return new ErrorLogEventArgs(new BotContext(bot, cancellationToken), exception);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="exception">Exception.</param>
        public ErrorLogEventArgs(IBotContext context, Exception exception)
            : base(context)
        {
            this.Exception = exception;
        }

        #endregion
    }
}
