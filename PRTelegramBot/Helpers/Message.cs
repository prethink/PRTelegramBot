using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Media;
using PRTelegramBot.Services.Messages;
using Telegram.Bot.Types;

namespace PRTelegramBot.Helpers
{
    [Obsolete($"Класс устарел. Смотрите в сторону {nameof(MessageSender)}, {nameof(MessageEditor)}, {nameof(MessageDeleter)}, {nameof(MediaEditor)}, {nameof(MediaSender)}")]
    public class Message
    {
        #region Методы

        /// <summary>
        /// Копирует коллекцию сообщений.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="messages">Сообщения.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Коллекция идентификаторов сообщений.</returns>
        [Obsolete($"Используйте {nameof(MessageCopier)}.{nameof(MessageCopier.CopyMessages)}")]
        public static async Task<List<MessageId>> CopyMessages(IBotContext context, List<Telegram.Bot.Types.Message> messages, long chatId, OptionMessage option = null)
        {
            return await MessageCopier.CopyMessages(context, messages, chatId, option);
        }

        /// <summary>
        /// Копировать сообщение.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Идентификатор сообщения.</returns>
        [Obsolete($"Используйте {nameof(MessageCopier)}.{nameof(MessageCopier.CopyMessage)}")]
        public static async Task<MessageId> CopyMessage(IBotContext context, Telegram.Bot.Types.Message message, long chatId, OptionMessage option = null)
        {
            return await MessageCopier.CopyMessage(context, message, chatId, option);
        }

        /// <summary>
        /// Сообщение ожидание обработки сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageSender)}.{nameof(MessageSender.AwaitAnswerBot)}")]
        public static async Task<Telegram.Bot.Types.Message> AwaitAnswerBot(IBotContext context, long chatId, string message = "⏳ Генерирую ответ...", OptionMessage option = null)
        {
            return await MessageSender.AwaitAnswerBot(context, chatId, message, option);
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="update">Обновление телерграм.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageSender)}.{nameof(MessageSender.Send)}")]
        public static async Task<Telegram.Bot.Types.Message> Send(IBotContext context, Update update, string text, OptionMessage option = null)
        {
            return await MessageSender.Send(context, update, text, option);
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageSender)}.{nameof(MessageSender.Send)}")]
        public static async Task<Telegram.Bot.Types.Message> Send(IBotContext context, string text, OptionMessage option = null)
        {
            return await MessageSender.Send(context, text, option);
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageSender)}.{nameof(MessageSender.Send)}")]
        public static async Task<Telegram.Bot.Types.Message> Send(IBotContext context, long chatId, string text, OptionMessage option = null)
        {
            return await MessageSender.Send(context, chatId, text, option);
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
        [Obsolete($"Используйте {nameof(MediaSender)}.{nameof(MediaSender.SendPhotoGroup)}")]
        public static async Task<Telegram.Bot.Types.Message[]> SendPhotoGroup(IBotContext context, long chatId, string text, List<string> filepaths, OptionMessage option = null)
        {
            return await MediaSender.SendPhotoGroup(context, chatId, text, filepaths, option);
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
        [Obsolete($"Используйте {nameof(MediaSender)}.{nameof(MediaSender.SendPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(IBotContext context, long chatId, string text, string filePath, OptionMessage option = null)
        {
            return await MediaSender.SendPhoto(context, chatId, text, filePath, option);
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
        [Obsolete($"Используйте {nameof(MediaSender)}.{nameof(MediaSender.SendFile)}")]
        public static async Task<Telegram.Bot.Types.Message> SendFile(IBotContext context, long chatId, string text, string filePath, OptionMessage option = null)
        {
            return await MediaSender.SendFile(context, chatId, text, filePath, option);
        }

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageEditor)}.{nameof(MessageEditor.Edit)}")]
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, long chatId, int messageId, string text, OptionMessage option = null)
        {
            return await MessageEditor.Edit(context, chatId, messageId, text, option);
        }

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageEditor)}.{nameof(MessageEditor.Edit)}")]
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, string text, OptionMessage option = null)
        {
            return await MessageEditor.Edit(context, text, option);
        }

        /// <summary>
        /// Редактировать текст под фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MediaEditor)}.{nameof(MediaEditor.EditCaption)}")]
        public static async Task<Telegram.Bot.Types.Message> EditCaption(IBotContext context, long chatId, int messageId, string text, OptionMessage option = null)
        {
            return await MediaEditor.EditCaption(context, chatId, messageId, text, option);
        }

        /// <summary>
        /// Редактирование фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="photoPath">Путь к фото.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MediaEditor)}.{nameof(MediaEditor.EditPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, string photoPath, OptionMessage option = null)
        {
            return await MediaEditor.EditPhoto(context, chatId, messageId, photoPath, option);
        }

        /// <summary>
        /// Удалить сообщение.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="option">Настройка сообщения.</param>
        [Obsolete($"Используйте {nameof(MessageDeleter)}.{nameof(MessageDeleter.DeleteMessage)}")]
        public static async Task DeleteMessage(IBotContext context, long chatId, int messageId, OptionMessage option = null)
        {
            await MessageDeleter.DeleteMessage(context, chatId, messageId, option);
        }

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MediaSender)}.{nameof(MediaSender.SendPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(IBotContext context, long chatId, string text, Stream stream, OptionMessage option = null)
        {
            return await MediaSender.SendPhoto(context, chatId, text, stream, option);
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
        [Obsolete($"Используйте {nameof(MediaSender)}.{nameof(MediaSender.SendPhotoWithUrl)}")]
        public static async Task<Telegram.Bot.Types.Message> SendPhotoWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage option = null)
        {
            return await MediaSender.SendPhotoWithUrl(context, chatId, msg, url, option);
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
        [Obsolete($"Используйте {nameof(MediaSender)}.{nameof(MediaSender.SendMediaWithUrl)}")]
        public static async Task<Telegram.Bot.Types.Message> SendMediaWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage option = null)
        {
            return await MediaSender.SendMediaWithUrl(context, chatId, msg, url, option);
        }

        /// <summary>
        /// Редактирование меню inline.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MessageEditor)}.{nameof(MessageEditor.EditInline)}")]
        public static async Task<Telegram.Bot.Types.Message> EditInline(IBotContext context, long chatId, int messageId, OptionMessage option = null)
        {
            return await MessageEditor.EditInline(context, chatId, messageId, option);  
        }

        /// <summary>
        /// Редактирование фото. 
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="filename">Название файла.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MediaEditor)}.{nameof(MediaEditor.EditPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage option = null)
        {
            return await MediaEditor.EditPhoto(context, chatId, messageId, stream, filename, option);
        }

        /// <summary>
        /// Редактировать inline меню вместе с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="media">Медиа.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        [Obsolete($"Используйте {nameof(MediaEditor)}.{nameof(MediaEditor.EditWithPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> EditWithPhoto(IBotContext context, long chatId, int messageId, string text, InputMedia media, OptionMessage option = null)
        {
            return await MediaEditor.EditWithPhoto(context, chatId, messageId, text, media, option);
        }


        /// <summary>
        /// Вывод уведомления пользователю.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="callbackQueryId">Идентификатор callback.</param>
        /// <param name="text">Текст.</param>
        /// <param name="showAlert">Показывать уведомление.</param>
        /// <param name="url">.</param>
        /// <param name="cacheTime">.</param>
        /// <returns>Task</returns>
        [Obsolete($"Используйте {nameof(MessageNotification)}.{nameof(MessageNotification.NotifyFromCallBack)}")]
        public static async Task NotifyFromCallBack(
            IBotContext context,
            string callbackQueryId,
            string text,
            bool showAlert = true,
            string? url = null,
            int? cacheTime = null)
        {
            await MessageNotification.NotifyFromCallBack(context, callbackQueryId, text, showAlert, url, cacheTime);
        }

        #endregion
    }
}
