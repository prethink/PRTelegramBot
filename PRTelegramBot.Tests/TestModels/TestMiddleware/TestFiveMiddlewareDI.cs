using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Tests.TestModels.TestMiddleware
{
    internal class TestFiveMiddlewareDI : MiddlewareBase
    {
        public const string NextMessage = "FiveDINext";
        public const string PrevMessage = "FiveDIPrev";

        public override int ExecutionOrder => 1;

        private TestLogWrapper logWrapper;

        public TestFiveMiddlewareDI(TestLogWrapper logWrapper)
        {
            this.logWrapper = logWrapper;
        }

        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            logWrapper.Logs.Add(NextMessage);
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            logWrapper.Logs.Add(PrevMessage);
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
