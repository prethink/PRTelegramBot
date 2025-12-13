using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Services.Messages
{
    public class MessageEditor
    {
        #region Методы

        /// <summary>
        /// Редактирование меню inline.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditInline(IBotContext context, long chatId, int messageId, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup;

            Telegram.Bot.Types.Message message = null;
            if (option?.MenuInlineKeyboardMarkup is not null)
            {
                message = await context.BotClient.EditMessageReplyMarkup(
                    chatId: chatId,
                    messageId: messageId,
                    replyMarkup: replyMarkup,
                    cancellationToken: option.CancellationToken);
            }

            return message;
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
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, long chatId, int messageId, string text, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup;
            var linkOptions = MessageUtils.CreateLinkPreviewOptionsFromOption(option);
            return await context.BotClient.EditMessageText(
                chatId: chatId,
                messageId: messageId,
                text: text,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                entities: option.Entities,
                linkPreviewOptions: linkOptions,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, string text, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            long chatId = context.GetChatId();
            int messageId = context.GetMessageId();

            var editMessage = await Edit(context, chatId, messageId, text, option);
            return editMessage;
        }

        #endregion
    }
}
