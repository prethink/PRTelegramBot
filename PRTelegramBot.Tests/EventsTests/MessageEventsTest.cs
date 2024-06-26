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
            var update = UpdateSetUp.CreateMessageTypeContact();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnContactHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnContactHandle)} event was not called.");
            bot.Events.MessageEvents.OnContactHandle -= EventHandler;
        }

        [Test]
        public async Task OnPollShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypePoll();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnPollHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnPollHandle)} event was not called.");
            bot.Events.MessageEvents.OnPollHandle -= EventHandler;
        }

        [Test]
        public async Task OnLocationShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeLocation();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnLocationHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnLocationHandle)} event was not called.");
            bot.Events.MessageEvents.OnLocationHandle -= EventHandler;
        }

        [Test]
        public async Task OnWebAppsShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeWebApp();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnWebAppsHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnWebAppsHandle)} event was not called.");
            bot.Events.MessageEvents.OnWebAppsHandle -= EventHandler;
        }

        [Test]
        public async Task OnDocumentShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeDocument();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnDocumentHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnDocumentHandle)} event was not called.");
            bot.Events.MessageEvents.OnDocumentHandle -= EventHandler;
        }

        [Test]
        public async Task OnAudioShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeAudio();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnAudioHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnAudioHandle)} event was not called.");
            bot.Events.MessageEvents.OnAudioHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeVideo();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoHandle -= EventHandler;
        }

        [Test]
        public async Task OnPhotoShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypePhoto();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnPhotoHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnPhotoHandle)} event was not called.");
            bot.Events.MessageEvents.OnPhotoHandle -= EventHandler;
        }

        [Test]
        public async Task OnStickerShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeSticker();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnStickerHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnStickerHandle)} event was not called.");
            bot.Events.MessageEvents.OnStickerHandle -= EventHandler;
        }

        [Test]
        public async Task OnVoiceShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeVoice();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVoiceHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVoiceHandle)} event was not called.");
            bot.Events.MessageEvents.OnVoiceHandle -= EventHandler;
        }

        [Test]
        public async Task OnVenueShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeVenue();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVenueHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVenueHandle)} event was not called.");
            bot.Events.MessageEvents.OnVenueHandle -= EventHandler;
        }

        [Test]
        public async Task OnGameShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeGame();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGameHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGameHandle)} event was not called.");
            bot.Events.MessageEvents.OnGameHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoNoteShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeVideoNote();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoNoteHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoNoteHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoNoteHandle -= EventHandler;
        }

        [Test]
        public async Task OnDiceShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeDice();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnDiceHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnDiceHandle)} event was not called.");
            bot.Events.MessageEvents.OnDiceHandle -= EventHandler;
        }

        [Test]
        public async Task OnAnimationShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateMessageTypeAnimation();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnAnimationHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnAnimationHandle)} event was not called.");
            bot.Events.MessageEvents.OnAnimationHandle -= EventHandler;
        }

    }
}
