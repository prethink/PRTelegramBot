using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace ConsoleExample.Middlewares
{
    public class ThreeMiddleware : MiddlewareBase
    {
        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            Console.WriteLine("Выполнение третьего обработчика перед update");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            Console.WriteLine("Выполнение третьего обработчика после update");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
