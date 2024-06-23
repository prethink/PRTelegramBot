using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.TCommands;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    public class ExamplePage
    {
        //Тестовые данные 1
        static List<string> pageData = new List<string>()
            {
                "Данные страница 1",
                "Данные страница 2",
                "Данные страница 3",
                "Данные страница 4",
                "Данные страница 5"
            };

        //Тестовые данные 2
        static List<string> pageDataTwo = new List<string>()
            {
                "TestДанные страница 1",
                "TestДанные страница 2",
                "TestДанные страница 3",
                "TestДанные страница 4",
                "TestДанные страница 5"
            };

        /// <summary>
        /// Напишите в чате "pages"
        /// </summary>
        [ReplyMenuHandler("pages")]
        public static async Task ExamplePages(ITelegramBotClient botClient, Update update)
        {
            //Беру текст для первого сообщения
            string msg = pageData[0];
            //Получаю контент с 1 страницы с размером страницы 1
            var data = await pageData.GetPaged<string>(1, 1);
            //Генерирую меню постраничного вывода с заголовком
            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            var message = await Helpers.Message.Send(botClient, update, msg, option);
        }

        /// <summary>
        /// Напишите в чате "pagestwo"
        /// </summary>
        [ReplyMenuHandler("pagestwo")]
        public static async Task ExamplePagesTwo(ITelegramBotClient botClient, Update update)
        {
            //Беру текст для первого сообщения
            string msg = pageDataTwo[0];
            //Получаю контент с 1 страницы с размером страницы 1
            var data = await pageDataTwo.GetPaged<string>(1, 1);
            //Генерирую меню постраничного вывода с заголовком
            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader2);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;

            var message = await Helpers.Message.Send(botClient, update, msg, option);
        }

        /// <summary>
        /// callback обработка постраничного вывода
        /// Обрабатывает одну точку входа
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage, PRTelegramBotCommand.PreviousPage, PRTelegramBotCommand.CurrentPage)]
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
                        //Получаю заголовок из данных
                        CustomTHeaderTwo header = (CustomTHeaderTwo)command.Data.Header;
                        //обрабатываю данные по заголовку
                        if(header == CustomTHeaderTwo.CustomPageHeader)
                        {
                            //Получаю номер страницы и указываю размер страницы
                            var data = await pageData.GetPaged<string>(command.Data.Page, 1);
                            //Генерирую постраничное меню
                            var button = new InlineCallback("⭐", CustomTHeader.CustomButton);
                            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader,button: button);
                            //Получаю результат из постраничного вывода
                            var pageResult = data.Results;
                            var option = new OptionMessage();
                            option.MenuInlineKeyboardMarkup = generateMenu;
                            string msg = "";
                            if (pageResult.Count > 0)
                            {
                                msg = pageResult.FirstOrDefault();
                            }
                            else
                            {
                                msg = "Нечего не найдено";
                            }
                            //Редактирую текущую страницу
                            await Helpers.Message.Edit(botClient, update, msg, option);
                        }
                        //обрабатываю данные по заголовку
                        else if (header == CustomTHeaderTwo.CustomPageHeader2)
                        {
                            //Получаю номер страницы и указываю размер страницы
                            var data = await pageDataTwo.GetPaged<string>(command.Data.Page, 1);
                            //Генерирую постраничное меню
                            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader2);
                            //Получаю результат из постраничного вывода
                            var pageResult = data.Results;
                            var option = new OptionMessage();
                            option.MenuInlineKeyboardMarkup = generateMenu;
                            string msg = "";
                            if (pageResult.Count > 0)
                            {
                                msg = pageResult.FirstOrDefault();
                            }
                            else
                            {
                                msg = "Нечего не найдено";
                            }
                            //Редактирую текущую страницу
                            await Helpers.Message.Edit(botClient, update, msg, option);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.CustomButton)]
        public static async Task FavoriteMessage(ITelegramBotClient botClient, Update update)
        {
            string msg = "Меню";
            //Создаем настройки сообщения
            var option = new OptionMessage();
            //Создаем список для меню
            var menuList = new List<KeyboardButton>();
            //Добавляем кнопку с текстом
            menuList.Add(new KeyboardButton("Кнопка 1"));
            //Добавляем кнопку с запросом на контакт пользователя
            menuList.Add(KeyboardButton.WithRequestContact("Отправить свой контакт"));
            //Добавляем кнопку с запросом на локацию пользователя
            menuList.Add(KeyboardButton.WithRequestLocation("Отправить свою локацию"));
            //Добавляем кнопку с запросом отправки чата боту
            menuList.Add(KeyboardButton.WithRequestChat("Отправить группу боту", new KeyboardButtonRequestChat(2, true) ));
            //Добавляем кнопку с запросом отправки пользователя боту
            menuList.Add(KeyboardButton.WithRequestUsers("Отправить пользователя боту", new KeyboardButtonRequestUsers() { RequestId = 1 }));
            //Добавляем кнопку с отправкой опроса
            menuList.Add(KeyboardButton.WithRequestPoll("Отправить свою голосование"));
            //Добавляем кнопку с запросом работы с WebApp
            menuList.Add(KeyboardButton.WithWebApp("WebApp", new WebAppInfo() { Url = "https://prethink.github.io/telegram/webapp.html" }));

            //Генерируем reply меню
            //1 столбец, коллекция пунктов меню, вертикальное растягивание меню, пункт в самом низу по умолчанию
            var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Главное меню");
            //Добавляем в настройки меню
            option.MenuReplyKeyboardMarkup = menu;
            await Helpers.Message.Send(botClient, update, msg, option);
        }
    }
}
