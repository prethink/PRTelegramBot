using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Executor for step-by-step command execution.
    /// </summary>
    internal sealed class ExecutorNextStepCommand : ExecutorMessageCommand
    {
        #region Base class

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.NextStep;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ExecutorNextStepCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
