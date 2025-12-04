using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
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
        [SlashHandler('_', "/get")]
        public static async Task ExampleSlashCommandGet(IBotContext context)
        {
            var args = context.GetSlashArgs();

            // Нет аргументов
            if (args.Count == 0)
            {
                await Helpers.Message.Send(context, "Команда /get");
                return;
            }

            // Один аргумент
            if (args.Count == 1)
            {
                await Helpers.Message.Send(context, $"Команда /get со значением: {args[0]}");
                return;
            }

            // Несколько аргументов
            string joinedArgs = string.Join(", ", args);
            await Helpers.Message.Send(context, $"Команда /get со значениями: {joinedArgs}");
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/int".
        /// Команда отработает при написание в чат "/int_1", значение 1 можно обработать.
        /// </summary>
        [SlashHandler('_', "/int")]
        public static async Task ExampleSlashIntCommandGet(IBotContext context)
        {
            var args = context.GetSlashArgs<int>();

            // Нет аргументов
            if (args.Count == 0)
            {
                await Helpers.Message.Send(context, "Команда /int");
                return;
            }

            // Один аргумент
            if (args.Count == 1)
            {
                await Helpers.Message.Send(context, $"Команда /int со значением: {args[0]}");
                return;
            }

            // Несколько аргументов
            string joinedArgs = string.Join(", ", args);
            await Helpers.Message.Send(context, $"Команда /int со значениями: {joinedArgs}");
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/bool".
        /// Команда отработает при написание в чат "/bool_true", значение 1 можно обработать.
        /// </summary>
        [SlashHandler('_', "/bool")]
        public static async Task ExampleSlashBoolCommandGet(IBotContext context)
        {
            var args = context.GetSlashArgs<bool>();

            // Нет аргументов
            if (args.Count == 0)
            {
                await Helpers.Message.Send(context, "Команда /bool");
                return;
            }

            // Один аргумент
            if (args.Count == 1)
            {
                await Helpers.Message.Send(context, $"Команда /bool со значением: {args[0]}");
                return;
            }

            // Несколько аргументов
            string joinedArgs = string.Join(", ", args);
            await Helpers.Message.Send(context, $"Команда /bool со значениями: {joinedArgs}");
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/start".
        /// Команда отработает при написание в чат "/start 1", значение 1 можно обработать.
        /// </summary>
        [SlashHandler(' ', "/start")]
        public static async Task ExampleSlashCommandStart(IBotContext context)
        {
            var args = context.GetSlashArgs();
            if (args.Count > 0)
            {
                string msgWithArgs = $"Команда /start со значением {args[0]}";
                await Helpers.Message.Send(context, msgWithArgs);
                return;
            }

            string msg = $"Команда /start";
            await Helpers.Message.Send(context, msg);
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
