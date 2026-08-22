using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;

namespace PRTelegramBot.Services.Messages
{
    /// <summary>
    /// Deletes messages.
    /// </summary>
    public class MessageDeleter
    {
        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="option">Message setting.</param>
        public static async Task DeleteMessage(IBotContext context, long chatId, int messageId, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            await context.BotClient.DeleteMessage(chatId, messageId, option.CancellationToken);
        }
    }
}
