using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Helper class that holds the settings used to send messages in Telegram.
    /// </summary>
    public sealed class OptionMessage
    {
        #region Fields and properties

        /// <summary>
        /// Adds a reply menu.
        /// </summary>
        public ReplyKeyboardMarkup MenuReplyKeyboardMarkup { get; set; }

        /// <summary>
        /// Adds an inline menu.
        /// </summary>
        public InlineKeyboardMarkup MenuInlineKeyboardMarkup { get; set; }

        /// <summary>
        /// Parse mode.
        /// </summary>
        public ParseMode ParseMode { get; set; } = ParseMode.Html;

        /// <summary>
        /// Clears the menu.
        /// </summary>
        public bool ClearMenu { get; set; }

        /// <summary>
        /// Message text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Message identifier.
        /// </summary>
        public int? MessageId { get; set; }

        /// <summary>
        /// Checks that the message is present.
        /// </summary>
        /// <returns>True if a message exists; False if it does not.</returns>
        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

        /// <summary>
        /// Topic / channel identifier.
        /// </summary>
        public int? MessageThreadId { get; set; }

        /// <summary>
        /// Indicates that the message content is protected.
        /// </summary>
        public bool ProtectedContent { get; set; }

        /// <summary>
        /// Cancellation token.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Message entities.
        /// </summary>
        public IEnumerable<MessageEntity>? Entities { get; set; }

        /// <summary>
        /// Disables web page previews.
        /// </summary>
        public bool DisableWebPagePreview { get; set; }

        /// <summary>
        /// Disables notifications.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        /// Disables content type detection.
        /// </summary>
        public bool DisableContentTypeDetection { get; set; }

        /// <summary>
        /// Identifier of the message to reply to.
        /// </summary>
        public int? ReplyToMessageId { get; set; }

        /// <summary>
        /// Identifier of the incoming ephemeral message to reply to.
        /// </summary>
        /// <remarks>
        /// A reply to an ephemeral message must itself be ephemeral, and Telegram accepts it
        /// only within 15 seconds of the original. Set this instead of
        /// <see cref="ReplyToMessageId"/> — an ephemeral message has no ordinary message id.
        /// </remarks>
        public int? ReplyToEphemeralMessageId { get; set; }

        /// <summary>
        /// Allows sending without a reply.
        /// </summary>
        public bool AllowSendingWithoutReply { get; set; }

        /// <summary>
        /// Message caption.
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// Message thumbnail.
        /// </summary>
        public InputFile? Thumbnail { get; set; }

        /// <summary>
        /// Indicates that the message contains a spoiler.
        /// </summary>
        public bool HasSpoiler { get; set; }

        /// <summary>
        /// Unique identifier of the business connection the message is sent on behalf of.
        /// </summary>
        public string? BusinessConnectionId { get; set; }

        /// <summary>
        /// Unique identifier of the message effect to add to the message. Private chats only.
        /// </summary>
        public string? MessageEffectId { get; set; }

        /// <summary>
        /// Allows up to 1000 messages per second, ignoring the broadcasting limits, for a fee
        /// in Telegram Stars that is withdrawn from the bot's balance.
        /// </summary>
        public bool AllowPaidBroadcast { get; set; }

        /// <summary>
        /// Identifier of the direct messages topic the message is sent to.
        /// Required when the message goes to a direct messages chat.
        /// </summary>
        public long? DirectMessagesTopicId { get; set; }

        /// <summary>
        /// Parameters of the suggested post to send. Direct messages chats only.
        /// </summary>
        public SuggestedPostParameters? SuggestedPostParameters { get; set; }

        /// <summary>
        /// Shows the caption above the media instead of below it.
        /// Applies to photos, copied messages and caption edits.
        /// </summary>
        public bool ShowCaptionAboveMedia { get; set; }

        /// <summary>
        /// Parameters of the ephemeral message to send.
        /// </summary>
        /// <remarks>
        /// An ephemeral message is shown to a single user as an overlay over the chat and is
        /// never stored in the chat history. Set at least
        /// <see cref="Telegram.Bot.Types.EphemeralMessageParameters.ReceiverUserId"/>; a plain
        /// <see cref="long"/> converts to it implicitly, so
        /// <c>EphemeralMessageParameters = userId</c> is enough for the simple case.
        /// </remarks>
        public EphemeralMessageParameters? EphemeralMessageParameters { get; set; }

        #endregion
    }
}
