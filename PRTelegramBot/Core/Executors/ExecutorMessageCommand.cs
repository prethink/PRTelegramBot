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
    /// Базовый исполнитель для команд типа сообщения.
    /// </summary>
    internal abstract class ExecutorMessageCommand : ExecutorCommandBase<string>
    {
        #region Базовый класс

        /// <inheritdoc />
        protected override async Task<InternalCheckResult> InternalCheck(IBotContext context, CommandHandler handler)
        {
            var method = handler.Method;
            var privileges = method.GetCustomAttribute<AccessAttribute>();
            var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
            var requireChat = method.GetCustomAttribute<RequiredTypeChatAttribute>();
            var whiteListAttribute = method.GetCustomAttribute<WhiteListAnonymousAttribute>();

            if (requireChat != null)
            {
                var currentType = context.Update?.Message?.Chat?.Type;
                if (currentType == null || !requireChat.TypesChat.Contains(currentType.Value))
                {
                    bot.Events.OnWrongTypeChatInvoke(context.CreateBotEventArgs());
                    return InternalCheckResult.WrongChatType;
                }
            }

            if (requireDate != null)
            {
                var currentType = context.Update?.Message?.Type;
                if (currentType == null || !requireDate.TypeMessages.Contains(currentType.Value))
                {
                    bot.Events.OnWrongTypeMessageInvoke(context.CreateBotEventArgs());
                    return InternalCheckResult.WrongMessageType;
                }
            }

            if (privileges != null)
            {
                bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(context, handler.ExecuteCommand, privileges.Mask));
                return InternalCheckResult.PrivilegeCheck;
            }

            var whiteListManager = bot.Options.WhiteListManager;
            var hasUserInWhiteList = await whiteListManager.HasUser(context.Update.GetChatId());

            if (whiteListManager.Settings == WhiteListSettings.OnlyCommands && whiteListManager.Count > 0)
            {
                if (whiteListAttribute == null && !hasUserInWhiteList)
                {
                    bot.Events.OnAccessDeniedInvoke(context.CreateBotEventArgs());
                    return InternalCheckResult.NotInWhiteList;
                }
            }

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

            return InternalCheckResult.Passed;
        }

        /// <inheritdoc />
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
