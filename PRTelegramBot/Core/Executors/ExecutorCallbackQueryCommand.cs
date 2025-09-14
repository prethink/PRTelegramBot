using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using System.Reflection;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Исполнитель для callbackQuery команд.
    /// </summary>
    internal sealed class ExecutorCallbackQueryCommand : ExecutorCommandBase<Enum>
    {
        #region Базовый класс

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.Inline;

        /// <inheritdoc />
        protected override async Task<InternalCheckResult> InternalCheck(IBotContext context, CommandHandler handler)
        {
            var currentCheckers = bot.Options.CommandCheckers.Where(x => x.CommandTypes.Contains(CommandType));
            if (currentCheckers.Any())
            {
                foreach (var commandChecker in currentCheckers)
                {
                    var result = await commandChecker.Checker.Check(context, handler);
                    if (result != InternalCheckResult.Passed)
                        return result;
                }
            }

            var method = handler.Method;
            var privileges = method.GetCustomAttribute<AccessAttribute>();
            var whiteListAttribute = method.GetCustomAttribute<WhiteListAnonymousAttribute>();

            if (privileges is not null)
            {
                bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(context, handler.ExecuteCommand, privileges.Mask));
                return InternalCheckResult.PrivilegeCheck;
            }

            var whiteListManager = bot.Options.WhiteListManager;
            var hasUserInWhiteList = await whiteListManager.HasUser(context.Update.GetChatId());

            if (whiteListManager.Settings == WhiteListSettings.OnlyCommands && whiteListManager.Count > 0)
            {
                if (whiteListAttribute is null && !hasUserInWhiteList)
                {
                    bot.Events.OnAccessDeniedInvoke(context.CreateBotEventArgs());
                    return InternalCheckResult.NotInWhiteList;
                }
            }
            return InternalCheckResult.Passed;
        }

        /// <inheritdoc />
        protected override bool CanExecute(Enum currentCommand, Enum command, CommandHandler handler)
        {
            if (handler.CommandComparison == CommandComparison.Equals)
            {
                if (currentCommand.Equals(command))
                    return true;
            }
            else
            {
                throw new NotImplementedException();
            }
            return false;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ExecutorCallbackQueryCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
