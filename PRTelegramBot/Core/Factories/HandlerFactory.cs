using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Factories
{
    internal class HandlerFactory
    {
        public CommandHandler CreateHandler(IBaseQueryAttribute attr, MethodInfo method, IServiceProvider serviceProvider)
        {
            if (attr is IStringQueryAttribute stringAttribute)
                return CreateStringHandler(stringAttribute, method, serviceProvider);
            else
                return new CommandHandler(method, serviceProvider);
        }

        public CommandHandler CreateHandler(IBaseQueryAttribute attr, Func<ITelegramBotClient, Update, Task> command, IServiceProvider serviceProvider)
        {
            if (attr is IStringQueryAttribute stringAttribute)
                return CreateStringHandler(stringAttribute, command, serviceProvider);
            else
                return new CommandHandler(command, serviceProvider);
        }

        private CommandHandler CreateStringHandler(IStringQueryAttribute attribute, MethodInfo method, IServiceProvider serviceProvider)
        {
            return new StringCommandHandler(method, serviceProvider, attribute.CommandComparison, attribute.StringComparison);
        }

        private CommandHandler CreateStringHandler(IStringQueryAttribute attribute, Func<ITelegramBotClient, Update, Task> command, IServiceProvider serviceProvider)
        {
            return new StringCommandHandler(command, serviceProvider, attribute.CommandComparison, attribute.StringComparison);
        }
    }
}
