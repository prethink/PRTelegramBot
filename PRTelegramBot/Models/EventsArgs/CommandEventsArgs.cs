using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Event that carries a reference to the command.
    /// </summary>
    public class CommandEventsArgs : BotEventArgs
    {
        #region Fields and properties

        /// <summary>
        /// The method to execute.
        /// </summary>
        public Func<IBotContext, Task> ExecuteMethod { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="executeMethod"></param>
        public CommandEventsArgs(IBotContext context, Func<IBotContext, Task> executeMethod)
            : base(context)
        {
            this.ExecuteMethod = executeMethod;
        }

        #endregion
    }
}
