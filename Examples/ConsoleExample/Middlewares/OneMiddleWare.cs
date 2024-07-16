using PRTelegramBot.Core.Middlewares;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Middlewares
{
    public class OneMiddleware : MiddlewareBase
    {
        public override async Task InvokeOnPreUpdateAsync(ITelegramBotClient botClient, Update update, Func<Task> next)
        {
            Console.WriteLine("Выполнение первого обработчика перед update");
            await base.InvokeOnPreUpdateAsync(botClient, update, next);
        }

        public override Task InvokeOnPostUpdateAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine("Выполнение первого обработчика после update");
            return base.InvokeOnPostUpdateAsync(botClient, update);
        }
    }
}
