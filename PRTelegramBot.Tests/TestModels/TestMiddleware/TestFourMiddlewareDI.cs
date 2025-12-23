using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Tests.TestModels.TestMiddleware
{
    internal class TestFourMiddlewareDI : MiddlewareBase
    {
        public const string NextMessage = "FourDINext";
        public const string PrevMessage = "FourDIPrev";

        public override int ExecutionOrder => 2;

        private TestLogWrapper logWrapper;

        public TestFourMiddlewareDI(TestLogWrapper logWrapper)
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
