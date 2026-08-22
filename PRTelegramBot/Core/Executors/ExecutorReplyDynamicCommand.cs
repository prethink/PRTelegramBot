using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Executor for dynamic reply commands.
    /// </summary>
    internal sealed class ExecutorReplyDynamicCommand : ExecutorMessageCommand
    {
        #region Base class

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.ReplyDynamic;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ExecutorReplyDynamicCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
