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
        /// <param name="chunkSize">Chunk size.</param>
        /// <returns>Collection of messages.</returns>
        public static IList<string> SplitIntoChunks(string text, int chunkSize)
        {
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
        public static OptionMessage CreateOptionsIfNull(OptionMessage option = null)
        {
            if (option is null)
                option = new OptionMessage();
            return option;
        }

        /// <summary>
        /// Creates a <see cref="ReplyParameters"/> object from the supplied <see cref="OptionMessage"/> options.
        /// </summary>
        /// <param name="option">Message options the reply parameters are taken from.</param>
        /// <returns>A <see cref="ReplyParameters"/> instance with the <see cref="ReplyParameters.MessageId"/> and <see cref="ReplyParameters.AllowSendingWithoutReply"/> fields filled in.</returns>
        public static ReplyParameters CreateReplyParametersFromOptions(OptionMessage option)
        {
            ReplyParameters parameters = new ReplyParameters();
            if (option.ReplyToMessageId is not null)
                parameters.MessageId = option.ReplyToMessageId.Value;
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
        public static ReplyMarkup? GetReplyMarkup(OptionMessage option = null)
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
