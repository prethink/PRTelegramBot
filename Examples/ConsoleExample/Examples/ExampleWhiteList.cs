using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Models.Enums;

namespace ConsoleExample.Examples
{
    internal class ExampleWhiteList
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Если включен белый список и в нем есть пользователи, отработает только для них.
        /// </summary>
        [ReplyMenuHandler("OnlyWhiteList")]
        public static async Task OnlyWhiteList(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(OnlyWhiteList);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Если выставлена настройка <see cref="WhiteListSettings.OnlyCommands"></see> и есть люди в белом списке, этот метод будет работать для всех.
        /// </summary>
        [WhiteListAnonymous]
        [ReplyMenuHandler("AllUsers")]
        public static async Task AllUsers(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(AllUsers);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
