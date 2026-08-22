using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for working with a message
    /// </summary>
    public static class MessageExtension
    {
        /// <summary>
        /// Automatically deletes the message after a given delay.
        /// </summary>
        /// <param name="message">The message to delete.</param>
        /// <param name="seconds">Number of seconds after which the message will be deleted.</param>
        /// <param name="context">Bot context.</param>
        public static void AutoDeleteMessage(this Message message, int seconds, IBotContext context)
        {
            if(message is null)
                return;

            _ = Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                await context.BotClient.DeleteMessage(context.Update.GetChatIdClass(), message.MessageId);
            });
        }

        /// <summary>
        /// Automatically edits the message after a given delay.
        /// </summary>
        /// <param name="message">The message to delete.</param>
        /// <param name="messageText">Message text.</param>
        /// <param name="seconds">Number of seconds after which the message will be deleted.</param>
        /// <param name="context">Bot context.</param>
        public static void AutoEditMessage(this Message message, string messageText, int seconds, IBotContext context)
        {
            if (message is null)
                return;

            _ = Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                await context.BotClient.EditMessageText(context.Update.GetChatIdClass(), message.MessageId, messageText);
            });
        }

        /// <summary>
        /// Automatically edits the message after a given delay, in a loop.
        /// </summary>
        /// <param name="message">The message to delete.</param>
        /// <param name="messageTexts">Collection of message texts.</param>
        /// <param name="seconds">Number of seconds after which the message will be deleted.</param>
        /// <param name="context">Bot context.</param>
        public static void AutoEditMessageCycle(this Message message, List<string> messageTexts, int seconds, IBotContext context)
        {
            if (message is null)
                return;

            _ = Task.Run(async () =>
            {
                foreach (var text in messageTexts) 
                {
                    await Task.Delay(seconds * 1000);
                    await context.BotClient.EditMessageText(context.Update.GetChatIdClass(), message.MessageId, text);
                }
            });
        }
    }
}
