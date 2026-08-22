using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    /// <summary>
    /// Handler for step-by-step command execution.
    /// </summary>
    internal sealed class NextStepCommandHandler
    {
        #region Methods

        /// <summary>
        /// Handles the next step.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>The processing result.</returns>
        public async Task<UpdateResult> Handle(IBotContext context)
        {
            try
            {
                if (!context.Update.HasStepHandler())
                    return UpdateResult.Continue;

                var step = context.Update.GetStepHandler()?.GetExecuteMethod();
                if (step is null)
                    return UpdateResult.NotFound;

                if(!context.Update.GetStepHandler()!.CanExecute())
                {
                    context.Update.ClearStepUserHandler();
                    return UpdateResult.Continue;
                }

                context.Current.Events.CommandsEvents.OnPreNextStepCommandHandleInvoke(context.CreateBotEventArgs());

                var executer = new ExecutorNextStepCommand(context.Current);
                var currentHandler = context.Current.Handler as Handler;
                if (currentHandler is null)
                    return UpdateResult.Continue;

                var resultExecute = await executer.ExecuteMethod(context, new CommandHandler(step, context.Current));
                if (resultExecute == CommandResult.Executed)
                {
                    context.Current.Events.CommandsEvents.OnPostNextStepCommandHandleInvoke(context.CreateBotEventArgs());
                    return UpdateResult.Handled;
                }

                return UpdateResult.Continue;
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<NextStepCommandHandler>().LogErrorInternal(ex);
                return UpdateResult.Error;
            }
        }

        /// <summary>
        /// Ignore the basic commands.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>True to ignore the basic commands; False not to ignore them.</returns>
        public bool IgnoreBasicCommand(IBotContext context)
        {
            if (!context.Update.HasStepHandler())
                return false;

            return context.Update?.GetStepHandler()?.IgnoreBasicCommands ?? false;
        }

        /// <summary>
        /// The last step has been executed.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>True if the last step was executed; False if it was not, or if this is not the last step.</returns>
        public bool LastStepExecuted(Update update)
        {
            if (!update.HasStepHandler())
                return false;

            return update.GetStepHandler().LastStepExecuted;
        }

        /// <summary>
        /// Clears the steps.
        /// </summary>
        /// <param name="update">Update.</param>
        public void ClearSteps(Update update)
        {
            if (!update.HasStepHandler())
                return;

            update.ClearStepUserHandler();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public NextStepCommandHandler() { }

        #endregion
    }
}
