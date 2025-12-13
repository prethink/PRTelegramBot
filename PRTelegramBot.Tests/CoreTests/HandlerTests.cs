using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class HandlerTests
    {
        private PRBotBase bot { get; set; }

        [OneTimeSetUp]
        public void SetUP()
        {
            bot = new PRBotBuilder("5555:Token").Build();
            bot.ReloadHandlers();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public async Task HandleUpdateAsyncShouldTriggerErrorLogWhenHandlerThrows()
        {
            var update = TestDataFactory.CreateUpdateTypeShippingQuery();
            bool errorEventCalled = false;
            Exception capturedException = null;

            bot.Events.OnErrorLog += (args) =>
            {
                errorEventCalled = true;
                capturedException = args.Exception;
                return Task.CompletedTask;
            };

            Task ThrowingHandler(BotEventArgs e)
            {
                throw new InvalidOperationException("Test exception");
            }

            bot.Events.UpdateEvents.OnShippingQueryHandle += ThrowingHandler;

            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);

            Assert.IsTrue(errorEventCalled, "Error event was not triggered.");
            Assert.IsNotNull(capturedException, "Exception was not captured.");
            Assert.IsInstanceOf<InvalidOperationException>(capturedException);

            bot.Events.UpdateEvents.OnShippingQueryHandle -= ThrowingHandler;
        }
    }
}
