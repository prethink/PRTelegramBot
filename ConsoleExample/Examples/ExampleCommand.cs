using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Attributes;
using PRTelegramBot.Commands.Constants;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models.Interface;
using PRTelegramBot.Helpers;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Helpers = PRTelegramBot.Helpers;
using Header = PRTelegramBot.Models.Enums.THeader;
using ConsoleExample.Models;

namespace ConsoleExample.Examples
{
    public class ExampleCommand
    {
        static int count = 0;
        #region Reply команды
        /// <summary>
        /// Напишите в чате "Пример"
        /// Напишите в чате /reply
        /// Пример с использование одновременно слеш команды и reply
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE)]
        [SlashHandler(SlashKeys.SL_EXAMPLE_WITH_REPLY)]
        public static async Task ExampleReply(ITelegramBotClient botClient, Update update)
        {
            //Пример как получить текст сообщения из JSON файла
            string msg = DictionaryJSON.GetMessage(nameof(MessageKeys.MSG_EXAMPLE_TEXT));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чате "ReplyMenu"
        /// Пример генерации reply меню
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_REPLY_MENU)]
        public static async Task ExampleReplyMenu(ITelegramBotClient botClient, Update update)
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
            menuList.Add(KeyboardButton.WithRequestChat("Отправить группу боту", new KeyboardButtonRequestChat() { RequestId = 2}));
            //Добавляем кнопку с запросом отправки пользователя боту
            menuList.Add(KeyboardButton.WithRequestUser("Отправить пользователя боту", new KeyboardButtonRequestUser() { RequestId = 1}));
            //Добавляем кнопку с отправкой опроса
            menuList.Add(KeyboardButton.WithRequestPoll("Отправить свою голосование"));
            //Добавляем кнопку с запросом работы с WebApp
            menuList.Add(KeyboardButton.WithWebApp("WebApp", new WebAppInfo() { Url = "https://prethink.github.io/telegram/webapp.html"}));
 
            //Генерируем reply меню
            //1 столбец, коллекция пунктов меню, вертикальное растягивание меню, пункт в самом низу по умолчанию
            var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Главное меню");
            //Добавляем в настройки меню
            option.MenuReplyKeyboardMarkup = menu;
            await Helpers.Message.Send(botClient, update, msg, option);
        }

        /// <summary>
        /// Напишите в чате "Скобки"
        /// Пример если в кнопки должно отображаться количество в скобках (2)
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.PR_EXAMPLE_BRACKETS)]
        public static async Task ExampleBracket(ITelegramBotClient botClient, Update update)
        {
            
            string msg = $"Значени {count}";
            //Создаем настройки сообщения
            var option = new OptionMessage();
            //Создаем список для меню
            var menuList = new List<KeyboardButton>();
            //Добавляем кнопку с текстом
            menuList.Add(new KeyboardButton(ReplyKeys.PR_EXAMPLE_BRACKETS+$" ({count})"));
            //Генерируем reply меню
            //1 столбец, коллекция пунктов меню, вертикальное растягивание меню, пункт в самом низу по умолчанию
            var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Главное меню");
            //Добавляем в настройки меню
            option.MenuReplyKeyboardMarkup = menu;
            await Helpers.Message.Send(botClient, update, msg, option);
            count++;
        }

        /// <summary>
        /// Напишите в чате "Пример 1" или "Пример 2"
        /// Пример с использованием разных reply команд для вывода одной и той же функции
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_ONE, ReplyKeys.RP_EXAMPLE_TWO)]
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task ExampleReplyMany(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Вы написали в чате {update.Message.Text}";
            await Helpers.Message.Send(botClient, update, msg);
        }


        /// <summary>
        /// Напишите в чате "AppConfig Кнопка" - значение можно поменять в appconfig.json
        /// Пример работы с кнопкой из JSON файла
        /// [RequiredTypeUpdate] Пример того что метод будет обрабатывать обновление только приватного чата
        /// [RequireDate]Пример того что метод будет обрабатывать только текстовые сообщения
        /// </summary>
        [ReplyMenuHandler(true, nameof(ReplyKeys.RP_EXAMPLE_FROM_JSON))]
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        [RequireTypeMessage(Telegram.Bot.Types.Enums.MessageType.Text)]
        public static async Task ExampleReplyJsonConfig(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Вы написали в чате {update.Message.Text} можете изменить значение команды в настройке appconfig.json";
            await Helpers.Message.Send(botClient, update, msg);
        }

        #endregion

        #region Inline команды
        /// <summary>
        /// Напишите в чате "InlineMenu"
        /// Пример с генерацией inline меню
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_INLINE_MENU)]
        public static async Task InlineMenu(ITelegramBotClient botClient, Update update)
        {
            /* Создание новой кнопки с callback данными
             * MessageKeys.GetValueButton(nameof(InlineKeys.IN_EXAMPLE_ONE)) - Название кнопки из JSON
             * Models.Enums.CallbackId.ExampleOne - Заголовок команды
             */
            var exampleItemOne = new InlineCallback(DictionaryJSON.GetButton(nameof(InlineKeys.IN_EXAMPLE_ONE)), CustomTHeader.ExampleOne);
            /* Создание новой кнопки с callback данными
             * InlineKeys.IN_EXAMPLE_TWO - Название кнопки из константы
             * Models.Enums.CallbackId.ExampleOne - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemTwo = new InlineCallback<EntityTCommand>(InlineKeys.IN_EXAMPLE_TWO, CustomTHeader.ExampleTwo, new EntityTCommand(2));
            /* Создание новой кнопки с callback данными
             * Models.Enums.CallbackId.ExampleOne - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemThree = new InlineCallback<EntityTCommand>("Пример 3", CustomTHeader.ExampleThree, new EntityTCommand(3));
            // Создает inline кнопку с ссылкой
            var url = new InlineURL("Google", "https://google.com");
            // Создаем кнопку для работы с webApp
            var webdata = new InlineWebApp("WA", "https://prethink.github.io/telegram/webapp.html");

            //IInlineContent - реализуют все inline кнопки
            List<IInlineContent> menu = new();
            
            menu.Add(exampleItemOne);
            menu.Add(exampleItemTwo);
            menu.Add(exampleItemThree);
            menu.Add(url);
            menu.Add(webdata);

            //Генерация меню на основе данных в 1 столбец
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);

            //Создание настроек для передачи в сообщение
            var option = new OptionMessage();
            //Передача меню в настройки
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "Пример работы меню";
            //Отправка сообщение с меню
            await Helpers.Message.Send(botClient, update, msg, option);
        }
        #endregion

        #region Слеш команды
        /// <summary>
        /// напиши команду в чате /example
        /// </summary>
        [SlashHandler(SlashKeys.SL_EXAMPLE)]
        public static async Task SlashCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Команда {SlashKeys.SL_EXAMPLE}";
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// напиши команду в чате /get_1
        /// </summary>
        [SlashHandler(SlashKeys.SL_EXAMPLE_GET)]
        public static async Task SlashCommandGet(ITelegramBotClient botClient, Update update)
        {
            if (update.Message.Text.Contains("_"))
            {
                var spl = update.Message.Text.Split("_");
                if (spl.Length > 1)
                {
                    string msg = $"Команда {SlashKeys.SL_EXAMPLE_GET} со значением {spl[1]}";
                    await Helpers.Message.Send(botClient, update, msg);
                }
                else
                {
                    string msg = $"Команда {SlashKeys.SL_EXAMPLE_GET}";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            else
            {
                string msg = $"Команда {SlashKeys.SL_EXAMPLE_GET}";
                await Helpers.Message.Send(botClient, update, msg);
            }
        }
        #endregion
    }
}
