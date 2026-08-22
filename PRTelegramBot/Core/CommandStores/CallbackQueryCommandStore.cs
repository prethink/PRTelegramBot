using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Store for callbackQuery commands.
    /// </summary>
    public sealed class CallbackQueryCommandStore : BaseCommandStore<Enum>
    {
        #region Base class

        /// <summary>
        /// Registers the commands.
        /// </summary>
        public override void RegisterCommand()
        {
            ReflectionUtils.FindEnumHeaders();
            MethodInfo[] methods = ReflectionUtils.FindStaticInlineCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(InlineCallbackHandlerAttribute<>), methods, Commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(InlineCallbackHandlerAttribute<>), methodsInClass, Commands, bot.Options.ServiceProvider);
            }
        }

        /// <summary>
        /// Adds a new command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="delegate">Method that handles the command.</param
        /// <returns>True if the command was added; False if it could not be added.</returns>
        public override bool AddCommand(Enum command, Func<IBotContext, Task> @delegate)
        {
            try
            {
                ReflectionUtils.AddEnumsHeader(command);
                Commands.Add(command, new CommandHandler(@delegate, bot));
                return true;
            }
            catch (Exception ex)
            {
                bot.GetLogger<CallbackQueryCommandStore>().LogErrorInternal(ex);
                return false;
            }
        }

        /// <summary>
        /// Removes the command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>True if the command was removed; False if it could not be removed.</returns>
        public override bool RemoveCommand(Enum command)
        {
            try
            {
                Commands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.GetLogger<CallbackQueryCommandStore>().LogErrorInternal(ex);
                return false;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public CallbackQueryCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
