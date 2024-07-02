using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples
{
    internal class ExampleUtils
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Awaiter message".
        /// Сначало будет отправлено сообщение о генерации данных, после двух секунд старое сообщение будет удалено и сразу появится новое. 
        /// </summary>
        [ReplyMenuHandler("Awaiter message")]
        public static async Task AdminExample(ITelegramBotClient botClient, Update update)
        {
            using(var messageAwaiter = new MessageAwaiter(botClient, update.GetChatId()))
            {
                await Task.Delay(2000);
                await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Генерация данных завершена.");
            }
            
        }
    }
}
