using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Store for slash commands.
    /// </summary>
    public sealed class SlashCommandStore : MessageCommandStore
    {
        #region Base class

        /// <summary>
        /// Registers the commands.
        /// </summary>
        public override void RegisterCommand()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticSlashCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(SlashHandlerAttribute), methods, Commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(SlashHandlerAttribute), methodsInClass, Commands, bot.Options.ServiceProvider);
            }
        }

        /// <summary>
        /// Adds a new command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="delegate">Method that handles the command.</param>
        /// <returns>True if the command was added; False if it could not be added.</returns>
        public override bool AddCommand(string command, Func<IBotContext, Task> @delegate)
        {
            if (!command.StartsWith('/'))
                command = "/" + command;

            return base.AddCommand(command, @delegate);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public SlashCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
