using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Helpers
{
    public class Message
    {
        /// <summary>
        /// Максимальный размер сообщения.
        /// </summary>
        public const int MAX_MESSAGE_LENGTH = 4000;

        /// <summary>
        /// Разбивает большое сообщение на блоки
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="chunkSize">Размер блока</param>
        /// <returns>Коллекция сообщений</returns>
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
        /// Копирует сообщение
        /// </summary>
        /// <param name="botClient">Клиент telegram бота</param>
        /// <param name="message">Сообщение</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns>Идентификатор сообщения</returns>
        public static async Task<MessageId> CopyMessage(ITelegramBotClient botClient, Telegram.Bot.Types.Message message, long chatId)
        {
            ChatId toMsg = new ChatId(chatId);
            ChatId fromMsg = new ChatId(message.Chat.Id);
            var rMessage = await botClient.CopyMessageAsync(toMsg, fromMsg, message.MessageId);
            return rMessage;
        }

        /// <summary>
        /// Копирует сообщение
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="messages">Сообщения</param>
        /// <param name="chatId">Идентификатор чата</param>
        public static async Task CopyMessage(ITelegramBotClient botClient, List<Telegram.Bot.Types.Message> messages, long chatId)
        {
            ChatId toMsg = new ChatId(chatId);
            foreach (var message in messages)
            {
                ChatId fromMsg = new ChatId(message.Chat.Id);
                await botClient.CopyMessageAsync(toMsg, fromMsg, message.MessageId);
            }
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="update">Обновление телерграм</param>
        /// <param name="msg">Текст</param>
        /// <param name="option">Настройка сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> Send(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)

        {
            option = MessageUtils.CreateOptionsIfNull(option);

            var message = await Send(botClient, update.GetChatId(), msg, option);
            return message;
        }

        /// <summary>
        /// Сообщение ожидание обработки сообщения
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> AwaitAnswerBot(ITelegramBotClient botClient, long chatId, string message = "⏳ Генерирую ответ...", OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            var sentMessage = await botClient.SendTextMessageAsync(
             chatId: chatId,
             text: message,
             parseMode: option.ParseMode);

            return sentMessage;
        }


        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="option">Настройка сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> Send(ITelegramBotClient botClient, long chatId, string msg, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            Telegram.Bot.Types.Message message;
            if (string.IsNullOrWhiteSpace(msg))
                return null;

            var length = msg.Length;
            if (length > MAX_MESSAGE_LENGTH)
            {
                var chunk = SplitIntoChunks(msg, MAX_MESSAGE_LENGTH);
                int count = 0;
                foreach (var item in chunk)
                {
                    count++;
                    if (count < chunk.Count)
                        await Send(botClient, chatId, item);
                    if (count == chunk.Count)
                        msg = item;
                }
            }


            return await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: option.ParseMode,
                            replyMarkup: replyMarkup); ;
        }

        /// <summary>
        /// Отправка группы фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="filepaths">Путь к файлам</param>
        /// <returns>Коллекция сообщений</returns>
        public static async Task<Telegram.Bot.Types.Message[]> SendPhotoGroup(ITelegramBotClient botClient, long chatId, string msg, List<string> filepaths)
        {
            List<InputMediaPhoto> media = new();

            bool isFirst = true;
            int count = 0;
            foreach (var item in filepaths)
            {
                if (isFirst)
                {
                    if (string.IsNullOrWhiteSpace(msg))
                    {
                        media.Add(new InputMediaPhoto(InputFile.FromString(item)));
                        isFirst = false;
                    }
                    else
                    {
                        media.Add(new InputMediaPhoto(InputFile.FromString(item)) { Caption = msg, ParseMode = ParseMode.Html });
                        isFirst = false;
                    }

                }
                else
                {
                    media.Add(new InputMediaPhoto(InputFile.FromString(item)));
                }
                count++;

            }

            var messages = await botClient.SendMediaGroupAsync(chatId, media.ToArray());
            return messages;
        }


        /// <summary>
        /// Отправка сообщения с фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="option">Настройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(ITelegramBotClient botClient, long chatId, string msg, string filePath, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            if (!System.IO.File.Exists(filePath))
                return await Send(botClient, chatId, msg, option);

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await SendPhoto(botClient, chatId, msg, fileStream, option);
        }

        /// <summary>
        /// Отправка сообщения с фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="stream">Поток</param>
        /// <param name="option">Настройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(ITelegramBotClient botClient, long chatId, string msg, Stream stream, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            return await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: InputFile.FromStream(stream),
                                caption: msg,
                                parseMode: option.ParseMode,
                                replyMarkup: replyMarkup
                                );
        }

        /// <summary>
        /// Отправка сообщения с фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="url">url</param>
        /// <param name="option">Настройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> SendPhotoWithUrl(ITelegramBotClient botClient, long chatId, string msg, string url, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            return await botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: InputFile.FromString(url),
                            caption: msg,
                            parseMode: option.ParseMode,
                            replyMarkup: replyMarkup
                            );
        }

        /// <summary>
        /// Отправка сообщения с фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="url">url</param>
        /// <param name="option">Настройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> SendMediaWithUrl(ITelegramBotClient botClient, long chatId, string msg, string url, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);

            return await botClient.SendDocumentAsync(
                            chatId: chatId,
                            document: InputFile.FromString(url),
                            caption: msg,
                            parseMode: option.ParseMode,
                            replyMarkup: replyMarkup
                            );
        }

        /// <summary>
        /// Отправка файла
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="msg">Текст</param>
        /// <param name="filePath">Путь к файлу</param>
        public static async Task SendFile(ITelegramBotClient botClient, long chatId, string msg, string filePath)
        {

            if (!System.IO.File.Exists(filePath))
            {
                await Send(botClient, chatId, msg);
                return;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var file = await botClient.SendDocumentAsync(chatId: chatId,
                                                            document: InputFile.FromStream(fileStream, Path.GetFileName(filePath)),
                                                            caption: msg
                                                            );
            }
        }


        /// <summary>
        /// Редактирование сообщения
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <param name="msg">Текст</param>
        /// <param name="option">Настройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(ITelegramBotClient botClient, long chatId, int messageId, string msg, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option);
            var inlineMarkup = replyMarkup as InlineKeyboardMarkup;

            return await botClient.EditMessageTextAsync(
                        chatId: chatId,
                        messageId: messageId,
                        text: msg,
                        parseMode: option.ParseMode,
                        replyMarkup: inlineMarkup);
        }

        /// <summary>
        /// Редактирование сообщения
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="update">Обновление телеграм</param>
        /// <param name="msg">Текст</param>
        /// <param name="option">Настройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            long chatId = update.GetChatId();
            int messageId = update.GetMessageId();

            var editmessage = await Edit(botClient, chatId, messageId, msg, option);
            return editmessage;
        }

        /// <summary>
        /// Редактировать текст под фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <param name="msg">Текст</param>
        /// <param name="option">Насройки сообщения</param>
        /// <returns>Сообщщение</returns>
        public static async Task<Telegram.Bot.Types.Message> EditCaption(ITelegramBotClient botClient, long chatId, int messageId, string msg, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await botClient.EditMessageCaptionAsync(
                        chatId: chatId,
                        messageId: messageId,
                        caption: msg,
                        parseMode: option.ParseMode,
                        replyMarkup: replyMarkup);
        }

        /// <summary>
        /// Редактирование меню inline
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <param name="option">Насройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> EditInline(ITelegramBotClient botClient, long chatId, int messageId, OptionMessage option)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            Telegram.Bot.Types.Message message = null;
            if (option?.MenuInlineKeyboardMarkup != null)
            {
                message = await botClient.EditMessageReplyMarkupAsync(
                        chatId: chatId,
                        messageId: messageId,
                        replyMarkup: option.MenuInlineKeyboardMarkup);
            }

            return message;
        }

        /// <summary>
        /// Редактирование фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <param name="photoPath">Путь к фоото</param>
        /// <param name="option">Насройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(ITelegramBotClient botClient, long chatId, int messageId, string photoPath, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            Telegram.Bot.Types.Message message;
            if (!System.IO.File.Exists(photoPath))
                return await EditInline(botClient, chatId, messageId, option);


            using (var fileStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await EditPhoto(botClient, chatId, messageId, fileStream, option: option);
        }

        /// <summary>
        /// Редактирование 
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <param name="stream">Поток</param>
        /// <param name="option">Насройки сообщения</param>
        /// <returns>Сообщения</returns>
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(ITelegramBotClient botClient, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = GetReplyMarkup(option) as InlineKeyboardMarkup;

            return await botClient.EditMessageMediaAsync(
                        chatId: chatId,
                        media: new InputMediaPhoto(InputFile.FromStream(stream, filename)),
                        messageId: messageId,
                        replyMarkup: replyMarkup);
        }

        /// <summary>
        /// Удаляет сообщение
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <returns></returns>
        public static async Task DeleteChat(ITelegramBotClient botClient, long chatId, int messageId)
        {
            await botClient.DeleteMessageAsync(chatId: chatId, messageId: messageId);
        }

        /// <summary>
        /// Редактировать inline меню вместе с фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <param name="msg">Текст</param>
        /// <param name="media">Медиа</param>
        /// <param name="option">Насройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> EditWithPhoto(ITelegramBotClient botClient, long chatId, int messageId, string msg, InputMedia media, OptionMessage option)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            Telegram.Bot.Types.Message message = null;
            if (option?.MenuInlineKeyboardMarkup != null)
            {
                message = await botClient.EditMessageMediaAsync(
                        chatId: chatId,
                        messageId: messageId,
                        media: media,
                        replyMarkup: option.MenuInlineKeyboardMarkup);

                message = await botClient.EditMessageCaptionAsync(
                        chatId: chatId,
                        messageId: messageId,
                        caption: msg,
                        parseMode: option.ParseMode,
                        replyMarkup: option.MenuInlineKeyboardMarkup);
            }

            return message;
        }

        /// <summary>
        /// Редактирование текста под фото
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="update">Обновление телеграм</param>
        /// <param name="msg">Текст</param>
        /// <param name="option">Насройки сообщения</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> EditCaption(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            long chatId = update.GetChatId();
            int messageId = update.GetMessageId();

            var editmessage = await EditCaption(botClient, chatId, messageId, msg, option);
            return editmessage;
        }

        /// <summary>
        /// Вывод уведомления пользователю
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="callbackQueryId">Идентификатор callback</param>
        /// <param name="msg">Текст</param>
        /// <param name="showAlert">Показывать уведомление</param>
        public static async Task NotifyFromCallBack(ITelegramBotClient botClient, string callbackQueryId, string msg, bool showAlert = true)
        {
            await botClient.AnswerCallbackQueryAsync(callbackQueryId, msg, showAlert);
        }

        private static IReplyMarkup? GetReplyMarkup(OptionMessage option)
        {
            IReplyMarkup replyMarkup = null;
            if (option.ClearMenu)
                replyMarkup = new ReplyKeyboardRemove();
            else if (option.MenuReplyKeyboardMarkup != null)
                replyMarkup = option.MenuReplyKeyboardMarkup;
            else if (option.MenuInlineKeyboardMarkup != null)
                replyMarkup = option.MenuInlineKeyboardMarkup;

            return replyMarkup;
        }
    }
}
