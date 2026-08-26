using FluentAssertions;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Media;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Tests.TestModels;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.ServicesTests
{
    /// <summary>
    /// Bot API 10.3 replaced the loose receiverUserId and callbackQueryId send parameters
    /// with a single <see cref="EphemeralMessageParameters"/> object.
    /// </summary>
    /// <remarks>
    /// An ephemeral message is shown to one user as an overlay and never enters the chat
    /// history, so these tests care about one thing above all: that the receiver actually
    /// reaches the request. A missing receiver would post the message to the whole chat.
    /// </remarks>
    public class EphemeralMessageTests
    {
        private const long ChatId = 555555;
        private const long UserId = 111;

        private static Update MessageUpdate()
        {
            return new Update
            {
                Message = new Message
                {
                    Id = 1,
                    Chat = new Chat { Id = ChatId },
                    From = new User { Id = UserId }
                }
            };
        }

        private static Update CallbackQueryUpdate(string callbackQueryId = "cq-1")
        {
            return new Update
            {
                CallbackQuery = new CallbackQuery
                {
                    Id = callbackQueryId,
                    From = new User { Id = UserId },
                    Message = new Message
                    {
                        Id = 1,
                        Chat = new Chat { Id = ChatId }
                    }
                }
            };
        }

        #region OptionMessage

        [Test]
        public async Task EphemeralParametersReachTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage
            {
                EphemeralMessageParameters = new EphemeralMessageParameters { ReceiverUserId = UserId }
            };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>()
                .EphemeralMessageParameters!.ReceiverUserId.Should().Be(UserId);
        }

        [Test]
        public async Task AnOrdinaryMessageCarriesNoEphemeralParameters()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, ChatId, "hello");

            bot.Single<SendMessageRequest>().EphemeralMessageParameters.Should().BeNull();
        }

        [Test]
        public void AUserIdIsEnoughToBuildTheParameters()
        {
            EphemeralMessageParameters parameters = UserId;

            parameters.ReceiverUserId.Should().Be(UserId);
        }

        [Test]
        public async Task PhotosCanBeEphemeralToo()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage
            {
                EphemeralMessageParameters = new EphemeralMessageParameters { ReceiverUserId = UserId }
            };

            await MediaSender.SendPhotoWithUrl(bot.Context.Object, ChatId, "a cat", "https://example.com/cat.jpg", option);

            bot.Single<SendPhotoRequest>()
                .EphemeralMessageParameters!.ReceiverUserId.Should().Be(UserId);
        }

        #endregion

        #region SendEphemeral

        [Test]
        public async Task SendEphemeralTakesTheReceiverFromTheUpdate()
        {
            var bot = new BotClientMock(MessageUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "only you can see this");

            var request = bot.Single<SendMessageRequest>();
            request.ChatId.Identifier.Should().Be(ChatId);
            request.EphemeralMessageParameters!.ReceiverUserId.Should().Be(UserId);
        }

        [Test]
        public async Task SendEphemeralCarriesTheCallbackQueryThatTriggeredIt()
        {
            var bot = new BotClientMock(CallbackQueryUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "only you can see this");

            var parameters = bot.Single<SendMessageRequest>().EphemeralMessageParameters!;
            parameters.ReceiverUserId.Should().Be(UserId);
            parameters.CallbackQueryId.Should().Be("cq-1");
        }

        [Test]
        public async Task SendEphemeralLeavesTheCallbackQueryEmptyForAPlainMessage()
        {
            var bot = new BotClientMock(MessageUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "only you can see this");

            bot.Single<SendMessageRequest>()
                .EphemeralMessageParameters!.CallbackQueryId.Should().BeNull();
        }

        [Test]
        public async Task ReplacingTheOriginalMessageIsOptedIntoExplicitly()
        {
            var bot = new BotClientMock(CallbackQueryUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "gone", replaceCallbackQueryMessage: true);

            bot.Single<SendMessageRequest>()
                .EphemeralMessageParameters!.ReplaceCallbackQueryMessage.Should().BeTrue();
        }

        /// <summary>
        /// The exact shape that failed with <c>MESSAGE_ID_INVALID</c>: replacing the message a
        /// button belongs to, while the framework also attached reply parameters naming nothing.
        /// </summary>
        [Test]
        public async Task ReplacingSendsNoEmptyReplyParameters()
        {
            var bot = new BotClientMock(CallbackQueryUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "gone", replaceCallbackQueryMessage: true);

            var request = bot.Single<SendMessageRequest>();
            request.EphemeralMessageParameters!.ReplaceCallbackQueryMessage.Should().BeTrue();
            request.ReplyParameters.Should().BeNull(
                "reply parameters naming neither a message nor an ephemeral message are rejected here");
        }

        [Test]
        public async Task ReplacingIsOffByDefault()
        {
            var bot = new BotClientMock(CallbackQueryUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "hello");

            bot.Single<SendMessageRequest>()
                .EphemeralMessageParameters!.ReplaceCallbackQueryMessage.Should().BeFalse();
        }

        [Test]
        public async Task ThereIsNothingToReplaceWithoutACallbackQuery()
        {
            var bot = new BotClientMock(MessageUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "hello", replaceCallbackQueryMessage: true);

            bot.Single<SendMessageRequest>()
                .EphemeralMessageParameters!.ReplaceCallbackQueryMessage.Should().BeFalse(
                    "Telegram rejects the flag when no callback query triggered the message");
        }

        [Test]
        public async Task SendEphemeralCanTargetSomebodyElse()
        {
            var bot = new BotClientMock(MessageUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, 999, "a word for the moderator");

            bot.Single<SendMessageRequest>()
                .EphemeralMessageParameters!.ReceiverUserId.Should().Be(999);
        }

        [Test]
        public async Task SendEphemeralKeepsTheOptionsItWasGiven()
        {
            var bot = new BotClientMock(MessageUpdate());
            var option = new OptionMessage { ProtectedContent = true };

            await MessageSender.SendEphemeral(bot.Context.Object, "hello", option);

            var request = bot.Single<SendMessageRequest>();
            request.ProtectContent.Should().BeTrue();
            request.EphemeralMessageParameters!.ReceiverUserId.Should().Be(UserId);
        }

        #endregion

        #region Replying inside an overlay

        /// <summary>
        /// The second way Telegram lets a bot that is not an administrator send an ephemeral
        /// message: reply to one it already received, within 15 seconds.
        /// </summary>
        [Test]
        public async Task AnIncomingEphemeralMessageIsRepliedTo()
        {
            var update = MessageUpdate();
            update.Message!.EphemeralMessageId = 77;
            var bot = new BotClientMock(update);

            await MessageSender.SendEphemeral(bot.Context.Object, "carrying on");

            bot.Single<SendMessageRequest>()
                .ReplyParameters!.EphemeralMessageId.Should().Be(77);
        }

        [Test]
        public async Task AnOrdinaryMessageHasNoEphemeralReplyTarget()
        {
            var bot = new BotClientMock(MessageUpdate());

            await MessageSender.SendEphemeral(bot.Context.Object, "hello");

            bot.Single<SendMessageRequest>()
                .ReplyParameters.Should().BeNull("there is nothing to reply to, so the field must be absent");
        }

        [Test]
        public async Task AnExplicitReplyTargetWins()
        {
            var update = MessageUpdate();
            update.Message!.EphemeralMessageId = 77;
            var bot = new BotClientMock(update);
            var option = new OptionMessage { ReplyToEphemeralMessageId = 5 };

            await MessageSender.SendEphemeral(bot.Context.Object, "carrying on", option);

            bot.Single<SendMessageRequest>()
                .ReplyParameters!.EphemeralMessageId.Should().Be(5,
                    "the caller chose a target, so the update must not overwrite it");
        }

        [Test]
        public async Task TheReplyTargetSurvivesAnOrdinarySend()
        {
            var bot = new BotClientMock(MessageUpdate());
            var option = new OptionMessage { ReplyToEphemeralMessageId = 77 };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>()
                .ReplyParameters!.EphemeralMessageId.Should().Be(77);
        }

        #endregion
    }
}
