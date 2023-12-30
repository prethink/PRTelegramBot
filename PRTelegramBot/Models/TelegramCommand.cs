using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using PRTelegramBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    public class TelegramCommand
    {
        public Func<ITelegramBotClient, Update, Task> Command { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public TelegramCommand(MethodInfo method, IServiceProvider ServiceProvider = null)
        {
            this.ServiceProvider = ServiceProvider;

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

        public TelegramCommand(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider = null)
        {
            this.ServiceProvider = ServiceProvider;
            this.Command = command;
        }
    }
}
