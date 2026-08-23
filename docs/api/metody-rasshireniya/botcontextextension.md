# BotContextExtension

```csharp
/// <summary>
/// Получает идентификатор чата в зависимости от типа сообщений.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Идентификатор чата.</returns>
/// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
public static long GetChatId(this IBotContext context)


/// <summary>
/// Получает идентификатор в формате класса.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Идентификатор в формате класса</returns>
public static ChatId GetChatIdClass(this IBotContext context)

/// <summary>
/// Попытаться получить идентификатор чата.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <param name="chatId">Идентификатор чата.</param>
/// <returns>True - удалось получить, false - нет.</returns>
public static bool TryGetChatId(this IBotContext context, out long chatId)

/// <summary>
/// Получает идентификатор сообщения.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Идентификатор сообщения.</returns>
/// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
public static int GetMessageId(this IBotContext context)

/// <summary>
/// Является ли идентификатор пользователским чатом.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>True - да, False - нет.</returns>
public static bool IsUserChatId(this IBotContext context)

/// <summary>
/// Информация о пользователе.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Информация о пользователе.</returns>
public static string GetInfoUser(this IBotContext context)

/// <summary>
/// Получает идентификатор пользователя из обновления Telegram.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Идентификатор пользователя (UserId).</returns>
public static long GetUserId(this IBotContext context)

/// <summary>
/// Создает кеш для пользователя.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="context">Контекст бота.</param>
/// <returns>Кэш.</returns>
public static TCache CreateCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Получает существующий кэш или создает новый.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="context">Контекст бота.</param>
/// <returns>Кэш.</returns>
/// <remarks>Если тип кэша отличается от существующего, будет создан кэш нового типа.</remarks>
public static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Получает кэш пользователя.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="context">Контекст бота.</param>
/// <returns>Кэш.</returns>
public static TCache GetCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Очищает кеш пользователя.
/// </summary>
/// <param name="context">Контекст бота.</param>
public static void ClearCacheData(this IBotContext context)

/// <summary>
/// Проверяет существуют ли кеш данные пользователя.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>True - есть кэш, False - нет кэша.</returns>
public static bool HasCacheData(this IBotContext context)

/// <summary>
/// Полностью удаляет кэш пользователя из словаря.
/// </summary>
/// <param name="context">Контекст бота.</param>
public static void RemoveCacheData(this IBotContext context)

/// <summary>
/// Регистрация следующего шага.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <param name="command">Следующая команда которая должна быть выполнена.</param>
public static void RegisterStepHandler(this IBotContext context, IExecuteStep command)

/// <summary>
/// Получает обработчик или null пользователя.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>обработчик или null.</returns>
public static TExecuteStep? GetStepHandler<TExecuteStep>(this IBotContext context) where TExecuteStep : IExecuteStep

/// <summary>
/// Получить текущий обработчик шага.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Обработчик или null.</returns>
public static IExecuteStep? GetStepHandler(this IBotContext context)

/// <summary>
/// Очищает шаги пользователя.
/// </summary>
/// <param name="context">Контекст бота.</param>
public static void ClearStepUserHandler(this IBotContext context)

/// <summary>
/// Проверяет есть ли шаг у пользователя.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>True - есть обработчик, False - нет обработчика.</returns>
public static bool HasStepHandler(this IBotContext context)

/// <summary>
/// Получить inline команду из callback данных используя контекст бота.
/// </summary>
/// <param name="context">Контекст.</param>
/// <returns>Команда или null.</returns>
public static InlineCallback GetCommandByCallbackOrNull(this IBotContext context)

/// <summary>
/// Получить inline команду из callback данных используя контекст бота.
/// </summary>
/// <typeparam name="T">Тип данных.</typeparam>
/// <param name="context">Контекст.</param>
/// <returns>Команда или null.</returns>
public static InlineCallback<T> GetCommandByCallbackOrNull<T>(this IBotContext context) 

/// <summary>
/// Получить аргументы слэш-команды.
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Коллекция аргументов.</returns>
public static List<string> GetSlashArgs(this IBotContext context)

/// <summary>
/// Получить аргументы слэш-команды конкретного типа.
/// </summary>
/// <typeparam name="T">Тип.</typeparam>
/// <param name="context">Контекст бота.</param>
/// <param name="throwOnError">Признак, что требуется выбросить исключение.</param>
/// <returns>Коллекция аргументов.</returns>
/// <exception cref="FormatException">Исключение.</exception>
public static List<T> GetSlashArgs<T>(this IBotContext context, bool throwOnError = false)
```
