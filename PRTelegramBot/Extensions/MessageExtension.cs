using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для работы с сообщением
    /// </summary>
    public static class MessageExtension
    {
        /// <summary>
        /// Автоматическое удаление сообщение через определенное время.
        /// </summary>
        /// <param name="message">Сообщение которое нужно удалить.</param>
        /// <param name="seconds">Через сколько секунд будет удалено сообщение.</param>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Update.</param>
        public static async Task AutoDeleteMessage(this Message message, int seconds, ITelegramBotClient botClient, Update update)
        {
            if(message == null)
                return;

            _ = Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                await botClient.DeleteMessageAsync(update.GetChatIdClass(), message.MessageId);
            });
        }

        /// <summary>
        /// Автоматическое редактирования сообщения через определенное время.
        /// </summary>
        /// <param name="message">Сообщение которое нужно удалить.</param>
        /// <param name="seconds">Через сколько секунд будет удалено сообщение.</param>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Update.</param>
        public static async Task AutoEditMessage(this Message message, string messageText, int seconds, ITelegramBotClient botClient, Update update)
        {
            if (message == null)
                return;

            _ = Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                await botClient.EditMessageTextAsync(update.GetChatIdClass(), message.MessageId, messageText);
            });
        }

        /// <summary>
        /// Автоматическое редактирования сообщения через определенное время в цикле.
        /// </summary>
        /// <param name="message">Сообщение которое нужно удалить.</param>
        /// <param name="seconds">Через сколько секунд будет удалено сообщение.</param>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Update.</param>
        public static async Task AutoEditMessageСycle(this Message message, List<string> messageTexts, int seconds, ITelegramBotClient botClient, Update update)
        {
            if (message == null)
                return;

            _ = Task.Run(async () =>
            {
                foreach (var text in messageTexts) 
                {
                    await Task.Delay(seconds * 1000);
                    await botClient.EditMessageTextAsync(update.GetChatIdClass(), message.MessageId, text);
                }
            });
        }
    }
}
