using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    public class ReplyMessageUpdateHandler : MessageCommandUpdateHandler
    {
        #region Поля и свойства

        public override MessageType TypeMessage => MessageType.Text;

        #endregion

        #region Методы

        public override async Task<ResultUpdate> Handle(Update update)
        {
            try
            {
                string command = update.Message.Text;
                RemoveBracketsIfExists(ref command);
                var resultExecute = await ExecuteCommand(command, update, commands);
                if (resultExecute != ResultCommand.Continue)
                    return ResultUpdate.Handled;

                return ResultUpdate.Continue;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return ResultUpdate.Error;
            }
        }

        /// <summary>
        /// Удалить скобки из сообщения
        /// </summary>
        /// <param name="command"></param>
        protected void RemoveBracketsIfExists(ref string command)
        {
            if (command.Contains("(") && command.Contains(")"))
                command = command.Remove(command.LastIndexOf("(") - 1);
        }

        protected override void RegisterCommands()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticMessageMenuHandlers(bot.Options.BotId);
            registerService.RegisterCommand(bot, typeof(ReplyMenuHandlerAttribute), methods, commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(ReplyMenuHandlerAttribute), methodsInClass, commands, serviceProvider);
            }
        }

        protected override ResultCommand InternalCheck(Update update, CommandHandler handler)
        {
            var method = handler.Command.Method;
            var privilages = method.GetCustomAttribute<AccessAttribute>();
            var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
            var requireUpdate = method.GetCustomAttribute<RequiredTypeChatAttribute>();
            var @delegate = handler.Command;

            if (requireUpdate != null)
            {
                if (!requireUpdate.TypesChat.Contains(update!.Message!.Chat.Type))
                {
                    bot.Events.OnWrongTypeChatInvoke(new BotEventArgs(bot, update));
                    return ResultCommand.WrongChatType;
                }
            }

            if (requireDate != null)
            {
                if (!requireDate.TypeMessages.Contains(update!.Message!.Type))
                {
                    bot.Events.OnWrongTypeMessageInvoke(new BotEventArgs(bot, update));
                    return ResultCommand.WrongMessageType;
                }
            }

            if (privilages != null)
            {
                bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(bot, update, @delegate, privilages.Mask));
                return ResultCommand.PrivilegeCheck;
            }
            return ResultCommand.Continue;
        }

        #endregion

        #region Конструкторы

        public ReplyMessageUpdateHandler(PRBot bot, IServiceProvider serviceProvider)
            : base(bot, serviceProvider)
        {
            RegisterCommands();
        }

        #endregion
    }
}
