using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Utils;

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
        public static async Task AwaiterExample (IBotContext context)
        {
            using(var messageAwaiter = new MessageAwaiter(context, "Обработка данных..."))
            {
                // Симуляция тяжелой операции.
                await Task.Delay(2000);
                await PRTelegramBot.Helpers.Message.Send(context, $"Генерация данных завершена.");
            }
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "AutoDelete".
        /// Сообщение будет удалено по истечению 10 секунд.
        /// </summary>
        [ReplyMenuHandler("AutoDelete")]
        public static async Task AutoDelete(IBotContext context)
        {
            var message = await PRTelegramBot.Helpers.Message.Send(context, $"Автоматическое удаление сообщения через 10 секунд");
            message.AutoDeleteMessage(10, context);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "AutoEdit".
        /// Сообщение будет отредактировано по истечению 10 секунд.
        /// </summary>
        [ReplyMenuHandler("AutoEdit")]
        public static async Task AutoEdit(IBotContext context)
        {
            var message = await PRTelegramBot.Helpers.Message.Send(context, $"Автоматическое редактирование сообщения через 10 секунд");
            message.AutoEditMessage("Текст изменился.", 10, context);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "AutoEditCycle".
        /// Сообщение постепенно будет редактироваться.
        /// </summary>
        [ReplyMenuHandler("AutoEditCycle")]
        public static async Task AutoEditCycle(IBotContext context)
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
            var message = await PRTelegramBot.Helpers.Message.Send(context, $"Автоматическое редактирование сообщения через 10 секунд");
            message.AutoEditMessageСycle(messages, 1, context);
        }
    }
}
