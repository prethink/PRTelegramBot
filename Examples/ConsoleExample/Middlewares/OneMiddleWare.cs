using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace ConsoleExample.Middlewares
{
    public class OneMiddleware : MiddlewareBase
    {
        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            Console.WriteLine("Выполнение первого обработчика перед update");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            Console.WriteLine("Выполнение первого обработчика после update");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
