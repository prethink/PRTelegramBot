using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using System.Reflection;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// String-based command handler.
    /// </summary>
    public sealed class StringCommandHandler : CommandHandler
    {
        #region Fields and properties

        /// <summary>
        /// String comparison.
        /// </summary>
        public StringComparison StringComparison { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        public StringCommandHandler(MethodInfo method)
            : this(method, null, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public StringCommandHandler(MethodInfo method, CommandComparison commandComparison)
            : this(method, null, commandComparison, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        /// <param name="bot">Bot.</param>
        public StringCommandHandler(MethodInfo method, PRBotBase bot)
            : this(method, bot, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        public StringCommandHandler(Func<IBotContext, Task> command)
            : this(command, null, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="bot">Bot.</param>
        public StringCommandHandler(Func<IBotContext, Task> command, PRBotBase bot)
            : this(command, bot, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public StringCommandHandler(Func<IBotContext, Task> command, CommandComparison commandComparison) 
            : this(command, null, commandComparison, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        /// <param name="bot">Bot.</param>
        /// <param name="commandComparison">Command comparison.</param>
        /// <param name="stringComparison">String comparison.</param>
        public StringCommandHandler(MethodInfo method, PRBotBase bot, CommandComparison commandComparison, StringComparison stringComparison) 
            : base(method, bot, commandComparison)
        {
            this.StringComparison = stringComparison;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="bot">Bot.</param>
        /// <param name="commandComparison">Command comparison.</param>
        /// <param name="stringComparison">String comparison.</param>
        public StringCommandHandler(Func<IBotContext, Task> command, PRBotBase bot, CommandComparison commandComparison, StringComparison stringComparison)
            : base(command, bot, commandComparison)
        {
            this.StringComparison = stringComparison;
        }

        #endregion
    }
}
