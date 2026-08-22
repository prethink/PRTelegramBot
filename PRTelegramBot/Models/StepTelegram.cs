using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Lets the user execute commands step by step.
    /// </summary>
    public sealed class StepTelegram : IExecuteStep
    {
        #region Properties and constants

        /// <summary>
        /// Reference to the method that has to be executed.
        /// </summary>
        public Func<IBotContext, Task> CommandDelegate { get; set; }

        /// <summary>
        /// The time until which the command may be executed.
        /// </summary>
        public DateTime? ExpiredTime { get; set; }

        /// <summary>
        /// Data cache.
        /// </summary>
        private ITelegramCache cache { get; set; }

        #endregion

        #region IExecuteStep

        /// <inheritdoc/>
        public bool LastStepExecuted { get; set; }

        /// <inheritdoc/>
        public bool IgnoreBasicCommands { get; set; }

        /// <inheritdoc/>
        public async Task<ExecuteStepResult> ExecuteStep(IBotContext context)
        {
            if (ExpiredTime is not null && DateTime.Now > ExpiredTime)
            {
                context.Update.ClearStepUserHandler();
                return ExecuteStepResult.ExpiredTime;
            }

            try
            {
                await CommandDelegate.Invoke(context);
                return ExecuteStepResult.Success;
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<StepTelegram>().LogErrorInternal(ex);
                return ExecuteStepResult.Failure;
            }
        }

        /// <inheritdoc/>
        public Func<IBotContext, Task> GetExecuteMethod()
        {
            return CommandDelegate;
        }

        /// <inheritdoc/>
        public bool CanExecute()
        {
            return ExpiredTime is null || DateTime.Now < ExpiredTime;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers the next step.
        /// </summary>
        /// <param name="nextStep">Method that handles the next step.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep)
        {
            RegisterNextStep(nextStep, null);
        }

        /// <summary>
        /// Registers the next step.
        /// </summary>
        /// <param name="nextStep">Method that handles the next step.</param>
        /// <param name="addTime">The time until which the command may be executed</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, TimeSpan addTime)
        {
            RegisterNextStep(nextStep, DateTime.Now.Add(addTime));
        }

        /// <summary>
        /// Registers the next step.
        /// </summary>
        /// <param name="nextStep">Method that handles the next step.</param>
        /// <param name="expiredTime"> The time until which the command may be executed.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, DateTime? expiredTime)
        {
            RegisterNextStep(nextStep, expiredTime, false);
        }

        /// <summary>
        /// Registers the next step.
        /// </summary>
        /// <param name="nextStep">Method that handles the next step.</param>
        /// <param name="expiredTime"> The time until which the command may be executed.</param>
        /// <param name="ignoreBasicCommands">Ignore the basic commands while steps are running.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, DateTime? expiredTime, bool ignoreBasicCommands)
        {
            CommandDelegate = nextStep;
            ExpiredTime = expiredTime;
            IgnoreBasicCommands = ignoreBasicCommands;
        }

        /// <summary>
        /// Gets the current cache
        /// </summary>
        /// <typeparam name="T">The class used to store the cache</typeparam>
        /// <returns>Cache</returns>
        public T GetCache<T>()
        {
            return cache is T resultCache 
                ? resultCache 
                : default;
        }

        #endregion

        #region Class constructors

        /// <summary>
        /// Creates a new next step.
        /// </summary>
        /// <param name="command">The command to execute</param>
        public StepTelegram(Func<IBotContext, Task> command)
            : this(command, null, null) { }

        /// <summary>
        /// Creates a new next step.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="cache">Cache.</param>
        public StepTelegram(Func<IBotContext, Task> command, ITelegramCache cache)
            : this(command, null, cache, false) { }

        /// <summary>
        /// Creates a new next step.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="expiredTime">Maximum lifetime of the command, after which it is ignored.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime expiredTime)
            : this(command, expiredTime, null, false) { }

        /// <summary>
        /// Creates a new next step.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="expiredTime">Maximum lifetime of the command, after which it is ignored.</param>
        /// <param name="cache">Cache.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime? expiredTime, ITelegramCache cache)
            : this(command, expiredTime, cache, false) { }

        /// <summary>
        /// Creates a new next step.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="expiredTime">Maximum lifetime of the command, after which it is ignored.</param>
        /// <param name="cache">Cache.</param>
        /// <param name="ignoreBasicCommands">Ignore the basic commands while steps are running.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime? expiredTime, ITelegramCache cache, bool ignoreBasicCommands)
        {
            this.cache = cache;
            IgnoreBasicCommands = ignoreBasicCommands;
            CommandDelegate = command;
            ExpiredTime = expiredTime;
        }

        #endregion
    }
}
