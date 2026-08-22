using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Base command executor.
    /// </summary>
    /// <typeparam name="TKey">Type of the command key.</typeparam>
    internal abstract class ExecutorCommandBase<TKey> 
        where TKey : notnull
    {
        #region Fields and properties

        /// <summary>
        /// Telegram bot.
        /// </summary>
        protected PRBotBase bot;

        /// <summary>
        /// Command type.
        /// </summary>
        public abstract CommandType CommandType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="context">Bot context.</param>
        /// <param name="commands">Commands.</param>
        /// <returns>The command execution result.</returns>
        public async Task<CommandResult> Execute(TKey command, IBotContext context, Dictionary<TKey, CommandHandler> commands)
        {
            foreach (var commandExecute in commands.OrderByDescending(x => x.Value.CommandComparison == CommandComparison.Equals))
            {
                if (CanExecute(command, commandExecute.Key, commandExecute.Value))
                    return await ExecuteMethod(context, commandExecute.Value);
            }
            return CommandResult.Continue;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="command">Command to execute.</param>
        /// <returns>The command execution result.</returns>
        public async Task<CommandResult> Execute(IBotContext context, CommandHandler command)
        {
            return await ExecuteMethod(context, command);
        }

        /// <summary>
        /// Gets the handler that executes the command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="context">Bot context.</param>
        /// <param name="commands">Commands.</param>
        /// <returns>The handler that executes the command, or null.</returns>
        public CommandHandler GetExecuteHandlerOrNull(TKey command, IBotContext context, Dictionary<TKey, CommandHandler> commands)
        {
            foreach (var commandExecute in commands.OrderByDescending(x => x.Value.CommandComparison == CommandComparison.Equals))
            {
                if (CanExecute(command, commandExecute.Key, commandExecute.Value))
                    return commandExecute.Value;
            }

            return null;
        }

        /// <summary>
        /// Executes the method.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="handler">Handler.</param>
        /// <returns>The command execution result.</returns>
        public virtual async Task<CommandResult> ExecuteMethod(IBotContext context, CommandHandler handler)
        {
            var result = await InternalCheck(context, handler);
            if (result != InternalCheckResult.Passed)
                return CommandResult.InternalCheck;

            await handler.ExecuteCommand(context);
            return CommandResult.Executed;
        }

        /// <summary>
        /// Internal check for <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="handler">Handler.</param>
        /// <returns>The result of the check.</returns>
        protected abstract Task<InternalCheckResult> InternalCheck(IBotContext context, CommandHandler handler);

        /// <summary>
        /// Whether the command can be executed.
        /// </summary>
        /// <param name="currentCommand">Current command.</param>
        /// <param name="commandFromCollection">The command from the collection.</param>
        /// <param name="handler">Command handler.</param>
        /// <returns>True if the command may be executed; False if it may not.</returns>
        protected abstract bool CanExecute(TKey currentCommand, TKey commandFromCollection, CommandHandler handler);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ExecutorCommandBase(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
