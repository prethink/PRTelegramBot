using PRTelegramBot.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utilities for working with messages and their options.
    /// </summary>
    public static class MessageUtils
    {
        /// <summary>
        /// Splits a long message into chunks.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="chunkSize">Chunk size. Must be greater than zero.</param>
        /// <returns>Collection of messages.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="chunkSize"/> is zero or negative: such a size can never
        /// consume the text and would loop forever.
        /// </exception>
        public static IList<string> SplitIntoChunks(string text, int chunkSize)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (chunkSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(chunkSize), chunkSize, "Chunk size must be greater than zero.");

            List<string> chunks = new List<string>();
            int offset = 0;
            while (offset < text.Length)
            {
                int size = Math.Min(chunkSize, text.Length - offset);
                chunks.Add(text.Substring(offset, size));
                offset += size;
            }
            return chunks;
        }

        /// <summary>
        /// Creates the options when option is null.
        /// </summary>
        /// <param name="option">Parameters.</param>
        /// <returns>An instance of the OptionMessage class.</returns>
        public static OptionMessage CreateOptionsIfNull(OptionMessage? option = null)
        {
            if (option is null)
                option = new OptionMessage();
            return option;
        }

        /// <summary>
        /// Creates a <see cref="ReplyParameters"/> object from the supplied <see cref="OptionMessage"/> options.
        /// </summary>
        /// <param name="option">Message options the reply parameters are taken from.</param>
        /// <returns>
        /// A <see cref="ReplyParameters"/> instance with the reply target filled in, or
        /// <see langword="null"/> when the options name no message to reply to.
        /// </returns>
        /// <remarks>
        /// The Bot API requires reply parameters to carry either <see cref="ReplyParameters.MessageId"/>
        /// or <see cref="ReplyParameters.EphemeralMessageId"/>. Sending the object with neither used to
        /// pass unnoticed on ordinary messages, which Telegram ignored, but it is rejected with
        /// <c>MESSAGE_ID_INVALID</c> on the paths that validate it — an ephemeral message replacing the
        /// one whose button was pressed, for instance. Returning nothing leaves the field off the
        /// request entirely, which is what "no reply" is supposed to look like.
        /// </remarks>
        public static ReplyParameters? CreateReplyParametersFromOptions(OptionMessage option)
        {
            if (option.ReplyToMessageId is null && option.ReplyToEphemeralMessageId is null)
                return null;

            ReplyParameters parameters = new ReplyParameters();
            if (option.ReplyToMessageId is not null)
                parameters.MessageId = option.ReplyToMessageId.Value;
            if (option.ReplyToEphemeralMessageId is not null)
                parameters.EphemeralMessageId = option.ReplyToEphemeralMessageId.Value;
            parameters.AllowSendingWithoutReply = option.AllowSendingWithoutReply;

            return parameters;
        }

        /// <summary>
        /// Creates a <see cref="LinkPreviewOptions"/> object from the supplied <see cref="OptionMessage"/> options.
        /// </summary>
        /// <param name="option">Message options the link preview setting is taken from.</param>
        /// <returns>A <see cref="LinkPreviewOptions"/> instance with the <see cref="LinkPreviewOptions.IsDisabled"/> property filled in.</returns>
        public static LinkPreviewOptions CreateLinkPreviewOptionsFromOption(OptionMessage option)
        {
            LinkPreviewOptions linkOptions = new LinkPreviewOptions();
            linkOptions.IsDisabled = option.DisableWebPagePreview;
            return linkOptions;
        }

        /// <summary>
        /// Gets the menu from the message options.
        /// </summary>
        /// <param name="option">Message parameters.</param>
        /// <returns>The generated menu, or null.</returns>
        public static ReplyMarkup? GetReplyMarkup(OptionMessage? option = null)
        {
            option = CreateOptionsIfNull(option);

            ReplyMarkup replyMarkup = null;
            if (option.ClearMenu)
                replyMarkup = new ReplyKeyboardRemove();
            else if (option.MenuReplyKeyboardMarkup is not null)
                replyMarkup = option.MenuReplyKeyboardMarkup;
            else if (option.MenuInlineKeyboardMarkup is not null)
                replyMarkup = option.MenuInlineKeyboardMarkup;

            return replyMarkup;
        }
    }
}
