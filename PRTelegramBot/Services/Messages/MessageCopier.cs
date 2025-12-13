using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services.Messages
{
    public class MessageCopier
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
        public static async Task<List<MessageId>> CopyMessages(IBotContext context, List<Message> messages, long chatId, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            List<MessageId> messageIds = new List<MessageId>();

            foreach (var message in messages)
                messageIds.Add(await CopyMessage(context, message, chatId, option));

            return messageIds;
        }

        /// <summary>
        /// Копировать сообщение.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Идентификатор сообщения.</returns>
        public static async Task<MessageId> CopyMessage(IBotContext context, Message message, long chatId, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            ChatId toMsg = new ChatId(chatId);
            ChatId fromMsg = new ChatId(message.Chat.Id);

            var messageId = await context.BotClient.CopyMessage(
                chatId: toMsg,
                fromChatId: fromMsg,
                messageId: message.MessageId,
                messageThreadId: option.MessageThreadId,
                caption: option.Caption,
                parseMode: option.ParseMode,
                captionEntities: option.Entities,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                replyMarkup: replyMarkup,
                cancellationToken: option.CancellationToken);
            return messageId;
        }

        #endregion
    }
}
