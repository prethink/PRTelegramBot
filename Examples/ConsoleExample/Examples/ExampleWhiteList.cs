using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples
{
    internal class ExampleWhiteList
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// If the white list is enabled and holds users, this runs only for them.
        /// </summary>
        [ReplyMenuHandler("OnlyWhiteList")]
        public static async Task OnlyWhiteList(IBotContext context)
        {
            string msg = nameof(OnlyWhiteList);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// If the <see cref="WhiteListSettings.OnlyCommands"></see> setting is on and the white list holds people, this method works for everyone.
        /// </summary>
        [WhiteListAnonymous]
        [ReplyMenuHandler("AllUsers")]
        public static async Task AllUsers(IBotContext context)
        {
            string msg = nameof(AllUsers);
            await MessageSender.Send(context, msg);
        }
    }
}
