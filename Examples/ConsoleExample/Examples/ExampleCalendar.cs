﻿using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using System.Globalization;

namespace ConsoleExample.Examples
{
    public class ExampleCalendar
    {
        /// <summary>
        /// Send Calendar to the chat
        /// Invokes the calendar command
        /// </summary>
        [ReplyMenuHandler("Calendar")]
        public static async Task PickCalendar(IBotContext context)
        {
            try
            {
                await CalendarUtils.Create(context, CustomTHeader.CalendarCallback, "Pick a date:");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Send EngCalendar to the chat
        /// Invokes the calendar command in English
        /// </summary>
        [ReplyMenuHandler("EngCalendar")]
        public static async Task EngPickCalendar(IBotContext context)
        {
            try
            {
                await CalendarUtils.Create(context, CultureInfo.GetCultureInfo("en-US", false), CustomTHeader.CalendarCallback, "Choose date:");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Handles the selected date
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.CalendarCallback)]
        public static async Task PickDate(IBotContext context)
        {
            var bot = context.Current;
            try
            {
                using (var inlineHandler = new InlineCallback<CalendarTCommand>(context))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    await MessageSender.Send(context, command.Data.Date.ToString());
                }
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
            }
        }
    }
}
