using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace ConsoleExample.Middlewares
{
    public class TwoMiddleware : MiddlewareBase
    {
        public override int ExecutionOrder => 3;

        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            Console.WriteLine("Running the second handler before the update");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            Console.WriteLine("Running the second handler after the update");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
