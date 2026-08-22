using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Event arguments for plain logs.
    /// </summary>
    public class CommonLogEventArgsCreator : EventArgs
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

        /// <summary>
        /// Context.
        /// </summary>
        public IBotContext Context { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="type">Type.</param>
        public CommonLogEventArgsCreator(string message, string type)
            : this(message, type, ConsoleColor.White, BotContext.CreateEmpty()) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="type">Type.</param>
        /// <param name="context">Bot context.</param>
        public CommonLogEventArgsCreator(string message, string type, IBotContext context)
            : this(message, type, ConsoleColor.White, context) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="type">Type.</param>
        /// <param name="color">Color.</param>
        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color)
            : this(message, type, color, BotContext.CreateEmpty()) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="type">Type.</param>
        /// <param name="color">Color.</param>
        /// <param name="context">Bot context.</param>
        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color, IBotContext context)
        {
            this.Message = message;
            this.Type = type;
            this.Color = color;
            this.Context = context;
        }

        #endregion
    }
}
