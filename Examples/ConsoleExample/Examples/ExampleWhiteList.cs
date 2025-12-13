using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples
{
    internal class ExampleWhiteList
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Если включен белый список и в нем есть пользователи, отработает только для них.
        /// </summary>
        [ReplyMenuHandler("OnlyWhiteList")]
        public static async Task OnlyWhiteList(IBotContext context)
        {
            string msg = nameof(OnlyWhiteList);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Если выставлена настройка <see cref="WhiteListSettings.OnlyCommands"></see> и есть люди в белом списке, этот метод будет работать для всех.
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
