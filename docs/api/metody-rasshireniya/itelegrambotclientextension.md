# ITelegramBotClientExtension

```csharp
/// <summary>
/// Проверяет пользователя, является ли он администратором бота.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <param name="update">Обновление из telegram.</param>
/// <returns>True - администратор, False - не администратор.</returns>
public static bool IsAdmin(this ITelegramBotClient botClient, Update update)

/// <summary>
/// Проверяет пользователя, является ли он администратором бота.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <param name="userId">Идентификатор пользователя.</param>
/// <returns>True - администратор, False - не администратор.</returns>
public static bool IsAdmin(this ITelegramBotClient botClient, long userId)

/// <summary>
/// Проверяет пользователя, присутствует ли в белом списке бота.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <param name="update">Обновление из telegram.</param>
/// <returns>True - есть в списке, False - нет в списке.</returns>
public static bool InWhiteList(this ITelegramBotClient botClient, Update update)

/// <summary>
/// Проверяет пользователя, присутствует ли в белом списке бота.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <param name="userId">Идентификатор пользователя.</param>
/// <returns>True - есть в списке, False - нет в списке.</returns>
public static bool InWhiteList(this ITelegramBotClient botClient, long userId)

/// <summary>
/// Возращает список администраторов бота.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <returns>Список идентификаторов.</returns>
public static List<long> GetAdminsIds(this ITelegramBotClient botClient)

/// <summary>
/// Возращает белый список пользователей.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <returns>Список идентификаторов.</returns>
public static List<long> GetWhiteListIds(this ITelegramBotClient botClient)

/// <summary>
/// Получить экземпляр класса бота.
/// </summary>
/// <param name="botClient">Бот клиент.</param>
/// <returns>Экземпляр класса или null.</returns>
public static PRBotBase GetBotDataOrNull(this ITelegramBotClient botClient)

/// <summary>
/// Вызов события простого лога.
/// </summary>
/// <param name="botClient">Бот.</param>
/// <param name="msg">Сообщение.</param>
/// <param name="typeEvent">Тип события.</param>
/// <param name="color">Цвет.</param>
public static void InvokeCommonLog(this ITelegramBotClient botClient, string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)

/// <summary>
/// Вызов события логирование ошибок.
/// </summary>
/// <param name="botClient">Бот.</param>
/// <param name="ex">Исключение.</param>
public static void InvokeErrorLog(this ITelegramBotClient botClient, Exception ex)

/// <summary>
/// Вызов события логирование ошибок.
/// </summary>
/// <param name="botClient">Бот.</param>
/// <param name="ex">Исключение.</param>
/// <param name="update">обновление.</param>
public static void InvokeErrorLog(this ITelegramBotClient botClient, Exception ex, Update update)

/// <summary>
/// Генерация реферальной ссылки.
/// </summary>
/// <param name="botClient">Бот.</param>
/// <param name="refLink">Текст реферальной ссылки.</param>
/// <returns>Сгенерированная реферальная ссылка https://t.me/{bot.Username}?start={refLink}.</returns>
/// <exception cref="ArgumentNullException">Вызывается в случае пустого текста.</exception>
public async static Task<string> GetGeneratedRefLink(this ITelegramBotClient botClient, string refLink)

/// <summary>
/// Получить значение из конфиг файла по ключу
/// </summary>
/// <typeparam name="TBotProvider">Провайдера работы с файлами.</typeparam>
/// <typeparam name="TReturn">Возращаемый тип.</typeparam>
/// <param name="botClient">Бот клиент.</param>
/// <param name="configKey">Ключ конфига.</param>
/// <param name="key">Ключ для значения.</param>
/// <returns>Значение из конфиг файла.</returns>
public static TReturn GetConfigValue<TBotProvider, TReturn>(this ITelegramBotClient botClient, string configKey, string key)where TBotProvider : IBotConfigProvider

/// <summary>
/// Попытаться получить значение из конфиг файла по ключу
/// </summary>
/// <typeparam name="TBotProvider">Провайдера работы с файлами.</typeparam>
/// <typeparam name="TReturn">Возращаемый тип.</typeparam>
/// <param name="botClient">Бот клиент.</param>
/// <param name="configKey">Ключ конфига.</param>
/// <param name="key">Ключ для значения.</param>
/// <param name="result">Значение.</param>
/// <returns>True - значение получено, False - не удалось получить значение.</returns>
public static bool TryGetConfigValue<TBotProvider, TReturn>(this ITelegramBotClient botClient, string configKey, string key, out TReturn result) where TBotProvider : IBotConfigProvider, new()
```
