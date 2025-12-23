using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using System.Reflection;

namespace PRTelegramBot.Core.Factories
{
    internal class HandlerFactory
    {
        public CommandHandler CreateHandler(IBaseQueryAttribute attr, MethodInfo method, PRBotBase bot)
        {
            if (attr is IStringQueryAttribute stringAttribute)
                return CreateStringHandler(stringAttribute, method, bot);
            else
                return new CommandHandler(method, bot);
        }

        public CommandHandler CreateHandler(IBaseQueryAttribute attr, Func<IBotContext, Task> command, PRBotBase bot)
        {
            if (attr is IStringQueryAttribute stringAttribute)
                return CreateStringHandler(stringAttribute, command, bot);
            else
                return new CommandHandler(command, bot);
        }

        private CommandHandler CreateStringHandler(IStringQueryAttribute attribute, MethodInfo method, PRBotBase bot)
        {
            return new StringCommandHandler(method, bot, attribute.CommandComparison, attribute.StringComparison);
        }

        private CommandHandler CreateStringHandler(IStringQueryAttribute attribute, Func<IBotContext, Task> command, PRBotBase bot)
        {
            return new StringCommandHandler(command, bot, attribute.CommandComparison, attribute.StringComparison);
        }
    }
}
