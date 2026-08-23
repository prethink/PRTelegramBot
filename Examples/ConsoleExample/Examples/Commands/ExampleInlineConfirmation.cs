using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleInlineConfirmation
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when the user sends InlineConfirm.
        /// The command's letter case is also ignored during the check.
        /// </summary>
        [ReplyMenuHandler("InlineConfirm")]
        public static async Task InlineConfirm(IBotContext context)
        {
            //The button that a confirmation has to be created for.
            var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>("Button with confirmation", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(3, ActionWithLastMessage.Delete));
            //Button wrapper.
            var exampleWithConfirmation = new InlineCallbackWithConfirmation(exampleInlineCallback, ActionWithLastMessage.Delete);

            //Create a new menu.
            List<IInlineContent> menu = new() { exampleWithConfirmation };
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);
            var option = new OptionMessage();

            //Pass the menu into the options
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "InlineCallback with confirmation";
            //Send a message with the menu
            await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// Example of handling an inline class.
        /// </summary>
        [ReplyMenuHandler("InlineClass")]
        public static async Task InlineClass(IBotContext context)
        {
            var exampleInlineCallback = new InlineCallback<StringTCommand>("Test1", ClassTHeader.DefaultTestClass, new StringTCommand("Test1"));
            var exampleInlineCallbackTwo = new InlineCallback<StringTCommand>("Test2", ClassTHeader.DefaultTestClass, new StringTCommand("Test2"));
            var exampleInlineCallbackThree = new InlineCallback<StringTCommand>("Test3", ClassTHeader.DefaultTestClass, new StringTCommand("Test3"));

            //Create a new menu.
            List<IInlineContent> menu = new() { exampleInlineCallback, exampleInlineCallbackTwo, exampleInlineCallbackThree };

            var keyboard = new InlineKeyboardBuilder()
                                    .AddButton(exampleInlineCallback)
                                    .AddButton(exampleInlineCallbackTwo, newRow:true)
                                    .AddRow()
                                    .AddRow()
                                    .AddButton(exampleInlineCallbackThree)
                                    .Build();   

            var option = new OptionMessage();

            //Pass the menu into the options
            option.MenuInlineKeyboardMarkup = keyboard;
            string msg = "InlineClass";

            //Send a message with the menu
            await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when the user sends InlineConfirm.
        /// The command's letter case is also ignored during the check.
        /// </summary>
        [ReplyMenuHandler("InlineConfirmWithBack")]
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleBack)]
        public static async Task InlineConfirmWithBack(IBotContext context)
        {
            //The button that a confirmation has to be created for.
            var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>("Button with confirmation", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(3, ActionWithLastMessage.Delete));
            //The back button handler, or a custom one.
            var exampleBack = new InlineCallback("Back", CustomTHeaderTwo.ExampleBack);

            //Button wrapper.
            var exampleWithConfirmation = new InlineCallbackWithConfirmation(exampleInlineCallback, ActionWithLastMessage.Edit, exampleBack);

            //Create a new menu.
            List<IInlineContent> menu = new() { exampleWithConfirmation };
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);
            var option = new OptionMessage();

            //Pass the menu into the options
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "InlineCallback with confirmation and a back or custom button handler";
            //Send a message with the menu
            if (context.Update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
                await MessageEditor.Edit(context, msg, option);
            else
                await MessageSender.Send(context, msg, option);
        }
    }
}
