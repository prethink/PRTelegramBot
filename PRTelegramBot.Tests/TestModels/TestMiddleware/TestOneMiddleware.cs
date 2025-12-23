using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Tests.TestModels.TestMiddleware
{
    public class TestOneMiddleware : MiddlewareBase
    {
        public const string NextMessage = "OneNext";
        public const string PrevMessage = "OnePrev";
        private List<string> log;

        public override int ExecutionOrder => 0;

        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            log.Add(NextMessage);
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            log.Add(PrevMessage);
            return base.InvokeOnPostUpdateAsync(context);
        }

        public TestOneMiddleware(List<string> log)
        {
            this.log = log;
        }
    }
}
