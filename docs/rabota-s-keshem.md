# Работа с кэшем

PRTelegramBot предоставляет базовый функционал работы с кэшем.

В библиотеке присутствует интерфейс [ITelegramCache](api/interfeisy/itelegramcache.md). Для работы к кэшем требуется реализовать данный интерфейс. В нашем примере создадим класс UserCache который его реализует и будет записывать временную информацию для каждого пользователя используя его Update.

Для примера создадим свой класс кэша

```csharp
public class UserCache : ITelegramCache
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Временные данные
    /// </summary>
    public string Data { get; set; }
 
    public bool ClearData()
    {
        Id = 0;
        Data = "";
        return true;
    }
}
```

Библиотека предоставляет следующие методы расширения для работы с кэшем.

```csharp
/// <summary>
/// Создает кеш для пользователя.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="context">Контекст бота.</param>
/// <returns>Кэш.</returns>
public static void CreateCacheData<T>(this IBotContext context) where T : ITelegramCache

/// <summary>
/// Получает существующий кэш или создает новый.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="context">Контекст бота.</param>
/// <returns>Кэш.</returns>
/// <remarks>Если тип кэша отличается от существующего, будет создан кэш нового типа.</remarks>
public static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache 

/// <summary>
/// Получает кэш пользователя
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>Кеш пользователя</returns>
public static T GetCacheData<T>(this IBotContext context) where T : ITelegramCache
 
/// <summary>
/// Очищает кэш пользователя
/// </summary>
/// <param name="update">Обновление данных telegram</param>
public static void ClearCacheData(this IBotContext context)
 
/// <summary>
/// Проверяет существуют ли кэш данные пользователя
/// </summary>
/// <param name="context">Контекст бота.</param>
/// <returns>true/false</returns>
public static bool HasCacheData(this IBotContext context)

/// <summary>
/// Полностью удаляет кэш пользователя из словаря.
/// </summary>
/// <param name="context">Контекст бота.</param>
public static void RemoveCacheData(this IBotContext context)
```

Пример

```csharp
/// <summary>
/// Напишите в боте "cache"
/// Функция записывает данные в кэш
/// </summary>
[ReplyMenuHandler("cache")]
public static async Task GetCache(IBotContext context)
{
    string msg = $"Запись в кэш пользователя данных: {context.GetChatId()}";
    //Записываем данные в кеш пользователя
    context.GetCacheData<UserCache>().Id = update.GetChatId();
    await PRTelegramBot.Helpers.Message.Send(context, msg);
}
 
/// <summary>
/// Напишите в боте "resultcache"
/// Функция получает данные из кэша
/// </summary>
[ReplyMenuHandler("resultcache")]
public static async Task CheckCache(IBotContext context)
{
    //Получаем данные с кэша
    var cache = context.GetCacheData<UserCache>();
    string msg = "";
    if(cache.Id != null)
    {
        msg = $"Данные в кэше пользователя: {cache.Id}";
    }
    else
    {
        msg = $"Данные в кэше пользователя отсутствуют.";
    }
    await PRTelegramBot.Helpers.Message.Send(context, msg);
}
 
/// <summary>
/// Напишите в боте "clearcache"
/// Функция очищает данные в кэше пользователя
/// </summary>
[ReplyMenuHandler("clearcache")]
public static async Task ClearCache(IBotContext context)
{
    string msg = "Тестирование функции пошагового выполнения";
    //Очищаем кеш для пользователя
    context.GetCacheData<UserCache>().ClearData();
    await PRTelegramBot.Helpers.Message.Send(context, msg);
}
```
