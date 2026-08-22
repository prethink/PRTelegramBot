using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;

namespace PRTelegramBot.Actions
{
    /// <summary>
    /// Calendar handler class.
    /// </summary>
    public class InlineCalendar
    {
        #region Methods

        /// <summary>
        /// Action: selecting a year or a month.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.YearMonthPicker)]
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
                    await MessageEditor.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineCalendar>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Action: selecting a month.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.PickMonth)]
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
                    await MessageEditor.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }


            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineCalendar>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Action: selecting a year.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.PickYear)]
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
                    await MessageEditor.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineCalendar>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Action: paging through the months.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.ChangeTo)]
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
                    await MessageEditor.EditInline(context, context.Update.CallbackQuery.Message.Chat.Id, context.Update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineCalendar>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Action: handling the selected date.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.PickDate)]
        public static async Task PickDate(IBotContext context)
        {
            try
            {
                using (var inlineHandler = new InlineCallback<CalendarTCommand>(context))
                {
                    var bot = context.Current;
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    command.Data.ActionWithLastMessage = (int)ActionWithLastMessage.Delete;
                    var callBackHandler = new InlineCallback<CalendarTCommand>(string.Empty, EnumHeaders.Instance.Get(command.Data.HeaderCallbackCommand), command.Data);
                    context.Update.CallbackQuery.Data = callBackHandler.GetContent() as string;
                    await bot.Handler.HandleUpdateAsync(context.BotClient, context.Update, bot.Options.CancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineCalendar>().LogErrorInternal(ex);
            }
        }

        #endregion
    }
}
