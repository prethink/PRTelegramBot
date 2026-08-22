using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Common command handler.
    /// </summary>
    public class CommandHandler 
    {
        #region Fields and properties

        /// <summary>
        /// Command comparison.
        /// </summary>
        public CommandComparison CommandComparison { get;}

        /// <summary>
        /// Bot.
        /// </summary>
        private PRBotBase bot { get; set; }

        /// <summary>
        /// Information about the method.
        /// </summary>
        public MethodInfo Method { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public async Task ExecuteCommand(IBotContext context)
        {
            if (Method is null)
                return;

            if (Method.IsStatic)
            {
                Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), Method, false);
                await ((Func<IBotContext, Task>)serverMessageHandler).Invoke(context);
            }
            else
            {
                if (bot.HasServiceProvider)
                {
                    using(var scope = bot.CreateServiceScope())
                    {
                        var instance = scope.ServiceProvider.GetRequiredService(Method.DeclaringType);
                        var instanceMethod = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), instance, Method);
                        await (((Func<IBotContext, Task>)instanceMethod)).Invoke(context);
                    }
                }
                else
                {
                    var instance = ReflectionUtils.CreateInstanceWithNullArguments(Method.DeclaringType);
                    var instanceMethod = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), instance, Method);
                    await (((Func<IBotContext, Task>)instanceMethod)).Invoke(context);
                }
            }
        }

        #endregion

        #region Class constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        public CommandHandler(MethodInfo method)
            : this(method, null , CommandComparison.Equals) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public CommandHandler(MethodInfo method, CommandComparison commandComparison)
            : this(method, null, commandComparison) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        /// <param name="bot">Bot.</param>
        public CommandHandler(MethodInfo method, PRBotBase bot)
            : this(method, bot , CommandComparison.Equals) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        public CommandHandler(Func<IBotContext, Task> command) 
            : this (command, null, CommandComparison.Equals) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="bot">Bot.</param>
        public CommandHandler(Func<IBotContext, Task> command, PRBotBase bot)
            : this(command, bot, CommandComparison.Equals) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public CommandHandler(Func<IBotContext, Task> command, CommandComparison commandComparison)
            : this(command, null, commandComparison) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="bot">Bot.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public CommandHandler(Func<IBotContext, Task> command, PRBotBase bot, CommandComparison commandComparison)
            : this(command.Method, bot, commandComparison) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">Method.</param>
        /// <param name="bot">Bot.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public CommandHandler(MethodInfo method, PRBotBase bot, CommandComparison commandComparison)
        {
            this.bot = bot;
            this.CommandComparison = commandComparison;
            this.Method = method;
        }

        #endregion
    }
}
