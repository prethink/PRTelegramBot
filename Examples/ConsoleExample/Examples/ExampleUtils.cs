using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;

namespace ConsoleExample.Examples
{
    internal class ExampleUtils
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Awaiter message" is sent to the chat.
        /// First the message 'Processing data...' is sent; after two seconds the old message is deleted and a new one appears at once. 
        /// </summary>
        [ReplyMenuHandler("Awaiter message")]
        public static async Task AwaiterExample (IBotContext context)
        {
            using(var messageAwaiter = new MessageAwaiter(context, "Processing data..."))
            {
                // Simulate a heavy operation.
                await Task.Delay(2000);
                await MessageSender.Send(context, $"Data generation finished.");
            }
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "AutoDelete" is sent to the chat.
        /// The message is deleted after 10 seconds.
        /// </summary>
        [ReplyMenuHandler("AutoDelete")]
        public static async Task AutoDelete(IBotContext context)
        {
            var message = await MessageSender.Send(context, $"The message will be deleted automatically in 10 seconds");
            message.AutoDeleteMessage(10, context);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "AutoEdit" is sent to the chat.
        /// The message is edited after 10 seconds.
        /// </summary>
        [ReplyMenuHandler("AutoEdit")]
        public static async Task AutoEdit(IBotContext context)
        {
            var message = await MessageSender.Send(context, $"The message will be edited automatically in 10 seconds");
            message.AutoEditMessage("The text has changed.", 10, context);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "AutoEditCycle" is sent to the chat.
        /// The message is edited gradually.
        /// </summary>
        [ReplyMenuHandler("AutoEditCycle")]
        public static async Task AutoEditCycle(IBotContext context)
        {
            var messages = new List<string>()
            {
                "10",
                "9",
                "8",
                "7",
                "6",
                "5",
                "4",
                "3",
                "2",
                "1",
                "All done.",
            };
            var message = await MessageSender.Send(context, $"The message will be edited automatically in 10 seconds");
            message.AutoEditMessageCycle(messages, 1, context);
        }
    }
}
