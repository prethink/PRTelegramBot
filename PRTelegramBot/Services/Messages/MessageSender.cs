using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services.Messages
{
    public class MessageSender
    {
        #region Методы

        /// <summary>
        /// Сообщение ожидание обработки сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> AwaitAnswerBot(IBotContext context, long chatId, string message = "⏳ Генерирую ответ...", OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var sentMessage = await Send(context, chatId, message, option);
            return sentMessage;
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="update">Обновление телерграм.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> Send(IBotContext context, Update update, string text, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            var message = await Send(context, update.GetChatId(), text, option);
            return message;
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> Send(IBotContext context, string text, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            var message = await Send(context, context.Update.GetChatId(), text, option);
            return message;
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> Send(IBotContext context, long chatId, string text, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            var linkOptions = MessageUtils.CreateLinkPreviewOptionsFromOption(option);

            if (text.Length > PRConstants.MAX_MESSAGE_LENGTH)
            {
                var chunk = MessageUtils.SplitIntoChunks(text, PRConstants.MAX_MESSAGE_LENGTH);
                int count = 0;
                foreach (var item in chunk)
                {
                    count++;
                    if (count < chunk.Count)
                        await Send(context, chatId, item, option);
                    if (count == chunk.Count)
                        text = item;
                }
            }

            return await context.BotClient.SendMessage(
                chatId: chatId,
                text: text,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                messageThreadId: option.MessageThreadId,
                entities: option.Entities,
                linkPreviewOptions: linkOptions,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                cancellationToken: option.CancellationToken);
        }

        #endregion
    }
}
