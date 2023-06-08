using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static PRTelegramBot.Core.TelegramService;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;

namespace PRTelegramBot.Helpers
{
    public class Message
    {
        /// <summary>
        /// Максимальный размер сообщения
        /// </summary>
        public const int MAX_MESSAGE_LENGTH = 4000;

        /// <summary>
        /// Разбивает большое сообщение на блоки
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="chunkSize">Размер блока</param>
        /// <returns>Коллекция сообщений</returns>
        static IList<string> SplitIntoChunks(string text, int chunkSize)
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
        /// Разбивает большое сообщение на блоки
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="chunkSize">Размер блока</param>
        /// <returns>Коллекция сообщений</returns>
        static IList<string> SplitIntoChunks(string text)
        {
            List<string> chunks = new List<string>();
            int offset = 0;
            while (offset < text.Length)
            {
                int size = Math.Min(MAX_MESSAGE_LENGTH, text.Length - offset);
                chunks.Add(text.Substring(offset, size));
                offset += size;
            }
            return chunks;
        }

        /// <summary>
        /// Копирует сообщение
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="message">Сообщение</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns>Идентификатор сообщения</returns>
        public static async Task<MessageId> CopyMessage(ITelegramBotClient botClient, Telegram.Bot.Types.Message message, long chatId)
        {
            try
            {
                ChatId toMsg = new ChatId(chatId);
                ChatId fromMsg = new ChatId(message.Chat.Id);
                var rMessage = await botClient.CopyMessageAsync(toMsg, fromMsg, message.MessageId);
                return rMessage;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Копирует сообщение
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="messages">Сообщения</param>
        /// <param name="chatId">Идентификатор чата</param>
        public static async Task CopyMessage(ITelegramBotClient botClient, List<Telegram.Bot.Types.Message> messages, long chatId)
        {
            try
            {
                ChatId toMsg = new ChatId(chatId);
                foreach (var message in messages)
                {

                    try
                    {
                        ChatId fromMsg = new ChatId(message.Chat.Id);
                        await botClient.CopyMessageAsync(toMsg, fromMsg, message.MessageId);
                    }
                    catch (Exception ex)
                    {
                        GetInstance().InvokeErrorLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
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
            var message = await Send(botClient, update.GetChatId(), msg, option);
            return message;
        }

        /// <summary>
        /// Сообщение ожидание обработки сообщения
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns>Сообщение</returns>
        public static async Task<Telegram.Bot.Types.Message> AwaitAnswerBot(ITelegramBotClient botClient, long chatId, string message = "")
        {
            string msg = "⏳ Генерирую ответ...";
            if(!string.IsNullOrEmpty(message))
            {
                msg = message;
            }
            var sentMessage = await botClient.SendTextMessageAsync(
             chatId: chatId,
             text: msg,
             parseMode: ParseMode.Html);

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

            Telegram.Bot.Types.Message message;
            if (string.IsNullOrWhiteSpace(msg))
            {
                return null;
            }

            var length = msg.Length;
            if (length > MAX_MESSAGE_LENGTH)
            {
                var chunk = SplitIntoChunks(msg, MAX_MESSAGE_LENGTH);
                int count = 0;
                foreach (var item in chunk)
                {
                    count++;
                    if (count < chunk.Count)
                    {
                        await Send(botClient, chatId, item);
                    }
                    if (count == chunk.Count)
                    {
                        msg = item;
                    }
                }
            }

            if (option == null)
            {
                message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: msg,
                        parseMode: ParseMode.Html);
            }
            else
            {
                if (option.ClearMenu)
                {
                    message = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: new ReplyKeyboardRemove());
                }
                else if (option.MenuReplyKeyboardMarkup != null)
                {
                    message = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuReplyKeyboardMarkup);
                }
                else if (option.MenuInlineKeyboardMarkup != null)
                {
                    message = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuInlineKeyboardMarkup);
                }
                else
                {
                    message = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: ParseMode.Html);
                }
            }
            return message;
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
            if (!System.IO.File.Exists(filePath))
            {
                return await Send(botClient, chatId, msg, option);
            }

            Telegram.Bot.Types.Message message;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return await SendPhoto(botClient, chatId, msg, fileStream, option);
            }
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


            Telegram.Bot.Types.Message message;

            if (option == null)
            {
                message = await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: InputFile.FromStream(stream),
                                caption: msg,
                                parseMode: ParseMode.Html
                                );
                return message;
            }
            else
            {
                if (option.MenuReplyKeyboardMarkup != null)
                {
                    message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: InputFile.FromStream(stream),
                                    caption: msg,
                                    parseMode: ParseMode.Html,
                                    replyMarkup: option.MenuReplyKeyboardMarkup
                                    );
                    return message;
                }
                else if (option.MenuInlineKeyboardMarkup != null)
                {
                    message = await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: InputFile.FromStream(stream),
                                caption: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuInlineKeyboardMarkup
                                );
                    return message;
                }
                return null;
            }
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

            Telegram.Bot.Types.Message message = null;
            if (option == null)
            {
                message = await botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: InputFile.FromString(url),
                            caption: msg,
                            parseMode: ParseMode.Html
                            );
            }
            else
            {
                if (option.MenuReplyKeyboardMarkup != null)
                {
                    message = await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: InputFile.FromString(url),
                                caption: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuReplyKeyboardMarkup
                                );
                }
                else if (option.MenuInlineKeyboardMarkup != null)
                {
                    message = await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: InputFile.FromString(url),
                                caption: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuInlineKeyboardMarkup
                                );
                }
                else
                {
                    message = await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: InputFile.FromString(url),
                                caption: msg,
                                parseMode: ParseMode.Html
                                );
                }
            }
            return message;




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

            Telegram.Bot.Types.Message message = null;
            if (option == null)
            {
                message = await botClient.SendDocumentAsync(
                            chatId: chatId,
                            document: InputFile.FromString(url),
                            caption: msg,
                            parseMode: ParseMode.Html
                            );
            }
            else
            {
                if (option.MenuReplyKeyboardMarkup != null)
                {
                    message = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromString(url),
                                caption: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuReplyKeyboardMarkup
                                );
                }
                else if (option.MenuInlineKeyboardMarkup != null)
                {
                    message = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromString(url),
                                caption: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuInlineKeyboardMarkup
                                );
                }
                else
                {
                    message = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromString(url),
                                caption: msg,
                                parseMode: ParseMode.Html
                                );
                }
            }
            return message;




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
            try
            {
                Telegram.Bot.Types.Message message;
                if (string.IsNullOrWhiteSpace(msg))
                {
                    return null;
                }

                if (option == null || option.MenuInlineKeyboardMarkup == null)
                {
                    message = await botClient.EditMessageTextAsync(
                            chatId: chatId,
                            messageId: messageId,
                            text: msg,
                            parseMode: ParseMode.Html);
                }
                else
                {
                    message = await botClient.EditMessageTextAsync(
                            chatId: chatId,
                            messageId: messageId,
                            text: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuInlineKeyboardMarkup);

                }

                return message;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }

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
            try
            {
                long chatId = update.GetChatId();
                int messageId = update.GetMessageId();

                var editmessage = await Edit(botClient, chatId, messageId, msg, option);
                return editmessage;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
            try
            {
                Telegram.Bot.Types.Message message;
                if (string.IsNullOrWhiteSpace(msg))
                {
                    return null;
                }

                if (option == null || option.MenuInlineKeyboardMarkup == null)
                {
                    message = await botClient.EditMessageCaptionAsync(
                            chatId: chatId,
                            messageId: messageId,
                            caption: msg,
                            parseMode: ParseMode.Html);
                }
                else
                {
                    message = await botClient.EditMessageCaptionAsync(
                            chatId: chatId,
                            messageId: messageId,
                            caption: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuInlineKeyboardMarkup);

                }

                return message;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
            try
            {
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
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
            try
            {
                Telegram.Bot.Types.Message message;
                if (!System.IO.File.Exists(photoPath))
                {
                    return await EditInline(botClient, chatId, messageId, option);
                }


                using (var fileStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return await EditPhoto(botClient, chatId, messageId, fileStream, option);
                }
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(ITelegramBotClient botClient, long chatId, int messageId, Stream stream, OptionMessage option = null)
        {
            try
            {
                Telegram.Bot.Types.Message message;

                if (option?.MenuInlineKeyboardMarkup != null)
                {
                    message = await botClient.EditMessageMediaAsync(
                            chatId: chatId,
                            media: new InputMediaPhoto(InputFile.FromStream(stream, "book")),
                            messageId: messageId,
                            replyMarkup: option.MenuInlineKeyboardMarkup);
                }
                else
                {
                    message = await botClient.EditMessageMediaAsync(
                            chatId: chatId,
                            media: new InputMediaPhoto(InputFile.FromStream(stream, "book")),
                            messageId: messageId);
                }

                return message;

            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
            try
            {
                await botClient.DeleteMessageAsync(chatId: chatId, messageId: messageId);
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
            }
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
            try
            {
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
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuInlineKeyboardMarkup);
                }

                return message;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
            try
            {
                long chatId = update.GetChatId();
                int messageId = update.GetMessageId();

                var editmessage = await EditCaption(botClient, chatId, messageId, msg, option);
                return editmessage;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
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
            try
            {
                await botClient.AnswerCallbackQueryAsync(callbackQueryId, msg, showAlert);
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
            }
        }
    }
}
