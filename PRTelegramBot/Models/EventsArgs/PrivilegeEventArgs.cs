using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Arguments used when checking privileges.
    /// </summary>
    public class PrivilegeEventArgs : CommandEventsArgs
    {
        #region Fields and properties

        /// <summary>
        /// Access mask.
        /// </summary>
        public int? Mask { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="executeMethod">The method to execute.</param>
        /// <param name="mask">Access mask.</param>
        public PrivilegeEventArgs(IBotContext context, Func<IBotContext, Task> executeMethod, int? mask)
            : base(context, executeMethod)
        {
            Mask = mask;
        }

        #endregion
    }
}
