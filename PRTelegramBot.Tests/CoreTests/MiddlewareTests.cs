using Moq;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Tests.Common.TestMiddleware;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class MiddlewareTests
    {
        [Test]
        public async Task MiddlewareExecutesBeforeAndAfterMainLogic()
        {
            var exceptedMessageMainLogin = "Main Logic";
            // Arrange
            var update = new Update { Id = 1 };
            var botClientMock = new Mock<ITelegramBotClient>();
            var log = new List<string>();

            var middlewareOne = new TestOneMiddleware(log);
            var middlewareTwo = new TestTwoMiddleware(log);
            var middlewareThree = new TestThreeMiddleware(log);

            var builder = new MiddlewareBuilder();
            var middlewareChain = builder.Build(new List<MiddlewareBase>() { middlewareOne, middlewareTwo, middlewareThree });

            // Act
            await middlewareChain.InvokeOnPreUpdateAsync(botClientMock.Object, update, async () =>
            {
                log.Add(exceptedMessageMainLogin);
            });

            // Assert
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
