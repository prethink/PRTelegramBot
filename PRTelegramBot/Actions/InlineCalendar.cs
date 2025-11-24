using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;

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
        public static async Task PickYearMonth(IBotContext context)
        {
            try
            {
                var command = context.GetCommandByCallbackOrNull<CalendarTCommand>();
                if (command is not null)
                {
                    var monthYearMarkup = Markup.PickMonthYear(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(context.Current, ex));
            }
        }

        /// <summary>
        /// Действие выбор месяца.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.PickMonth)]
        public static async Task PickMonth(IBotContext context)
        {
            try
            {
                var command = context.GetCommandByCallbackOrNull<CalendarTCommand>();
                if (command is not null)
                {
                    var monthPickerMarkup = Markup.PickMonth(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthPickerMarkup;
                    await Helpers.Message.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }


            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(context.Current, ex));
            }
        }

        /// <summary>
        /// Действие выбор года.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.PickYear)]
        public static async Task PickYear(IBotContext context)
        {
            try
            {
                var command = context.GetCommandByCallbackOrNull<CalendarTCommand>();
                if (command is not null)
                {
                    var monthYearMarkup = Markup.PickYear(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(context.Current, ex));
            }
        }

        /// <summary>
        /// Действие перелистывание месяца.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.ChangeTo)]
        public static async Task ChangeToHandler(IBotContext context)
        {
            try
            {
                var command = context.GetCommandByCallbackOrNull<CalendarTCommand>();
                if (command is not null)
                {
                    var calendarMarkup = Markup.Calendar(command.Data.Date, CultureInfo.GetCultureInfo(command.Data.Culture, false), command.Data.HeaderCallbackCommand);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = calendarMarkup;
                    await Helpers.Message.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(context.Current, ex));
            }
        }

        /// <summary>
        /// Действие обработка выбраной даты.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.PickDate)]
        public static async Task PickDate(IBotContext context)
        {
            var bot = context.Current;
            try
            {
                using (var inlineHandler = new InlineCallback<CalendarTCommand>(context))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    command.Data.ActionWithLastMessage = (int)ActionWithLastMessage.Delete;
                    var callBackHandler = new InlineCallback<CalendarTCommand>(string.Empty, EnumHeaders.Instance.Get(command.Data.HeaderCallbackCommand), command.Data);
                    context.Update.CallbackQuery.Data = callBackHandler.GetContent() as string;
                    await bot.Handler.HandleUpdateAsync(context.BotClient, context.Update, bot.Options.CancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, ex));
            }
        }

        #endregion
    }
}
