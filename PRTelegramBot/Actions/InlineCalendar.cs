using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Actions
{
    /// <summary>
    /// Класс обработчик календаря.
    /// </summary>
    public class InlineCalendar
    {
        #region Методы

        /// <summary>
        /// Действие выбор года или месяца.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.YearMonthPicker)]
        public static async Task PickYearMonth(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthYearMarkup = Markup.PickMonthYear(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                var bot = botClient.GetBotDataOrNull();
                bot.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Действие выбор месяца.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.PickMonth)]
        public static async Task PickMonth(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthPickerMarkup = Markup.PickMonth(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthPickerMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }


            }
            catch (Exception ex)
            {
                var bot = botClient.GetBotDataOrNull();
                bot.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Действие выбор года.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.PickYear)]
        public static async Task PickYear(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthYearMarkup = Markup.PickYear(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                var bot = botClient.GetBotDataOrNull();
                bot.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Действие перелистывание месяца.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.ChangeTo)]
        public static async Task ChangeToHandler(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var calendarMarkup = Markup.Calendar(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = calendarMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                var bot = botClient.GetBotDataOrNull();
                bot.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Действие обработка выбраной даты.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.PickDate)]
        public static async Task PickDate(ITelegramBotClient botClient, Update update)
        {
            var bot = botClient.GetBotDataOrNull();
            try
            {
                using (var inlineHandler = new InlineCallback<CalendarTCommand>(botClient, update))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    command.Data.ActionWithLastMessage = (int)ActionWithLastMessage.Delete;
                    var callBackHandler = new InlineCallback<CalendarTCommand>("", EnumHeaders.Instance.Get(command.Data.HeaderCallbackCommand), command.Data);
                    update.CallbackQuery.Data = callBackHandler.GetContent() as string;
                    await botClient.GetBotDataOrNull().Handler.HandleUpdateAsync(botClient, update, bot.Options.CancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
            }
        }

        #endregion
    }
}
