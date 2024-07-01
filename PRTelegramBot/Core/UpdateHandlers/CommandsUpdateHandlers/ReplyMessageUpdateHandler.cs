using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик reply команд.
    /// </summary>
    public class ReplyMessageUpdateHandler : MessageCommandUpdateHandler
    {
        #region Базовый класс

        public override CommandType CommandType => CommandType.Reply;

        public override MessageType TypeMessage => MessageType.Text;

        public override async Task<UpdateResult> Handle(Update update)
        {
            try
            {
                string command = update.Message.Text;
                RemoveBracketsIfExists(ref command);
                bot.Events.MessageEvents.OnPreReplyCommandHandleInvoke(new BotEventArgs(bot, update));
                var resultExecute = await ExecuteCommand(command, update, commands);
                if (resultExecute != CommandResult.Continue)
                {
                    bot.Events.MessageEvents.OnPostReplyCommandHandleInvoke(new BotEventArgs(bot, update));
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

        protected override void RegisterCommands()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticReplyCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(ReplyMenuHandlerAttribute), methods, commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(ReplyMenuHandlerAttribute), methodsInClass, commands, bot.Options.ServiceProvider);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удалить скобки из сообщения
        /// </summary>
        /// <param name="command"></param>
        protected void RemoveBracketsIfExists(ref string command)
        {
            if (command.Contains("(") && command.Contains(")"))
                command = command.Remove(command.LastIndexOf("(") - 1);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ReplyMessageUpdateHandler(PRBotBase bot)
            : base(bot)
        {
            RegisterCommands();
        }

        #endregion
    }
}
