using System.Reflection;
using FluentAssertions;
using Telegram.Bot.Requests;

namespace PRTelegramBot.Tests.CoverageGuards
{
    /// <summary>
    /// Guards that fail when a Bot API request gains a parameter nobody has looked at.
    /// </summary>
    /// <remarks>
    /// These tests do not decide whether a parameter belongs on <c>OptionMessage</c> — some are
    /// supplied by the caller, some are irrelevant to this framework. They only make a new
    /// parameter impossible to miss. When one fails: decide what to do with the newcomer,
    /// then add it to the list below so the test goes green again.
    ///
    /// Five send parameters sat unexposed for several releases because nothing watched for them.
    /// </remarks>
    public class RequestParameterCoverageTests
    {
        private static readonly Dictionary<Type, string[]> ReviewedParameters = new()
        {
            [typeof(SendMessageRequest)] = new[]
            {
                "ChatId", "Text",
                "ParseMode", "Entities", "LinkPreviewOptions", "ReplyParameters", "ReplyMarkup",
                "MessageThreadId", "DisableNotification", "ProtectContent",
                "BusinessConnectionId", "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId", "SuggestedPostParameters",
                "ReceiverUserId", "CallbackQueryId"
            },
            [typeof(SendPhotoRequest)] = new[]
            {
                "ChatId", "Photo", "Caption",
                "ParseMode", "CaptionEntities", "ReplyParameters", "ReplyMarkup",
                "MessageThreadId", "DisableNotification", "ProtectContent", "HasSpoiler",
                "ShowCaptionAboveMedia",
                "BusinessConnectionId", "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId", "SuggestedPostParameters",
                "ReceiverUserId", "CallbackQueryId"
            },
            [typeof(SendDocumentRequest)] = new[]
            {
                "ChatId", "Document", "Caption", "Thumbnail",
                "ParseMode", "CaptionEntities", "ReplyParameters", "ReplyMarkup",
                "MessageThreadId", "DisableNotification", "ProtectContent",
                "DisableContentTypeDetection",
                "BusinessConnectionId", "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId", "SuggestedPostParameters",
                "ReceiverUserId", "CallbackQueryId"
            },
            [typeof(SendMediaGroupRequest)] = new[]
            {
                "ChatId", "Media",
                "ReplyParameters", "MessageThreadId", "DisableNotification", "ProtectContent",
                "BusinessConnectionId", "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId"
            },
            [typeof(CopyMessageRequest)] = new[]
            {
                "ChatId", "FromChatId", "MessageId", "Caption",
                "ParseMode", "CaptionEntities", "ReplyParameters", "ReplyMarkup",
                "MessageThreadId", "DisableNotification", "ProtectContent",
                "ShowCaptionAboveMedia",
                "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId", "SuggestedPostParameters",
                // reviewed and deliberately not exposed: it belongs to video copying
                "VideoStartTimestamp"
            },
            [typeof(EditMessageTextRequest)] = new[]
            {
                "ChatId", "MessageId", "Text",
                "ParseMode", "Entities", "LinkPreviewOptions", "ReplyMarkup",
                "BusinessConnectionId",
                // reviewed and deliberately not exposed: a distinct message kind, not an option
                "RichMessage"
            },
            [typeof(EditMessageCaptionRequest)] = new[]
            {
                "ChatId", "MessageId", "Caption",
                "ParseMode", "CaptionEntities", "ReplyMarkup",
                "ShowCaptionAboveMedia", "BusinessConnectionId"
            },
            [typeof(EditMessageReplyMarkupRequest)] = new[]
            {
                "ChatId", "MessageId", "ReplyMarkup", "BusinessConnectionId"
            },
            [typeof(DeleteMessageRequest)] = new[]
            {
                "ChatId", "MessageId"
            },
            [typeof(AnswerCallbackQueryRequest)] = new[]
            {
                "CallbackQueryId", "Text", "ShowAlert", "Url", "CacheTime"
            }
        };

        private static IEnumerable<TestCaseData> Requests()
        {
            foreach (var pair in ReviewedParameters)
                yield return new TestCaseData(pair.Key, pair.Value).SetName($"Reviewed_{pair.Key.Name}");
        }

        [TestCaseSource(nameof(Requests))]
        public void RequestHasNoUnreviewedParameters(Type requestType, string[] reviewed)
        {
            var actual = requestType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(x => x.Name)
                .ToList();

            var unreviewed = actual.Except(reviewed).ToList();

            unreviewed.Should().BeEmpty(
                "Telegram.Bot added parameters to {0} that nobody has looked at yet. Decide whether " +
                "OptionMessage should expose them and whether the services should forward them, " +
                "then list them in RequestParameterCoverageTests. New: {1}",
                requestType.Name,
                string.Join(", ", unreviewed));
        }

        [TestCaseSource(nameof(Requests))]
        public void ReviewedParametersStillExist(Type requestType, string[] reviewed)
        {
            var actual = requestType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .ToHashSet();

            var gone = reviewed.Where(name => !actual.Contains(name)).ToList();

            gone.Should().BeEmpty(
                "{0} no longer has parameters this test claims to know about. Telegram.Bot removed " +
                "them, so anything forwarding them needs revisiting. Removed: {1}",
                requestType.Name,
                string.Join(", ", gone));
        }
    }
}
