﻿﻿using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;
using Telegram.Bot;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleInlineCommands
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "InlineMenu" is sent to the chat.
        /// Example of generating an inline menu
        /// Configuration file setup performed when a PRBot instance is created <see cref="Program"/>
        /// </summary>
        [ReplyMenuHandler("InlineMenu")]
        public static async Task InlineMenu(IBotContext context)
        {
            /*
             *  A bot instance is created in program.cs:
             *   
             *  var telegram = new PRBotBuilder(string.Empty)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
             *      .Build();
             *  
             *  AddConfigPath - adds the path to the configuration file.
             *  ExampleConstants.BUTTONS_FILE_KEY - the key 
             *  ".\\Configs\\buttons.json" - path to the configuration file.
             *  
             */

            /*
             *  context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE")
             *  BotConfigJsonProvider - the provider that works with json files.
             *  string - the return type.
             *  ExampleConstants.BUTTONS_FILE_KEY - the config key.
             *  IN_EXAMPLE_ONE - key of the button text in the buttons.json file
             * 
             */

            /* Creating a new button with callback data
             * context`.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE") - the button name taken from json
             * CustomTHeaderTwo.ExampleOne - the command header
             */
            var exampleItemOne = new InlineCallback(context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE"), CustomTHeaderTwo.ExampleOne);
            /* Creating a new button with callback data
             * InlineKeys.IN_EXAMPLE_TWO - the button name taken from a constant
             * CustomTHeaderTwo.ExampleTwo - the command header
             * new EntityTCommand(2) - the data that has to be passed
             */
            var exampleItemTwo = new InlineCallback<EntityTCommand<long>>("Example with a large number", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(2_000_000_000_000_000_000));
            /* Creating a new button with callback data
             * CustomTHeaderTwo.ExampleThree - the command header
             * new EntityTCommand(3) - the data that has to be passed
             */

            var exampleItemThree = new InlineCallback<EntityTCommand<string>>("Example with a long text", CustomTHeaderTwo.ExampleThree, new EntityTCommand<string>("There is no doubt that relationship diagrams will be declared a violation of universal ethical and moral standards. There is a debatable point of view stating roughly the following: the key features of the project structure, initiated purely synthetically, have been verified in a timely manner. The significance of these problems is so obvious that a high-technology concept of the social order gives a wide circle of specialists a part in shaping a rethinking of foreign economic policies. Thus, a high-technology concept of the social order plays an important role in shaping experiments that are striking in their scale and grandeur. Cartel agreements do not allow a situation in which thorough studies of competitors, overcoming the difficult economic situation that has developed, are blocked within the bounds of their own rational constraints. Each of us understands the obvious thing: the implementation of the planned targets reveals an urgent need for both self-sufficient and externally dependent conceptual solutions. Equally, the conviction of some opponents unambiguously defines every participant as capable of making their own decisions regarding the highest-priority requirements. Everyday practice shows that the implementation of the planned targets ensures the relevance of the distribution of internal reserves and resources. In their striving to improve the quality of life, they forget that the basic vector of development ensures the relevance of the tasks set by society."));

            var inlineStep = new InlineCallback("Inline Step", CustomTHeader.InlineWithStep);

            //Commands added after the bot has started
            var exampleAddCommand = new InlineCallback("Dynamically added command 1", AddCustomTHeader.TestAddCommand);
            var exampleAddCommandTwo = new InlineCallback("Dynamically added command 2", AddCustomTHeader.TestAddCommandTwo);

            // Creates an inline button with a link
            var url = new InlineURL("Google", "https://google.com");
            // Create a button that works with a webApp
            var webdata = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

            var keyboard = new InlineKeyboardBuilder()
                .AddButton(exampleItemOne)
                .AddButton(exampleItemTwo, newRow:true)
                .AddButton(exampleItemThree, newRow: true)
                .AddButton(exampleAddCommand, newRow: true)
                .AddRow()
                .AddButton(exampleAddCommandTwo)
                .AddButton(inlineStep)
                .AddRow()
                .AddButton(url)
                .AddButton(webdata)
                .Build();

            //Create the options to pass into the message
            var option = new OptionMessage();
            //Pass the menu into the options
            option.MenuInlineKeyboardMarkup = keyboard;
            string msg = "Menu example";
            //Send a message with the menu
            await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// callback handling
        /// Handles a single entry point
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleOne)]
        public static async Task Inline(IBotContext context)
        {
            try
            {
                //Try to convert the callback data to the required type
                var command = context.GetCommandByCallbackOrNull();
                if (command != null)
                {
                    string msg = "The callback command has been executed";
                    await MessageSender.Send(context, msg);
                }
            }
            catch (Exception ex)
            {
                //Exception handling
            }
        }


        /// <summary>
        /// callback handling
        /// This method can handle several entry points
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleTwo)]
        public static async Task InlineTwo(IBotContext context)
        {
            try
            {
                //Try to convert the callback data to the required type
                var command = context.GetCommandByCallbackOrNull<EntityTCommand<long>>();
                if (command != null)
                {
                    string msg = $"The identifier you passed: {command.Data.EntityId}";
                    if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await MessageEditor.Edit(context, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await context.BotClient.DeleteMessage(context.Update.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
                        }
                        await MessageSender.Send(context, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception handling
            }
        }

        /// <summary>
        /// callback handling
        /// This method can handle several entry points
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleThree)]
        public static async Task InlineThree(IBotContext context)
        {
            try
            {
                //Try to convert the callback data to the required type
                var command = context.GetCommandByCallbackOrNull<EntityTCommand<string>>();
                if (command != null)
                {
                    string msg = $"The identifier you passed: {command.Data.EntityId}";
                    if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await MessageEditor.Edit(context, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await context.BotClient.DeleteMessage(context.Update.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
                        }
                        await MessageSender.Send(context, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception handling
            }
        }
    }
}
