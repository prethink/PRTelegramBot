using FluentAssertions;
using PRTelegramBot.Wrappers;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.ServicesTests
{
    /// <summary>
    /// Pins dependency behavior used by callers of the framework's serializers and media helpers.
    /// These regressions were fixed in Telegram.Bot 22.10.3.2 and 22.10.3.3.
    /// </summary>
    public class TelegramBotCompatibilityTests
    {
        private static IEnumerable<TestCaseData> ReplyMarkups()
        {
            yield return new TestCaseData(new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Open", "open")))
                .SetArgDisplayNames(nameof(InlineKeyboardMarkup));
            yield return new TestCaseData(new ReplyKeyboardMarkup(new KeyboardButton("Share"))
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            }).SetArgDisplayNames(nameof(ReplyKeyboardMarkup));
            yield return new TestCaseData(new ReplyKeyboardRemove { Selective = true })
                .SetArgDisplayNames(nameof(ReplyKeyboardRemove));
            yield return new TestCaseData(new ForceReplyMarkup { Selective = true, InputFieldPlaceholder = "Answer" })
                .SetArgDisplayNames(nameof(ForceReplyMarkup));
        }

        [TestCaseSource(nameof(ReplyMarkups))]
        public void ReplyMarkupRoundTripPreservesConcreteTypeAndPayload(ReplyMarkup markup)
        {
            var serializer = new JsonSerializerWrapper(JsonBotAPI.Options);

            var json = serializer.Serialize<ReplyMarkup>(markup);
            var restored = serializer.Deserialize<ReplyMarkup>(json);

            restored.Should().BeOfType(markup.GetType());
            restored.Should().BeEquivalentTo(markup, options => options.RespectingRuntimeTypes());
        }

        [Test]
        public void DocumentSurvivesRichMediaSerialization()
        {
            var serializer = new JsonSerializerWrapper(JsonBotAPI.Options);
            IInputRichMedia media = new InputMediaDocument(InputFile.FromFileId("existing-document"))
            {
                Caption = "Report"
            };

            var json = serializer.Serialize<IInputRichMedia>(media);
            var restored = serializer.Deserialize<IInputRichMedia>(json);

            var document = restored.Should().BeOfType<InputMediaDocument>().Subject;
            document.Caption.Should().Be("Report");
            document.Media.Should().BeOfType<InputFileId>().Which.Id.Should().Be("existing-document");
            json.Should().Contain("\"type\":\"document\"");
        }

        [Test]
        public void LivePhotoSurvivesAlbumSerialization()
        {
            var serializer = new JsonSerializerWrapper(JsonBotAPI.Options);
            IAlbumInputMedia media = new InputMediaLivePhoto(
                InputFile.FromFileId("existing-video"), InputFile.FromFileId("existing-photo"))
            {
                Caption = "Live photo",
                ShowCaptionAboveMedia = true,
                HasSpoiler = true
            };

            var json = serializer.Serialize<IAlbumInputMedia>(media);
            var restored = serializer.Deserialize<IAlbumInputMedia>(json);

            var livePhoto = restored.Should().BeOfType<InputMediaLivePhoto>().Subject;
            livePhoto.Media.Should().BeOfType<InputFileId>().Which.Id.Should().Be("existing-video");
            livePhoto.Photo.Should().BeOfType<InputFileId>().Which.Id.Should().Be("existing-photo");
            livePhoto.Caption.Should().Be("Live photo");
            livePhoto.ShowCaptionAboveMedia.Should().BeTrue();
            livePhoto.HasSpoiler.Should().BeTrue();
            json.Should().Contain("\"type\":\"live_photo\"");
        }

        [TestCase("<b>A😀B</b>", 3)]
        [TestCase("<i>😀🚀</i>&amp;", 3)]
        [TestCase("A&#128512;B", 3)]
        [TestCase("<b>Report</b>&amp;", 7)]
        public void HtmlPlainLengthCountsUnicodeCharacters(string html, int expected)
        {
            HtmlText.PlainLength(html).Should().Be(expected);
        }

        [Test]
        public void HtmlPlainLengthAgreesWithTruncationForEmoji()
        {
            var truncated = HtmlText.Truncate("<b>A😀B🚀C</b>", 3, "");

            HtmlText.ToPlain(truncated).Should().Be("A😀B");
            HtmlText.PlainLength(truncated).Should().Be(3);
        }
    }
}
