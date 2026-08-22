using FluentAssertions;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Tests.TestModels;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.ServicesTests
{
    public class MessageSenderTests
    {
        private const long ChatId = 555555;

        private static Update UpdateForChat(long chatId = ChatId)
        {
            return new Update
            {
                Message = new Message
                {
                    Id = 1,
                    Chat = new Chat { Id = chatId },
                    From = new User { Id = 111 }
                }
            };
        }

        #region Chat and text

        [Test]
        public async Task SendPutsTheTextAndChatIntoTheRequest()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, ChatId, "hello");

            var request = bot.Single<SendMessageRequest>();
            request.Text.Should().Be("hello");
            request.ChatId.Identifier.Should().Be(ChatId);
        }

        [Test]
        public async Task SendTakesTheChatFromTheUpdate()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, UpdateForChat(4242), "hello");

            bot.Single<SendMessageRequest>().ChatId.Identifier.Should().Be(4242);
        }

        [Test]
        public async Task SendTakesTheChatFromTheContextUpdate()
        {
            var bot = new BotClientMock(UpdateForChat(2424));

            await MessageSender.Send(bot.Context.Object, "hello");

            bot.Single<SendMessageRequest>().ChatId.Identifier.Should().Be(2424);
        }

        #endregion

        #region Options mapping

        [Test]
        public async Task HtmlIsTheDefaultParseMode()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, ChatId, "hello");

            bot.Single<SendMessageRequest>().ParseMode.Should().Be(ParseMode.Html);
        }

        [Test]
        public async Task ParseModeIsTakenFromTheOptions()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { ParseMode = ParseMode.Markdown };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().ParseMode.Should().Be(ParseMode.Markdown);
        }

        [Test]
        public async Task NotificationAndProtectionAreTakenFromTheOptions()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { DisableNotification = true, ProtectedContent = true };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            var request = bot.Single<SendMessageRequest>();
            request.DisableNotification.Should().BeTrue();
            request.ProtectContent.Should().BeTrue();
        }

        [Test]
        public async Task TheThreadIdIsTakenFromTheOptions()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { MessageThreadId = 77 };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().MessageThreadId.Should().Be(77);
        }

        [Test]
        public async Task DisablingThePreviewReachesTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { DisableWebPagePreview = true };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().LinkPreviewOptions!.IsDisabled.Should().BeTrue();
        }

        [Test]
        public async Task ReplyingToAMessageReachesTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { ReplyToMessageId = 99, AllowSendingWithoutReply = true };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            var parameters = bot.Single<SendMessageRequest>().ReplyParameters!;
            parameters.MessageId.Should().Be(99);
            parameters.AllowSendingWithoutReply.Should().BeTrue();
        }

        [Test]
        public async Task TheReplyKeyboardReachesTheRequest()
        {
            var bot = new BotClientMock();
            var keyboard = new ReplyKeyboardMarkup(new KeyboardButton("button"));
            var option = new OptionMessage { MenuReplyKeyboardMarkup = keyboard };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().ReplyMarkup.Should().BeSameAs(keyboard);
        }

        [Test]
        public async Task TheInlineKeyboardReachesTheRequest()
        {
            var bot = new BotClientMock();
            var keyboard = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("text", "data"));
            var option = new OptionMessage { MenuInlineKeyboardMarkup = keyboard };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().ReplyMarkup.Should().BeSameAs(keyboard);
        }

        [Test]
        public async Task ClearingTheMenuRemovesTheKeyboard()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { ClearMenu = true };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().ReplyMarkup.Should().BeOfType<ReplyKeyboardRemove>();
        }

        [Test]
        public async Task NoKeyboardIsSentWhenNoneWasRequested()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, ChatId, "hello");

            bot.Single<SendMessageRequest>().ReplyMarkup.Should().BeNull();
        }

        [Test]
        public async Task TheBusinessConnectionReachesTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { BusinessConnectionId = "bc-1" };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().BusinessConnectionId.Should().Be("bc-1");
        }

        [Test]
        public async Task TheMessageEffectReachesTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { MessageEffectId = "effect-1" };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().MessageEffectId.Should().Be("effect-1");
        }

        [Test]
        public async Task PaidBroadcastReachesTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { AllowPaidBroadcast = true };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().AllowPaidBroadcast.Should().BeTrue();
        }

        [Test]
        public async Task TheDirectMessagesTopicReachesTheRequest()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { DirectMessagesTopicId = 555 };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().DirectMessagesTopicId.Should().Be(555);
        }

        [Test]
        public async Task TheSuggestedPostParametersReachTheRequest()
        {
            var bot = new BotClientMock();
            var parameters = new SuggestedPostParameters();
            var option = new OptionMessage { SuggestedPostParameters = parameters };

            await MessageSender.Send(bot.Context.Object, ChatId, "hello", option);

            bot.Single<SendMessageRequest>().SuggestedPostParameters.Should().BeSameAs(parameters);
        }

        [Test]
        public async Task TheNewOptionsAreUnsetByDefault()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, ChatId, "hello");

            var request = bot.Single<SendMessageRequest>();
            request.BusinessConnectionId.Should().BeNull();
            request.MessageEffectId.Should().BeNull();
            request.AllowPaidBroadcast.Should().BeFalse();
            request.DirectMessagesTopicId.Should().BeNull();
            request.SuggestedPostParameters.Should().BeNull();
        }

        #endregion

        #region Long messages

        [Test]
        public async Task AShortMessageIsSentAsASingleRequest()
        {
            var bot = new BotClientMock();

            await MessageSender.Send(bot.Context.Object, ChatId, new string('x', PRConstants.MAX_MESSAGE_LENGTH));

            bot.SentCount.Should().Be(1);
        }

        /// <summary>
        /// Telegram rejects messages longer than the limit, so a long text is split
        /// and sent as several messages.
        /// </summary>
        [Test]
        public async Task ALongMessageIsSplitIntoSeveralRequests()
        {
            var bot = new BotClientMock();
            var text = new string('x', PRConstants.MAX_MESSAGE_LENGTH + 1);

            await MessageSender.Send(bot.Context.Object, ChatId, text);

            bot.SentCount.Should().Be(2);
        }

        [Test]
        public async Task TheSplitMessagesTogetherHoldTheWholeText()
        {
            var bot = new BotClientMock();
            var text = string.Concat(Enumerable.Range(0, 9000).Select(i => (char)('a' + i % 26)));

            await MessageSender.Send(bot.Context.Object, ChatId, text);

            bot.SentCount.Should().Be(3);
            var sent = bot.Requests.Cast<SendMessageRequest>().Select(x => x.Text);
            string.Concat(sent).Should().Be(text);
        }

        [Test]
        public async Task EveryPieceOfASplitMessageGoesToTheSameChat()
        {
            var bot = new BotClientMock();
            var text = new string('x', PRConstants.MAX_MESSAGE_LENGTH * 2 + 10);

            await MessageSender.Send(bot.Context.Object, ChatId, text);

            bot.Requests.Cast<SendMessageRequest>()
                .Should().OnlyContain(x => x.ChatId.Identifier == ChatId);
        }

        [Test]
        public async Task NoPieceExceedsTheLengthLimit()
        {
            var bot = new BotClientMock();
            var text = new string('x', PRConstants.MAX_MESSAGE_LENGTH * 2 + 10);

            await MessageSender.Send(bot.Context.Object, ChatId, text);

            bot.Requests.Cast<SendMessageRequest>()
                .Should().OnlyContain(x => x.Text.Length <= PRConstants.MAX_MESSAGE_LENGTH);
        }

        #endregion

        #region AwaitAnswerBot

        [Test]
        public async Task AwaitAnswerBotSendsTheDefaultPlaceholder()
        {
            var bot = new BotClientMock();

            await MessageSender.AwaitAnswerBot(bot.Context.Object, ChatId);

            bot.Single<SendMessageRequest>().Text.Should().Contain("Generating a reply");
        }

        [Test]
        public async Task AwaitAnswerBotSendsTheGivenText()
        {
            var bot = new BotClientMock();

            await MessageSender.AwaitAnswerBot(bot.Context.Object, ChatId, "please wait");

            bot.Single<SendMessageRequest>().Text.Should().Be("please wait");
        }

        #endregion
    }
}
