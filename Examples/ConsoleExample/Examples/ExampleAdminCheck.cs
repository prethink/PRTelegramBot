using ConsoleExample.Attributes;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples
{
    public class ExampleAdminCheck
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Admin" is sent to the chat.
        /// Checks whether the current user has administrator privileges.
        /// </summary>
        [ReplyMenuHandler("Admin")]
        public static async Task AdminExample(IBotContext context)
        {
            bool isAdminUpdate = await context.IsAdmin();
            bool isAdminById = await context.IsAdmin(context.Update.GetChatId()) ;
            await MessageSender.Send(context, $"You are an administrator of the bot: {isAdminById} {isAdminUpdate}");
        }


        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Admins only" is sent to the chat.
        /// Example of a custom checker and a custom attribute in action.
        /// </summary>
        [AdminOnlyExample]
        [ReplyMenuHandler("Admins only")]
        public static async Task AdminOnlyExample(IBotContext context)
        {
            bool isAdminUpdate = await context.IsAdmin();
            bool isAdminById = await context.IsAdmin(context.Update.GetChatId());
            await MessageSender.Send(context, $"You are an administrator of the bot: {isAdminById} {isAdminUpdate}");
        }
    }
}
