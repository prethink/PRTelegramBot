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
        public InputFile? thumbnail { get; set; }

        /// <summary>
        /// Indicates that the message contains a spoiler.
        /// </summary>
        public bool HasSpoiler { get; set; }

        #endregion
    }
}
