using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик для inline команд.
    /// </summary>
    public sealed class InlineUpdateHandler : CommandUpdateHandler<Enum>
    {
        #region Поля и свойств

        public override UpdateType TypeUpdate => UpdateType.CallbackQuery;

        #endregion

        #region Методы

        public override async Task<UpdateResult> Handle(Update update)
        {
            try
            {
                var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = $"The user {update.GetInfoUser().Trim()} invoked the command {command.CommandType.GetDescription()}";
                    bot.Events.OnCommonLogInvoke(msg, "CallBackCommand", ConsoleColor.Magenta);

                    var resultExecute = await ExecuteCommand(command.CommandType, update, commands);
                    if (resultExecute == CommandResult.Executed)
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

        public override bool AddCommand(Enum command, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            try
            {
                ReflectionUtils.AddEnumsHeader(command);
                commands.Add(command, new CommandHandler(@delegate));
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
        }

        public override bool RemoveCommand(Enum command)
        {
            try
            {
                commands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
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

        protected override void RegisterCommands()
        {
            ReflectionUtils.FindEnumHeaders();
            MethodInfo[] methods = ReflectionUtils.FindStaticInlineCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(InlineCallbackHandlerAttribute<>), methods, commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(InlineCallbackHandlerAttribute<>), methodsInClass, commands, bot.Options.ServiceProvider);
            }
        }

        protected override async Task<InternalCheckResult> InternalCheck(Update update, CommandHandler handler)
        {
            {
                var method = handler.Command.Method;
                var privilages = method.GetCustomAttribute<AccessAttribute>();
                var whiteListAttribute = method.GetCustomAttribute<WhiteListAnonymousAttribute>();
                var @delegate = handler.Command;

                if (privilages != null)
                {
                    bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(bot, update, @delegate, privilages.Mask));
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
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public InlineUpdateHandler(PRBotBase bot)
            : base(bot)
        {
            RegisterCommands();
        }

        #endregion
    }
}
