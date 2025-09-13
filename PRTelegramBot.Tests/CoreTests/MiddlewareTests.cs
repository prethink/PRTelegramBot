using Moq;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Tests.TestModels.TestMiddleware;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class MiddlewareTests
    {
        [Test]
        public async Task MiddlewareExecutesBeforeAndAfterMainLogic()
        {
            var exceptedMessageMainLogin = "Main Logic";
            var update = new Update { Id = 1 };
            var botContextMock = new Mock<IBotContext>();
            var log = new List<string>();

            var middlewareOne = new TestOneMiddleware(log);
            var middlewareTwo = new TestTwoMiddleware(log);
            var middlewareThree = new TestThreeMiddleware(log);

            var builder = new MiddlewareBuilder();
            var middlewareChain = builder.Build(new List<MiddlewareBase>() { middlewareOne, middlewareTwo, middlewareThree });

            await middlewareChain.InvokeOnPreUpdateAsync(botContextMock.Object, async () =>
            {
                log.Add(exceptedMessageMainLogin);
            });

            var expectedLog = new List<string>
            {
                TestOneMiddleware.NextMessage,
                TestTwoMiddleware.NextMessage,
                TestThreeMiddleware.NextMessage,
                exceptedMessageMainLogin,
                TestThreeMiddleware.PrevMessage,
                TestTwoMiddleware.PrevMessage,
                TestOneMiddleware.PrevMessage,
            };

            Assert.AreEqual(expectedLog, log);
        }
    }
}
