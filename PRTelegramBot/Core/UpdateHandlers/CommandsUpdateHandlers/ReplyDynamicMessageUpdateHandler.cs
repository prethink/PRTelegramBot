using PRTelegramBot.Attributes;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик для динамических reply команд.
    /// </summary>
    public sealed class ReplyDynamicMessageUpdateHandler : MessageCommandUpdateHandler
    {
        #region Поля и свойства

        public override MessageType TypeMessage => MessageType.Text;

        #endregion

        #region Методы

        public override async Task<UpdateResult> Handle(Update update)
        {
            try
            {
                string command = update.Message.Text;
                RemoveBracketsIfExists(ref command);
                bot.Events.MessageEvents.OnPreDynamicReplyCommandHandleInvoke(new BotEventArgs(bot, update));
                var resultExecute = await ExecuteCommand(command, update, commands);
                if (resultExecute != CommandResult.Continue)
                {
                    bot.Events.MessageEvents.OnPostDynamicReplyCommandHandleInvoke(new BotEventArgs(bot, update));
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
            MethodInfo[] methods = ReflectionUtils.FindStaticDynamicReplyCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(ReplyMenuDynamicHandlerAttribute), methods, commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(ReplyMenuDynamicHandlerAttribute), methodsInClass, commands, bot.Options.ServiceProvider);
            }
        }

        /// <summary>
        /// Внутрення проверка для <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения проверки.</returns>
        protected override async Task<InternalCheckResult> InternalCheck(Update update, CommandHandler handler)
        {
            var currentCheckers = bot.Options.CommandCheckers.Where(x => x.CommandTypes.Contains(CommandType.DynamicReply));
            if (currentCheckers.Any())
            {
                foreach (var commandChecker in currentCheckers)
                {
                    var result = await commandChecker.Checker.Check(bot, update, handler);
                    if (result != InternalCheckResult.Passed)
                        return result;
                }
            }
            return await base.InternalCheck(update, handler);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ReplyDynamicMessageUpdateHandler(PRBotBase bot)
            : base(bot) 
        { 
            RegisterCommands(); 
        }

        #endregion
    }
}
