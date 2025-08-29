using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
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
        #region Поля и свойства

        /// <summary>
        /// Бот.
        /// </summary>
        private readonly PRBotBase bot;

        #endregion

        #region Методы

        public async Task<UpdateResult> Handle(PRBotBase bot, Update update)
        {
            try
            {
                if (!update.HasStepHandler())
                    return UpdateResult.Continue;

                var step = update.GetStepHandler()?.GetExecuteMethod();
                if (step is null)
                    return UpdateResult.NotFound;

                if(!update.GetStepHandler()!.CanExecute())
                {
                    update.ClearStepUserHandler();
                    return UpdateResult.Continue;
                }

                bot.Events.CommandsEvents.OnPreNextStepCommandHandleInvoke(new BotEventArgs(bot, update));

                var executer = new ExecutorNextStepCommand(bot);
                var currentHandler = bot.Handler as Handler;
                if (currentHandler == null)
                    return UpdateResult.Continue;

                var resultExecute = await executer.ExecuteMethod(bot, update, new CommandHandler(step));
                if (resultExecute == CommandResult.Executed)
                {
                    bot.Events.CommandsEvents.OnPostNextStepCommandHandleInvoke(new BotEventArgs(bot, update));
                    return UpdateResult.Handled;
                }

                return UpdateResult.Continue;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
                return UpdateResult.Error;
            }
        }

        /// <summary>
        /// Игнорировать базовые команды.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>True - игнорировать основные команды, False - не игнорировать.</returns>
        public bool IgnoreBasicCommand(Update update)
        {
            if (!update.HasStepHandler())
                return false;

            return update?.GetStepHandler()?.IgnoreBasicCommands ?? false;
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
        /// <param name="bot">Бот.</param>
        public NextStepCommandHandler(PRBotBase bot)
        { 
            this.bot = bot;
        }

        #endregion
    }
}
