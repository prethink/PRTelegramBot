using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utilities for working with the calendar.
    /// </summary>
    public static class CalendarUtils
    {
        /// <summary>
        /// Creates a new calendar.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="culture">Calendar language.</param>
        /// <param name="headerCallbackCommand">Callback command header.</param>
        /// <param name="option">Message parameters.</param>
        /// <param name="message">Message text.</param>
        public static async Task Create(IBotContext context, CultureInfo culture, Enum headerCallbackCommand, OptionMessage option, string message)
        {
            var calendarMarkup = Markup.Calendar(DateTime.Today, culture, Convert.ToInt32(headerCallbackCommand));
            option.MenuInlineKeyboardMarkup = calendarMarkup;
            option.MenuReplyKeyboardMarkup = null;
            await MessageSender.Send(context, message, option);
        }

        /// <summary>
        /// Creates a new calendar.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="culture">Calendar language.</param>
        /// <param name="headerCallbackCommand">Callback command header.</param>
        /// <param name="message">Message text.</param>
        public static async Task Create(IBotContext context, CultureInfo culture, Enum headerCallbackCommand, string message)
        {
            var option = new OptionMessage();
            await Create(context, culture, headerCallbackCommand, option, message);
        }

        /// <summary>
        /// Creates a new calendar.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="headerCallbackCommand">Callback command header.</param>
        /// <param name="message">Message text.</param>
        public static async Task Create(IBotContext context, Enum headerCallbackCommand, string message)
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU", false);
            await Create(context, culture, headerCallbackCommand, message);
        }
    }
}
