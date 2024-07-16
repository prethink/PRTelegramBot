using PRTelegramBot.Attributes;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleSlashCommands
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/example".
        /// </summary>
        [SlashHandler("/example")]
        public static async Task ExampleSlashCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Команда /example";
            msg += "\n /get_1 - команда 1" +
                "\n /get_2 - команда 2" +
                "\n /get_3 - команда 3" +
                "\n /get_4 - команда 4";
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/get".
        /// Команда отработает при написание в чат "/get_1", значение 1 можно обработать.
        /// </summary>
        [SlashHandler("/get")]
        public static async Task ExampleSlashCommandGet(ITelegramBotClient botClient, Update update)
        {
            if (update.Message.Text.Contains("_"))
            {
                var spl = update.Message.Text.Split("_");
                if (spl.Length > 1)
                {
                    string msg = $"Команда /get со значением {spl[1]}";
                    await Helpers.Message.Send(botClient, update, msg);
                }
                else
                {
                    string msg = $"Команда /get";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            else
            {
                string msg = $"Команда /get";
                await Helpers.Message.Send(botClient, update, msg);
            }
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/equals", сработает только если текст сообщения будет /equals но при этом регистро не зависимо.
        /// /equals_1 не сработает.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, "/equals")]
        public static async Task ExampleSlashEqualsCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleSlashEqualsCommand);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/equalsreg", сработает только если текст сообщения будет /equalsreg но при этом регистро зависимо.
        /// Не сработает/equals_1, /equalsreG, /Equalsreg.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, StringComparison.Ordinal, "/equalsreg")]
        public static async Task ExampleSlashEqualsRegisterCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleSlashEqualsRegisterCommand);
            await Helpers.Message.Send(botClient, update, msg);
        }
    }
}
