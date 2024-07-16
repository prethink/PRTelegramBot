using ConsoleExample.Examples.Events;
using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using PRTelegramBot.Extensions;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleReplyCommands
    {
        static int count = 0;

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает если 'Команда содержит текст' будет содержаться в тексте сообщения.
        /// Так же при проверки будет проигнорирован регистр команды.
        /// </summary>
        [ReplyMenuHandler(CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, "Команда содержит текст")]
        public static async Task ReplyExampleOne(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ReplyExampleOne);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает если 'Точное совпадение команды' будет точное совпадения текста сообщения за исключением регистра.
        /// </summary>
        [ReplyMenuHandler("Точное совпадение команды")]
        public static async Task ReplyExampleTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ReplyExampleTwo);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Напишите в чате "Пример 1" или "Пример 2".
        /// Пример с использованием разных reply команд для работы с 1 функцией.
        /// </summary>
        [ReplyMenuHandler("Пример 1", "Пример 2")]
        public static async Task ExampleReplyMany(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleReplyMany);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Меню".
        /// В результате сгенерируется меню.
        /// </summary>
        [ReplyMenuHandler("Reply Меню")]
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
            menuList.Add(KeyboardButton.WithRequestChat("Отправить группу боту", new KeyboardButtonRequestChat(2, true)));
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

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Пример динамического текста сообщения".
        /// Пример работы с текстом из json файла.
        /// Настройка конфигурационных файла при создание экземпляра PRBot <see cref="Program"/>
        /// </summary>
        [ReplyMenuHandler("Пример динамического текста сообщения")]
        public static async Task ExampleDynamicReply(ITelegramBotClient botClient, Update update)
        {
            /*
             *  В program.cs создается экземпляр бота:
             *   
             *  var telegram = new PRBotBuilder("")
             *      .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
             *      .Build();
             *  
             *  AddConfigPath - добавляет путь для конфигурационного файла.
             *  ExampleConstants.MESSAGES_FILE_KEY - ключ 
             *  ".\\Configs\\messages.json" - путь до конфигурационного файла.
             *  
             */

            /*
             *  botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.MESSAGES_FILE_KEY, "MSG_EXAMPLE_TEXT")
             *  BotConfigJsonProvider - провайдер который работает с json файлами.
             *  string - возращаемый тип.
             *  ExampleConstants.MESSAGES_FILE_KEY - ключ конфига.
             *  MSG_EXAMPLE_TEXT - ключ текста сообщения из json файла messages.json
             * 
             */

            // Получаем текст сообщения по ключу из json файла.
            string msg = botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.MESSAGES_FILE_KEY, "MSG_EXAMPLE_TEXT");
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Скобки".
        /// Пример работы меню со скобками.
        /// </summary>
        [ReplyMenuHandler("Скобки")]
        public static async Task ExampleBracket(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Значени {count}";
            //Создаем настройки сообщения
            var option = new OptionMessage();
            //Создаем список для меню
            var menuList = new List<KeyboardButton>();
            //Добавляем кнопку с текстом
            menuList.Add(new KeyboardButton($"Скобки ({count})"));
            //Генерируем reply меню
            //1 столбец, коллекция пунктов меню, вертикальное растягивание меню, пункт в самом низу по умолчанию
            var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Главное меню");
            //Добавляем в настройки меню
            option.MenuReplyKeyboardMarkup = menu;
            await Helpers.Message.Send(botClient, update, msg, option);
            count++;
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Проверка доступа".
        /// Перед выполнение метода срабатывает событие проверки привилегий <see cref="ExampleEvents.OnCheckPrivilege"/>
        /// </summary>
        [Access((int)(UserPrivilege.Guest | UserPrivilege.Registered))]
        [ReplyMenuHandler("Проверка доступа")]
        public static async Task ExampleAccess(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleAccess);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат значения по ключу "DYNAMIC_COMMANT_EXAMPLE" из файла commands.json.
        /// Настройка конфигурационных файла при создание экземпляра PRBot <see cref="Program"/>
        /// "DYNAMIC_COMMANT_EXAMPLE": "Динамическая команда"
        /// </summary>
        [ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMANT_EXAMPLE))]
        public static async Task ExampleReplyDynamicCommand(ITelegramBotClient botClient, Update update)
        {
            /*
             *  Создание провайдера работы с json файлом commands.json
             *  var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
             *  
             *  Выгрузка всех команд в формате ключ:значение
             *  var dynamicCommands = botJsonProvider.GetKeysAndValues();
             *
             *  var telegram = new PRBotBuilder("")
             *                      .AddReplyDynamicCommands(dynamicCommands)
             *                      .Build();
             * 
             * .AddReplyDynamicCommands(dynamicCommands) - добавляет в список все динамические команды.
             * 
             * [ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMANT_EXAMPLE))] - работа динамической команды по ключу DYNAMIC_COMMANT_EXAMPLE
             */

            string msg = nameof(ExampleReplyDynamicCommand);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чате "Приватная команда"
        /// Требуемый чат должнен быть приватным.
        /// </summary>
        [ReplyMenuHandler("Приватная команда")]
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task ExampleReplyRequeretPrivate(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleReplyRequeretPrivate);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чате "Сообщение только из текста"
        /// Требуемый тип сообщения должен содержать только текст.
        /// </summary>
        [ReplyMenuHandler("Сообщение только из текста")]
        [RequireTypeMessage(Telegram.Bot.Types.Enums.MessageType.Text)]
        public static async Task ExampleReplyRequiredText(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleReplyRequiredText);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 1.
        /// Команда отработает при написание в чат "Пример команды для бота id 1".
        /// Пример работы с текстом из json файла.
        /// </summary>
        [ReplyMenuHandler(1, "Пример команды для бота id 1")]
        public static async Task ExampleReplyBotIdOne(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleReplyBotIdOne);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для любого бота с любым botid.
        /// Команда отработает при написание в чат "Команда для всех ботов".
        /// </summary>
        [ReplyMenuHandler(-1, "Команда для всех ботов")]
        public static async Task ReplyExampleAllBots(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ReplyExampleAllBots);
            await Helpers.Message.Send(botClient, update, msg);
        }
    }
}
