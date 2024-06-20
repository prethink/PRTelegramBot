using PRTelegramBot.Attributes;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик для динамических reply команд.
    /// </summary>
    public sealed class ReplyDynamicMessageUpdateHandler : ReplyMessageUpdateHandler
    {
        #region Методы

        protected override void RegisterCommands()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticMessageMenuDictionaryHandlers(bot.Options.BotId);
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
            : base(bot) { }

        #endregion
    }
}
