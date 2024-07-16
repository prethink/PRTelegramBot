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
        /// Сначало будет отправлено сообщение 'Обработка данных...', после двух секунд старое сообщение будет удалено и сразу появится новое. 
        /// </summary>
        [ReplyMenuHandler("Awaiter message")]
        public static async Task AwaiterExample (ITelegramBotClient botClient, Update update)
        {
            using(var messageAwaiter = new MessageAwaiter(botClient, update.GetChatId(), "Обработка данных..."))
            {
                // Симуляция тяжелой операции.
                await Task.Delay(2000);
                await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Генерация данных завершена.");
            }
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "AutoDelete".
        /// Сообщение будет удалено по истечению 10 секунд.
        /// </summary>
        [ReplyMenuHandler("AutoDelete")]
        public static async Task AutoDelete(ITelegramBotClient botClient, Update update)
        {
            var message = await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Автоматическое удаление сообщения через 10 секунд");
            await message.AutoDeleteMessage(10, botClient, update);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "AutoEdit".
        /// Сообщение будет отредактировано по истечению 10 секунд.
        /// </summary>
        [ReplyMenuHandler("AutoEdit")]
        public static async Task AutoEdit(ITelegramBotClient botClient, Update update)
        {
            var message = await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Автоматическое редактирование сообщения через 10 секунд");
            await message.AutoEditMessage("Текст изменился.", 10, botClient, update);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "AutoEditCycle".
        /// Сообщение постепенно будет редактироваться.
        /// </summary>
        [ReplyMenuHandler("AutoEditCycle")]
        public static async Task AutoEditCycle(ITelegramBotClient botClient, Update update)
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
                "Все готово.",
            };
            var message = await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Автоматическое редактирование сообщения через 10 секунд");
            await message.AutoEditMessageСycle(messages, 1, botClient, update);
        }
    }
}
