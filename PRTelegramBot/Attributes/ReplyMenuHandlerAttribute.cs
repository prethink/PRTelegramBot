using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Attribute for handling reply methods.
    /// </summary>
    public sealed class ReplyMenuHandlerAttribute 
        : StringQueryAttribute, IBaseQueryAttribute
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(params string[] commands)
            : this(0, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long botId, params string[] commands)
            : this(botId, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, params string[] commands)
            : this(botIds, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(CommandComparison commandComparison, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(StringComparison stringComparison, params string[] commands)
            : this(0, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
            : this(botId, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
            : this(botIds, CommandComparison.Equals, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this([botId], commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public ReplyMenuHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : base(botIds, commandComparison, stringComparison)
        {
            this.commands.AddRange(commands);
        }

        #endregion
    }
}
