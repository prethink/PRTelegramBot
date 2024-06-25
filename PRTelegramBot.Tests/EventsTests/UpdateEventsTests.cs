using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Tests.Common;

namespace PRTelegramBot.Tests.EventsTests
{
    internal class UpdateEventsTests
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
        public async Task OnEditedMessageShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeEditedMessage();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnEditedMessageHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnEditedMessageHandle)} event was not called.");
            bot.Events.UpdateEvents.OnEditedMessageHandle -= EventHandler;
        }

        [Test]
        public async Task OnChannelPostShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeChannelPost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChannelPostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChannelPostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChannelPostHandle -= EventHandler;
        }

        [Test]
        public async Task OnEditedChannelPostShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeEditedChannelPost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnEditedChannelPostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnEditedChannelPostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnEditedChannelPostHandle -= EventHandler;
        }

        [Test]
        public async Task OnEditedBusinessConnectionShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeBusinessConnection();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnBusinessConnectionHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnBusinessConnectionHandle)} event was not called.");
            bot.Events.UpdateEvents.OnBusinessConnectionHandle -= EventHandler;
        }

        [Test]
        public async Task OnEditedBusinessMessageShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeEditedBusinessMessage();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnEditedBusinessMessageHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnEditedBusinessMessageHandle)} event was not called.");
            bot.Events.UpdateEvents.OnEditedBusinessMessageHandle -= EventHandler;
        }

        [Test]
        public async Task OnDeletedBusinessMessagesHandleShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeDeletedBusinessMessages();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnDeletedBusinessMessagesHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnDeletedBusinessMessagesHandle)} event was not called.");
            bot.Events.UpdateEvents.OnDeletedBusinessMessagesHandle -= EventHandler;
        }

        [Test]
        public async Task OnMessageReactionHandleShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeMessageReaction();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnMessageReactionHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnMessageReactionHandle)} event was not called.");
            bot.Events.UpdateEvents.OnMessageReactionHandle -= EventHandler;
        }

        [Test]
        public async Task OnMessageReactionCountShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeMessageReactionCount();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnMessageReactionCountHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnMessageReactionCountHandle)} event was not called.");
            bot.Events.UpdateEvents.OnMessageReactionCountHandle -= EventHandler;
        }

        [Test]
        public async Task OnInlineQueryShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeInlineQuery();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnInlineQueryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnInlineQueryHandle)} event was not called.");
            bot.Events.UpdateEvents.OnInlineQueryHandle -= EventHandler;
        }

        [Test]
        public async Task OnChosenInlineResultShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeChosenInlineResult();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChosenInlineResultHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChosenInlineResultHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChosenInlineResultHandle -= EventHandler;
        }

        [Test]
        public async Task OnShippingQueryShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeShippingQuery();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnShippingQueryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnShippingQueryHandle)} event was not called.");
            bot.Events.UpdateEvents.OnShippingQueryHandle -= EventHandler;
        }

        [Test]
        public async Task OnPreCheckoutQueryShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypePreCheckoutQuery();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPreCheckoutQueryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPreCheckoutQueryHandle)} event was not called.");
            bot.Events.UpdateEvents.OnPreCheckoutQueryHandle -= EventHandler;
        }

        [Test]
        public async Task OnPollShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypePoll();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPollHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPollHandle)} event was not called.");
            bot.Events.UpdateEvents.OnPollHandle -= EventHandler;
        }

        [Test]
        public async Task OnPollAnswerShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypePollAnswer();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPollAnswerHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPollAnswerHandle)} event was not called.");
            bot.Events.UpdateEvents.OnPollAnswerHandle -= EventHandler;
        }

        [Test]
        public async Task OnMyChatMemberShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeMyChatMember();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnMyChatMemberHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnMyChatMemberHandle)} event was not called.");
            bot.Events.UpdateEvents.OnMyChatMemberHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatMemberShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeChatMember();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChatMemberHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChatMemberHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChatMemberHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatJoinRequestShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeChatJoinRequest();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChatJoinRequestHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChatJoinRequestHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChatJoinRequestHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatBoostShouldBeInvoked()
        {
            var update = UpdateSetUp.CreateUpdateTypeChatBoost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChatBoostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.botClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChatBoostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChatBoostHandle -= EventHandler;
        }

        [Test]
        public async Task OnRemovedChatBoostShouldBeInvoked()
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
