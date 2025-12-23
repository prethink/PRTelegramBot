using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using System.Diagnostics;

namespace AspNetExample.MiddleWares
{
    public class DIMiddleware : MiddlewareBase
    {
        public override int ExecutionOrder => 1;

        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            Debug.WriteLine($"PreMiddleWare {nameof(DIMiddleware)}");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            Debug.WriteLine($"PostMiddleWare {nameof(DIMiddleware)}");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
