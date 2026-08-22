using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Attribute for inline commands.
    /// </summary>
    public sealed class InlineCallbackHandlerAttribute<T> : BaseQueryAttribute<Enum>
        where T : Enum
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commands">Commands.</param>
        public InlineCallbackHandlerAttribute(params T[] commands)
            : this(0, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commands">Commands.</param>
        public InlineCallbackHandlerAttribute(long botId, params T[] commands) 
            : this([botId], commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commands">Commands.</param>
        public InlineCallbackHandlerAttribute(long[] botIds, params T[] commands)
            : base(botIds, CommandComparison.Equals)
        {
            foreach (var command in commands)
                this.commands.Add((Enum)command);
        }

        #endregion
    }
}
