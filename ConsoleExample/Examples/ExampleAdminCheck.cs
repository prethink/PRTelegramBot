using ConsoleExample.Attributes;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples
{
    public class ExampleAdminCheck
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Админ".
        /// Проверка текущего пользователя на привилегии администратора.
        /// </summary>
        [ReplyMenuHandler("Админ")]
        public static async Task AdminExample(ITelegramBotClient botClient, Update update)
        {
            bool isAdminUpdate = await botClient.IsAdmin(update);
            bool isAdminById = await botClient.IsAdmin(update.GetChatId()) ;
            await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Вы администратор бота: {isAdminById} {isAdminUpdate}");
        }


        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Только админы".
        /// Пример работы кастомного чекера и кастомного атрибута.
        /// </summary>
        [AdminOnlyExample]
        [ReplyMenuHandler("Только админы")]
        public static async Task AdminOnlyExample(ITelegramBotClient botClient, Update update)
        {
            bool isAdminUpdate = await botClient.IsAdmin(update);
            bool isAdminById = await botClient.IsAdmin(update.GetChatId());
            await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Вы администратор бота: {isAdminById} {isAdminUpdate}");
        }
    }
}
