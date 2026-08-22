using Microsoft.Extensions.Logging;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleSlashCommands
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/example" is sent to the chat.
        /// </summary>
        [SlashHandler("/example")]
        public static async Task ExampleSlashCommand(IBotContext context)
        {
            context.Current.GetLogger<ExampleSlashCommands>().LogWarning("Hello world");
            string msg = $"Command /example";
            msg += "\n /get_1 - command 1" +
                "\n /get_2 - command 2" +
                "\n /get_3 - command 3" +
                "\n /get_4 - command 4";
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/get" is sent to the chat.
        /// The command runs when "/get_1" is sent to the chat; the value 1 can be processed.
        /// </summary>
        [SlashHandler('_', "/get")]
        public static async Task ExampleSlashCommandGet(IBotContext context)
        {
            var args = context.GetSlashArgs();

            // No arguments
            if (args.Count == 0)
            {
                await MessageSender.Send(context, "Command /get");
                return;
            }

            // One argument
            if (args.Count == 1)
            {
                await MessageSender.Send(context, $"Command /get with the value: {args[0]}");
                return;
            }

            // Several arguments
            string joinedArgs = string.Join(", ", args);
            await MessageSender.Send(context, $"Command /get with the values: {joinedArgs}");
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/int" is sent to the chat.
        /// The command runs when "/int_1" is sent to the chat; the value 1 can be processed.
        /// </summary>
        [SlashHandler('_', "/int")]
        public static async Task ExampleSlashIntCommandGet(IBotContext context)
        {
            var args = context.GetSlashArgs<int>();

            // No arguments
            if (args.Count == 0)
            {
                await MessageSender.Send(context, "Command /int");
                return;
            }

            // One argument
            if (args.Count == 1)
            {
                await MessageSender.Send(context, $"Command /int with the value: {args[0]}");
                return;
            }

            // Several arguments
            string joinedArgs = string.Join(", ", args);
            await MessageSender.Send(context, $"Command /int with the values: {joinedArgs}");
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/bool" is sent to the chat.
        /// The command runs when "/bool_true" is sent to the chat; the value 1 can be processed.
        /// </summary>
        [SlashHandler('_', "/bool")]
        public static async Task ExampleSlashBoolCommandGet(IBotContext context)
        {
            var args = context.GetSlashArgs<bool>();

            // No arguments
            if (args.Count == 0)
            {
                await MessageSender.Send(context, "Command /bool");
                return;
            }

            // One argument
            if (args.Count == 1)
            {
                await MessageSender.Send(context, $"Command /bool with the value: {args[0]}");
                return;
            }

            // Several arguments
            string joinedArgs = string.Join(", ", args);
            await MessageSender.Send(context, $"Command /bool with the values: {joinedArgs}");
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/start" is sent to the chat.
        /// The command runs when "/start 1" is sent to the chat; the value 1 can be processed.
        /// </summary>
        [SlashHandler(' ', "/start")]
        public static async Task ExampleSlashCommandStart(IBotContext context)
        {
            var args = context.GetSlashArgs();
            if (args.Count > 0)
            {
                string msgWithArgs = $"Command /start with the value {args[0]}";
                await MessageSender.Send(context, msgWithArgs);
                return;
            }

            string msg = $"Command /start";
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/equals" is sent to the chat; it triggers only if the message text is /equals, case-insensitively.
        /// /equals_1 will not trigger.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, "/equals")]
        public static async Task ExampleSlashEqualsCommand(IBotContext context)
        {
            string msg = nameof(ExampleSlashEqualsCommand);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "/equalsreg" is sent to the chat; it triggers only if the message text is /equalsreg, case-sensitively.
        /// Will not trigger for /equals_1, /equalsreG, /Equalsreg.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, StringComparison.Ordinal, "/equalsreg")]
        public static async Task ExampleSlashEqualsRegisterCommand(IBotContext context)
        {
            string msg = nameof(ExampleSlashEqualsRegisterCommand);
            await MessageSender.Send(context, msg);
        }
    }
}
