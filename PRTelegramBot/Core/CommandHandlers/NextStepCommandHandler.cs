using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    /// <summary>
    /// Обработчик пошагового выполнение команд.
    /// </summary>
    internal sealed class NextStepCommandHandler
    {
        #region Методы

        /// <summary>
        /// Обработать следующий шаг.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Результат обработки.</returns>
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
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
                return UpdateResult.Error;
            }
        }

        /// <summary>
        /// Игнорировать базовые команды.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - игнорировать основные команды, False - не игнорировать.</returns>
        public bool IgnoreBasicCommand(IBotContext context)
        {
            if (!context.Update.HasStepHandler())
                return false;

            return context.Update?.GetStepHandler()?.IgnoreBasicCommands ?? false;
        }

        /// <summary>
        /// Последний шаг выполненен.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>True - последний шаг выполнен, False - не выполнен или это не последний шаг.</returns>
        public bool LastStepExecuted(Update update)
        {
            if (!update.HasStepHandler())
                return false;

            return update.GetStepHandler().LastStepExecuted;
        }

        /// <summary>
        /// Очистить шаги.
        /// </summary>
        /// <param name="update">Обновление.</param>
        public void ClearSteps(Update update)
        {
            if (!update.HasStepHandler())
                return;

            update.ClearStepUserHandler();
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public NextStepCommandHandler() { }

        #endregion
    }
}
