using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Helpers
{
    public class Message
    {
        #region Константы

        /// <summary>
        /// Максимальный размер сообщения.
        /// </summary>
        public const int MAX_MESSAGE_LENGTH = 4000;

        #endregion

        #region Методы

        /// <summary>
        /// Разбивает большое сообщение на блоки.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="chunkSize">Размер блока.</param>
        /// <returns>Коллекция сообщений.</returns>
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
        /// Копировать сообщение.
        /// </summary>
        /// <param name="botClient">Клиент telegram бота.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Идентификатор сообщения.</returns>
        public static async Task<MessageId> CopyMessage(ITelegramBotClient botClient, Telegram.Bot.Types.Message message, long chatId, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);
            ChatId toMsg = new ChatId(chatId);
            ChatId fromMsg = new ChatId(message.Chat.Id);

            var rMessage = await botClient.CopyMessageAsync(
                                    chatId: toMsg,
                                    fromChatId: fromMsg,
                                    messageId: message.MessageId,
                                    messageThreadId: option.MessageThreadId,
                                    caption: option.Caption,
                                    parseMode: option.ParseMode,
                                    captionEntities: option.Entities,
                                    disableNotification: option.DisableNotification,
                                    protectContent: option.ProtectedContent,
                                    replyToMessageId: option.ReplyToMessageId,
                                    allowSendingWithoutReply: option.AllowSendingWithoutReply,
                                    replyMarkup: replyMarkup,
                                    cancellationToken: option.CancellationToken);
            return rMessage;
        }

        /// <summary>
        /// Копирует коллекцию сообщений.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="messages">Сообщения.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Коллекция идентификаторов сообщений.</returns>
        public static async Task<List<MessageId>> CopyMessages(ITelegramBotClient botClient, List<Telegram.Bot.Types.Message> messages, long chatId, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            List<MessageId> messageIds = new List<MessageId>();
            foreach (var message in messages)
                messageIds.Add(await CopyMessage(botClient, message, chatId, option));
            return messageIds;
        }

        /// <summary>
        /// Сообщение ожидание обработки сообщения.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> AwaitAnswerBot(ITelegramBotClient botClient, long chatId, string message = "⏳ Генерирую ответ...", OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var sentMessage = await Send(botClient, chatId, message, option);
            return sentMessage;
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="update">Обновление телерграм.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Send(ITelegramBotClient botClient, Update update, string text, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);

            var message = await Send(botClient, update.GetChatId(), text, option);
            return message;
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Send(ITelegramBotClient botClient, long chatId, string text, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            if (text.Length > MAX_MESSAGE_LENGTH)
            {
                var chunk = SplitIntoChunks(text, MAX_MESSAGE_LENGTH);
                int count = 0;
                foreach (var item in chunk)
                {
                    count++;
                    if (count < chunk.Count)
                        await Send(botClient, chatId, item, option);
                    if (count == chunk.Count)
                        text = item;
                }
            }

            return await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: text,
                            parseMode: option.ParseMode,
                            replyMarkup: replyMarkup,
                            messageThreadId: option.MessageThreadId,
                            entities: option.Entities,
                            disableWebPagePreview: option.DisableWebPagePreview,
                            disableNotification: option.DisableNotification,
                            protectContent: option.ProtectedContent,
                            replyToMessageId: option.ReplyToMessageId,
                            allowSendingWithoutReply: option.AllowSendingWithoutReply,
                            cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка группы фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filepaths">Путь к файлам.</param>
        /// <returns>Коллекция сообщений.</returns>
        public static async Task<Telegram.Bot.Types.Message[]> SendPhotoGroup(ITelegramBotClient botClient, long chatId, string text, List<string> filepaths, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            List<InputMediaPhoto> media = new();

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

            return await botClient.SendMediaGroupAsync(
                chatId: chatId, 
                media: media.ToArray(), 
                messageThreadId:option.MessageThreadId, 
                disableNotification: option.DisableNotification, 
                protectContent: option.ProtectedContent,
                replyToMessageId: option.ReplyToMessageId,
                allowSendingWithoutReply: option.AllowSendingWithoutReply,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата<./param>
        /// <param name="text">Текст.</param>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(ITelegramBotClient botClient, long chatId, string text, string filePath, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);

            if (!System.IO.File.Exists(filePath))
                return await Send(botClient, chatId, text, option);

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await SendPhoto(botClient, chatId, text, fileStream, option);
        }

        /// <summary>
        /// Отправка файла.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filePath">Путь к файлу.</param>
        public static async Task SendFile(ITelegramBotClient botClient, long chatId, string text, string filePath, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);
            if (!System.IO.File.Exists(filePath))
            {
                await Send(botClient, chatId, text, option);
                return;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var file = await botClient.SendDocumentAsync(chatId: chatId,
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
                                                            replyToMessageId: option.ReplyToMessageId,
                                                            allowSendingWithoutReply: option.AllowSendingWithoutReply,
                                                            cancellationToken: option.CancellationToken);
            }
        }

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(ITelegramBotClient botClient, long chatId, int messageId, string text, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await botClient.EditMessageTextAsync(
                        chatId: chatId,
                        messageId: messageId,
                        text: text,
                        parseMode: option.ParseMode,
                        replyMarkup: replyMarkup,
                        entities: option.Entities,
                        disableWebPagePreview: option.DisableWebPagePreview,
                        cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="update">Обновление телеграм.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(ITelegramBotClient botClient, Update update, string text, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);

            long chatId = update.GetChatId();
            int messageId = update.GetMessageId();

            var editmessage = await Edit(botClient, chatId, messageId, text, option);
            return editmessage;
        }

        /// <summary>
        /// Редактировать текст под фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщщения.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditCaption(ITelegramBotClient botClient, long chatId, int messageId, string text, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await botClient.EditMessageCaptionAsync(
                        chatId: chatId,
                        messageId: messageId,
                        caption: text,
                        parseMode: option.ParseMode,
                        replyMarkup: replyMarkup,
                        captionEntities: option.Entities,
                        cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Редактирование фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="photoPath">Путь к фото.</param>
        /// <param name="option">Насройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(ITelegramBotClient botClient, long chatId, int messageId, string photoPath, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);

            Telegram.Bot.Types.Message message;
            if (!System.IO.File.Exists(photoPath))
                return await EditInline(botClient, chatId, messageId, option);


            using (var fileStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await EditPhoto(botClient, chatId, messageId, fileStream, option: option);
        }

        /// <summary>
        /// Удалить сообщение.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        public static async Task DeleteMessage(ITelegramBotClient botClient, long chatId, int messageId, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            await botClient.DeleteMessageAsync(chatId, messageId, option.CancellationToken);
        }

        /// <summary>
        /// Редактирование текста под фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="update">Обновление телеграм.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditCaption(ITelegramBotClient botClient, Update update, string text, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);

            long chatId = update.GetChatId();
            int messageId = update.GetMessageId();

            var editmessage = await EditCaption(botClient, chatId, messageId, text, option);
            return editmessage;
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота<./param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(ITelegramBotClient botClient, long chatId, string text, Stream stream, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            return await botClient.SendPhotoAsync(
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
                                replyToMessageId: option.ReplyToMessageId,
                                allowSendingWithoutReply: option.AllowSendingWithoutReply,
                                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="msg">Текст.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> SendPhotoWithUrl(ITelegramBotClient botClient, long chatId, string msg, string url, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            return await botClient.SendPhotoAsync(
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
                            replyToMessageId: option.ReplyToMessageId,
                            allowSendingWithoutReply: option.AllowSendingWithoutReply,
                            cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="msg">Текст.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> SendMediaWithUrl(ITelegramBotClient botClient, long chatId, string msg, string url, OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            return await botClient.SendDocumentAsync(
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
                            replyToMessageId: option.ReplyToMessageId,
                            allowSendingWithoutReply: option.AllowSendingWithoutReply,
                            cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Редактирование меню inline.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="option">Насройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditInline(ITelegramBotClient botClient, long chatId, int messageId, OptionMessage option)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option) as InlineKeyboardMarkup;

            Telegram.Bot.Types.Message message = null;
            if (option?.MenuInlineKeyboardMarkup != null)
            {
                message = await botClient.EditMessageReplyMarkupAsync(
                        chatId: chatId,
                        messageId: messageId,
                        replyMarkup: replyMarkup,
                        cancellationToken: option.CancellationToken);
            }

            return message;
        }

        /// <summary>
        /// Редактирование фото. 
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="option">Насройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(ITelegramBotClient botClient, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage option = null)
        {
            option = CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await botClient.EditMessageMediaAsync(
                        chatId: chatId,
                        media: new InputMediaPhoto(InputFile.FromStream(stream, filename)),
                        messageId: messageId,
                        replyMarkup: replyMarkup,
                        cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Редактировать inline меню вместе с фото.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="media">Медиа.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditWithPhoto(ITelegramBotClient botClient, long chatId, int messageId, string text, InputMedia media, OptionMessage option)
        {
            option = CreateOptionsIfNull(option);

            Telegram.Bot.Types.Message message = null;
            if (option?.MenuInlineKeyboardMarkup != null)
            {
                await botClient.EditMessageMediaAsync(
                        chatId: chatId,
                        messageId: messageId,
                        media: media,
                        replyMarkup: option.MenuInlineKeyboardMarkup,
                        cancellationToken: option.CancellationToken);

                message = await botClient.EditMessageCaptionAsync(
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
        /// Вывод уведомления пользователю.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="callbackQueryId">Идентификатор callback.</param>
        /// <param name="text">Текст.</param>
        /// <param name="showAlert">Показывать уведомление.</param>
        /// <param name="url">.</param>
        /// <param name="cacheTime">.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Task</returns>
        public static async Task NotifyFromCallBack(
            ITelegramBotClient botClient,
            string callbackQueryId,
            string text,
            bool showAlert = true,
            string? url = null,
            int? cacheTime = null,
            CancellationToken cancellationToken = default)
        {
            await botClient.AnswerCallbackQueryAsync(callbackQueryId, text, showAlert, url, cacheTime, cancellationToken);
        }

        /// <summary>
        /// Создает параместры если option null.
        /// </summary>
        /// <param name="option">Параметры.</param>
        /// <returns>Экземпляр класса OptionMessage.</returns>
        private static OptionMessage CreateOptionsIfNull(OptionMessage option = null)
        {
            if (option == null)
                option = new OptionMessage();
            return option;
        }

        /// <summary>
        /// Получает меню из параметров сообщения.
        /// </summary>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Готовое меню или null.</returns>
        private static IReplyMarkup? GetReplyMarkup(OptionMessage option)
        {
            option = CreateOptionsIfNull(option);

            IReplyMarkup replyMarkup = null;
            if (option.ClearMenu)
                replyMarkup = new ReplyKeyboardRemove();
            else if (option.MenuReplyKeyboardMarkup != null)
                replyMarkup = option.MenuReplyKeyboardMarkup;
            else if (option.MenuInlineKeyboardMarkup != null)
                replyMarkup = option.MenuInlineKeyboardMarkup;

            return replyMarkup;
        }

        #endregion
    }
}
