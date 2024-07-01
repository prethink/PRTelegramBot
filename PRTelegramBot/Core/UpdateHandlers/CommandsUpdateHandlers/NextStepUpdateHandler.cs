using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик пошагового выполнение команд.
    /// </summary>
    public sealed class NextStepUpdateHandler : ExecuteHandler
    {
        #region Поля и свойства

        public override UpdateType TypeUpdate => UpdateType.Message;

        #endregion

        #region Методы

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

        public override async Task<UpdateResult> Handle(Update update)
        {
            try
            {
                if (!update.HasStepHandler())
                    return UpdateResult.Continue;

                var step = update.GetStepHandler()?.GetExecuteMethod();
                if (step is null)
                    return UpdateResult.NotFound;
                bot.Events.MessageEvents.OnPostNextStepCommandHandleInvoke(new BotEventArgs(bot, update));
                var resultExecute = await ExecuteMethod(update, new CommandHandler(step));
                if (resultExecute == CommandResult.Executed)
                {
                    bot.Events.MessageEvents.OnPreNextStepCommandHandleInvoke(new BotEventArgs(bot, update));
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

        protected override async Task<InternalCheckResult> InternalCheck(Update update, CommandHandler handler)
        {
            var currentCheckers = bot.Options.CommandCheckers.Where(x => x.CommandTypes.Contains(CommandType.NextStep));
            if (currentCheckers.Any())
            {
                foreach (var commandChecker in currentCheckers)
                {
                    var result = await commandChecker.Checker.Check(bot, update, handler);
                    if (result != InternalCheckResult.Passed)
                        return result;
                }
            }
            return InternalCheckResult.Passed;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public NextStepUpdateHandler(PRBotBase bot)
            : base(bot) { }

        #endregion
    }
}
