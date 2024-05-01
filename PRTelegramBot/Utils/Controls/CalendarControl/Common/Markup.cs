using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils.Controls.CalendarControl.Common
{
    /// <summary>
    /// Генерация разметки календаря
    /// </summary>
    public static class Markup
    {
        /// <summary>
        /// Разметка калердаря
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="dtfi">Формат даты</param>
        /// <returns>Inline меню</returns>
        public static InlineKeyboardMarkup Calendar(in DateTime date, DateTimeFormatInfo dtfi, int command = 0)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(Row.Date(date, dtfi, command));
            keyboardRows.Add(Row.DayOfWeek(dtfi, command));
            keyboardRows.AddRange(Row.Month(date, dtfi, command));
            keyboardRows.Add(Row.Controls(date, command));

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка месяца года
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="dtfi">Формат даты</param>
        /// <returns>Inline меню</returns>
        public static InlineKeyboardMarkup PickMonthYear(in DateTime date, DateTimeFormatInfo dtfi, int command = 0)
        {
            var keyboardRows = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(date.ToString("MMMM", dtfi), THeader.PickMonth, new CalendarTCommand(date, command))),
                    MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(date.ToString("yyyy", dtfi), THeader.PickYear, new CalendarTCommand(date, command)))
                },
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>("<<", THeader.ChangeTo, new CalendarTCommand(date, command))),
                    " "
                }
            };

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка выбора месяца
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="dtfi">Формат даты</param>
        /// <returns>Inline меню</returns>
        public static InlineKeyboardMarkup PickMonth(in DateTime date, DateTimeFormatInfo dtfi, int command = 0)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            for (int month = 0, row = 0; month < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, month++)
                {
                    var day = new DateTime(date.Year, month + 1, 1);

                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(dtfi.MonthNames[month], THeader.YearMonthPicker, new CalendarTCommand(day, command)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date, command);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка выбора года
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="dtfi">Формат даты</param>
        /// <returns>Inline меню</returns>
        public static InlineKeyboardMarkup PickYear(in DateTime date, DateTimeFormatInfo dtfi, int command = 0)
        {
            var keyboardRows = new InlineKeyboardButton[6][];

            var startYear = date.AddYears(-7);

            for (int i = 0, row = 0; i < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, i++)
                {
                    var day = startYear.AddYears(i);
                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(day.ToString("yyyy", dtfi), THeader.YearMonthPicker, new CalendarTCommand(day, command)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date, command);
            keyboardRows[5] = Row.ChangeYear(date, command);

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}
