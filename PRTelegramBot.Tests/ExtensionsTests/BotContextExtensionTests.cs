using FluentAssertions;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.ExtensionsTests
{
    public class BotContextExtensionTests
    {
        private const long ChatId = 555555;
        private const long UserId = 111111;
        private const long BotId = 999999;

        private static BotContext CreateContext(Update update)
        {
            return new BotContext(new PRBotDummy(), update);
        }

        private static BotContext CreateMessageContext()
        {
            return CreateContext(new Update
            {
                Message = new Message
                {
                    Id = 42,
                    Chat = new Chat { Id = ChatId },
                    From = new User { Id = UserId }
                }
            });
        }

        private static BotContext CreateCallbackQueryContext()
        {
            return CreateContext(new Update
            {
                CallbackQuery = new CallbackQuery
                {
                    Id = "callback-id",
                    From = new User { Id = UserId },
                    Message = new Message
                    {
                        Id = 42,
                        Chat = new Chat { Id = ChatId },
                        From = new User { Id = BotId, IsBot = true }
                    }
                }
            });
        }

        #region Identifiers

        [Test]
        public void GetChatIdReadsTheChatFromTheUpdate()
        {
            CreateMessageContext().GetChatId().Should().Be(ChatId);
        }

        [Test]
        public void GetChatIdClassWrapsTheSameIdentifier()
        {
            CreateMessageContext().GetChatIdClass().Identifier.Should().Be(ChatId);
        }

        [Test]
        public void GetMessageIdReadsTheMessageFromTheUpdate()
        {
            CreateMessageContext().GetMessageId().Should().Be(42);
        }

        [Test]
        public void GetUserIdReturnsThePresserForCallbackQuery()
        {
            var context = CreateCallbackQueryContext();

            context.GetUserId().Should().Be(UserId);
            context.GetUserId().Should().NotBe(BotId);
        }

        [Test]
        public void TryGetChatIdReturnsFalseForAnEmptyUpdate()
        {
            var context = CreateContext(new Update());

            context.TryGetChatId(out var chatId).Should().BeFalse();
            chatId.Should().Be(0);
        }

        [Test]
        public void IsUserChatIdDistinguishesPrivateChatsFromGroups()
        {
            CreateMessageContext().IsUserChatId().Should().BeTrue();

            var group = CreateContext(new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = -100500 },
                    From = new User { Id = UserId }
                }
            });

            group.IsUserChatId().Should().BeFalse();
        }

        [Test]
        public void GetInfoUserReturnsChatDetails()
        {
            var context = CreateContext(new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = ChatId, FirstName = "John", Username = "jdoe" },
                    From = new User { Id = UserId }
                }
            });

            context.GetInfoUser().Should().Be($"{ChatId} John jdoe");
        }

        #endregion

        #region Slash arguments

        [Test]
        public void GetSlashArgsReturnsAnEmptyListWhenNothingWasStored()
        {
            CreateMessageContext().GetSlashArgs().Should().BeEmpty();
        }

        [Test]
        public void GetSlashArgsReturnsTheStoredArguments()
        {
            var context = CreateMessageContext();
            context.SetCustomData(new List<string> { "one", "two" });

            context.GetSlashArgs().Should().BeEquivalentTo("one", "two");
        }

        [Test]
        public void TypedGetSlashArgsConvertsEveryValue()
        {
            var context = CreateMessageContext();
            context.SetCustomData(new List<string> { "1", "2", "3" });

            context.GetSlashArgs<int>().Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Test]
        public void TypedGetSlashArgsSkipsValuesItCannotConvert()
        {
            var context = CreateMessageContext();
            context.SetCustomData(new List<string> { "1", "not-a-number", "3" });

            context.GetSlashArgs<int>().Should().BeEquivalentTo(new[] { 1, 3 });
        }

        [Test]
        public void TypedGetSlashArgsThrowsWhenAskedTo()
        {
            var context = CreateMessageContext();
            context.SetCustomData(new List<string> { "not-a-number" });

            context.Invoking(x => x.GetSlashArgs<int>(throwOnError: true))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void TypedGetSlashArgsReturnsAnEmptyListWhenNothingWasStored()
        {
            CreateMessageContext().GetSlashArgs<int>().Should().BeEmpty();
        }

        [Test]
        public void TypedGetSlashArgsConvertsBooleans()
        {
            var context = CreateMessageContext();
            context.SetCustomData(new List<string> { "true", "false" });

            context.GetSlashArgs<bool>().Should().BeEquivalentTo(new[] { true, false });
        }

        [Test]
        public void GetSlashArgsIgnoresCustomDataOfAnotherType()
        {
            var context = CreateMessageContext();
            context.SetCustomData(42);

            context.GetSlashArgs().Should().BeEmpty();
        }

        #endregion
    }
}
