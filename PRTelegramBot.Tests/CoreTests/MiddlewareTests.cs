using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework.Constraints;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Tests.TestModels;
using PRTelegramBot.Tests.TestModels.TestMiddleware;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class MiddlewareTests
    {
        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public async Task MiddlewareExecutesBeforeAndAfterMainLogicWithoutDI()
        {
            var exceptedMessageMainLogin = "Main Logic";
            var update = new Update { Id = 1 };
            var botContextMock = new Mock<IBotContext>();
            var log = new List<string>();

            var middlewareOne = new TestOneMiddleware(log);
            var middlewareTwo = new TestTwoMiddleware(log);
            var middlewareThree = new TestThreeMiddleware(log);

            var bot = new PRBotBuilder("555:ttt").AddMiddlewares(new List<MiddlewareBase>() { middlewareThree, middlewareTwo, middlewareOne }.ToArray()).Build();

            var builder = new MiddlewareBuilder();
            var middlewareChain = builder.Build(bot);

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

        [Test]
        public async Task MiddlewareExecutesBeforeAndAfterMainLogicWithDI()
        {
            var exceptedMessageMainLogin = "Main Logic";
            var update = new Update { Id = 1 };
            var botContextMock = new Mock<IBotContext>();
            var log = new List<string>();

            var middlewareOne = new TestOneMiddleware(log);
            var middlewareTwo = new TestTwoMiddleware(log);
            var middlewareThree = new TestThreeMiddleware(log);

            var serviceProvider = new TestServiceProvider();
            var testLogWrapper = new TestLogWrapper(log);
            ServiceCollection services = new();
            services.AddSingleton<TestLogWrapper>(testLogWrapper);
            services.AddTransient<MiddlewareBase, TestFourMiddlewareDI>();
            services.AddTransient<MiddlewareBase, TestFiveMiddlewareDI>();
            using ServiceProvider provider = services.BuildServiceProvider();

            var bot = new PRBotBuilder("555:ttt")
                .AddMiddlewares(new List<MiddlewareBase>() { middlewareThree, middlewareTwo, middlewareOne }.ToArray())
                .SetServiceProvider(provider)
                .Build();

            var builder = new MiddlewareBuilder();
            using (var scope = new BotDataScope(bot))
            {
                var middlewareChain = builder.Build(bot);
                await middlewareChain.InvokeOnPreUpdateAsync(botContextMock.Object, async () =>
                {
                    log.Add(exceptedMessageMainLogin);
                });

                var expectedLog = new List<string>
                {
                    TestOneMiddleware.NextMessage,
                    TestFiveMiddlewareDI.NextMessage,
                    TestFourMiddlewareDI.NextMessage,
                    TestTwoMiddleware.NextMessage,
                    TestThreeMiddleware.NextMessage,
                    exceptedMessageMainLogin,
                    TestThreeMiddleware.PrevMessage,
                    TestTwoMiddleware.PrevMessage,
                    TestFourMiddlewareDI.PrevMessage,
                    TestFiveMiddlewareDI.PrevMessage,
                    TestOneMiddleware.PrevMessage,
                };

                Assert.AreEqual(expectedLog, log);
            }
        }
    }
}
