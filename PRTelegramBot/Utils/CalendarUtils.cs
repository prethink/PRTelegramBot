using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Утилиты для работы с календерем
    /// </summary>
    public static class CalendarUtils
    {
        /// <summary>
        /// Создать новый календарь.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="update">Update.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <param name="message">Текст сообщение.</param>
        public static async Task Create(ITelegramBotClient botClient, Update update, CultureInfo culture, Enum headerCallbackCommand, OptionMessage option, string message)
        {
            var calendarMarkup = Markup.Calendar(DateTime.Today, culture, Convert.ToInt32(headerCallbackCommand));
            option.MenuInlineKeyboardMarkup = calendarMarkup;
            option.MenuReplyKeyboardMarkup = null;
            await Helpers.Message.Send(botClient, update.GetChatId(), message, option);
        }

        /// <summary>
        /// Создать новый календарь.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="update">Update.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="message">Текст сообщение.</param>
        public static async Task Create(ITelegramBotClient botClient, Update update, CultureInfo culture, Enum headerCallbackCommand, string message)
        {
            var option = new OptionMessage();
            await Create(botClient, update, culture, headerCallbackCommand, option, message);
        }

        /// <summary>
        /// Создать новый календарь.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="update">Update.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="message">Текст сообщение.</param>
        public static async Task Create(ITelegramBotClient botClient, Update update, Enum headerCallbackCommand, string message)
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU", false);
            await Create(botClient, update, culture, headerCallbackCommand, message);
        }
    }
}
