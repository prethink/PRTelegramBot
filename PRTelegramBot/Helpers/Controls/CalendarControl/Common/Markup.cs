using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;

namespace CalendarPicker.CalendarControl
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
        public static InlineKeyboardMarkup Calendar(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(Row.Date(date, dtfi));
            keyboardRows.Add(Row.DayOfWeek(dtfi));
            keyboardRows.AddRange(Row.Month(date, dtfi));
            keyboardRows.Add(Row.Controls(date));

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка месяца года
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="dtfi">Формат даты</param>
        /// <returns>Inline меню</returns>
        public static InlineKeyboardMarkup PickMonthYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallback<CallendarTCommand>(date.ToString("MMMM", dtfi), THeader.PickMonth, new CallendarTCommand(date))),
                    MenuGenerator.GetInlineButton(new InlineCallback<CallendarTCommand>(date.ToString("yyyy", dtfi), THeader.PickYear, new CallendarTCommand(date)))
                },
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallback<CallendarTCommand>("<<", THeader.ChangeTo, new CallendarTCommand(date))),
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
        public static InlineKeyboardMarkup PickMonth(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            for (int month = 0, row = 0; month < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, month++)
                {
                    var day = new DateTime(date.Year, month + 1, 1);

                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallback<CallendarTCommand>(dtfi.MonthNames[month], THeader.YearMonthPicker, new CallendarTCommand(day)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка выбора года
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="dtfi">Формат даты</param>
        /// <returns>Inline меню</returns>
        public static InlineKeyboardMarkup PickYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[6][];

            var startYear = date.AddYears(-7);

            for (int i = 0, row = 0; i < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, i++)
                {
                    var day = startYear.AddYears(i);
                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallback<CallendarTCommand>(day.ToString("yyyy", dtfi), THeader.YearMonthPicker, new CallendarTCommand(day)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date);
            keyboardRows[5] = Row.ChangeYear(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}
