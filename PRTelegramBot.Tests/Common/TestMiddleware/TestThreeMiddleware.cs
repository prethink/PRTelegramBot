using PRTelegramBot.Core.Middlewares;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.Common.TestMiddleware
{
    public class TestThreeMiddleware : MiddlewareBase
    {
        public const string NextMessage = "ThreeNext";
        public const string PrevMessage = "ThreePrev";
        private List<string> log;

        public override async Task InvokeOnPreUpdateAsync(ITelegramBotClient botClient, Update update, Func<Task> next)
        {
            log.Add(NextMessage);
            await base.InvokeOnPreUpdateAsync(botClient, update, next);
        }

        public override Task InvokeOnPostUpdateAsync(ITelegramBotClient botClient, Update update)
        {
            log.Add(PrevMessage);
            return base.InvokeOnPostUpdateAsync(botClient, update);
        }

        public TestThreeMiddleware(List<string> log)
        {
            this.log = log;
        }
    }
}
