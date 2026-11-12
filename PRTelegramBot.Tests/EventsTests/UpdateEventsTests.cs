using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

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
            var update = TestDataFactory.CreateUpdateTypeEditedMessage();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnEditedMessageHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnEditedMessageHandle)} event was not called.");
            bot.Events.UpdateEvents.OnEditedMessageHandle -= EventHandler;
        }

        [Test]
        public async Task OnChannelPostShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeChannelPost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChannelPostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChannelPostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChannelPostHandle -= EventHandler;
        }

        [Test]
        public async Task OnEditedChannelPostShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeEditedChannelPost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnEditedChannelPostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnEditedChannelPostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnEditedChannelPostHandle -= EventHandler;
        }

        [Test]
        public async Task OnEditedBusinessConnectionShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeBusinessConnection();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnBusinessConnectionHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnBusinessConnectionHandle)} event was not called.");
            bot.Events.UpdateEvents.OnBusinessConnectionHandle -= EventHandler;
        }

        [Test]
        public async Task OnEditedBusinessMessageShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeEditedBusinessMessage();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnEditedBusinessMessageHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnEditedBusinessMessageHandle)} event was not called.");
            bot.Events.UpdateEvents.OnEditedBusinessMessageHandle -= EventHandler;
        }

        [Test]
        public async Task OnDeletedBusinessMessagesHandleShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeDeletedBusinessMessages();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnDeletedBusinessMessagesHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnDeletedBusinessMessagesHandle)} event was not called.");
            bot.Events.UpdateEvents.OnDeletedBusinessMessagesHandle -= EventHandler;
        }

        [Test]
        public async Task OnMessageReactionHandleShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeMessageReaction();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnMessageReactionHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnMessageReactionHandle)} event was not called.");
            bot.Events.UpdateEvents.OnMessageReactionHandle -= EventHandler;
        }

        [Test]
        public async Task OnMessageReactionCountShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeMessageReactionCount();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnMessageReactionCountHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnMessageReactionCountHandle)} event was not called.");
            bot.Events.UpdateEvents.OnMessageReactionCountHandle -= EventHandler;
        }

        [Test]
        public async Task OnInlineQueryShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeInlineQuery();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnInlineQueryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnInlineQueryHandle)} event was not called.");
            bot.Events.UpdateEvents.OnInlineQueryHandle -= EventHandler;
        }

        [Test]
        public async Task OnChosenInlineResultShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeChosenInlineResult();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChosenInlineResultHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChosenInlineResultHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChosenInlineResultHandle -= EventHandler;
        }

        [Test]
        public async Task OnShippingQueryShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeShippingQuery();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnShippingQueryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnShippingQueryHandle)} event was not called.");
            bot.Events.UpdateEvents.OnShippingQueryHandle -= EventHandler;
        }

        [Test]
        public async Task OnPreCheckoutQueryShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypePreCheckoutQuery();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPreCheckoutQueryHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPreCheckoutQueryHandle)} event was not called.");
            bot.Events.UpdateEvents.OnPreCheckoutQueryHandle -= EventHandler;
        }

        [Test]
        public async Task OnPollShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypePoll();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPollHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPollHandle)} event was not called.");
            bot.Events.UpdateEvents.OnPollHandle -= EventHandler;
        }

        [Test]
        public async Task OnPollAnswerShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypePollAnswer();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPollAnswerHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPollAnswerHandle)} event was not called.");
            bot.Events.UpdateEvents.OnPollAnswerHandle -= EventHandler;
        }

        [Test]
        public async Task OnMyChatMemberShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeMyChatMember();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnMyChatMemberHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnMyChatMemberHandle)} event was not called.");
            bot.Events.UpdateEvents.OnMyChatMemberHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatMemberShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeChatMember();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChatMemberHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChatMemberHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChatMemberHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatJoinRequestShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeChatJoinRequest();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChatJoinRequestHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChatJoinRequestHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChatJoinRequestHandle -= EventHandler;
        }

        [Test]
        public async Task OnChatBoostShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeChatBoost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnChatBoostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnChatBoostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnChatBoostHandle -= EventHandler;
        }

        [Test]
        public async Task OnRemovedChatBoostShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeRemovedChatBoost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnRemovedChatBoostHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnRemovedChatBoostHandle)} event was not called.");
            bot.Events.UpdateEvents.OnRemovedChatBoostHandle -= EventHandler;
        }

        [Test]
        public async Task OnPostUpdateShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeRemovedChatBoost();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnPostUpdate += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPostUpdate)} event was not called.");
            bot.Events.UpdateEvents.OnPostUpdate -= EventHandler;
        }

        [Test]
        public async Task OnPreUpdateShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeRemovedChatBoost();
            bool eventCalled = false;

            async Task<UpdateResult> EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return UpdateResult.Continue;
            }

            bot.Events.UpdateEvents.OnPreUpdate += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnPreUpdate)} event was not called.");
            bot.Events.UpdateEvents.OnPreUpdate -= EventHandler;
        }

        [Test]
        public async Task InlineCallbackCommandCanExecuteWithoutAChatWhenAllowlistIsEmpty()
        {
            var update = new Update
            {
                CallbackQuery = new CallbackQuery
                {
                    Id = "inline-callback",
                    InlineMessageId = "inline-message",
                    From = new User { Id = 123 },
                },
            };
            var context = new BotContext(bot, update);
            var command = new CommandHandler(MarkInlineCommandExecuted);

            var result = await new ExecutorCallbackQueryCommand(bot).Execute(context, command);

            Assert.That(result, Is.EqualTo(CommandResult.Executed));
            Assert.That(context.TryGetCustomValue<string>(out var value), Is.True);
            Assert.That(value, Is.EqualTo("executed"));
        }

        private static Task MarkInlineCommandExecuted(IBotContext context)
        {
            ((BotContext)context).SetCustomData("executed");
            return Task.CompletedTask;
        }

        [Test]
        public async Task WidgetQueryReachesItsEventWithChatAndMessageIds()
        {
            var update = new Update
            {
                WidgetQuery = new WidgetQuery
                {
                    Id = "widget-query",
                    From = new User { Id = 123 },
                    Chat = new Chat { Id = 456 },
                    MessageId = 789,
                    ChatInstance = "chat-instance",
                    Data = "ping",
                    Type = new WidgetQueryTypeData(),
                },
            };
            Update? received = null;
            Task OnWidgetQuery(BotEventArgs e)
            {
                received = e.Context.Update;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnWidgetQueryHandle += OnWidgetQuery;
            try
            {
                await ((Handler)bot.Handler).UpdateAsync(new BotContext(bot, update));
            }
            finally
            {
                bot.Events.UpdateEvents.OnWidgetQueryHandle -= OnWidgetQuery;
            }

            Assert.That(received, Is.SameAs(update));
            Assert.That(update.GetChatId(), Is.EqualTo(456L));
            Assert.That(update.GetMessageId(), Is.EqualTo(789));
            Assert.That(update.GetUserId(), Is.EqualTo(123L));
        }

        [Test]
        public async Task SharedWidgetQueryReachesItsEventWithoutAChat()
        {
            var update = new Update
            {
                SharedWidgetQuery = new SharedWidgetQuery
                {
                    Id = "shared-widget-query",
                    From = new User { Id = 123 },
                    WidgetId = "shared-widget",
                    Data = "ping",
                    Type = new WidgetQueryTypeData(),
                },
            };
            Update? received = null;
            Task OnSharedWidgetQuery(BotEventArgs e)
            {
                received = e.Context.Update;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnSharedWidgetQueryHandle += OnSharedWidgetQuery;
            try
            {
                await ((Handler)bot.Handler).UpdateAsync(new BotContext(bot, update));
            }
            finally
            {
                bot.Events.UpdateEvents.OnSharedWidgetQueryHandle -= OnSharedWidgetQuery;
            }

            Assert.That(received, Is.SameAs(update));
            Assert.That(update.TryGetChatId(out _), Is.False);
            Assert.That(update.GetUserId(), Is.EqualTo(123L));
        }

        /// <summary>
        /// Bot API 10.3: the user pressed the stop button on a streamed draft.
        /// </summary>
        [Test]
        public async Task OnStoppedMessageGenerationShouldBeInvoked()
        {
            var update = TestDataFactory.CreateUpdateTypeStoppedMessageGeneration();
            bool eventCalled = false;

            Task EventHandler(BotEventArgs e)
            {
                eventCalled = true;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnStoppedMessageGenerationHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            Assert.IsTrue(eventCalled, $"The {nameof(bot.Events.UpdateEvents.OnStoppedMessageGenerationHandle)} event was not called.");
            bot.Events.UpdateEvents.OnStoppedMessageGenerationHandle -= EventHandler;
        }

        /// <summary>
        /// The update tells the bot which draft was stopped, so the handler can clean it up.
        /// </summary>
        [Test]
        public async Task StoppedMessageGenerationCarriesTheDraft()
        {
            var update = TestDataFactory.CreateUpdateTypeStoppedMessageGeneration();
            long? draftId = null;

            Task EventHandler(BotEventArgs e)
            {
                draftId = e.Context.Update.StoppedMessageGeneration?.DraftId;
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnStoppedMessageGenerationHandle += EventHandler;
            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, new CancellationToken());
            bot.Events.UpdateEvents.OnStoppedMessageGenerationHandle -= EventHandler;

            Assert.That(draftId, Is.EqualTo(3_000_000_000L));
        }
    }
}
