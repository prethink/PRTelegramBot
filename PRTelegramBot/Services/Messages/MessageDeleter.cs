using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;

namespace PRTelegramBot.Services.Messages
{
    public class MessageDeleter
    {
        /// <summary>
        /// Удалить сообщение.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="option">Настройка сообщения.</param>
        public static async Task DeleteMessage(IBotContext context, long chatId, int messageId, OptionMessage option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            await context.BotClient.DeleteMessage(chatId, messageId, option.CancellationToken);
        }
    }
}
