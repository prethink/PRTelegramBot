﻿using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleExample.Examples
{
    public class ExamplePage
    {
        //Test data 1
        static List<string> pageData = new List<string>()
        {
            "Data page 1",
            "Data page 2",
            "Data page 3",
            "Data page 4",
            "Data page 5"
        };

        //Test data 2
        static List<string> pageDataTwo = new List<string>()
        {
            "TestData page 1",
            "TestData page 2",
            "TestData page 3",
            "TestData page 4",
            "TestData page 5"
        };

        /// <summary>
        /// Send "pages" in the chat
        /// </summary>
        [ReplyMenuHandler("pages")]
        public static async Task ExamplePages(IBotContext context)
        {
            //Take the text for the first message
            string msg = pageData[0];
            //Get the content of page 1 with a page size of 1
            var data = await pageData.GetPaged<string>(1, 1);
            //Generate the paginated menu with a header
            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            var message = await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// Send "pagestwo" in the chat
        /// </summary>
        [ReplyMenuHandler("pagestwo")]
        public static async Task ExamplePagesTwo(IBotContext context)
        {
            //Take the text for the first message
            string msg = pageDataTwo[0];
            //Get the content of page 1 with a page size of 1
            var data = await pageDataTwo.GetPaged<string>(1, 1);
            //Generate the paginated menu with a header
            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader2);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;

            var message = await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// callback handling for paginated output
        /// Handles a single entry point
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage, PRTelegramBotCommand.PreviousPage, PRTelegramBotCommand.CurrentPage)]
        public static async Task InlinenPage(IBotContext context)
        {
            try
            {
                //Try to convert the callback data to the required type
                if (context.Update.CallbackQuery?.Data != null)
                {
                    var command = context.GetCommandByCallbackOrNull<PageTCommand>();
                    if (command != null)
                    {
                        //Get the header out of the data
                        CustomTHeaderTwo header = (CustomTHeaderTwo)command.Data.Header;
                        //handle the data by its header
                        if(header == CustomTHeaderTwo.CustomPageHeader)
                        {
                            //Get the page number and set the page size
                            var data = await pageData.GetPaged<string>(command.Data.Page, 1);
                            //Generate the paginated menu
                            var button = new InlineCallback("⭐", CustomTHeader.CustomButton);
                            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader, button: button);
                            //Get the result of the paginated output
                            var pageResult = data.Results;
                            var option = new OptionMessage();
                            option.MenuInlineKeyboardMarkup = generateMenu;
                            string msg = string.Empty;
                            if (pageResult.Count > 0)
                            {
                                msg = pageResult.FirstOrDefault();
                            }
                            else
                            {
                                msg = "Nothing was found";
                            }
                            //Edit the current page
                            await MessageEditor.Edit(context, msg, option);
                        }
                        //handle the data by its header
                        else if (header == CustomTHeaderTwo.CustomPageHeader2)
                        {
                            //Get the page number and set the page size
                            var data = await pageDataTwo.GetPaged<string>(command.Data.Page, 1);
                            //Generate the paginated menu
                            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader2);
                            //Get the result of the paginated output
                            var pageResult = data.Results;
                            var option = new OptionMessage();
                            option.MenuInlineKeyboardMarkup = generateMenu;
                            string msg = string.Empty;
                            if (pageResult.Count > 0)
                            {
                                msg = pageResult.FirstOrDefault();
                            }
                            else
                            {
                                msg = "Nothing was found";
                            }
                            //Edit the current page
                            await MessageEditor.Edit(context, msg, option);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Exception handling
            }
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.CustomButton)]
        public static async Task FavoriteMessage(IBotContext context)
        {
            string msg = "Menu";
            //Create the message options
            var option = new OptionMessage();
            //Create the list for the menu
            var menuList = new List<KeyboardButton>();
            //Add a button with text
            menuList.Add(new KeyboardButton("Button 1"));
            //Add a button that requests the user's contact
            menuList.Add(KeyboardButton.WithRequestContact("Share my contact"));
            //Add a button that requests the user's location
            menuList.Add(KeyboardButton.WithRequestLocation("Share my location"));
            //Add a button that requests a chat to be sent to the bot
            menuList.Add(KeyboardButton.WithRequestChat("Send a group to the bot", new KeyboardButtonRequestChat(2, true) ));
            //Add a button that requests a user to be sent to the bot
            menuList.Add(KeyboardButton.WithRequestUsers("Send a user to the bot", new KeyboardButtonRequestUsers() { RequestId = 1 }));
            //Add a button that sends a poll
            menuList.Add(KeyboardButton.WithRequestPoll("Send a poll", new KeyboardButtonPollType()));
            //Add a button that opens a WebApp
            menuList.Add(KeyboardButton.WithWebApp("WebApp", new WebAppInfo() { Url = "https://prethink.github.io/telegram/webapp.html" }));

            //Generate the reply menu
            //1 column, the collection of menu items, vertical menu stretching, the item pinned at the very bottom by default
            var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Main menu");
            //Add the menu to the options
            option.MenuReplyKeyboardMarkup = menu;
            await MessageSender.Send(context, msg, option);
        }
    }
}
