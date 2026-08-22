using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Event arguments for plain logs.
    /// </summary>
    public class CommonLogEventArgs : BotEventArgs
    {
        #region Fields and properties

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Type.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Color.
        /// </summary>
        public ConsoleColor Color { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="e">Factory that creates the event arguments.</param>
        public CommonLogEventArgs(IBotContext context, CommonLogEventArgsCreator e) : base(context)
        {
            this.Message = e.Message;
            this.Type = e.Type;
            this.Color = e.Color;
        }

        #endregion
    }
}
