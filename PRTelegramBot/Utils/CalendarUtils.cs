using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Утилиты для работы с календарем.
    /// </summary>
    public static class CalendarUtils
    {
        /// <summary>
        /// Создать новый календарь.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <param name="message">Текст сообщение.</param>
        public static async Task Create(IBotContext context, CultureInfo culture, Enum headerCallbackCommand, OptionMessage option, string message)
        {
            var calendarMarkup = Markup.Calendar(DateTime.Today, culture, Convert.ToInt32(headerCallbackCommand));
            option.MenuInlineKeyboardMarkup = calendarMarkup;
            option.MenuReplyKeyboardMarkup = null;
            await MessageSender.Send(context, message, option);
        }

        /// <summary>
        /// Создать новый календарь.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="message">Текст сообщение.</param>
        public static async Task Create(IBotContext context, CultureInfo culture, Enum headerCallbackCommand, string message)
        {
            var option = new OptionMessage();
            await Create(context, culture, headerCallbackCommand, option, message);
        }

        /// <summary>
        /// Создать новый календарь.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="message">Текст сообщение.</param>
        public static async Task Create(IBotContext context, Enum headerCallbackCommand, string message)
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU", false);
            await Create(context, culture, headerCallbackCommand, message);
        }
    }
}
