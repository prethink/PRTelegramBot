using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface for step-by-step command execution.
    /// </summary>
    public interface IExecuteStep
    {
        /// <summary>
        /// Ignore the basic commands while steps are running.
        /// </summary>
        public bool IgnoreBasicCommands { get; set; }

        /// <summary>
        /// Whether this was the last step and it has completed.
        /// </summary>
        public bool LastStepExecuted { get; set; }

        /// <summary>
        /// Gets the reference to the method that has to be executed.
        /// </summary>
        /// <returns>The method to execute.</returns>
        Func<IBotContext, Task> GetExecuteMethod();

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>The command execution result.</returns>
        Task<ExecuteStepResult> ExecuteStep(IBotContext context);

        /// <summary>
        /// Whether the step can be executed
        /// </summary>
        /// <returns>True for yes / False for no.</returns>
        bool CanExecute();
    }
}
