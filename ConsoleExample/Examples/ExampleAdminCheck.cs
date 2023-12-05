using PRTelegramBot.Attributes;
using PRTelegramBot.Commands.Constants;
using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;

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
