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
    public sealed class ReplyDynamicMessageUpdateHandler : ReplyMessageUpdateHandler
    {
        #region Поля и свойства

        public override MessageType TypeMessage => MessageType.Text;

        public override CommandType CommandType => CommandType.DynamicReply;

        #endregion

        #region Методы


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


        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ReplyDynamicMessageUpdateHandler(PRBotBase bot)
            : base(bot) 
        { 
        }

        #endregion
    }
}
