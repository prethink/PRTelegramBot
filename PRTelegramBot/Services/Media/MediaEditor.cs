using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Services.Media
{
    public class MediaEditor
    {
        #region Methods

        /// <summary>
        /// Edits a photo. 
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="stream">Stream.</param>
        /// <param name="filename">File name.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await context.BotClient.EditMessageMedia(
                chatId: chatId,
                media: new InputMediaPhoto(InputFile.FromStream(stream, filename)),
                messageId: messageId,
                replyMarkup: replyMarkup,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Edits the inline menu together with the photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="media">Media.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> EditWithPhoto(IBotContext context, long chatId, int messageId, string text, InputMedia media, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            Message message = null;
            if (option?.MenuInlineKeyboardMarkup is not null)
            {
                await context.BotClient.EditMessageMedia(
                    chatId: chatId,
                    messageId: messageId,
                    media: media,
                    replyMarkup: option.MenuInlineKeyboardMarkup,
                    cancellationToken: option.CancellationToken);

                message = await context.BotClient.EditMessageCaption(
                    chatId: chatId,
                    messageId: messageId,
                    caption: text,
                    parseMode: option.ParseMode,
                    replyMarkup: option.MenuInlineKeyboardMarkup,
                    cancellationToken: option.CancellationToken);
            }

            return message;
        }

        /// <summary>
        /// Edits a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="photoPath">Path to the photo.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> EditPhoto(IBotContext context, long chatId, int messageId, string photoPath, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            if (!File.Exists(photoPath))
                return await MessageEditor.EditInline(context, chatId, messageId, option);

            using (var fileStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await EditPhoto(context, chatId, messageId, fileStream, option: option);
        }

        /// <summary>
        /// Edits the caption under the photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> EditCaption(IBotContext context, long chatId, int messageId, string text, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await context.BotClient.EditMessageCaption(
                chatId: chatId,
                messageId: messageId,
                caption: text,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                captionEntities: option.Entities,
                cancellationToken: option.CancellationToken);
        }

        #endregion
    }
}
