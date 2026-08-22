using FluentAssertions;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Tests.TestModels;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.ServicesTests
{
    public class MessageServicesTests
    {
        private const long ChatId = 555555;
        private const int MessageId = 42;

        private static Update UpdateForChat(long chatId = ChatId, int messageId = MessageId)
        {
            return new Update
            {
                Message = new Message
                {
                    Id = messageId,
                    Chat = new Chat { Id = chatId },
                    From = new User { Id = 111 }
                }
            };
        }

        private static InlineKeyboardMarkup Inline() =>
            new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("text", "data"));

        #region MessageEditor

        [Test]
        public async Task EditSendsTheNewTextForTheGivenMessage()
        {
            var bot = new BotClientMock();

            await MessageEditor.Edit(bot.Context.Object, ChatId, MessageId, "updated");

            var request = bot.Single<EditMessageTextRequest>();
            request.Text.Should().Be("updated");
            request.ChatId.Identifier.Should().Be(ChatId);
            request.MessageId.Should().Be(MessageId);
        }

        [Test]
        public async Task EditTakesTheChatAndMessageFromTheContext()
        {
            var bot = new BotClientMock(UpdateForChat(1212, 7));

            await MessageEditor.Edit(bot.Context.Object, "updated");

            var request = bot.Single<EditMessageTextRequest>();
            request.ChatId.Identifier.Should().Be(1212);
            request.MessageId.Should().Be(7);
        }

        [Test]
        public async Task EditCarriesTheParseModeAndPreviewOptions()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { ParseMode = ParseMode.MarkdownV2, DisableWebPagePreview = true };

            await MessageEditor.Edit(bot.Context.Object, ChatId, MessageId, "updated", option);

            var request = bot.Single<EditMessageTextRequest>();
            request.ParseMode.Should().Be(ParseMode.MarkdownV2);
            request.LinkPreviewOptions!.IsDisabled.Should().BeTrue();
        }

        /// <summary>
        /// Editing text only accepts an inline keyboard, so a reply keyboard is dropped.
        /// </summary>
        [Test]
        public async Task EditIgnoresAReplyKeyboard()
        {
            var bot = new BotClientMock();
            var option = new OptionMessage { MenuReplyKeyboardMarkup = new ReplyKeyboardMarkup(new KeyboardButton("button")) };

            await MessageEditor.Edit(bot.Context.Object, ChatId, MessageId, "updated", option);

            bot.Single<EditMessageTextRequest>().ReplyMarkup.Should().BeNull();
        }

        [Test]
        public async Task EditKeepsAnInlineKeyboard()
        {
            var bot = new BotClientMock();
            var keyboard = Inline();
            var option = new OptionMessage { MenuInlineKeyboardMarkup = keyboard };

            await MessageEditor.Edit(bot.Context.Object, ChatId, MessageId, "updated", option);

            bot.Single<EditMessageTextRequest>().ReplyMarkup.Should().BeSameAs(keyboard);
        }

        [Test]
        public async Task EditInlineReplacesTheKeyboard()
        {
            var bot = new BotClientMock();
            var keyboard = Inline();
            var option = new OptionMessage { MenuInlineKeyboardMarkup = keyboard };

            await MessageEditor.EditInline(bot.Context.Object, ChatId, MessageId, option);

            var request = bot.Single<EditMessageReplyMarkupRequest>();
            request.ChatId.Identifier.Should().Be(ChatId);
            request.MessageId.Should().Be(MessageId);
            request.ReplyMarkup.Should().BeSameAs(keyboard);
        }

        /// <summary>
        /// Without an inline keyboard there is nothing to replace, so nothing is sent.
        /// </summary>
        [Test]
        public async Task EditInlineSendsNothingWithoutAnInlineKeyboard()
        {
            var bot = new BotClientMock();

            var result = await MessageEditor.EditInline(bot.Context.Object, ChatId, MessageId);

            bot.SentCount.Should().Be(0);
            result.Should().BeNull();
        }

        #endregion

        #region MessageDeleter

        [Test]
        public async Task DeleteMessageTargetsTheGivenMessage()
        {
            var bot = new BotClientMock();

            await MessageDeleter.DeleteMessage(bot.Context.Object, ChatId, MessageId);

            var request = bot.Single<DeleteMessageRequest>();
            request.ChatId.Identifier.Should().Be(ChatId);
            request.MessageId.Should().Be(MessageId);
        }

        #endregion

        #region MessageCopier

        [Test]
        public async Task CopyMessageMovesFromTheSourceChatToTheTarget()
        {
            var bot = new BotClientMock();
            var source = new Message { Id = 7, Chat = new Chat { Id = 100 } };

            await MessageCopier.CopyMessage(bot.Context.Object, source, 200);

            var request = bot.Single<CopyMessageRequest>();
            request.FromChatId.Identifier.Should().Be(100);
            request.ChatId.Identifier.Should().Be(200);
            request.MessageId.Should().Be(7);
        }

        [Test]
        public async Task CopyMessageCarriesTheCaptionOptions()
        {
            var bot = new BotClientMock();
            var source = new Message { Id = 7, Chat = new Chat { Id = 100 } };
            var option = new OptionMessage { Caption = "note", ProtectedContent = true, DisableNotification = true };

            await MessageCopier.CopyMessage(bot.Context.Object, source, 200, option);

            var request = bot.Single<CopyMessageRequest>();
            request.Caption.Should().Be("note");
            request.ProtectContent.Should().BeTrue();
            request.DisableNotification.Should().BeTrue();
        }

        [Test]
        public async Task CopyMessagesSendsOneRequestPerMessage()
        {
            var bot = new BotClientMock();
            var messages = new List<Message>
            {
                new Message { Id = 1, Chat = new Chat { Id = 100 } },
                new Message { Id = 2, Chat = new Chat { Id = 100 } },
                new Message { Id = 3, Chat = new Chat { Id = 100 } }
            };

            var result = await MessageCopier.CopyMessages(bot.Context.Object, messages, 200);

            bot.SentCount.Should().Be(3);
            result.Should().HaveCount(3);
        }

        [Test]
        public async Task CopyMessagesKeepsTheOrderOfTheSourceMessages()
        {
            var bot = new BotClientMock();
            var messages = new List<Message>
            {
                new Message { Id = 11, Chat = new Chat { Id = 100 } },
                new Message { Id = 22, Chat = new Chat { Id = 100 } }
            };

            await MessageCopier.CopyMessages(bot.Context.Object, messages, 200);

            bot.At<CopyMessageRequest>(0).MessageId.Should().Be(11);
            bot.At<CopyMessageRequest>(1).MessageId.Should().Be(22);
        }

        [Test]
        public async Task CopyMessagesSendsNothingForAnEmptyList()
        {
            var bot = new BotClientMock();

            var result = await MessageCopier.CopyMessages(bot.Context.Object, new List<Message>(), 200);

            bot.SentCount.Should().Be(0);
            result.Should().BeEmpty();
        }

        #endregion

        #region MessageNotification

        [Test]
        public async Task NotifyFromCallBackAnswersTheCallbackQuery()
        {
            var bot = new BotClientMock();

            await MessageNotification.NotifyFromCallBack(bot.Context.Object, "query-id", "done");

            var request = bot.Single<AnswerCallbackQueryRequest>();
            request.CallbackQueryId.Should().Be("query-id");
            request.Text.Should().Be("done");
        }

        [Test]
        public async Task NotifyFromCallBackShowsAnAlertByDefault()
        {
            var bot = new BotClientMock();

            await MessageNotification.NotifyFromCallBack(bot.Context.Object, "query-id", "done");

            bot.Single<AnswerCallbackQueryRequest>().ShowAlert.Should().BeTrue();
        }

        [Test]
        public async Task NotifyFromCallBackCanSkipTheAlert()
        {
            var bot = new BotClientMock();

            await MessageNotification.NotifyFromCallBack(bot.Context.Object, "query-id", "done", showAlert: false);

            bot.Single<AnswerCallbackQueryRequest>().ShowAlert.Should().BeFalse();
        }

        [Test]
        public async Task NotifyFromCallBackCarriesTheUrlAndCacheTime()
        {
            var bot = new BotClientMock();

            await MessageNotification.NotifyFromCallBack(
                bot.Context.Object, "query-id", "done", true, "https://example.com", 30);

            var request = bot.Single<AnswerCallbackQueryRequest>();
            request.Url.Should().Be("https://example.com");
            request.CacheTime.Should().Be(30);
        }

        #endregion
    }
}
