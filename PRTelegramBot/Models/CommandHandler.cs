using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    public class CommandHandler 
    {
        #region Свойства и константы
        public CommandComparison CommandComparison { get;}
        public Func<ITelegramBotClient, Update, Task> Command { get; }
        private IServiceProvider serviceProvider { get; set; }

        #endregion

        #region Конструкторы класса

        public CommandHandler(MethodInfo method)
            : this(method, null , CommandComparison.Equals) { }

        public CommandHandler(MethodInfo method, CommandComparison commandComparison)
            : this(method, null, commandComparison) { }

        public CommandHandler(MethodInfo method, IServiceProvider ServiceProvider)
            : this(method, ServiceProvider , CommandComparison.Equals) { }

        public CommandHandler(MethodInfo method, IServiceProvider ServiceProvider, CommandComparison commandComparison)
        {
            this.serviceProvider = ServiceProvider;
            this.CommandComparison = commandComparison;

            if (method.IsStatic)
            {
                Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), method, false);
                Command = (Func<ITelegramBotClient, Update, Task>)serverMessageHandler;
            }
            else
            {
                object instance = null;
                if (ServiceProvider != null)
                {
                    using (var scope = ServiceProvider.CreateScope())
                    {
                        instance = scope.ServiceProvider.GetRequiredService(method.DeclaringType);
                    }
                }
                else
                {
                    instance = ReflectionUtils.CreateInstanceWithNullArguments(method.DeclaringType);
                }
                var instanceMethod = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), instance, method);
                Command = ((Func<ITelegramBotClient, Update, Task>)instanceMethod);
            }
            CommandComparison = commandComparison;
        }

        public CommandHandler(Func<ITelegramBotClient, Update, Task> command) 
            : this (command, null, CommandComparison.Equals) { }

        public CommandHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider)
            : this(command, ServiceProvider, CommandComparison.Equals) { }

        public CommandHandler(Func<ITelegramBotClient, Update, Task> command, CommandComparison commandComparison)
            : this(command, null, commandComparison) { }

        public CommandHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider, CommandComparison commandComparison)
        {
            this.serviceProvider = ServiceProvider;
            this.Command = command;
            this.CommandComparison = commandComparison;
        }

        #endregion
    }
}
