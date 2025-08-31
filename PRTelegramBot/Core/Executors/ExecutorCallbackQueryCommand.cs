using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using System.Reflection;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Исполнитель для callbackQuery команд.
    /// </summary>
    internal sealed class ExecutorCallbackQueryCommand : ExecutorCommandBase<Enum>
    {
        #region Базовый класс

        public override CommandType CommandType => CommandType.Inline;

        protected override async Task<InternalCheckResult> InternalCheck(PRBotBase bot, Update update, CommandHandler handler)
        {
            var currentCheckers = bot.Options.CommandCheckers.Where(x => x.CommandTypes.Contains(CommandType));
            if (currentCheckers.Any())
            {
                foreach (var commandChecker in currentCheckers)
                {
                    var result = await commandChecker.Checker.Check(bot, update, handler);
                    if (result != InternalCheckResult.Passed)
                        return result;
                }
            }

            var method = handler.Method;
            var privilages = method.GetCustomAttribute<AccessAttribute>();
            var whiteListAttribute = method.GetCustomAttribute<WhiteListAnonymousAttribute>();

            if (privilages != null)
            {
                bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(bot, update, handler.ExecuteCommand, privilages.Mask));
                return InternalCheckResult.PrivilegeCheck;
            }

            var whiteListManager = bot.Options.WhiteListManager;
            var hasUserInWhiteList = await whiteListManager.HasUser(update.GetChatId());

            if (whiteListManager.Settings == WhiteListSettings.OnlyCommands && whiteListManager.Count > 0)
            {
                if (whiteListAttribute == null && !hasUserInWhiteList)
                {
                    bot.Events.OnAccessDeniedInvoke(new BotEventArgs(bot, update));
                    return InternalCheckResult.NotInWhiteList;
                }
            }
            return InternalCheckResult.Passed;
        }

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
