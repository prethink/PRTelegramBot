using ConsoleExample.Examples.Events;
using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleReplyCommands
    {
        static int count = 0;

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs if 'Command contains text' is contained in the message text.
        /// The command's letter case is also ignored during the check.
        /// </summary>
        [ReplyMenuHandler(CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, "Command contains text")]
        public static async Task ReplyExampleOne(IBotContext context)
        {
            string msg = nameof(ReplyExampleOne);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when the message text matches 'Exact command match' exactly, ignoring case.
        /// </summary>
        [ReplyMenuHandler("Exact command match")]
        public static async Task ReplyExampleTwo(IBotContext context)
        {
            string msg = nameof(ReplyExampleTwo);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// Send "Example 1" or "Example 2" in the chat.
        /// Example of binding several reply commands to a single method.
        /// </summary>
        [ReplyMenuHandler("Example 1", "Example 2")]
        public static async Task ExampleReplyMany(IBotContext context)
        {
            string msg = nameof(ExampleReplyMany);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Menu" is sent to the chat.
        /// As a result, a menu is generated.
        /// </summary>
        [ReplyMenuHandler("Reply menu")]
        public static async Task ExampleReplyMenu(IBotContext context)
        {
            string msg = "Menu";
            //Create the message options
            var option = new OptionMessage();
            var keyboard = new ReplyKeyboardBuilder()
                            .SetResizeKeyboard(true)
                            .AddButton("Button 1")
                            .AddRequestContact("Share my contact", newRow:true)
                            .AddRequestLocation("Share my location")
                            .AddRow()
                            .AddRequestChat("Send a group to the bot", new KeyboardButtonRequestChat(2, true))
                            .AddRequestUsers("Send a user to the bot", new KeyboardButtonRequestUsers() { RequestId = 1 })
                            .AddRequestPoll("Send a poll", new KeyboardButtonPollType())
                            .AddEmptyButton(3, newRow:true)
                            .AddRow()
                            .AddButtonWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html")
                            .SetMainMenuButton("Main menu")
                            .Build();

            //Add the menu to the options
            option.MenuReplyKeyboardMarkup = keyboard;
            await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Dynamic message text example" is sent to the chat.
        /// Example of working with text from a json file.
        /// Configuration file setup performed when a PRBot instance is created <see cref="Program"/>
        /// </summary>
        [ReplyMenuHandler("Dynamic message text example")]
        public static async Task ExampleDynamicReply(IBotContext context)
        {
            /*
             *  A bot instance is created in program.cs:
             *   
             *  var telegram = new PRBotBuilder(string.Empty)
             *      .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
             *      .Build();
             *  
             *  AddConfigPath - adds the path to the configuration file.
             *  ExampleConstants.MESSAGES_FILE_KEY - the key 
             *  ".\\Configs\\messages.json" - path to the configuration file.
             *  
             */

            /*
             *  botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.MESSAGES_FILE_KEY, "MSG_EXAMPLE_TEXT")
             *  BotConfigJsonProvider - the provider that works with json files.
             *  string - the return type.
             *  ExampleConstants.MESSAGES_FILE_KEY - the config key.
             *  MSG_EXAMPLE_TEXT - key of the message text in the messages.json file
             * 
             */

            // Get the message text by key from the json file.
            string msg = context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.MESSAGES_FILE_KEY, "MSG_EXAMPLE_TEXT");
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Brackets" is sent to the chat.
        /// Example of a menu with brackets.
        /// </summary>
        [ReplyMenuHandler("Brackets")]
        public static async Task ExampleBracket(IBotContext context)
        {
            string msg = $"Value {count}";
            //Create the message options
            var option = new OptionMessage();
            //Create the list for the menu
            var menuList = new List<KeyboardButton>();
            //Add a button with text
            menuList.Add(new KeyboardButton($"Brackets ({count})"));
            //Generate the reply menu
            //1 column, the collection of menu items, vertical menu stretching, the item pinned at the very bottom by default
            var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Main menu");
            //Add the menu to the options
            option.MenuReplyKeyboardMarkup = menu;
            await MessageSender.Send(context, msg, option);
            count++;
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Access check" is sent to the chat.
        /// Before the method runs, the privilege check event <see cref="ExampleEvents.OnCheckPrivilege"/> is raised
        /// </summary>
        [Access((int)(UserPrivilege.Guest | UserPrivilege.Registered))]
        [ReplyMenuHandler("Access check")]
        public static async Task ExampleAccess(IBotContext context)
        {
            string msg = nameof(ExampleAccess);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when the value stored under the "DYNAMIC_COMMANT_EXAMPLE" key in commands.json is sent to the chat.
        /// Configuration file setup performed when a PRBot instance is created <see cref="Program"/>
        /// "DYNAMIC_COMMANT_EXAMPLE": "Dynamic command"
        /// </summary>
        [ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMANT_EXAMPLE))]
        public static async Task ExampleReplyDynamicCommand(IBotContext context)
        {
            /*
             *  Creating a provider that works with the commands.json file
             *  var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
             *  
             *  Retrieving all commands as key:value pairs
             *  var dynamicCommands = botJsonProvider.GetKeysAndValues();
             *
             *  var telegram = new PRBotBuilder(string.Empty)
             *                      .AddReplyDynamicCommands(dynamicCommands)
             *                      .Build();
             * 
             * .AddReplyDynamicCommands(dynamicCommands) - adds all dynamic commands to the list.
             * 
             * [ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMANT_EXAMPLE))] - runs the dynamic command bound to the DYNAMIC_COMMANT_EXAMPLE key
             */

            string msg = nameof(ExampleReplyDynamicCommand);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Send "Private command" in the chat
        /// The required chat must be a private one.
        /// </summary>
        [ReplyMenuHandler("Private command")]
        [RequireChatType(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task ExampleReplyRequirePrivate(IBotContext context)
        {
            string msg = nameof(ExampleReplyRequirePrivate);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Send "Text-only message" in the chat
        /// The required message type must contain text only.
        /// </summary>
        [ReplyMenuHandler("Text-only message")]
        [RequireMessageType(Telegram.Bot.Types.Enums.MessageType.Text)]
        public static async Task ExampleReplyRequiredText(IBotContext context)
        {
            string msg = nameof(ExampleReplyRequiredText);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for the bot with botId 1.
        /// The command runs when "Example command for bot id 1" is sent to the chat.
        /// Example of working with text from a json file.
        /// </summary>
        [ReplyMenuHandler(1, "Example command for bot id 1")]
        public static async Task ExampleReplyBotIdOne(IBotContext context)
        {
            string msg = nameof(ExampleReplyBotIdOne);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// The command will run for any bot with any botId.
        /// The command runs when "Command for all bots" is sent to the chat.
        /// </summary>
        [ReplyMenuHandler(PRConstants.ALL_BOTS_ID, "Command for all bots")]
        public static async Task ReplyExampleAllBots(IBotContext context)
        {
            string msg = nameof(ReplyExampleAllBots);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// A reply command that delays processing of the update.
        /// </summary>
        [ReplyMenuHandler("Block10")]
        public static async Task ReplyBlockUpdate(IBotContext context)
        {
            string msg = nameof(ReplyBlockUpdate);
            await MessageSender.Send(context, msg);

            await Task.Delay(10000);

            await MessageSender.Send(context, "Done waiting");
        }

    }
}
