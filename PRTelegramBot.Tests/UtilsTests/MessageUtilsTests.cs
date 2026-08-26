using FluentAssertions;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.UtilsTests
{
    public class MessageUtilsTests
    {
        #region SplitIntoChunks

        [Test]
        public void SplitIntoChunksSplitsEvenly()
        {
            var chunks = MessageUtils.SplitIntoChunks("abcdef", 2);

            chunks.Should().BeEquivalentTo(new[] { "ab", "cd", "ef" }, options => options.WithStrictOrdering());
        }

        [Test]
        public void SplitIntoChunksKeepsTheRemainderInTheLastChunk()
        {
            var chunks = MessageUtils.SplitIntoChunks("abcdefg", 3);

            chunks.Should().BeEquivalentTo(new[] { "abc", "def", "g" }, options => options.WithStrictOrdering());
        }

        [Test]
        public void SplitIntoChunksReturnsOneChunkWhenTheTextIsShorter()
        {
            MessageUtils.SplitIntoChunks("abc", 100).Should().BeEquivalentTo("abc");
        }

        [Test]
        public void SplitIntoChunksReturnsNothingForAnEmptyText()
        {
            MessageUtils.SplitIntoChunks(string.Empty, 10).Should().BeEmpty();
        }

        [Test]
        public void SplitIntoChunksSplitsIntoSingleCharacters()
        {
            MessageUtils.SplitIntoChunks("abc", 1).Should().BeEquivalentTo("a", "b", "c");
        }

        [Test]
        public void SplitIntoChunksKeepsTheWholeTextTogether()
        {
            var text = new string('x', 9000);

            var chunks = MessageUtils.SplitIntoChunks(text, 4000);

            chunks.Should().HaveCount(3);
            string.Concat(chunks).Should().Be(text);
        }

        /// <summary>
        /// A chunk size of zero can never consume the text, so the loop would never end.
        /// </summary>
        [TestCase(0)]
        [TestCase(-1)]
        public void SplitIntoChunksRejectsANonPositiveChunkSize(int chunkSize)
        {
            var act = () => MessageUtils.SplitIntoChunks("abc", chunkSize);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SplitIntoChunksRejectsNullText()
        {
            var act = () => MessageUtils.SplitIntoChunks(null!, 10);

            act.Should().Throw<ArgumentNullException>();
        }

        #endregion

        #region CreateOptionsIfNull

        [Test]
        public void CreateOptionsIfNullCreatesOptionsWhenNoneWereGiven()
        {
            MessageUtils.CreateOptionsIfNull(null).Should().NotBeNull();
        }

        [Test]
        public void CreateOptionsIfNullKeepsTheGivenOptions()
        {
            var option = new OptionMessage { Message = "kept" };

            MessageUtils.CreateOptionsIfNull(option).Should().BeSameAs(option);
        }

        #endregion

        #region Reply parameters

        [Test]
        public void ReplyParametersCarryTheMessageBeingRepliedTo()
        {
            var option = new OptionMessage { ReplyToMessageId = 77 };

            MessageUtils.CreateReplyParametersFromOptions(option)!.MessageId.Should().Be(77);
        }

        /// <summary>
        /// The Bot API needs reply parameters to name either a message or an ephemeral message.
        /// An object naming neither is rejected on the paths that validate it, so there must not
        /// be one at all when nothing is being replied to.
        /// </summary>
        [Test]
        public void ThereAreNoReplyParametersWhenThereIsNoReply()
        {
            var option = new OptionMessage();

            MessageUtils.CreateReplyParametersFromOptions(option).Should().BeNull();
        }

        [Test]
        public void ReplyParametersCarryTheEphemeralMessageBeingRepliedTo()
        {
            var option = new OptionMessage { ReplyToEphemeralMessageId = 77 };

            MessageUtils.CreateReplyParametersFromOptions(option)!.EphemeralMessageId.Should().Be(77);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ReplyParametersCarryAllowSendingWithoutReply(bool allow)
        {
            var option = new OptionMessage { ReplyToMessageId = 77, AllowSendingWithoutReply = allow };

            MessageUtils.CreateReplyParametersFromOptions(option)!
                .AllowSendingWithoutReply.Should().Be(allow);
        }

        /// <summary>
        /// Without a reply target the flag has nothing to apply to.
        /// </summary>
        [Test]
        public void AllowSendingWithoutReplyAloneDoesNotMakeReplyParameters()
        {
            var option = new OptionMessage { AllowSendingWithoutReply = true };

            MessageUtils.CreateReplyParametersFromOptions(option).Should().BeNull();
        }

        #endregion

        #region Link preview

        [TestCase(true)]
        [TestCase(false)]
        public void LinkPreviewIsDisabledAccordingToTheOptions(bool disabled)
        {
            var option = new OptionMessage { DisableWebPagePreview = disabled };

            MessageUtils.CreateLinkPreviewOptionsFromOption(option).IsDisabled.Should().Be(disabled);
        }

        #endregion

        #region GetReplyMarkup

        [Test]
        public void GetReplyMarkupReturnsNothingWithoutOptions()
        {
            MessageUtils.GetReplyMarkup(null).Should().BeNull();
        }

        [Test]
        public void GetReplyMarkupReturnsNothingWhenNoMenuWasSet()
        {
            MessageUtils.GetReplyMarkup(new OptionMessage()).Should().BeNull();
        }

        [Test]
        public void GetReplyMarkupRemovesTheKeyboardWhenAskedToClearTheMenu()
        {
            var option = new OptionMessage { ClearMenu = true };

            MessageUtils.GetReplyMarkup(option).Should().BeOfType<ReplyKeyboardRemove>();
        }

        [Test]
        public void GetReplyMarkupReturnsTheReplyKeyboard()
        {
            var keyboard = new ReplyKeyboardMarkup(new KeyboardButton("button"));
            var option = new OptionMessage { MenuReplyKeyboardMarkup = keyboard };

            MessageUtils.GetReplyMarkup(option).Should().BeSameAs(keyboard);
        }

        [Test]
        public void GetReplyMarkupReturnsTheInlineKeyboard()
        {
            var keyboard = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("text", "data"));
            var option = new OptionMessage { MenuInlineKeyboardMarkup = keyboard };

            MessageUtils.GetReplyMarkup(option).Should().BeSameAs(keyboard);
        }

        /// <summary>
        /// Clearing the menu wins over any keyboard that was also supplied.
        /// </summary>
        [Test]
        public void ClearMenuTakesPrecedenceOverTheKeyboards()
        {
            var option = new OptionMessage
            {
                ClearMenu = true,
                MenuReplyKeyboardMarkup = new ReplyKeyboardMarkup(new KeyboardButton("button")),
                MenuInlineKeyboardMarkup = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("text", "data"))
            };

            MessageUtils.GetReplyMarkup(option).Should().BeOfType<ReplyKeyboardRemove>();
        }

        /// <summary>
        /// When both keyboards are set, the reply keyboard is the one that is used.
        /// </summary>
        [Test]
        public void ReplyKeyboardTakesPrecedenceOverTheInlineKeyboard()
        {
            var reply = new ReplyKeyboardMarkup(new KeyboardButton("button"));
            var option = new OptionMessage
            {
                MenuReplyKeyboardMarkup = reply,
                MenuInlineKeyboardMarkup = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("text", "data"))
            };

            MessageUtils.GetReplyMarkup(option).Should().BeSameAs(reply);
        }

        #endregion
    }
}
