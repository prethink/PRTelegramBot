using PRTelegramBot.Attributes;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Store for reply commands.
    /// </summary>
    public class ReplyCommandStore : MessageCommandStore
    {
        #region Base class

        /// <summary>
        /// Registers the commands.
        /// </summary>
        public override void RegisterCommand()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticReplyCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(ReplyMenuHandlerAttribute), methods, Commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(ReplyMenuHandlerAttribute), methodsInClass, Commands, bot.Options.ServiceProvider);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public ReplyCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
