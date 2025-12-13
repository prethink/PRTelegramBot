using PRTelegramBot.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils
{
    public static class MessageUtils
    {
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
        /// Создает параметры если option null.
        /// </summary>
        /// <param name="option">Параметры.</param>
        /// <returns>Экземпляр класса OptionMessage.</returns>
        public static OptionMessage CreateOptionsIfNull(OptionMessage option = null)
        {
            if (option is null)
                option = new OptionMessage();
            return option;
        }

        /// <summary>
        /// Создаёт объект <see cref="ReplyParameters"/> на основе переданных опций <see cref="OptionMessage"/>.
        /// </summary>
        /// <param name="option">Опции сообщения, из которых извлекаются параметры ответа.</param>
        /// <returns>Экземпляр <see cref="ReplyParameters"/> с заполненными полями <see cref="ReplyParameters.MessageId"/> и <see cref="ReplyParameters.AllowSendingWithoutReply"/>.</returns>
        public static ReplyParameters CreateReplyParametersFromOptions(OptionMessage option)
        {
            ReplyParameters parameters = new ReplyParameters();
            if (option.ReplyToMessageId is not null)
                parameters.MessageId = option.ReplyToMessageId.Value;
            parameters.AllowSendingWithoutReply = option.AllowSendingWithoutReply;

            return parameters;
        }

        /// <summary>
        /// Создаёт объект <see cref="LinkPreviewOptions"/> на основе переданных опций <see cref="OptionMessage"/>.
        /// </summary>
        /// <param name="option">Опции сообщения, из которых извлекается настройка предпросмотра ссылки.</param>
        /// <returns>Экземпляр <see cref="LinkPreviewOptions"/> с заполненным свойством <see cref="LinkPreviewOptions.IsDisabled"/>.</returns>
        public static LinkPreviewOptions CreateLinkPreviewOptionsFromOption(OptionMessage option)
        {
            LinkPreviewOptions linkOptions = new LinkPreviewOptions();
            linkOptions.IsDisabled = option.DisableWebPagePreview;
            return linkOptions;
        }

        /// <summary>
        /// Получает меню из параметров сообщения.
        /// </summary>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Готовое меню или null.</returns>
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
