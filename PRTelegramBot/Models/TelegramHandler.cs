using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    public class TelegramHandler
    {
        #region Свойства и константы

        public Func<ITelegramBotClient, Update, Task> Command { get; set; }
        private IServiceProvider serviceProvider { get; set; }

        #endregion

        #region Конструкторы класса

        public TelegramHandler(MethodInfo method, IServiceProvider ServiceProvider = null)
        {
            this.serviceProvider = ServiceProvider;

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
        }

        public TelegramHandler(Func<ITelegramBotClient, Update, Task> command) 
            : this (command, null) { }

        public TelegramHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider = null)
        {
            this.serviceProvider = ServiceProvider;
            this.Command = command;
        }

        #endregion
    }
}
