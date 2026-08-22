using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Event arguments used when the user sends /start_data.
    /// </summary>
    public class StartEventArgs : BotEventArgs
    {
        #region Fields and properties

        /// <summary>
        /// Data.
        /// </summary>
        public string Data;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="data">Data.</param>
        public StartEventArgs(IBotContext context, string data)
            : base(context)
        {
            Data = data;
        }

        #endregion
    }
}
