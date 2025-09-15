﻿using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils.Controls.CalendarControl.Common
{
    /// <summary>
    /// Генерация разметки календаря.
    /// </summary>
    public static class Markup
    {
        #region Методы

        /// <summary>
        /// Разметка календаря.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="command">Заголовок команды.</param>
        /// <returns>Inline меню.</returns>
        public static InlineKeyboardMarkup Calendar(in DateTime date, CultureInfo culture, int command = 0)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(Row.Date(date, culture, command));
            keyboardRows.Add(Row.DayOfWeek(culture, command));
            keyboardRows.AddRange(Row.Month(date, culture, command));
            keyboardRows.Add(Row.Controls(date, command));

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка месяца года.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="command">Заголовок команды.</param>
        /// <returns>Inline меню.</returns>
        public static InlineKeyboardMarkup PickMonthYear(in DateTime date, CultureInfo culture, int command = 0)
        {
            var dtfi = culture.DateTimeFormat;
            var keyboardRows = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(date.ToString("MMMM", dtfi), PRTelegramBotCommand.PickMonth, new CalendarTCommand(date, culture, command))),
                    MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(date.ToString("yyyy", dtfi), PRTelegramBotCommand.PickYear, new CalendarTCommand(date, culture, command)))
                },
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>("<<", PRTelegramBotCommand.ChangeTo, new CalendarTCommand(date, command))),
                    " "
                }
            };

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка выбора месяца.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="command">Заголовок команды.</param>
        /// <returns>Inline меню.</returns>
        public static InlineKeyboardMarkup PickMonth(in DateTime date, CultureInfo culture, int command = 0)
        {
            var dtfi = culture.DateTimeFormat;
            var keyboardRows = new InlineKeyboardButton[5][];

            for (int month = 0, row = 0; month < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, month++)
                {
                    var day = new DateTime(date.Year, month + 1, 1);

                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(dtfi.MonthNames[month], PRTelegramBotCommand.YearMonthPicker, new CalendarTCommand(day, command)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date, command);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        /// <summary>
        /// Разметка выбора года.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="culture">Язык календаря.</param>
        /// <param name="command">Заголовок команды.</param>
        /// <returns>Inline меню.</returns>
        public static InlineKeyboardMarkup PickYear(in DateTime date, CultureInfo culture, int command = 0)
        {
            var dtfi = culture.DateTimeFormat;
            var keyboardRows = new InlineKeyboardButton[6][];

            var startYear = date.AddYears(-7);

            for (int i = 0, row = 0; i < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, i++)
                {
                    var day = startYear.AddYears(i);
                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallback<CalendarTCommand>(day.ToString("yyyy", dtfi), PRTelegramBotCommand.YearMonthPicker, new CalendarTCommand(day, command)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date, command);
            keyboardRows[5] = Row.ChangeYear(date, command);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        #endregion
    }
}
