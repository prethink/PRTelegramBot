using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;

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
            var update = TestDataFactory.CreateMessageTypeContact();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnContactHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnContactHandle)} event was not called.");
            bot.Events.MessageEvents.OnContactHandle -= EventHandler;
        }

        [Test]
        public async Task OnPollShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypePoll();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnPollHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnPollHandle)} event was not called.");
            bot.Events.MessageEvents.OnPollHandle -= EventHandler;
        }

        [Test]
        public async Task OnLocationShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeLocation();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnLocationHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnLocationHandle)} event was not called.");
            bot.Events.MessageEvents.OnLocationHandle -= EventHandler;
        }

        [Test]
        public async Task OnWebAppsShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeWebApp();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnWebAppsHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnWebAppsHandle)} event was not called.");
            bot.Events.MessageEvents.OnWebAppsHandle -= EventHandler;
        }

        [Test]
        public async Task OnDocumentShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeDocument();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnDocumentHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnDocumentHandle)} event was not called.");
            bot.Events.MessageEvents.OnDocumentHandle -= EventHandler;
        }

        [Test]
        public async Task OnAudioShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeAudio();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnAudioHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnAudioHandle)} event was not called.");
            bot.Events.MessageEvents.OnAudioHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVideo();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoHandle -= EventHandler;
        }

        [Test]
        public async Task OnPhotoShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypePhoto();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnPhotoHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnPhotoHandle)} event was not called.");
            bot.Events.MessageEvents.OnPhotoHandle -= EventHandler;
        }

        [Test]
        public async Task OnStickerShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeSticker();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnStickerHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnStickerHandle)} event was not called.");
            bot.Events.MessageEvents.OnStickerHandle -= EventHandler;
        }

        [Test]
        public async Task OnVoiceShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVoice();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVoiceHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVoiceHandle)} event was not called.");
            bot.Events.MessageEvents.OnVoiceHandle -= EventHandler;
        }

        [Test]
        public async Task OnTextShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeText();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnTextHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnTextHandle)} event was not called.");
            bot.Events.MessageEvents.OnTextHandle -= EventHandler;
        }

        [Test]
        public async Task OnVenueShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVenue();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVenueHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVenueHandle)} event was not called.");
            bot.Events.MessageEvents.OnVenueHandle -= EventHandler;
        }

        [Test]
        public async Task OnGameShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGame();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGameHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGameHandle)} event was not called.");
            bot.Events.MessageEvents.OnGameHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoNoteShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVideoNote();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoNoteHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoNoteHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoNoteHandle -= EventHandler;
        }

        [Test]
        public async Task OnDiceShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeDice();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnDiceHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnDiceHandle)} event was not called.");
            bot.Events.MessageEvents.OnDiceHandle -= EventHandler;
        }

        [Test]
        public async Task OnAnimationShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeAnimation();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnAnimationHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnAnimationHandle)} event was not called.");
            bot.Events.MessageEvents.OnAnimationHandle -= EventHandler;
        }

        [Test]
        public async Task OnStoryShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeStory();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnStoryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnStoryHandle)} event was not called.");
            bot.Events.MessageEvents.OnStoryHandle -= EventHandler;
        }

        [Test]
        public async Task OnPassportDataShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypePassportData();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnPassportDataHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnPassportDataHandle)} event was not called.");
            bot.Events.MessageEvents.OnPassportDataHandle -= EventHandler;
        }

        [Test]
        public async Task OnGiveawayCreatedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGiveawayCreated();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGiveawayCreatedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGiveawayCreatedHandle)} event was not called.");
            bot.Events.MessageEvents.OnGiveawayCreatedHandle -= EventHandler;
        }

        [Test]
        public async Task OnLeftChatMemberShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeLeftChatMember();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChatMemberLeftHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChatMemberLeftHandle)} event was not called.");
            bot.Events.MessageEvents.OnChatMemberLeftHandle -= EventHandler;
        }

        [Test]
        public async Task OnNewChatTitleShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeNewChatTitle();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChatTitleChangedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChatTitleChangedHandle)} event was not called.");
            bot.Events.MessageEvents.OnChatTitleChangedHandle -= EventHandler;
        }

        [Test]
        public async Task OnNewChatPhotoShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeNewChatPhoto();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChatPhotoChangedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChatPhotoChangedHandle)} event was not called.");
            bot.Events.MessageEvents.OnChatPhotoChangedHandle -= EventHandler;
        }

        [Test]
        public async Task OnDeleteChatPhotoShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeDeleteChatPhoto();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChatPhotoDeletedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChatPhotoDeletedHandle)} event was not called.");
            bot.Events.MessageEvents.OnChatPhotoDeletedHandle -= EventHandler;
        }

        [Test]
        public async Task OnGroupChatCreatedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGroupChatCreated();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGroupCreatedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGroupCreatedHandle)} event was not called.");
            bot.Events.MessageEvents.OnGroupCreatedHandle -= EventHandler;
        }

        [Test]
        public async Task OnSupergroupChatCreatedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeSupergroupChatCreated();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnSupergroupCreatedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnSupergroupCreatedHandle)} event was not called.");
            bot.Events.MessageEvents.OnSupergroupCreatedHandle -= EventHandler;
        }

        [Test]
        public async Task OnChannelChatCreatedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeChannelChatCreated();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChannelCreatedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChannelCreatedHandle)} event was not called.");
            bot.Events.MessageEvents.OnChannelCreatedHandle -= EventHandler;
        }

        [Test]
        public async Task OnMessageAutoDeleteTimerChangedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeMessageAutoDeleteTimerChanged();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnMessageAutoDeleteTimerChangedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnMessageAutoDeleteTimerChangedHandle)} event was not called.");
            bot.Events.MessageEvents.OnMessageAutoDeleteTimerChangedHandle -= EventHandler;
        }

        [Test]
        public async Task OnMigrateToChatIdShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeMigrateToChatId();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnMigratedFromGroupHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnMigratedFromGroupHandle)} event was not called.");
            bot.Events.MessageEvents.OnMigratedFromGroupHandle -= EventHandler;
        }

        [Test]
        public async Task OnMigrateFromChatIdShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeMigrateFromChatId();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnMigratedToSupergroupHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnMigratedToSupergroupHandle)} event was not called.");
            bot.Events.MessageEvents.OnMigratedToSupergroupHandle -= EventHandler;
        }

        [Test]
        public async Task OnPinnedMessageShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypePinnedMessage();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnMessagePinnedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnMessagePinnedHandle)} event was not called.");
            bot.Events.MessageEvents.OnMessagePinnedHandle -= EventHandler;
        }

        [Test]
        public async Task OnConnectedWebsiteShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeConnectedWebsite();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnWebsiteConnectedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnWebsiteConnectedHandle)} event was not called.");
            bot.Events.MessageEvents.OnWebsiteConnectedHandle -= EventHandler;
        }

        [Test]
        public async Task OnInvoiceShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeInvoice();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnInvoiceHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnInvoiceHandle)} event was not called.");
            bot.Events.MessageEvents.OnInvoiceHandle -= EventHandler;
        }

        [Test]
        public async Task OnSuccessfulPaymentShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeSuccessfulPayment();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnSuccessfulPaymentHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnSuccessfulPaymentHandle)} event was not called.");
            bot.Events.MessageEvents.OnSuccessfulPaymentHandle -= EventHandler;
        }

        [Test]
        public async Task OnUsersSharedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeUsersShared();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnUserSharedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnUserSharedHandle)} event was not called.");
            bot.Events.MessageEvents.OnUserSharedHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatSharedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeChatShared();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChatSharedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChatSharedHandle)} event was not called.");
            bot.Events.MessageEvents.OnChatSharedHandle -= EventHandler;
        }

        [Test]
        public async Task OnWriteAccessAllowedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeWriteAccessAllowed();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnWriteAccessAllowedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnWriteAccessAllowedHandle)} event was not called.");
            bot.Events.MessageEvents.OnWriteAccessAllowedHandle -= EventHandler;
        }

        [Test]
        public async Task OnPassportDataAllowedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypePassportData();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnPassportDataHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnPassportDataHandle)} event was not called.");
            bot.Events.MessageEvents.OnPassportDataHandle -= EventHandler;
        }

        [Test]
        public async Task OnProximityAlertTriggeredShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeProximityAlertTriggered();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnProximityAlertTriggeredHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnProximityAlertTriggeredHandle)} event was not called.");
            bot.Events.MessageEvents.OnProximityAlertTriggeredHandle -= EventHandler;
        }

        [Test]
        public async Task OnBoostAddedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeBoostAdded();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnBoostAddedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnBoostAddedHandle)} event was not called.");
            bot.Events.MessageEvents.OnBoostAddedHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatBackgroundSetShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeChatBackgroundSet();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnChatBackgroundSetHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnChatBackgroundSetHandle)} event was not called.");
            bot.Events.MessageEvents.OnChatBackgroundSetHandle -= EventHandler;
        }

        [Test]
        public async Task OnForumTopicCreatedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeForumTopicCreated();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnForumTopicCreatedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnForumTopicCreatedHandle)} event was not called.");
            bot.Events.MessageEvents.OnForumTopicCreatedHandle -= EventHandler;
        }

        [Test]
        public async Task OnForumTopicEditedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeForumTopicEdited();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnForumTopicEditedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnForumTopicEditedHandle)} event was not called.");
            bot.Events.MessageEvents.OnForumTopicEditedHandle -= EventHandler;
        }

        [Test]
        public async Task OnForumTopicClosedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeForumTopicClosed();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnForumTopicClosedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnForumTopicClosedHandle)} event was not called.");
            bot.Events.MessageEvents.OnForumTopicClosedHandle -= EventHandler;
        }

        [Test]
        public async Task OnForumTopicReopenedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeForumTopicReopened();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnForumTopicReopenedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnForumTopicReopenedHandle)} event was not called.");
            bot.Events.MessageEvents.OnForumTopicReopenedHandle -= EventHandler;
        }

        [Test]
        public async Task OnGeneralForumTopicHiddenShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGeneralForumTopicHidden();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGeneralForumTopicHiddenHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGeneralForumTopicHiddenHandle)} event was not called.");
            bot.Events.MessageEvents.OnGeneralForumTopicHiddenHandle -= EventHandler;
        }

        [Test]
        public async Task OnGeneralForumTopicUnhiddenShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGeneralForumTopicUnhidden();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGeneralForumTopicUnhiddenHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGeneralForumTopicUnhiddenHandle)} event was not called.");
            bot.Events.MessageEvents.OnGeneralForumTopicUnhiddenHandle -= EventHandler;
        }

        [Test]
        public async Task OnGiveawayShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGiveaway();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGiveawayHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGiveawayHandle)} event was not called.");
            bot.Events.MessageEvents.OnGiveawayHandle -= EventHandler;
        }


        [Test]
        public async Task OnGiveawayWinnersShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGiveawayWinners();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGiveawayWinnersHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGiveawayWinnersHandle)} event was not called.");
            bot.Events.MessageEvents.OnGiveawayWinnersHandle -= EventHandler;
        }

        [Test]
        public async Task OnGiveawayCompletedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeGiveawayCompleted();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnGiveawayCompletedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnGiveawayCompletedHandle)} event was not called.");
            bot.Events.MessageEvents.OnGiveawayCompletedHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoChatScheduledShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVideoChatScheduled();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoChatScheduledHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoChatScheduledHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoChatScheduledHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoChatStartedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVideoChatStarted();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoChatStartedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoChatStartedHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoChatStartedHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoChatEndedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVideoChatEnded();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoChatEndedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoChatEndedHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoChatEndedHandle -= EventHandler;
        }

        [Test]
        public async Task OnVideoChatParticipantsInvitedShouldBeInvoked()
        {
            var update = TestDataFactory.CreateMessageTypeVideoChatParticipantsInvited();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.MessageEvents.OnVideoChatParticipantsInvitedHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.MessageEvents.OnVideoChatParticipantsInvitedHandle)} event was not called.");
            bot.Events.MessageEvents.OnVideoChatParticipantsInvitedHandle -= EventHandler;
        }

        /* TODO Тесты для
         *  OnPreReplyCommandHandle
            OnPostReplyCommandHandle
            OnPreDynamicReplyCommandHandle
            OnPostDynamicReplyCommandHandle
            OnPreSlashCommandHandle
            OnPostSlashCommandHandle
            OnPreInlineCommandHandle
            OnPostInlineCommandHandle
            OnPreNextStepCommandHandle
            OnPostNextStepCommandHandle
         * 
         */
    }
}
