using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Services.Media
{
    /// <summary>
    /// Sends media: photos, photo groups, files and media by URL.
    /// </summary>
    public class MediaSender
    {
        #region Methods

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="stream">Stream.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, Stream stream, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            return await context.BotClient.SendPhoto(
                chatId: chatId,
                photo: InputFile.FromStream(stream),
                caption: text,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                messageThreadId: option.MessageThreadId,
                captionEntities: option.Entities,
                hasSpoiler: option.HasSpoiler,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                messageEffectId: option.MessageEffectId,
                businessConnectionId: option.BusinessConnectionId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                suggestedPostParameters: option.SuggestedPostParameters,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, string filePath, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            if (!File.Exists(filePath))
                return await MessageSender.Send(context, chatId, text, option);

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await SendPhoto(context, chatId, text, fileStream, option);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="msg">Text.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> SendPhotoWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            return await context.BotClient.SendPhoto(
                chatId: chatId,
                photo: InputFile.FromString(url),
                caption: msg,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                messageThreadId: option.MessageThreadId,
                captionEntities: option.Entities,
                hasSpoiler: option.HasSpoiler,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                messageEffectId: option.MessageEffectId,
                businessConnectionId: option.BusinessConnectionId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                suggestedPostParameters: option.SuggestedPostParameters,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="msg">Text.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> SendMediaWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            return await context.BotClient.SendDocument(
                chatId: chatId,
                document: InputFile.FromString(url),
                caption: msg,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                messageThreadId: option.MessageThreadId,
                captionEntities: option.Entities,
                disableContentTypeDetection: option.DisableContentTypeDetection,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                messageEffectId: option.MessageEffectId,
                businessConnectionId: option.BusinessConnectionId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                suggestedPostParameters: option.SuggestedPostParameters,
                cancellationToken: option.CancellationToken);

            #endregion
        }

        /// <summary>
        /// Sends a group of photos.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="filepaths">Paths to the files.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Collection of messages.</returns>
        public static async Task<Message[]> SendPhotoGroup(IBotContext context, long chatId, string text, List<string> filepaths, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            List<InputMediaPhoto> media = new();
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            bool isFirst = true;
            int count = 0;
            foreach (var item in filepaths)
            {
                if (isFirst)
                {
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        media.Add(new InputMediaPhoto(InputFile.FromString(item)));
                        isFirst = false;
                    }
                    else
                    {
                        media.Add(new InputMediaPhoto(InputFile.FromString(item)) { Caption = text, ParseMode = ParseMode.Html });
                        isFirst = false;
                    }

                }
                else
                {
                    media.Add(new InputMediaPhoto(InputFile.FromString(item)));
                }
                count++;

            }

            return await context.BotClient.SendMediaGroup(
                chatId: chatId,
                media: media.ToArray(),
                messageThreadId: option.MessageThreadId,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                messageEffectId: option.MessageEffectId,
                businessConnectionId: option.BusinessConnectionId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Sends a file.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> SendFile(IBotContext context, long chatId, string text, string filePath, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            if (!File.Exists(filePath))
            {
                var message = await MessageSender.Send(context, chatId, text, option);
                return message;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var message = await context.BotClient.SendDocument(chatId: chatId,
                    document: InputFile.FromStream(fileStream, Path.GetFileName(filePath)),
                    caption: text,
                    messageThreadId: option.MessageThreadId,
                    replyMarkup: replyMarkup,
                    thumbnail: option.Thumbnail,
                    parseMode: option.ParseMode,
                    captionEntities: option.Entities,
                    disableContentTypeDetection: option.DisableContentTypeDetection,
                    disableNotification: option.DisableNotification,
                    protectContent: option.ProtectedContent,
                    replyParameters: replyParams,
                    messageEffectId: option.MessageEffectId,
                    businessConnectionId: option.BusinessConnectionId,
                    allowPaidBroadcast: option.AllowPaidBroadcast,
                    directMessagesTopicId: option.DirectMessagesTopicId,
                    suggestedPostParameters: option.SuggestedPostParameters,
                    cancellationToken: context.CancellationToken);

                return message;
            }
        }
    }
}
