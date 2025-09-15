using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Tests.TestModels.TestMiddleware
{
    public class TestThreeMiddleware : MiddlewareBase
    {
        public const string NextMessage = "ThreeNext";
        public const string PrevMessage = "ThreePrev";
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

        public TestThreeMiddleware(List<string> log)
        {
            this.log = log;
        }
    }
}
