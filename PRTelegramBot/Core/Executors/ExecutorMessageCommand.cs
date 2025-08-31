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
    /// Базовый исполнитель для команд типа сообщения.
    /// </summary>
    internal abstract class ExecutorMessageCommand : ExecutorCommandBase<string>
    {
        #region Базовый класс

        protected override async Task<InternalCheckResult> InternalCheck(PRBotBase bot, Update update, CommandHandler handler)
        {
            var method = handler.Method;
            var privilages = method.GetCustomAttribute<AccessAttribute>();
            var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
            var requireChat = method.GetCustomAttribute<RequiredTypeChatAttribute>();
            var whiteListAttribute = method.GetCustomAttribute<WhiteListAnonymousAttribute>();

            if (requireChat != null)
            {
                var currentType = update?.Message?.Chat?.Type;
                if (currentType == null || !requireChat.TypesChat.Contains(currentType.Value))
                {
                    bot.Events.OnWrongTypeChatInvoke(new BotEventArgs(bot, update));
                    return InternalCheckResult.WrongChatType;
                }
            }

            if (requireDate != null)
            {
                var currentType = update?.Message?.Type;
                if (currentType == null || !requireDate.TypeMessages.Contains(currentType.Value))
                {
                    bot.Events.OnWrongTypeMessageInvoke(new BotEventArgs(bot, update));
                    return InternalCheckResult.WrongMessageType;
                }
            }

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

            return InternalCheckResult.Passed;
        }

        protected override bool CanExecute(string currentCommand, string command, CommandHandler handler)
        {
            if (handler.CommandComparison == CommandComparison.Equals)
            {
                if (handler is StringCommandHandler stringHandler && currentCommand.Equals(command, stringHandler.StringComparison))
                    return true;
            }
            else if (handler.CommandComparison == CommandComparison.Contains)
            {
                if (handler is StringCommandHandler stringHandler && currentCommand.Contains(command, stringHandler.StringComparison))
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
        public ExecutorMessageCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
