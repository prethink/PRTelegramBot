using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Tests.Common;

namespace PRTelegramBot.Tests.EventsTests
{
    internal class MessageEventsTest
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
        public async Task OnContactShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeRemovedChatBoost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnRemovedChatBoostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnRemovedChatBoostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnRemovedChatBoostHandle -= EventHandler;
        }
    }
}
