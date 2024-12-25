using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Registrars
{
    internal class InlineClassRegistrar
    {
        public static void Register(PRBotBase bot)
        {
            bot.InlineClassHandlerInstances.Clear();

            var serviceProvider = bot.Options.ServiceProvider;
            foreach (var handler in bot.Options.CommandOptions.InlineClassHandlers)
            {
                if(handler.Value.IsAssignableTo(typeof(ICallbackQueryCommandHandler)))
                {
                    var instance = InstanceFactory.GetOrCreate(handler.Value, serviceProvider);
                    if(instance is ICallbackQueryCommandHandler inlineHandler)
                        bot.InlineClassHandlerInstances.Add(handler.Key, inlineHandler);

                    continue;        
                }
            }
        }
    }
}
