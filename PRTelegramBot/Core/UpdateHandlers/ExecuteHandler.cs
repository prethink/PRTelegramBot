using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Обработчик выполнения обновление.
    /// </summary>
    public abstract class ExecuteHandler : UpdateHandler
    {
        #region Поля и свойства

        /// <summary>
        /// Тип команд.
        /// </summary>
        public abstract CommandType CommandType { get; }

        public override UpdateType TypeUpdate => UpdateType.Message;

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить метод.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения команды.</returns>
        protected virtual async Task<CommandResult> ExecuteMethod(Update update, CommandHandler handler)
        {
            var @delegate = handler.Command;

            var result = await InternalCheck(update, handler);
            if (result != InternalCheckResult.Passed)
                return CommandResult.InternalCheck;

            await @delegate(bot.botClient, update);
            return CommandResult.Executed;
        }

        /// <summary>
        /// Внутрення проверка для <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения проверки.</returns>
        protected virtual async Task<InternalCheckResult> InternalCheck(Update update, CommandHandler handler)
        {
            var method = handler.Command.Method;
            var privilages = method.GetCustomAttribute<AccessAttribute>();
            var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
            var requireChat = method.GetCustomAttribute<RequiredTypeChatAttribute>();
            var whiteListAttribute = method.GetCustomAttribute<WhiteListAnonymousAttribute>();
            var @delegate = handler.Command;

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
                bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(bot, update, @delegate, privilages.Mask));
                return InternalCheckResult.PrivilegeCheck;
            }

            var whiteListManager = bot.Options.WhiteListManager;
            var hasUserInWhiteList = await whiteListManager.HasUser(update.GetChatId());

            if (whiteListManager.Settings == WhiteListSettings.OnlyCommands && whiteListManager.Count > 0)
            {
                if(whiteListAttribute == null && !hasUserInWhiteList)
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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        protected ExecuteHandler(PRBotBase bot)
            : base(bot) { }

        #endregion
    }
}
