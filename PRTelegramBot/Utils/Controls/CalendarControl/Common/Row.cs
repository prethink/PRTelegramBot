using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils.Controls.CalendarControl.Common
{
    /// <summary>
    /// Создает inline строки для календаря.
    /// </summary>
    public static class Row
    {
        /// <summary>
        /// Генерация даты.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <returns>Коллекция inline кнопок.</returns>
        public static IEnumerable<InlineKeyboardButton> Date(in DateTime date, CultureInfo culture, int command = 0)
        {
            var dtfi = culture.DateTimeFormat;
            return new InlineKeyboardButton[]
            {
                MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>($"» {date.ToString("Y", dtfi)} «", PRTelegramBotCommand.YearMonthPicker, new CalendarTCommand(date, culture, command)))
            };

        }

        /// <summary>
        /// Коллекция дней недели.
        /// </summary>
        /// <param name="culture">Язык календаря.</param>
        /// <returns>Коллекция inline кнопок.</returns>
        public static IEnumerable<InlineKeyboardButton> DayOfWeek(CultureInfo culture, int command = 0)
        {
            var dtfi = culture.DateTimeFormat;
            var dayNames = new InlineKeyboardButton[7];

            var firstDayOfWeek = (int)dtfi.FirstDayOfWeek;
            for (int i = 0; i < 7; i++)
            {
                yield return dtfi.AbbreviatedDayNames[(firstDayOfWeek + i) % 7];
            }
        }

        /// <summary>
        /// Коллекция месецов.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <returns>Коллекция inline кнопок.</returns>
        public static IEnumerable<IEnumerable<InlineKeyboardButton>> Month(DateTime date, CultureInfo culture, int command = 0)
        {
            var dtfi = culture.DateTimeFormat;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

            for (int dayOfMonth = 1, weekNum = 0; dayOfMonth <= lastDayOfMonth; weekNum++)
            {
                yield return NewWeek(weekNum, ref dayOfMonth);
            }

            IEnumerable<InlineKeyboardButton> NewWeek(int weekNum, ref int dayOfMonth)
            {
                var week = new InlineKeyboardButton[7];

                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    if (weekNum == 0 && dayOfWeek < FirstDayOfWeek()
                       ||
                       dayOfMonth > lastDayOfMonth
                    )
                    {
                        week[dayOfWeek] = " ";
                        continue;
                    }

                    week[dayOfWeek] = MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(dayOfMonth.ToString(), PRTelegramBotCommand.PickDate, new CalendarTCommand(new DateTime(date.Year, date.Month, dayOfMonth), command)));
                    dayOfMonth++;
                }
                return week;

                int FirstDayOfWeek() =>
                    (7 + (int)firstDayOfMonth.DayOfWeek - (int)dtfi.FirstDayOfWeek) % 7;
            }
        }

        /// <summary>
        /// Генерация контролов для переходов по месяцам.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <returns>Коллекция inline кнопок.</returns>
        public static IEnumerable<InlineKeyboardButton> Controls(in DateTime date, int command = 0) =>
            new InlineKeyboardButton[]
            {
                MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>("<", PRTelegramBotCommand.ChangeTo, new CalendarTCommand(date.AddMonths(-1), command))),
                " ",
                MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(">", PRTelegramBotCommand.ChangeTo, new CalendarTCommand(date.AddMonths(1), command))),
            };

        /// <summary>
        /// Возращение к выбору месяца года.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <returns>Массив inline кнопок.</returns>
        public static InlineKeyboardButton[] BackToMonthYearPicker(in DateTime date, int command = 0) =>
            new InlineKeyboardButton[3]
            {
                MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>("<<", PRTelegramBotCommand.YearMonthPicker, new CalendarTCommand(date, command))),
                " ",
                " "
            };

        /// <summary>
        /// Смена года.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <returns>Массив inline кнопок.</returns>
        public static InlineKeyboardButton[] ChangeYear(in DateTime date, int command = 0) =>
            new InlineKeyboardButton[3]
            {
                MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>("<", PRTelegramBotCommand.PickYear, new CalendarTCommand(date.AddYears(-12), command))),
                " ",
                MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(">", PRTelegramBotCommand.PickYear, new CalendarTCommand(date.AddYears(12), command)))
            };
    }
}
