using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Tests.Common;

namespace PRTelegramBot.Tests.EventsTests
{
    internal class EventTests
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
        [TestCase("hello", "hello ssss")]
        [TestCase("test", "test")]
        [TestCase("gamer", "gamer 1")]
        [TestCase("523523532", "523523532 1")]
        [TestCase("22222", "22222")]
        public async Task OnUserStartWithArgsShouldBeInvoked(string exceptedDeepLink, string message)
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
        public async Task OnMissingCommandShouldBeInvoked()
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
        //public async Task OnCheckPrivilegeShouldBeInvoked()
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
        //public async Task OnWrongTypeMessageShouldBeInvoked()
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
        //public async Task OnWrongTypeChatShouldBeInvoked()
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

        [Test]
        public async Task OnAccessDeniedShouldBeInvoked()
        {
            var testUserId = 55555;
            var update = UpdateSetUp.CreateUpdateWithTypeMessage();
            await bot.Options.WhiteListManager.AddUser(testUserId);
            bool eventCalled = false;
            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }
            bot.Events.OnAccessDenied += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnAccessDenied)} event was not called.");
            bot.Events.OnAccessDenied -= EventHandler;
            await bot.Options.WhiteListManager.RemoveUser(testUserId);
        }

        [Test]
        public async Task OnAccessDeniedShouldBeNotInvoked()
        {
            long userId = 5555;
            var update = UpdateSetUp.CreateUpdateWithTypeMessage(userId);
            await bot.Options.WhiteListManager.AddUser(userId);
            bool eventCalled = false;
            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }
            bot.Events.OnAccessDenied += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsFalse(eventCalled, $"The {nameof(bot.Events.OnAccessDenied)} event was not called.");
            bot.Events.OnAccessDenied -= EventHandler;
            await bot.Options.WhiteListManager.RemoveUser(userId);
        }

        [Test]
        public async Task OnErrorLog()
        {
            var tcs = new TaskCompletionSource<bool>();
            bot.Events.OnErrorLog += EventHandler;
            bot.Events.OnErrorLogInvoke(new Exception("Error"));
            Task EventHandler(BotEventArgs e)
            {
                tcs.SetResult(true);
                return Task.CompletedTask;
            }
            bool eventCalled = await TimeoutAfter(tcs.Task, TimeSpan.FromSeconds(1));
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnErrorLog)} event was not called.");
            bot.Events.OnErrorLog -= EventHandler;
        }

        [Test]
        public async Task OnCommonLog()
        {
            var tcs = new TaskCompletionSource<bool>();
            bot.Events.OnCommonLog += EventHandler;
            bot.Events.OnCommonLogInvoke("Test");
            Task EventHandler(BotEventArgs e)
            {
                tcs.SetResult(true);
                return Task.CompletedTask;
            }
            bool eventCalled = await TimeoutAfter(tcs.Task, TimeSpan.FromSeconds(1));
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.OnCommonLog)} event was not called.");
            bot.Events.OnCommonLog -= EventHandler;
        }

        private async Task<bool> TimeoutAfter(Task task, TimeSpan timeout)
        {
            var timeoutTask = Task.Delay(timeout);
            var completedTask = await Task.WhenAny(task, timeoutTask);
            return completedTask == task;
        }
    }
}
