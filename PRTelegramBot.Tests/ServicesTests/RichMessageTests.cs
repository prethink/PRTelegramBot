using FluentAssertions;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Tests.TestModels;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.ServicesTests
{
    /// <summary>
    /// A rich message carries headings, lists and media as structured blocks rather than as
    /// entities over one run of text, so Telegram sends it through its own request.
    /// </summary>
    /// <remarks>
    /// The framework could already receive one through <c>OnRichMessageHandle</c> but had no
    /// way to send one, which forced callers down to the bare client and cost them every
    /// <see cref="OptionMessage"/> setting. These tests pin the option mapping that fixes that.
    /// </remarks>
    public class RichMessageTests
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

        #region Sending

        [Test]
        public async Task HtmlBecomesARichMessage()
        {
            var bot = new BotClientMock();

            await MessageSender.SendRichMessage(bot.Context.Object, ChatId, "<h1>Report</h1>");

            var request = bot.Single<SendRichMessageRequest>();
            request.ChatId.Identifier.Should().Be(ChatId);
            request.RichMessage.Should().NotBeNull();
        }

        /// <summary>
        /// The HTML is carried as HTML rather than parsed into blocks on our side —
        /// Telegram does the parsing. Worth pinning, because it decides what a caller
        /// can inspect on the way out.
        /// </summary>
        [Test]
        public async Task TheHtmlIsCarriedAsHtml()
        {
            var bot = new BotClientMock();

            await MessageSender.SendRichMessage(bot.Context.Object, ChatId, "<h1>Report</h1>");

            var rich = bot.Single<SendRichMessageRequest>().RichMessage;
            rich.Html.Should().Contain("Report");
            rich.Blocks.Should().BeNull();
        }

        [Test]
        public async Task TheChatComesFromTheUpdateWhenItIsNotGiven()
        {
            var bot = new BotClientMock(MessageUpdate());

            await MessageSender.SendRichMessage(bot.Context.Object, "<h1>Report</h1>");

            bot.Single<SendRichMessageRequest>().ChatId.Identifier.Should().Be(ChatId);
        }

        [Test]
        public async Task AHandBuiltMessageIsPassedThroughUntouched()
        {
            var bot = new BotClientMock();
            var rich = new InputRichMessage
            {
                Blocks = new InputRichBlock[] { new InputRichBlockParagraph { Text = "Built by hand" } }
            };

            await MessageSender.SendRichMessage(bot.Context.Object, ChatId, rich);

            bot.Single<SendRichMessageRequest>().RichMessage.Should().BeSameAs(rich);
        }

        [Test]
        public void SendingNothingIsRejected()
        {
            var bot = new BotClientMock();

            var sendNullHtml = async () =>
                await MessageSender.SendRichMessage(bot.Context.Object, ChatId, (string)null!);
            var sendNullMessage = async () =>
                await MessageSender.SendRichMessage(bot.Context.Object, ChatId, (InputRichMessage)null!);

            sendNullHtml.Should().ThrowAsync<ArgumentNullException>();
            sendNullMessage.Should().ThrowAsync<ArgumentNullException>();
        }

        #endregion

        #region Options mapping

        [Test]
        public async Task EveryOptionARichMessageAcceptsIsForwarded()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage
            {
                MenuInlineKeyboardMarkup = new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("Docs", "https://example.com")),
                MessageThreadId = 7,
                DisableNotification = true,
                ProtectedContent = true,
                MessageEffectId = "effect",
                BusinessConnectionId = "business",
                AllowPaidBroadcast = true,
                DirectMessagesTopicId = 99,
                EphemeralMessageParameters = new EphemeralMessageParameters { ReceiverUserId = UserId }
            };

            await MessageSender.SendRichMessage(bot.Context.Object, ChatId, "<h1>Report</h1>", option);

            var request = bot.Single<SendRichMessageRequest>();
            request.ReplyMarkup.Should().NotBeNull();
            request.MessageThreadId.Should().Be(7);
            request.DisableNotification.Should().BeTrue();
            request.ProtectContent.Should().BeTrue();
            request.MessageEffectId.Should().Be("effect");
            request.BusinessConnectionId.Should().Be("business");
            request.AllowPaidBroadcast.Should().BeTrue();
            request.DirectMessagesTopicId.Should().Be(99);
            request.EphemeralMessageParameters!.ReceiverUserId.Should().Be(UserId);
        }

        [Test]
        public async Task ARichMessageCanBeEphemeralToo()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage
            {
                EphemeralMessageParameters = UserId
            };

            await MessageSender.SendRichMessage(bot.Context.Object, ChatId, "<h1>Just for you</h1>", option);

            bot.Single<SendRichMessageRequest>()
                .EphemeralMessageParameters!.ReceiverUserId.Should().Be(UserId);
        }

        [Test]
        public async Task WithoutOptionsNothingIsSetBeyondTheMessage()
        {
            var bot = new BotClientMock();

            await MessageSender.SendRichMessage(bot.Context.Object, ChatId, "<h1>Report</h1>");

            var request = bot.Single<SendRichMessageRequest>();
            request.ReplyMarkup.Should().BeNull();
            request.ProtectContent.Should().BeFalse();
            request.EphemeralMessageParameters.Should().BeNull();
        }

        #endregion
    }
}
