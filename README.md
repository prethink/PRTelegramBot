Уроки по PRTelegramBot - [prethink.ru](https://prethink.ru/category/uroki/)

# 📰 Описание
Библиотека для быстрого и удобного создания telegram ботов.   
Пример использования в консольном приложении. https://github.com/prethink/PRTelegramBot/tree/master/ConsoleExample

# 💎 Возможности

 - Регистрация и автоматическая обработка команд (reply) и ответов;
 - Обработка команд, содержащих скобки, например, "Лайки (5)";
 - Создание меню ответов;
 - Регистрация и обработка встроенных (Inline) команд;
 - Генерация встроенных кнопок и меню;
 - Работа с inline календарем;
 - Постраничный вывод сообщений;
 - Регистрация и обработка команд с использованием слеша [/];
 - Работа со словарем;
 - Выполнение команд пошагово;
 - Хранение кэша данных пользователей;
 - Возможность ограничить доступ к определенным функциям только выбранным пользователям;
 - Возможность добавления администраторов для управления телеграм-ботом;
 - Возможность использования белого списка пользователей, которые могут пользоваться ботом.

# 🔑 Зависимости

 - TelegramBot v19.0.0 https://github.com/TelegramBots/Telegram.Bot
 - Microsoft.Extensions.Configuration.Binder v7.0.0 https://github.com/dotnet/runtime
 - Microsoft.Extensions.Configuration.Json v7.0.0 https://github.com/dotnet/runtime
# 🧱 Интегрированные пакеты
 - CalendarPicker | karb0f0s   https://github.com/karb0f0s/CalendarPicker

# 🎌 Быстрый старт
## Настройка конфигурации
Для работы бота требуется скопировать токен из бота BotFather и вставить в файл  telegram.json в поле Token из секции TelegramConfig, этого достаточно чтобы бот мог запуститься.

> Для получения токена от BotFather в Telegram, следуйте этим шагам:
> 
> 1. Откройте Telegram и найдите "BotFather". Это официальный бот Telegram, который поможет вам создать и настроить своего бота.
> 2. Начните диалог с BotFather, нажав кнопку "Start" или отправив ему команду "/start".
> 3. Отправьте команду "/newbot", чтобы создать нового бота.
> 4. BotFather попросит вас ввести имя для вашего бота. Введите желаемое имя и следуйте инструкциям.
> 5. После успешного создания бота BotFather выдаст вам токен доступа. Это будет выглядеть примерно так: "1234567890:ABCDEFGHIJKLMNOPQRSTUVXYZ".
> 6. Скопируйте полученный токен. Этот токен уникален для вашего бота и используется для аутентификации и взаимодействия с API Telegram.

### 📔 Configs/telegram.json
telegram - хранит информацию о подключении к базе данных и телеграм api;
```json
//После изменения конфигурации обязательно перезапустите программу!!!
{
  "TelegramConfig": {
    // Токен для телеграм бота
    "Token": "",
    //Идентификаторы администраторов бота, 
    //Пример Admins": [5125555, 23542352, 32452352, 34534534],
    "Admins": [],
    //Белый список пользователей, которые могут пользоваться ботом, если список пустой ботом могут пользоваться все, 
    //Пример WhiteListUsers": [5125555, 23542352, 32452352, 34534534],
    "WhiteListUsers": [],
    //Показывать или нет что значение кнопок не найдено в словаре
    "ShowErrorNotFoundNameButton": false
  }
}
```

### 📔 Configs/appconfig.json
appconfig - хранит информацию настройки программы, и позволяет менять их без необходимости перекомпиляции. Данные которые хранит:
- Дополнительные переменные;
- Словарик для текстовых сообщений;
```json
    "Messages": {
      "MSG_MAIN_MENU": "Главное меню",
      "MSG_EXAMPLE_TEXT": "Пример текста сообщения из appconfig.json"
    }
 ```
- Словарик для наименований кнопок.
```json
    "Buttons": {
      "RP_START": "/start",
      "RP_MENU": "/menu",
      "RP_MAIN_MENU": "🏠 Главное меню",
      "RP_EXAMPLE_FROM_JSON": "AppConfig Кнопка",
      "IN_EXAMPLE_ONE": "Пример 1 appconfig.json"
    }
```
### Создание своих методов команд
> В вашей программе можно создавать функции в любом классе, при условии, что сигнатура метода соответствует заданному примеру.    
> Для автоматического поиска команд используется класс "ReflectionFinder".   
```csharp
        [Атрибуты обработки]
        public static async Task НазваниеМетода(ITelegramBotClient название, Update название)
        {
            //Тело функции
        }

```


Пример запуска через ***Program.cs*** в консольном приложение   
```csharp
using NLog;
using static PRTelegramBot.Core.TelegramService;
using PRTelegramBot.Extensions;
using PRTelegramBot.Core;
using PRTelegramBot.Configs;
using ConsoleExample.Examples;

//Конфигурация NLog
NLogConfigurate.Configurate();
//Словарик для логгеров
Dictionary<string, Logger> LoggersContainer = new Dictionary<string, Logger>();
//Команда для завершения приложения
const string EXIT_COMMAND = "exit";

//Запуск программы
Console.WriteLine("Запуск программы");
Console.WriteLine($"Для закрытие программы напишите {EXIT_COMMAND}");

#region запуск телеграм бота
var telegram = TelegramService.GetInstance();

//Подписка на простые логи
telegram.OnLogCommon                += Telegram_OnLogCommon;
//Подписка на логи с ошибками
telegram.OnLogError                 += Telegram_OnLogError;
//Запуск работы бота
await telegram.Start();

if(telegram.Handler != null)
{
    //Обработка обновление кроме message и callback
    telegram.Handler.OnUpdate                       += Handler_OnUpdate;

    //Обработка не правильный тип сообщений
    telegram.Handler.Router.OnWrongTypeMessage      += ExampleEvent.OnWrongTypeMessage;

    //Обработка пользователь написал в чат start с deeplink
    telegram.Handler.Router.OnUserStartWithArgs     += ExampleEvent.OnUserStartWithArgs;

    //Обработка проверка привилегий
    telegram.Handler.Router.OnCheckPrivilege        += ExampleEvent.OnCheckPrivilege;

    //Обработка пропущенной  команды
    telegram.Handler.Router.OnMissingCommand        += ExampleEvent.OnMissingCommand;

    //Обработка не верного типа чата
    telegram.Handler.Router.OnWrongTypeChat         += ExampleEvent.OnWrongTypeChat;

    //Обработка локаций
    telegram.Handler.Router.OnLocationHandle        += ExampleEvent.OnLocationHandle;

    //Обработка контактных данных
    telegram.Handler.Router.OnContactHandle         += ExampleEvent.OnContactHandle;

    //Обработка голосований
    telegram.Handler.Router.OnPollHandle            += ExampleEvent.OnPollHandle;

    //Обработка WebApps
    telegram.Handler.Router.OnWebAppsHandle         += ExampleEvent.OnWebAppsHandle;

    //Обработка, пользователю отказано в доступе
    telegram.Handler.Router.OnAccessDenied          += ExampleEvent.OnAccessDenied;

    //Обработка сообщения с документом
    telegram.Handler.Router.OnDocumentHandle        += ExampleEvent.OnDocumentHandle;

    //Обработка сообщения с аудио
    telegram.Handler.Router.OnAudioHandle           += ExampleEvent.OnAudioHandle;

    //Обработка сообщения с видео
    telegram.Handler.Router.OnVideoHandle           += ExampleEvent.OnVideoHandle;

    //Обработка сообщения с фото
    telegram.Handler.Router.OnPhotoHandle           += ExampleEvent.OnPhotoHandle;

    //Обработка сообщения с стикером
    telegram.Handler.Router.OnStickerHandle         += ExampleEvent.OnStickerHandle;

    //Обработка сообщения с голосовым сообщением
    telegram.Handler.Router.OnVoiceHandle           += ExampleEvent.OnVoiceHandle;

    //Обработка сообщения с неизвестным типом
    telegram.Handler.Router.OnUnknownHandle         += ExampleEvent.OnUnknownHandle;

    //Обработка сообщения с местоположением
    telegram.Handler.Router.OnVenueHandle           += ExampleEvent.OnVenueHandle;

    //Обработка сообщения с игрой
    telegram.Handler.Router.OnGameHandle            += ExampleEvent.OnGameHandle;

    //Обработка сообщения с видеозаметкой
    telegram.Handler.Router.OnVideoNoteHandle       += ExampleEvent.OnVideoNoteHandle;

    //Обработка сообщения с игральной костью
    telegram.Handler.Router.OnDiceHandle            += ExampleEvent.OnDiceHandle;

}

#endregion

#region Работа фоновых задач
var tasker = new Tasker(10);
tasker.Start();
#endregion



#region Логи
//Обработка ошибок
void Telegram_OnLogError(Exception ex, long? id = null)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string errorMsg = $"{DateTime.Now}: {ex.ToString()}";


    if (ex is Telegram.Bot.Exceptions.ApiRequestException apiEx)
    {
        errorMsg = $"{DateTime.Now}: {apiEx.ToString()}";
        if (apiEx.Message.Contains("Forbidden: bot was blocked by the user"))
        {
            string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
            Telegram_OnLogCommon(msg, TelegramEvents.BlockedBot, ConsoleColor.Red);
            return;
        }
        else if (apiEx.Message.Contains("BUTTON_USER_PRIVACY_RESTRICTED"))
        {
            string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
            Telegram_OnLogCommon(msg, TelegramEvents.BlockedBot, ConsoleColor.Red);
            return;
        }
        else if (apiEx.Message.Contains("group chat was upgraded to a supergroup chat"))
        {
            errorMsg += $"\n newChatId: {apiEx?.Parameters?.MigrateToChatId.GetValueOrDefault()}";
        }

    }

    if (LoggersContainer.TryGetValue("Error", out var logger))
    {
        logger.Error(errorMsg);
    }
    else
    {
        var nextLogger = LogManager.GetLogger("Error");
        nextLogger.Error(errorMsg);
        LoggersContainer.Add("Error", nextLogger);
    }
    Console.WriteLine(errorMsg);
    Console.ResetColor();
}

//Обработка общих логов
void Telegram_OnLogCommon(string msg, TelegramEvents eventType, ConsoleColor color = ConsoleColor.Blue)
{
    Console.ForegroundColor = color;
    string formatMsg = $"{DateTime.Now}: {msg}";
    Console.WriteLine(formatMsg);
    Console.ResetColor();

    if (LoggersContainer.TryGetValue(eventType.GetDescription(), out var logger))
    {
        logger.Info(formatMsg);
    }
    else
    {
        var nextLogger = LogManager.GetLogger(eventType.GetDescription());
        nextLogger.Info(formatMsg);
        LoggersContainer.Add(eventType.GetDescription(), nextLogger);
    }
}
#endregion

//Ожидание ввода команды чтобы приложение не закрылось
while (true)
{
    var result = Console.ReadLine();
    if (result.ToLower() == EXIT_COMMAND)
    {
        Environment.Exit(0);
    }
}
```

Ниже будут приведены примеры как создавать свои команды обработки для телеграма.

## Примеры использования
Примеры готовых функций есть в консольном приложение по пути ***/Examples***
- ***ExampleCalendar.cs*** - Пример работы с календарем.
- ***ExampleCommand.cs*** - Пример как создавать reply, inline и слеш команды.
- ***ExampleHandlers.cs*** - Пример как можно обрабатывать callback данные.
- ***ExampleStepCommand.cs*** - Пример c пошаговым выполнением команд.
- ***ExampleUserCache.cs*** - Пример c кэш данными пользователя.    
- ***ExampleEvent.cs*** - Пример обработки разных типов сообщений.    
- ***WebApp.html*** - Скромный пример что требуется для WebApp страницы.   


### Работа с Reply командами

> Для регистрации и обработки Reply команд используется ***ReplyMenuHandlerAttribute***.    
> Первый аргумент атрибута проверяет является ли команда приоритетной. Используется при работе с шагами, ниже приведен пример как работают шаги.     
> Второй аргумент название команды.    

```csharp
        [ReplyMenuHandler(true, "Название команды")]
        public static async Task НазваниеМетода(ITelegramBotClient название, Update название)
        {
          //Обработка
        }
       
        
        [ReplyMenuHandler(true, "Название команды", "Название команды")]
        public static async Task НазваниеМетода(ITelegramBotClient название, Update название)
        {
          //Обработка
        }
```


Пример
```csharp
        static int count = 0;
        /// <summary>
        /// Напишите в чате "Название команды"
        /// true/false обозначает будет ли проигнорирован следующий шаг обработки (что за шаги можно узнать дальше)
        /// </summary>
        [ReplyMenuHandler(true, "Название команды")]
        public static async Task ExampleReply(ITelegramBotClient botClient, Update update)
        {
            string msg = "Cообщение";
            await Helpers.Message.Send(botClient, update, msg);
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
        /// Напишите в чате "Пример"
        /// Напишите в чате /reply
        /// Пример с использование одновременно слеш команды и reply
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE)]
        [SlashHandler(SlashKeys.SL_EXAMPLE_WITH_REPLY)]
        public static async Task ExampleReply(ITelegramBotClient botClient, Update update)
        {
            //Пример как получить текст сообщения из JSON файла
            string msg = MessageKeys.GetMessage(nameof(MessageKeys.MSG_EXAMPLE_TEXT));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чате "Пример 1" или "Пример 2"
        /// Пример с использованием разных reply команд для вывода одной и той же функции
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_ONE, ReplyKeys.RP_EXAMPLE_TWO)]
        [RequiredTypeChatAttribute(Telegram.Bot.Types.Enums.ChatType.Private)]
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
        [RequiredTypeChatAttribute(Telegram.Bot.Types.Enums.ChatType.Private)]
        [RequireTypeMessageAttribute(Telegram.Bot.Types.Enums.MessageType.Text)]
        public static async Task ExampleReplyJsonConfig(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Вы написали в чате {update.Message.Text} можете изменить значение команды в настройке appconfig.json";
            await Helpers.Message.Send(botClient, update, msg);
        }
```
#### Создание Reply меню
```csharp
        /// <summary>
        /// Напишите в чате "ReplyMenu"
        /// Пример генерации reply меню
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_REPLY_MENU)]
        [SlashHandler(SlashKeys.SL_EXAMPLE_WITH_REPLY)]
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
```
### Работа с Inline командами
**ВНИМАНИЕ: Максимальный допустимый размер данных для обработки в callback 128 байт!**     
> Базовые заголовки Inline команд ***Models/Enums/THeader.cs***     
```csharp
    /// <summary>
    /// Идентификаторы для callback команд
    /// </summary>
    public enum THeader
    {
        [Description("Пустая команда")]
        None,
        [Description("Выбрать месяц")]
        PickMonth,
        [Description("Выбрать год")]
        PickYear,
        [Description("Изменение календаря")]
        ChangeTo,
        [Description("Выбор года месяца")]
        YearMonthPicker,
        [Description("Выбрать дату")]
        PickDate,
        [Description("Следующая страница")]
        NextPage,
        [Description("Текущая страница")]
        CurrentPage,
        [Description("Предыдущая страница")]
        PreviousPage,
    }
```
> В консольном примере используется ***Models/CustomTHeader.cs***, вы можете создать свой файл с перечислением, важно только, чтобы название файла имело в себе ***THeader***. Это нужно для того, чтобы через рефлексию отработал определенный механизм.     
> Кто знаком с asp.net mvc знают, что имя файла контроллеров должно содержать "Controller", здесь аналогичная история.
```csharp
    public enum CustomTHeader
    {
        [Description("Бесплатный ВИП")]
        GetFreeVIP,
        [Description("Вип на 1 день")]
        GetVipOneDay,
        [Description("Вип на 1 неделю")]
        GetVipOneWeek,
        [Description("Вип на 1 месяц")]
        GetVipOneMonth,
        [Description("Вип навсегда")]
        GetVipOneForever,
        [Description("Пример 1")]
        ExampleOne,
        [Description("Пример 2")]
        ExampleTwo,
        [Description("Пример 3")]
        ExampleThree,
    }   
```
> ***/Models/InlineButtons/InlineCallback.cs*** - Создает кнопку с callback.   
>  ***/Models/InlineButtons/InlineURL.cs*** - Создает кнопку с гиперссылкой.   
>  ***/Models/InlineButtons/InlineWebApp.cs*** - Создает кнопку для работы с WebApp.   

Для обработки inline команд используется ***InlineCallbackHandlerAttribute***    
***!!!ВНИМАНИЕ значение в перечислениях THeader должны быть уникальны!!!***    

```csharp
        [InlineCallbackHandler<Перечисление>(Перечисление.Значение)]
        public static async Task НазваниеМетода(ITelegramBotClient название, Update название)
        {
            //Обработка
        }
        
        [InlineCallbackHandler<Перечисление>(Перечисление.Значение, Перечисление.Значение)]
        public static async Task НазваниеМетода(ITelegramBotClient название, Update название)
        {
            //Обработка
        }
```

#### Создание Inline меню

```csharp
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
```
#### Обработка Inline данных

```csharp
        /// <summary>
        /// callback обработка
        /// Обрабатывает одну точку входа
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleOne)]
        public static async Task Inline(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = "Выполнена команда callback";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// callback обработка
        /// Данный метод может обработать несколько точек входа
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleTwo, CustomTHeader.ExampleThree)]
        public static async Task InlineTwo(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = InlineCallback<EntityTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = $"Идентификатор который вы передали {command.Data.EntityId}";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
```

### Работа с календарем

```csharp
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
                var calendarMarkup = Markup.Calendar(DateTime.Today, dtfi);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = calendarMarkup;
                await Helpers.Message.Send(botClient, update.GetChatId(), $"Выберите дату:", option);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
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
                var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthYearMarkup = Markup.PickMonthYear(command.Data.Date, dtfi);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
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
                var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthPickerMarkup = Markup.PickMonth(command.Data.Date, dtfi);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthPickerMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }


            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
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
                var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var monthYearMarkup = Markup.PickYear(command.Data.Date, dtfi);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = monthYearMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
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
                var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var calendarMarkup = Markup.Calendar(command.Data.Date, dtfi);
                    var option = new OptionMessage();
                    option.MenuInlineKeyboardMarkup = calendarMarkup;
                    await Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }

        }

        /// <summary>
        /// Обработка выбранной  даты
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickDate)]
        public static async Task PickDate(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    var data = command.Data.Date;
                    //Обработка данных даты;
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
```

### Работа со слеш командами
```csharp
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
```
### Использование словарика для наименования кнопок

> в appconfig.json хранится список наименований кнопок в секции ***Buttons***   
Пример использование наименования кнопок из JSON   
```csharp
DictionaryJSON.GetButton(nameof(InlineKeys.IN_EXAMPLE_ONE))
DictionaryJSON.GetButton("IN_EXAMPLE_ONE")
```

Для того чтобы работало значение IN_EXAMPLE_ONE оно должно присутствовать в appconfig.json   
### Использование словарика для сообщений

> в appconfig.json хранится список сообщений в секции ***Messages***   
Пример использование сообщения из JSON   
```csharp
DictionaryJSON.GetMessage(nameof(MessageKeys.MSG_EXAMPLE_TEXT));
DictionaryJSON.GetMessage("MSG_EXAMPLE_TEXT");
```


Для того чтобы работало значение MSG_EXAMPLE_TEXT оно должно присуствовать в appconfig.json
### Пример работы с кэшем пользователя

> Класс для хранения временных данных пользователей ***/Models/TelegramCache.cs***      
> В консольном примере используется класс ***UserCache*** который наследует TelegramCache. При желании вы можете создать свой класс любыми свойствами.   

```csharp
        /// <summary>
        /// Напишите в боте "cache"
        /// Функция записывает данные в кэш
        /// </summary>
        [ReplyMenuHandler(true, "cache")]
        public static async Task GetCache(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Запись в кэш пользователя данных: {update.GetChatId()}";
            //Записываем данные в кеш пользователя
            update.GetCacheData<UserCache>().Id = update.GetChatId();
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в боте "resultcache"
        /// Функция получает данные из кэша
        /// </summary>
        [ReplyMenuHandler(true, "resultcache")]
        public static async Task CheckCache(ITelegramBotClient botClient, Update update)
        {
            //Получаем данные с кеша
            var cache = update.GetCacheData<UserCache>();
            string msg = "";
            if(cache.Id != null)
            {
                msg = $"Данные в кэше пользователя: {cache.Id}";
            }
            else
            {
                msg = $"Данные в кэше пользователя отсутствуют.";
            }
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в боте "clearcache"
        /// Функция очищает данные в кэше пользователя
        /// </summary>
        [ReplyMenuHandler(true, "clearcache")]
        public static async Task ClearCache(ITelegramBotClient botClient, Update update)
        {
            string msg = "Тестирование функции пошагового выполнения";
            //Очищаем кеш для пользователя
            update.GetCacheData<UserCache>().ClearData();
            await Helpers.Message.Send(botClient, update, msg);
        }
```



### Пример работы с шагами пользователя
ВНИМАНИЕ: Чтобы назначить следующий шаг у метода должна быть следующая сигнатура ***Task Название(ITelegramBotClient название, Update название)***
> Класс для обработки шагов ***/Extensions/Step.cs***   
> Класс для работы шагами  ***/Models/StepTelegram.cs***   
```csharp
        /// <summary>
        /// Напишите в чате "stepstart"
        /// Метод регистрирует следующий шаг пользователя
        /// </summary>
        [ReplyMenuHandler(false, "stepstart")]
        public static async Task StepStart(ITelegramBotClient botClient, Update update)
        {
            string msg = "Тестирование функции пошагового выполнения\nНапишите ваше имя";
            //Регистрация следующего шага пользователя
            update.RegisterNextStep(new StepTelegram(StepOne));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максимальным времени выполнения
        /// </summary>
        public static async Task StepOne(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 1 - Ваше имя {update.Message.Text}" +
                        $"\nВведите дату рождения";
            //Запись временных данных в кэщ пользователя
            update.GetCacheData<UserCache>().Data = $"Имя: {update.Message.Text}\n";
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.RegisterNextStep(new StepTelegram(StepTwo, DateTime.Now.AddMinutes(5)));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 2 - дата рождения {update.Message.Text}" +
                         $"\nНапиши любой текст, чтобы увидеть результат";
            //Запись временных данных в кэщ пользователя
            update.GetCacheData<UserCache>().Data += $"Дата рождения: {update.Message.Text}\n";
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.RegisterNextStep(new StepTelegram(StepThree, DateTime.Now.AddMinutes(5)));

            //Настройки для сообщения
            var option = new OptionMessage();
            //Добавление пустого reply меню с кнопкой "Главное меню"
            //Функция является приоритетной, если пользователь нажмет эту кнопку будет выполнена функция главного меню, а не следующего шага.
            option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, DictionaryJSON.GetButton(nameof(ReplyKeys.RP_MAIN_MENU)));
            await Helpers.Message.Send(botClient, update, msg, option);
        }


        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepThree(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 3 - Результат:\n{update.GetCacheData<UserCache>().Data}" +
                         $"\nПоследовательность шагов очищена.";
            //Последний шаг
            update.ClearStepUser();
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Если есть следующий шаг, он будет проигнорирован при выполнение данной команды
        /// Потому что в ReplyMenuHandler значение первого аргумента установлено в true, что значит приоритетная команда
        /// </summary>
        [ReplyMenuHandler(true, "ignorestep")]
        public static async Task IngoreStep(ITelegramBotClient botClient, Update update)
        {
            string msg = "";
            if (update.HasStep())
            {
                msg = "Следующий шаг проигнорирован";
            }
            else
            {
                msg = "Следующий шаг отсутствовал";
            }
            
            await Helpers.Message.Send(botClient, update, msg);
        }
```

### Пример ограничения выполнение функции только определенной группой пользователей

Если к некоторым функциям требуется ограничить доступ, можно воспользоваться атрибутом Access, он принимает в себя значение типа int.    

Вариант использования либо через int либо через flags enum    

> [Access((int)(UserPrivilege.Guest | UserPrivilege.Registered))]     
> [Access(11)]     


```csharp
        /// <summary>
        /// Напишите в чате "Access"
        /// Пример генерации reply меню
        /// </summary>
        [Access((int)(UserPrivilege.Guest | UserPrivilege.Registered))]
        [ReplyMenuHandler(true, "Access")]
        public static async Task ExampleAccess(ITelegramBotClient botClient, Update update)
        {
            string msg = "Проверка привилегий";
            await Helpers.Message.Send(botClient, update, msg);
        }
```

Обязательно нужно подписаться на событие OnCheckPrivilege   

```csharp
    //Обработка проверка привилегий
    telegram.Handler.Router.OnCheckPrivilege        += ExampleEvent.OnCheckPrivilege;
```

Теперь в OnCheckPrivilege пишем свою логику проверки прав доступа к функции, пример использования приведен ниже.  

```csharp
        /// <summary>
        /// Событие проверки привелегий пользователя
        /// </summary>
        /// <param name="callback">callback функция выполняется в случае успеха</param>
        /// <param name="flags">Флаги которые должны присуствовать</param>
        public static async Task OnCheckPrivilege(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update, Router.TelegramCommand callback, int? flags = null)
        {
            if(flags != null)
            {
                var flag = flags.Value;
                //Проверяем флаги через int
                if(update.GetIntPrivilege().Contains(flag))
                {
                    await callback(botclient, update);
                    return;
                }

                //Проверяем флаги через enum UserPrivilage
                if (((UserPrivilege)flag).HasFlag(update.GetFlagPrivilege()))
                {
                    await callback(botclient, update);
                    return;
                }

                string errorMsg = "У вас нет доступа к данной функции";
                await PRTelegramBot.Helpers.Message.Send(botclient, update, errorMsg);
                return;
            }
            string msg = "Проверка привилегий";
            await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
        }
```

### Работа с постраничным выводом

```csharp
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
        [ReplyMenuHandler(true, "pages")]
        public static async Task ExamplePages(ITelegramBotClient botClient, Update update)
        {
            //Беру текст для первого сообщения
            string msg = pageData[0];
            //Получаю контент с 1 страницы с размером страницы 1
            var data = await pageData.GetPaged<string>(1, 1);
            //Генерирую меню постраничного вывода с заголовком
            var generateMenu = PRTelegramBot.Helpers.TG.MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPageHeader);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            var message = await Helpers.Message.Send(botClient, update, msg, option);
        }

        /// <summary>
        /// Напишите в чате "pagestwo"
        /// </summary>
        [ReplyMenuHandler(true, "pagestwo")]
        public static async Task ExamplePagesTwo(ITelegramBotClient botClient, Update update)
        {
            //Беру текст для первого сообщения
            string msg = pageDataTwo[0];
            //Получаю контент с 1 страницы с размером страницы 1
            var data = await pageDataTwo.GetPaged<string>(1, 1);
            //Генерирую меню постраничного вывода с заголовком
            var generateMenu = PRTelegramBot.Helpers.TG.MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPageHeader2);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            var message = await Helpers.Message.Send(botClient, update, msg, option);
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
                        //Получаю заголовок из данных
                        CustomTHeader header = (CustomTHeader)command.Data.Header;
                        //обрабатываю данные по заголовку
                        if(header == CustomTHeader.CustomPageHeader)
                        {
                            //Получаю номер страницы и указываю размер страницы
                            var data = await pageData.GetPaged<string>(command.Data.Page, 1);
                            //Генерирую постраничное меню
                            var generateMenu = Helpers.TG.MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPageHeader);
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
                        else if (header == CustomTHeader.CustomPageHeader2)
                        {
                            //Получаю номер страницы и указываю размер страницы
                            var data = await pageDataTwo.GetPaged<string>(command.Data.Page, 1);
                            //Генерирую постраничное меню
                            var generateMenu = Helpers.TG.MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPageHeader2);
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
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
    }
```

### [📎 Основные элементы в структуре проекта](https://github.com/prethink/-PR-TelegramBot/wiki/%D0%9E%D1%81%D0%BD%D0%BE%D0%B2%D0%BD%D1%8B%D0%B5-%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D1%8B-%D0%B2-%D1%81%D1%82%D1%80%D1%83%D0%BA%D1%82%D1%83%D1%80%D0%B5-%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B0)

