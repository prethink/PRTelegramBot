using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace ConsoleExample.Middlewares
{
    public class TwoMiddleware : MiddlewareBase
    {
        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            Console.WriteLine("Выполнение второго обработчика перед update");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            Console.WriteLine("Выполнение второго обработчика после update");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
