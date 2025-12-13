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
        #region Методы

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
        /// Редактировать inline меню вместе с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="media">Медиа.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
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
        /// Редактирование фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="photoPath">Путь к фото.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> EditPhoto(IBotContext context, long chatId, int messageId, string photoPath, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            if (!File.Exists(photoPath))
                return await MessageEditor.EditInline(context, chatId, messageId, option);

            using (var fileStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return await EditPhoto(context, chatId, messageId, fileStream, option: option);
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
