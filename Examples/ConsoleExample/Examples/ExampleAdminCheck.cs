using ConsoleExample.Attributes;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;

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
        public static async Task AdminExample(IBotContext context)
        {
            bool isAdminUpdate = await context.IsAdmin();
            bool isAdminById = await context.IsAdmin(context.Update.GetChatId()) ;
            await PRTelegramBot.Helpers.Message.Send(context, $"Вы администратор бота: {isAdminById} {isAdminUpdate}");
        }


        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Только админы".
        /// Пример работы кастомного чекера и кастомного атрибута.
        /// </summary>
        [AdminOnlyExample]
        [ReplyMenuHandler("Только админы")]
        public static async Task AdminOnlyExample(IBotContext context)
        {
            bool isAdminUpdate = await context.IsAdmin();
            bool isAdminById = await context.IsAdmin(context.Update.GetChatId());
            await PRTelegramBot.Helpers.Message.Send(context, $"Вы администратор бота: {isAdminById} {isAdminUpdate}");
        }
    }
}
