using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace ConsoleExample.Middlewares
{
    public class ThreeMiddleware : MiddlewareBase
    {
        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            var bot = CurrentScope.Bot;
            var ctx = CurrentScope.Context;

            Console.WriteLine("Выполнение третьего обработчика перед update");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            var bot = CurrentScope.Bot;
            var ctx = CurrentScope.Context;

            Console.WriteLine("Выполнение третьего обработчика после update");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
