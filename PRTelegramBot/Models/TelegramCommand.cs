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
        public Type Type { get; set; }
        public bool IsStatic { get; set; }
        public MethodInfo Method { get; set; }
        public Func<ITelegramBotClient, Update, Task> Command { get; set; }
        public Func<ITelegramBotClient, Update, CustomParameters, Task> CommandWithCustomParams { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public Func<ITelegramBotClient, Update, Task> GetExecuteMethod()
        {
            if(Command != null)
            {
                return Command;
            }

            if (!IsStatic && Type != null)
            {
                object instance = null;
                if(ServiceProvider != null)
                {
                    using (var scope = ServiceProvider.CreateScope())
                    {
                        instance = scope.ServiceProvider.GetRequiredService(Type);
                    }
                }
                else
                {
                    instance = ReflectionUtils.CreateInstanceWithNullArguments(Type);
                }
                var instanceMethod = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), instance, Method);
                return ((Func<ITelegramBotClient, Update, Task>)instanceMethod);
            }


            throw new NotImplementedException();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CustomParameters param = null)
        {
            if(CommandWithCustomParams != null)
            {
                CommandWithCustomParams.Invoke(botClient, update, param);
                return;
            }
            var method = GetExecuteMethod();
            if(method != null)
            {
                await method(botClient, update);
                return;
            }

            throw new NotImplementedException();    
         
        }

    }
}
