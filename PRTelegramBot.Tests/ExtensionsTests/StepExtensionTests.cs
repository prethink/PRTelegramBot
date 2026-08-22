using FluentAssertions;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.ExtensionsTests
{
    /// <summary>
    /// Step handlers live in static state keyed by "{botId}-{chatId}", so every test uses
    /// its own chat and cleans up after itself.
    /// </summary>
    public class StepExtensionTests
    {
        private static long nextChatId = 800000;
        private static int nextUpdateId = 800000;

        private readonly List<Update> created = new();

        private class SampleStep : IExecuteStep
        {
            public bool IgnoreBasicCommands { get; set; }

            public bool LastStepExecuted { get; set; }

            public bool CanExecute() => true;

            public Func<IBotContext, Task> GetExecuteMethod() => _ => Task.CompletedTask;

            public Task<ExecuteStepResult> ExecuteStep(IBotContext context)
                => Task.FromResult(ExecuteStepResult.Success);
        }

        private class OtherStep : IExecuteStep
        {
            public bool IgnoreBasicCommands { get; set; }

            public bool LastStepExecuted { get; set; }

            public bool CanExecute() => false;

            public Func<IBotContext, Task> GetExecuteMethod() => _ => Task.CompletedTask;

            public Task<ExecuteStepResult> ExecuteStep(IBotContext context)
                => Task.FromResult(ExecuteStepResult.Success);
        }

        private static PRBotBase CreateBot(long botId)
        {
            return new PRBotDummy(opt =>
            {
                opt.Client = new TelegramBotClient("35425:token");
                opt.Token = "35425:token";
                opt.BotId = botId;
            }, null);
        }

        private Update CreateLinkedUpdate(PRBotBase bot, long? chatId = null)
        {
            var update = new Update
            {
                Id = Interlocked.Increment(ref nextUpdateId),
                Message = new Message
                {
                    Id = 1,
                    Chat = new Chat { Id = chatId ?? Interlocked.Increment(ref nextChatId) },
                    From = new User { Id = 111 }
                }
            };

            update.AddTelegramClient(bot);
            created.Add(update);
            return update;
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var update in created)
            {
                update.ClearStepUserHandler();
                update.ClearTelegramClient();
            }

            created.Clear();
        }

        [Test]
        public void ThereIsNoStepHandlerUntilOneIsRegistered()
        {
            var update = CreateLinkedUpdate(CreateBot(1));

            update.HasStepHandler().Should().BeFalse();
            update.GetStepHandler().Should().BeNull();
        }

        [Test]
        public void RegisterStepHandlerStoresTheHandler()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            var step = new SampleStep();

            update.RegisterStepHandler(step);

            update.HasStepHandler().Should().BeTrue();
            update.GetStepHandler().Should().BeSameAs(step);
        }

        [Test]
        public void TypedGetStepHandlerReturnsTheHandler()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            var step = new SampleStep();
            update.RegisterStepHandler(step);

            update.GetStepHandler<SampleStep>().Should().BeSameAs(step);
        }

        [Test]
        public void TypedGetStepHandlerReturnsNothingForAnotherType()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            update.RegisterStepHandler(new SampleStep());

            update.GetStepHandler<OtherStep>().Should().BeNull();
        }

        [Test]
        public void RegisteringAgainReplacesThePreviousHandler()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            var first = new SampleStep();
            var second = new SampleStep();

            update.RegisterStepHandler(first);
            update.RegisterStepHandler(second);

            update.GetStepHandler().Should().BeSameAs(second);
        }

        [Test]
        public void ClearStepUserHandlerRemovesTheHandler()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            update.RegisterStepHandler(new SampleStep());

            update.ClearStepUserHandler();

            update.HasStepHandler().Should().BeFalse();
            update.GetStepHandler().Should().BeNull();
        }

        [Test]
        public void ClearingWhenThereIsNoHandlerDoesNothing()
        {
            var update = CreateLinkedUpdate(CreateBot(1));

            update.Invoking(x => x.ClearStepUserHandler()).Should().NotThrow();
            update.HasStepHandler().Should().BeFalse();
        }

        /// <summary>
        /// A step sequence belongs to a user of a bot, not to a single update.
        /// </summary>
        [Test]
        public void TwoUpdatesFromTheSameChatShareTheStepHandler()
        {
            var bot = CreateBot(1);
            var chatId = Interlocked.Increment(ref nextChatId);
            var first = CreateLinkedUpdate(bot, chatId);
            var second = CreateLinkedUpdate(bot, chatId);
            var step = new SampleStep();

            first.RegisterStepHandler(step);

            second.GetStepHandler().Should().BeSameAs(step);
        }

        [Test]
        public void DifferentChatsHaveTheirOwnStepHandler()
        {
            var bot = CreateBot(1);
            var first = CreateLinkedUpdate(bot);
            var second = CreateLinkedUpdate(bot);

            first.RegisterStepHandler(new SampleStep());

            second.HasStepHandler().Should().BeFalse();
        }

        [Test]
        public void DifferentBotsHaveTheirOwnStepHandlerForTheSameChat()
        {
            var chatId = Interlocked.Increment(ref nextChatId);
            var first = CreateLinkedUpdate(CreateBot(1), chatId);
            var second = CreateLinkedUpdate(CreateBot(2), chatId);

            first.RegisterStepHandler(new SampleStep());

            second.HasStepHandler().Should().BeFalse();
        }

        [Test]
        public void StepCallsThrowWhenTheUpdateIsNotLinkedToABot()
        {
            var update = new Update
            {
                Id = Interlocked.Increment(ref nextUpdateId),
                Message = new Message { Id = 1, Chat = new Chat { Id = 1 }, From = new User { Id = 1 } }
            };

            update.Invoking(x => x.HasStepHandler()).Should().Throw<KeyNotFoundException>();
        }
    }
}
