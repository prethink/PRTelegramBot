using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Helpers;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.TCommands;
using Telegram.Bot.Types;
using Telegram.Bot;
using Helpers = PRTelegramBot.Helpers;
using PRTelegramBot.Commands.Constants;
using PRTelegramBot.Models;
using PRTelegramBot.Extensions;
using ConsoleExample.Models;

namespace ConsoleExample.Examples
{
    public class ExamplePage
    {
        //Тестовые данные
        static List<string> pageData = new List<string>()
            {
                "Данные страница 1",
                "Данные страница 2",
                "Данные страница 3",
                "Данные страница 4",
                "Данные страница 5"
            };

        /// <summary>
        /// Напишите в чате "pages"
        /// </summary>
        [ReplyMenuHandler(true, "pages")]
        public static async Task ExamplePages(ITelegramBotClient botClient, Update update)
        {
            string msg = pageData[0];
            var data = await pageData.GetPaged<string>(1, 1);
            var generateMenu = PRTelegramBot.Helpers.TG.MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            var message = await Helpers.Message.Send(botClient, update, msg, option);
            update.GetCacheData<UserCache>().LastMessage = message;
        }

        /// <summary>
        /// callback обработка постраничного вывода
        /// Обрабатывает одну точку входа
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.NextPage, THeader.PreviousPage, THeader.CurrentPage)]
        public static async Task InlinenPage(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                if (update.CallbackQuery?.Data != null)
                {
                    var command = InlineCallback<PageTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                    if (command != null)
                    {
                        var data = await pageData.GetPaged<string>(command.Data.Page, 1);
                        var generateMenu = PRTelegramBot.Helpers.TG.MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount);
                        var pageResult = data.Results;
                        var option = new OptionMessage();
                        option.MenuInlineKeyboardMarkup = generateMenu;
                        string msg = "";
                        if(pageResult.Count > 0)
                        {
                            msg = pageResult.FirstOrDefault();
                        }
                        else
                        {
                            msg = "Нечего не найдено";
                        }
                        await Helpers.Message.Edit(botClient, update, msg, option);
                    }
                }

            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }


    }
}
