using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Tests.Common;

namespace PRTelegramBot.Tests.EventsTests
{
    internal class EventTests
    {
        private PRBot bot { get; set; }

        [OneTimeSetUp]
        public void SetUP()
        {
            bot = new PRBotBuilder("5555:Token").Build();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        [TestCase("hello", "hello ssss")]
        [TestCase("test", "test")]
        [TestCase("gamer", "gamer 1")]
        [TestCase("523523532", "523523532 1")]
        [TestCase("22222", "22222")]
        public async Task OnUserStartWithArgs(string exceptedDeepLink, string message)
        {
            var update = UpdateSetUp.CreateWithStartDeepLink(message);
            bool eventCalled = false;
            string capturedExpectedDeepLink = exceptedDeepLink;

            Task EventHandler(StartEventArgs e)
            {
                eventCalled = true;
                Assert.AreEqual(capturedExpectedDeepLink, e.Data);
                return Task.CompletedTask;
            }

            bot.Events.OnUserStartWithArgs += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnUserStartWithArgs)} event was not called.");
            bot.Events.OnUserStartWithArgs -= EventHandler;
        }

        [Test]
        public async Task OnMissingCommand()
        {
            var update = UpdateSetUp.CreateWithTextMessage("Fgasdfsadjasofdhjasfhasokfhjao");
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.OnMissingCommand += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnMissingCommand)} event was not called.");
            bot.Events.OnMissingCommand -= EventHandler;
        }

        //[Test]
        //public async Task OnCheckPrivilege()
        //{
        //    var update = UpdateSetUp.CreateWithTextMessage("");
        //    bool eventCalled = false;

        //    Task EventHandler(BotEventArgs e)
        //    {
        //        eventCalled = true;
        //        return Task.CompletedTask;
        //    }

        //    bot.Events.OnCheckPrivilege += EventHandler;
        //    await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
        //    Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnCheckPrivilege)} event was not called.");
        //    bot.Events.OnCheckPrivilege -= EventHandler;
        //}

        //[Test]
        //public async Task OnWrongTypeMessage()
        //{
        //    var update = UpdateSetUp.CreateWithTextMessage("");
        //    bool eventCalled = false;
        //    Task EventHandler(BotEventArgs e)
        //    {
        //        eventCalled = true;
        //        return Task.CompletedTask;
        //    }
        //    bot.Events.OnWrongTypeMessage += EventHandler;
        //    await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
        //    Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnWrongTypeMessage)} event was not called.");
        //    bot.Events.OnWrongTypeMessage -= EventHandler;
        //}

        //[Test]
        //public async Task OnWrongTypeChat()
        //{
        //    var update = UpdateSetUp.CreateWithTextMessage("");
        //    bool eventCalled = false;
        //    Task EventHandler(BotEventArgs e)
        //    {
        //        eventCalled = true;
        //        return Task.CompletedTask;
        //    }
        //    bot.Events.OnWrongTypeChat += EventHandler;
        //    await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
        //    Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnWrongTypeChat)} event was not called.");
        //    bot.Events.OnWrongTypeChat -= EventHandler;
        //}

        //[Test]
        //public async Task OnAccessDenied()
        //{
        //    var update = UpdateSetUp.CreateWithTextMessage("");
        //    bool eventCalled = false;
        //    Task EventHandler(BotEventArgs e)
        //    {
        //        eventCalled = true;
        //        return Task.CompletedTask;
        //    }
        //    bot.Events.OnAccessDenied += EventHandler;
        //    await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
        //    Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnAccessDenied)} event was not called.");
        //    bot.Events.OnAccessDenied -= EventHandler;
        //}
    }
}
