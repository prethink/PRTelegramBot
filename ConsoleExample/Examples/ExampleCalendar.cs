using CalendarPicker.CalendarControl;
using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Helpers;
using System.Globalization;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;
using Helpers = PRTelegramBot.Helpers;
using THeader = PRTelegramBot.Models.Enums.THeader;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Core;
using PRTelegramBot.Commands.Constants;
using ConsoleExample.Models;

namespace ConsoleExample.Examples
{
    public class ExampleCalendar
    {
        /// <summary>
        /// Русский формат даты
        /// </summary>
        public static DateTimeFormatInfo dtfi = CultureInfo.GetCultureInfo("ru-RU", false).DateTimeFormat;

        /// <summary>
        /// Напишите в чат Calendar
        /// Вызов команды календаря
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_CALENDAR)]
        public static async Task PickCalendar(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var calendarMarkup = Markup.Calendar(DateTime.Today, dtfi,(int)CustomTHeader.ExampleThree);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = calendarMarkup;
                await Helpers.Message.Send(botClient, update.GetChatId(), $"Выберите дату:", option);
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        /// <summary>
        /// Выбор года или месяца
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.YearMonthPicker)]
        public static async Task PickYearMonth(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthYearMarkup = Markup.PickMonthYear(command.Data.Date, dtfi,command.Data.LastCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        /// <summary>
        /// Выбор месяца
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickMonth)]
        public static async Task PickMonth(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthPickerMarkup = Markup.PickMonth(command.Data.Date, dtfi, command.Data.LastCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthPickerMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }


            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        /// <summary>
        /// Выбор года
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickYear)]
        public static async Task PickYear(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthYearMarkup = Markup.PickYear(command.Data.Date, dtfi, command.Data.LastCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }


        /// <summary>
        /// Перелистывание месяца
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.ChangeTo)]
        public static async Task ChangeToHandler(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var calendarMarkup = Markup.Calendar(command.Data.Date, dtfi, command.Data.LastCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = calendarMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }

        }

        /// <summary>
        /// Обработка выбраной даты
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickDate)]
        public static async Task PickDate(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var type = command.Data.GetLastCommandEnum<CustomTHeader>();
                    var data = command.Data.Date;
                    //Обработка данных даты;
                    await Helpers.Message.Send(botClient, update, data.ToString());
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }
    }
}
