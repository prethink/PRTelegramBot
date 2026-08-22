using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Base event arguments for bots.
    /// </summary>
    public class BotEventArgs : EventArgs
    {
        #region Fields and properties

        /// <summary>
        /// Bot context.
        /// </summary>
        public IBotContext Context { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the event arguments for the bot.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Base event arguments for bots</returns>
        public static BotEventArgs CreateEventArgs(IBotContext context)
        {
            return new BotEventArgs(context);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public BotEventArgs(IBotContext context)
        {
            Context = context;
        }

        #endregion
    }
}
