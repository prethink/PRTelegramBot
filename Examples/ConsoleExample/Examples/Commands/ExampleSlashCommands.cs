using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
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
        public static async Task ExampleSlashCommand(IBotContext context)
        {
            string msg = $"Команда /example";
            msg += "\n /get_1 - команда 1" +
                "\n /get_2 - команда 2" +
                "\n /get_3 - команда 3" +
                "\n /get_4 - команда 4";
            await Helpers.Message.Send(context, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/get".
        /// Команда отработает при написание в чат "/get_1", значение 1 можно обработать.
        /// </summary>
        [SlashHandler("/get")]
        public static async Task ExampleSlashCommandGet(IBotContext context)
        {
            if (context.Update.Message.Text.Contains("_"))
            {
                var spl = context.Update.Message.Text.Split("_");
                if (spl.Length > 1)
                {
                    string msg = $"Команда /get со значением {spl[1]}";
                    await Helpers.Message.Send(context, msg);
                }
                else
                {
                    string msg = $"Команда /get";
                    await Helpers.Message.Send(context, msg);
                }
            }
            else
            {
                string msg = $"Команда /get";
                await Helpers.Message.Send(context, msg);
            }
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/equals", сработает только если текст сообщения будет /equals но при этом регистро не зависимо.
        /// /equals_1 не сработает.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, "/equals")]
        public static async Task ExampleSlashEqualsCommand(IBotContext context)
        {
            string msg = nameof(ExampleSlashEqualsCommand);
            await Helpers.Message.Send(context, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/equalsreg", сработает только если текст сообщения будет /equalsreg но при этом регистро зависимо.
        /// Не сработает/equals_1, /equalsreG, /Equalsreg.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, StringComparison.Ordinal, "/equalsreg")]
        public static async Task ExampleSlashEqualsRegisterCommand(IBotContext context)
        {
            string msg = nameof(ExampleSlashEqualsRegisterCommand);
            await Helpers.Message.Send(context, msg);
        }
    }
}
