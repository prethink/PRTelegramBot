using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Services.Media
{
    public class MediaSender
    {
        #region Методы

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, Stream stream, OptionMessage option = null)
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
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, string filePath, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            if (!File.Exists(filePath))
                return await MessageSender.Send(context, chatId, text, option);

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await SendPhoto(context, chatId, text, fileStream, option);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="msg">Текст.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendPhotoWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage option = null)
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
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="msg">Текст.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendMediaWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage option = null)
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
                cancellationToken: option.CancellationToken);

            #endregion
        }

        /// <summary>
        /// Отправка группы фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filepaths">Путь к файлам.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Коллекция сообщений.</returns>
        public static async Task<Message[]> SendPhotoGroup(IBotContext context, long chatId, string text, List<string> filepaths, OptionMessage option = null)
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
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка файла.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendFile(IBotContext context, long chatId, string text, string filePath, OptionMessage option = null)
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
                    thumbnail: option.thumbnail,
                    parseMode: option.ParseMode,
                    captionEntities: option.Entities,
                    disableContentTypeDetection: option.DisableContentTypeDetection,
                    disableNotification: option.DisableNotification,
                    protectContent: option.ProtectedContent,
                    replyParameters: replyParams,
                    cancellationToken: context.CancellationToken);

                return message;
            }
        }
    }
}
