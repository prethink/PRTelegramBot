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
        public StringComparison Comparison { get; set; }
        public Func<ITelegramBotClient, Update, Task> Command { get; set; }
        private IServiceProvider serviceProvider { get; set; }

        #endregion

        #region Конструкторы класса

        public TelegramHandler(MethodInfo method)
            : this(method, null, StringComparison.OrdinalIgnoreCase) { }

        public TelegramHandler(MethodInfo method, StringComparison comparison)
            : this(method, null, comparison) { }

        public TelegramHandler(MethodInfo method, IServiceProvider ServiceProvider)
            : this(method, ServiceProvider, StringComparison.OrdinalIgnoreCase) { }

        public TelegramHandler(MethodInfo method, IServiceProvider ServiceProvider, StringComparison comparison)
        {
            this.serviceProvider = ServiceProvider;
            this.Comparison = comparison;

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
            Comparison = comparison;
        }

        public TelegramHandler(Func<ITelegramBotClient, Update, Task> command) 
            : this (command, null, StringComparison.OrdinalIgnoreCase) { }

        public TelegramHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider)
            : this(command, ServiceProvider, StringComparison.OrdinalIgnoreCase) { }

        public TelegramHandler(Func<ITelegramBotClient, Update, Task> command, StringComparison comparison)
            : this(command, null, comparison) { }

        public TelegramHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider, StringComparison comparison)
        {
            this.serviceProvider = ServiceProvider;
            this.Command = command;
            this.Comparison = comparison;
        }

        #endregion
    }
}
