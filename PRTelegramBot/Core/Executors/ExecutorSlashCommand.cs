using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Executor for slash commands.
    /// </summary>
    internal sealed class ExecutorSlashCommand : ExecutorMessageCommand
    {
        #region Base class

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.Slash;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ExecutorSlashCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
