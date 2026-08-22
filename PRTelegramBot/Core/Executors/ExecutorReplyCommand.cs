using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Executor for reply commands.
    /// </summary>
    internal sealed class ExecutorReplyCommand : ExecutorMessageCommand
    {
        #region Base class

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.Reply;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ExecutorReplyCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
