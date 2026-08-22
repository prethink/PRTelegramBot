using PRTelegramBot.Attributes;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Store for dynamic reply commands.
    /// </summary>
    public sealed class ReplyDynamicCommandStore : ReplyCommandStore
    {
        #region Base class

        /// <summary>
        /// Registers the commands.
        /// </summary>
        public override void RegisterCommand()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticDynamicReplyCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(ReplyMenuDynamicHandlerAttribute), methods, Commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(ReplyMenuDynamicHandlerAttribute), methodsInClass, Commands, bot.Options.ServiceProvider);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ReplyDynamicCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
