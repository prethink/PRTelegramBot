using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples
{
    public class ExampleAdminCheck
    {
        /// <summary>
        /// Пример проверки пользователя на администратора
        /// </summary>
        [ReplyMenuHandler("Admin")]
        public static async Task AdminExample(ITelegramBotClient botClient, Update update)
        {
            bool isAdminUpdate = botClient.IsAdmin(update);
            bool isAdminById = botClient.IsAdmin(update.GetChatId()) ;
            await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Вы администратор бота: {isAdminById} {isAdminUpdate}");
        }
    }
}
