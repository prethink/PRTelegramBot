# 📰 Описание
Библиотека для быстрого и удобного создание telegram ботов.   
Пример использования в консольном приложение.

# 💎 Возможности

 - Автоматическая регистрация и обработка reply команд;
 - Обработка команд со скобками, пример Лайки(5);
 - Генерация reply меню;
 - Автоматическая регистрация и обработка Inline команд;
 - Генерация Inline кнопок и меню;
 - Автоматическая регистрация и обработка слеш команд [/];
 - Работа со словарем;
 - Выполнение пошагово команд;
 - Обработка фоновых задач;
 - Хранение кэш данных пользователей.
 - Возможность добавить администраторов телеграм бота.
 - Возможность использовать белый список пользователей, которые могут пользоваться ботом.

# ⏳ Планируется сделать
-
# 🔑 Зависимости

 - TelegramBot v19.0.0 https://github.com/TelegramBots/Telegram.Bot
 - NLog v5.1.1 https://github.com/NLog/NLog
 - Microsoft.Extensions.Configuration.Binder v7.0.0 https://github.com/dotnet/runtime
 - Microsoft.Extensions.Configuration.Json v7.0.0 https://github.com/dotnet/runtime
# 🧱 Интегрированные пакеты
 - CalendarPicker | karb0f0s   https://github.com/karb0f0s/CalendarPicker

# 🎌 Быстрый старт
## Настройка конфигурации
Для работы бота требуется скопировать токен из бота BotFather и вставить в файл  dbconfig.json в поле Token из секции TelegramConfig, этого достаточно чтобы бот мог запуститься.

> Для получения токена от BotFather в Telegram, следуйте этим шагам:
> 
> 1. Откройте Telegram и найдите "BotFather". Это официальный бот Telegram, который поможет вам создать и настроить своего бота.
> 2. Начните диалог с BotFather, нажав кнопку "Start" или отправив ему команду "/start".
> 3. Отправьте команду "/newbot", чтобы создать нового бота.
> 4. BotFather попросит вас ввести имя для вашего бота. Введите желаемое имя и следуйте инструкциям.
> 5. После успешного создания бота BotFather выдаст вам токен доступа. Это будет выглядеть примерно так: "1234567890:ABCDEFGHIJKLMNOPQRSTUVXYZ".
> 6. Скопируйте полученный токен. Этот токен уникален для вашего бота и используется для аутентификации и взаимодействия с API Telegram.

### Configs/telegram.json
telegram - хранит информацию о подключении к базе данных и телеграм api;
```json
//После изменения конфигурации обязательно перезапустите программу!!!
{
  "TelegramConfig": {
    // Токен для телеграм бота
    "Token": "",
    //Идентификаторы администраторов бота
    "Admins": [],
    //Белый список пользователей которые могут пользоваться ботом, если список пустой ботом могут пользоваться все
    "WhiteListUsers": [],
    //Показывать или нет что значение кнопок не найдено в словаре
    "ShowErrorNotFoundNameButton": false
  }
}
```

### Configs/appconfig.json
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
> Вы можете создавать функции в любом месте, главное чтобы сигнатура метода была как в примере.   
Для автоматического поиска команд используется класс "ReflectionFinder"
```csharp
        [Атрибуты обработки]
        public static async Task НазваниеМетода(ITelegramBotClient название, Update название)
        {
            //Тело функции
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
Пример
```csharp
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
> Заголовки Inline команд ***Models/Enums/CallbackId.cs***   
> Классы для создания Inline кнопок ***/Models/InlineCallback.cs***, ***/Models/InlineURL.cs***,***/Models/InlineWebApp.cs***

#### Создание Inline меню

```csharp
        /// <summary>
        /// Пример с генерацией inline меню
        /// </summary>
        [ReplyMenuHandler(true, ReplyKeys.RP_EXAMPLE_MENU)]
        public static async Task InlineMenu(ITelegramBotClient botClient, Update update)
        {
            /* Создание новой кнопки с callback данными
             * MessageKeys.GetValueButton(nameof(InlineKeys.IN_EXAMPLE_ONE)) - Название кнопки из JSON
             * Models.Enums.CallbackId.ExampleOne - Заголовок команды
             */
            var exampleItemOne = new InlineCallback(MessageKeys.GetValueButton(nameof(InlineKeys.IN_EXAMPLE_ONE)), Models.Enums.CallbackId.ExampleOne);
            /* Создание новой кнопки с callback данными
             * InlineKeys.IN_EXAMPLE_TWO - Название кнопки из константы
             * Models.Enums.CallbackId.ExampleOne - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemTwo = new InlineCallback<EntityTCommand>(InlineKeys.IN_EXAMPLE_TWO, Models.Enums.CallbackId.ExampleTwo, new EntityTCommand(2));
            /* Создание новой кнопки с callback данными
             * Models.Enums.CallbackId.ExampleOne - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemThree = new InlineCallback<EntityTCommand>("Пример 3", Models.Enums.CallbackId.ExampleThree, new EntityTCommand(3));
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
        [InlineCallbackHandler(Models.Enums.CallbackId.ExampleOne)]
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
        [InlineCallbackHandler(Models.Enums.CallbackId.ExampleTwo, Models.Enums.CallbackId.ExampleThree)]
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

> Класс для хранения временных данных пользователей ***/Models/UserCache.cs***

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
            update.GetCacheData().Id = update.GetChatId();
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
            var cache = update.GetCacheData();
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
            update.GetCacheData().ClearData();
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
            string msg = "Тестирование функции пошагового выполнения";
            //Регистрация следующего шага пользователя
            update.RegisterNextStep(new StepTelegram(StepOne));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максималным времение выполнения
        /// </summary>
        public static async Task StepOne(ITelegramBotClient botClient, Update update)
        {
            string msg = "Шаг 1";
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.RegisterNextStep(new StepTelegram(StepTwo, DateTime.Now.AddMinutes(5)));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = "Шаг 2";
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.RegisterNextStep(new StepTelegram(StepThree, DateTime.Now.AddMinutes(5)));

            //Настройки для сообщения
            var option = new OptionMessage();
            //Добавление пустого reply меню с кнопкой "Главное меню"
            //Функция является приоритетной, если пользователь нажмет эту кнопку будет выполнена функция главного меню, а не следующего шага.
            option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, MessageKeys.GetValueButton(nameof(ReplyKeys.RP_MAIN_MENU)));
            await Helpers.Message.Send(botClient, update, msg, option);
        }


        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepThree(ITelegramBotClient botClient, Update update)
        {
            string msg = "Шаг 3";
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

### [📎 Основные элементы в структуре проекта](https://github.com/prethink/-PR-TelegramBot/wiki/%D0%9E%D1%81%D0%BD%D0%BE%D0%B2%D0%BD%D1%8B%D0%B5-%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D1%8B-%D0%B2-%D1%81%D1%82%D1%80%D1%83%D0%BA%D1%82%D1%83%D1%80%D0%B5-%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B0)

