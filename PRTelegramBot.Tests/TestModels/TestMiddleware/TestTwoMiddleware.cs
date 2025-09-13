using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Tests.TestModels.TestMiddleware
{
    public class TestTwoMiddleware : MiddlewareBase
    {
        public const string NextMessage = "TwoNext";
        public const string PrevMessage = "TwoPrev";
        private List<string> log;

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

        public TestTwoMiddleware(List<string> log)
        {
            this.log = log;
        }

    }
}
