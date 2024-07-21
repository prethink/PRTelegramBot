using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    public class ExampleCalendar
    {
        /// <summary>
        /// Напишите в чат Calendar
        /// Вызов команды календаря
        /// </summary>
        [ReplyMenuHandler("Calendar")]
        public static async Task PickCalendar(ITelegramBotClient botClient, Update update)
        {
            try
            {
                await CalendarUtils.Create(botClient, update, CustomTHeader.CalendarCallback, "Выберите дату:");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Напишите в чат EngCalendar
        /// Вызов команды календаря на английском языке
        /// </summary>
        [ReplyMenuHandler("EngCalendar")]
        public static async Task EngPickCalendar(ITelegramBotClient botClient, Update update)
        {
            try
            {
                await CalendarUtils.Create(botClient, update, CultureInfo.GetCultureInfo("en-US", false), CustomTHeader.CalendarCallback, "Choose date:");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Обработка выбраной даты
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.CalendarCallback)]
        public static async Task PickDate(ITelegramBotClient botClient, Update update)
        {
            var bot = botClient.GetBotDataOrNull();
            try
            {
                using (var inlineHandler = new InlineCallback<CalendarTCommand>(botClient, update))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    await Helpers.Message.Send(botClient, update, command.Data.Date.ToString());
                }
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
            }
        }
    }
}
