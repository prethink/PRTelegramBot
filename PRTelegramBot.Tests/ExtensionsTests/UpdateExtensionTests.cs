using FluentAssertions;
using PRTelegramBot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Tests.ExtensionsTests
{
    public class UpdateExtensionTests
    {
        private const long ChatId = 555555;
        private const long UserId = 111111;
        private const long BotId = 999999;

        #region Factories

        private static Chat NewChat(long id = ChatId) => new Chat { Id = id };

        private static User NewUser(long id = UserId) => new User { Id = id };

        private static Message Msg(long chatId = ChatId, long? fromId = UserId, int id = 42)
        {
            return new Message
            {
                Id = id,
                Chat = NewChat(chatId),
                From = fromId is null ? null : NewUser(fromId.Value)
            };
        }

        private static Update CreateMessageUpdate() => new Update { Message = Msg() };

        private static Update CreateCallbackQueryUpdate(bool withMessage = true)
        {
            var callback = new CallbackQuery
            {
                Id = "callback-id",
                // The user who pressed the button.
                From = NewUser()
            };

            if (withMessage)
            {
                // A message with a callback button is sent by the bot, so its From is the bot.
                callback.Message = Msg(fromId: BotId);
            }

            return new Update { CallbackQuery = callback };
        }

        #endregion

        #region GetChatId

        private static IEnumerable<TestCaseData> ChatIdCases()
        {
            yield return new TestCaseData(new Update { Message = Msg() }).SetName("GetChatId_Message");
            yield return new TestCaseData(new Update { BusinessMessage = Msg() }).SetName("GetChatId_BusinessMessage");
            yield return new TestCaseData(new Update { ChannelPost = Msg(fromId: null) }).SetName("GetChatId_ChannelPost");
            yield return new TestCaseData(new Update { EditedMessage = Msg() }).SetName("GetChatId_EditedMessage");
            yield return new TestCaseData(new Update { EditedChannelPost = Msg(fromId: null) }).SetName("GetChatId_EditedChannelPost");
            yield return new TestCaseData(new Update { EditedBusinessMessage = Msg() }).SetName("GetChatId_EditedBusinessMessage");
            yield return new TestCaseData(new Update { ChatBoost = new ChatBoostUpdated { Chat = NewChat() } }).SetName("GetChatId_ChatBoost");
            yield return new TestCaseData(new Update { RemovedChatBoost = new ChatBoostRemoved { Chat = NewChat() } }).SetName("GetChatId_RemovedChatBoost");
            yield return new TestCaseData(new Update { ChatJoinRequest = new ChatJoinRequest { Chat = NewChat(), From = NewUser() } }).SetName("GetChatId_ChatJoinRequest");
            yield return new TestCaseData(new Update { ChatMember = new ChatMemberUpdated { Chat = NewChat(), From = NewUser() } }).SetName("GetChatId_ChatMember");
            yield return new TestCaseData(new Update { MyChatMember = new ChatMemberUpdated { Chat = NewChat(), From = NewUser() } }).SetName("GetChatId_MyChatMember");
            yield return new TestCaseData(new Update { DeletedBusinessMessages = new BusinessMessagesDeleted { Chat = NewChat() } }).SetName("GetChatId_DeletedBusinessMessages");
            yield return new TestCaseData(new Update { MessageReaction = new MessageReactionUpdated { Chat = NewChat() } }).SetName("GetChatId_MessageReaction");
            yield return new TestCaseData(new Update { MessageReactionCount = new MessageReactionCountUpdated { Chat = NewChat() } }).SetName("GetChatId_MessageReactionCount");
            yield return new TestCaseData(new Update { PollAnswer = new PollAnswer { VoterChat = NewChat() } }).SetName("GetChatId_PollAnswer");
        }

        [TestCaseSource(nameof(ChatIdCases))]
        public void GetChatIdReadsTheChatOfEverySupportedUpdate(Update update)
        {
            update.GetChatId().Should().Be(ChatId);
        }

        [Test]
        public void GetChatIdUsesUserChatIdForBusinessConnection()
        {
            var update = new Update
            {
                BusinessConnection = new BusinessConnection { Id = "bc", User = NewUser(), UserChatId = ChatId }
            };

            update.GetChatId().Should().Be(ChatId);
        }

        [Test]
        public void GetChatIdReturnsChatIdForCallbackQuery()
        {
            CreateCallbackQueryUpdate().GetChatId().Should().Be(ChatId);
        }

        [Test]
        public void GetChatIdThrowsWhenCallbackQueryHasNoMessage()
        {
            CreateCallbackQueryUpdate(withMessage: false)
                .Invoking(x => x.GetChatId())
                .Should().Throw<InvalidOperationException>();
        }

        /// <summary>
        /// A poll answer coming from a user rather than a chat carries no voter chat.
        /// </summary>
        [Test]
        public void GetChatIdThrowsWhenPollAnswerHasNoVoterChat()
        {
            var update = new Update { PollAnswer = new PollAnswer { User = NewUser() } };

            update.Invoking(x => x.GetChatId())
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetChatIdThrowsNotImplementedForUnsupportedUpdate()
        {
            new Update { Poll = new Poll() }
                .Invoking(x => x.GetChatId())
                .Should().Throw<NotImplementedException>();
        }

        [Test]
        public void GetChatIdClassWrapsTheSameIdentifier()
        {
            CreateMessageUpdate().GetChatIdClass().Identifier.Should().Be(ChatId);
        }

        #endregion

        #region GetUserId

        private static IEnumerable<TestCaseData> UserIdCases()
        {
            yield return new TestCaseData(new Update { Message = Msg() }).SetName("GetUserId_Message");
            yield return new TestCaseData(new Update { BusinessMessage = Msg() }).SetName("GetUserId_BusinessMessage");
            yield return new TestCaseData(new Update { EditedMessage = Msg() }).SetName("GetUserId_EditedMessage");
            yield return new TestCaseData(new Update { EditedBusinessMessage = Msg() }).SetName("GetUserId_EditedBusinessMessage");
            yield return new TestCaseData(new Update { EditedChannelPost = Msg() }).SetName("GetUserId_EditedChannelPost");
            yield return new TestCaseData(new Update { ChatJoinRequest = new ChatJoinRequest { Chat = NewChat(), From = NewUser() } }).SetName("GetUserId_ChatJoinRequest");
            yield return new TestCaseData(new Update { ChatMember = new ChatMemberUpdated { Chat = NewChat(), From = NewUser() } }).SetName("GetUserId_ChatMember");
            yield return new TestCaseData(new Update { MyChatMember = new ChatMemberUpdated { Chat = NewChat(), From = NewUser() } }).SetName("GetUserId_MyChatMember");
        }

        [TestCaseSource(nameof(UserIdCases))]
        public void GetUserIdReadsTheSenderOfEverySupportedUpdate(Update update)
        {
            update.GetUserId().Should().Be(UserId);
        }

        /// <summary>
        /// The user who pressed the button is CallbackQuery.From.
        /// CallbackQuery.Message.From would be the bot that sent the message.
        /// </summary>
        [Test]
        public void GetUserIdReturnsPresserNotBotForCallbackQuery()
        {
            var update = CreateCallbackQueryUpdate();

            update.GetUserId().Should().Be(UserId);
            update.GetUserId().Should().NotBe(BotId);
        }

        [Test]
        public void GetUserIdWorksForCallbackQueryWithoutMessage()
        {
            CreateCallbackQueryUpdate(withMessage: false).GetUserId().Should().Be(UserId);
        }

        /// <summary>
        /// Channel posts are sent by the channel, not by a user, so From is empty.
        /// </summary>
        [Test]
        public void GetUserIdThrowsInvalidOperationForChannelPost()
        {
            new Update { ChannelPost = Msg(fromId: null) }
                .Invoking(x => x.GetUserId())
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetUserIdThrowsInvalidOperationWhenMessageHasNoSender()
        {
            new Update { Message = Msg(fromId: null) }
                .Invoking(x => x.GetUserId())
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetUserIdThrowsNotImplementedForUnsupportedUpdate()
        {
            new Update { PollAnswer = new PollAnswer { User = NewUser() } }
                .Invoking(x => x.GetUserId())
                .Should().Throw<NotImplementedException>();
        }

        #endregion

        #region GetMessageId

        [Test]
        public void GetMessageIdReturnsIdForMessage()
        {
            CreateMessageUpdate().GetMessageId().Should().Be(42);
        }

        [Test]
        public void GetMessageIdReturnsIdForCallbackQuery()
        {
            CreateCallbackQueryUpdate().GetMessageId().Should().Be(42);
        }

        [Test]
        public void GetMessageIdThrowsWhenCallbackQueryHasNoMessage()
        {
            CreateCallbackQueryUpdate(withMessage: false)
                .Invoking(x => x.GetMessageId())
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetMessageIdThrowsNotImplementedForOtherUpdates()
        {
            new Update { ChannelPost = Msg(fromId: null) }
                .Invoking(x => x.GetMessageId())
                .Should().Throw<NotImplementedException>();
        }

        #endregion

        #region TryGetChatId and IsUserChatId

        [Test]
        public void TryGetChatIdReturnsTrueForMessage()
        {
            CreateMessageUpdate().TryGetChatId(out var chatId).Should().BeTrue();
            chatId.Should().Be(ChatId);
        }

        [Test]
        public void TryGetChatIdReturnsFalseInsteadOfThrowing()
        {
            var update = CreateCallbackQueryUpdate(withMessage: false);

            update.TryGetChatId(out var chatId).Should().BeFalse();
            chatId.Should().Be(0);
        }

        [Test]
        public void TryGetChatIdReturnsFalseForUnsupportedUpdate()
        {
            new Update { Poll = new Poll() }.TryGetChatId(out _).Should().BeFalse();
        }

        [Test]
        public void IsUserChatIdIsTrueForPositiveChatId()
        {
            CreateMessageUpdate().IsUserChatId().Should().BeTrue();
        }

        [Test]
        public void IsUserChatIdIsFalseForGroupChatId()
        {
            new Update { Message = Msg(chatId: -100123456) }.IsUserChatId().Should().BeFalse();
        }

        /// <summary>
        /// The chat id cannot be read here, and the method answers false instead of throwing.
        /// </summary>
        [Test]
        public void IsUserChatIdIsFalseWhenTheChatCannotBeDetermined()
        {
            CreateCallbackQueryUpdate(withMessage: false).IsUserChatId().Should().BeFalse();
        }

        #endregion

        #region GetInfoUser

        [Test]
        public void GetInfoUserReturnsChatDetails()
        {
            var update = new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = ChatId, FirstName = "John", LastName = "Doe", Username = "jdoe" },
                    From = NewUser()
                }
            };

            update.GetInfoUser().Should().Be($"{ChatId} John Doe jdoe");
        }

        [Test]
        public void GetInfoUserSkipsMissingChatDetails()
        {
            var update = new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = ChatId, FirstName = "John" },
                    From = NewUser()
                }
            };

            update.GetInfoUser().Should().Be($"{ChatId} John");
        }

        [Test]
        public void GetInfoUserReturnsOnlyTheIdWhenThereAreNoNames()
        {
            CreateMessageUpdate().GetInfoUser().Should().Be(ChatId.ToString());
        }

        [Test]
        public void GetInfoUserReturnsEmptyStringWhenCallbackQueryHasNoMessage()
        {
            CreateCallbackQueryUpdate(withMessage: false).GetInfoUser().Should().BeEmpty();
        }

        [Test]
        public void GetInfoUserReturnsEmptyStringWhenPollAnswerHasNoVoterChat()
        {
            new Update { PollAnswer = new PollAnswer { User = NewUser() } }
                .GetInfoUser().Should().BeEmpty();
        }

        [Test]
        public void GetInfoUserReturnsEmptyStringForUnsupportedUpdate()
        {
            new Update { Poll = new Poll() }.GetInfoUser().Should().BeEmpty();
        }

        #endregion

        #region Update type

        [Test]
        public void UpdateTypeIsResolvedForCallbackQuery()
        {
            CreateCallbackQueryUpdate().Type.Should().Be(UpdateType.CallbackQuery);
        }

        [Test]
        public void UpdateTypeIsResolvedForMessage()
        {
            CreateMessageUpdate().Type.Should().Be(UpdateType.Message);
        }

        #endregion
    }
}
