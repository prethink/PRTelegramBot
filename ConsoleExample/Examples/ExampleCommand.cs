using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    public class ExampleCommand
    {
        static int count = 0;

        #region Reply команды

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
        /// Перед выполнение метода срабатывает событие проверки привилегий <see cref="ExampleEvent.OnCheckPrivilege"/>
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

        #endregion

        #region Inline команды

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "InlineMenu".
        /// Пример с генерацией inline меню
        /// Настройка конфигурационных файла при создание экземпляра PRBot <see cref="Program"/>
        /// </summary>
        [ReplyMenuHandler("InlineMenu")]
        public static async Task InlineMenu(ITelegramBotClient botClient, Update update)
        {
            /*
             *  В program.cs создается экземпляр бота:
             *   
             *  var telegram = new PRBotBuilder("")
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
             *      .Build();
             *  
             *  AddConfigPath - добавляет путь для конфигурационного файла.
             *  ExampleConstants.BUTTONS_FILE_KEY - ключ 
             *  ".\\Configs\\buttons.json" - путь до конфигурационного файла.
             *  
             */

            /*
             *  botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE")
             *  BotConfigJsonProvider - провайдер который работает с json файлами.
             *  string - возращаемый тип.
             *  ExampleConstants.BUTTONS_FILE_KEY - ключ конфига.
             *  IN_EXAMPLE_ONE - ключ текста кнопки из json файла buttons.json
             * 
             */

            /* Создание новой кнопки с callback данными
             * botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE") - Название кнопки из json
             * CustomTHeaderTwo.ExampleOne - Заголовок команды
             */
            var exampleItemOne = new InlineCallback(botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE"), CustomTHeaderTwo.ExampleOne);
            /* Создание новой кнопки с callback данными
             * InlineKeys.IN_EXAMPLE_TWO - Название кнопки из константы
             * CustomTHeaderTwo.ExampleTwo - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemTwo = new InlineCallback<EntityTCommand<long>>("Пример 2", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(2));
            /* Создание новой кнопки с callback данными
             * CustomTHeaderTwo.ExampleThree - Заголовок команды
             * new EntityTCommand(3) - Данные которые требуется передать
             */
            var exampleItemThree = new InlineCallback<EntityTCommand<long>>("Пример 3", CustomTHeaderTwo.ExampleThree, new EntityTCommand<long>(3));

            var inlineStep = new InlineCallback("Inline Step", CustomTHeader.InlineWithStep);

            //Команды который добавлены после запуска бота
            var exampleAddCommand = new InlineCallback("Команда добавленная динамически 1", AddCustomTHeader.TestAddCommand);
            var exampleAddCommandTwo = new InlineCallback("Команда добавленная динамически 2", AddCustomTHeader.TestAddCommandTwo);

            // Создает inline кнопку с ссылкой
            var url = new InlineURL("Google", "https://google.com");
            // Создаем кнопку для работы с webApp
            var webdata = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

            //IInlineContent - реализуют все inline кнопки
            List<IInlineContent> menu = new();
            menu.Add(exampleItemOne);
            menu.Add(exampleItemTwo);
            menu.Add(exampleItemThree);
            menu.Add(exampleAddCommand);
            menu.Add(exampleAddCommandTwo);
            menu.Add(inlineStep);
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

        #region Slash команды

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/example".
        /// </summary>
        [SlashHandler("/example")]
        public static async Task ExampleSlashCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Команда /example";
            msg += "\n /get_1 - команда 1" +
                "\n /get_2 - команда 2" +
                "\n /get_3 - команда 3" +
                "\n /get_4 - команда 4";
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/get".
        /// Команда отработает при написание в чат "/get_1", значение 1 можно обработать.
        /// </summary>
        [SlashHandler("/get")]
        public static async Task ExampleSlashCommandGet(ITelegramBotClient botClient, Update update)
        {
            if (update.Message.Text.Contains("_"))
            {
                var spl = update.Message.Text.Split("_");
                if (spl.Length > 1)
                {
                    string msg = $"Команда /get со значением {spl[1]}";
                    await Helpers.Message.Send(botClient, update, msg);
                }
                else
                {
                    string msg = $"Команда /get";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            else
            {
                string msg = $"Команда /get";
                await Helpers.Message.Send(botClient, update, msg);
            }
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/equals", сработает только если текст сообщения будет /equals но при этом регистро не зависимо.
        /// /equals_1 не сработает.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, "/equals")]
        public static async Task ExampleSlashEqualsCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleSlashEqualsCommand);
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "/equalsreg", сработает только если текст сообщения будет /equalsreg но при этом регистро зависимо.
        /// Не сработает/equals_1, /equalsreG, /Equalsreg.
        /// </summary>
        [SlashHandler(CommandComparison.Equals, StringComparison.Ordinal, "/equalsreg")]
        public static async Task ExampleSlashEqualsRegisterCommand(ITelegramBotClient botClient, Update update)
        {
            string msg = nameof(ExampleSlashEqualsRegisterCommand);
            await Helpers.Message.Send(botClient, update, msg);
        }

        #endregion
    }
}
