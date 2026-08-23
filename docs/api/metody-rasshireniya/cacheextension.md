# CacheExtension

```csharp
/// <summary>
/// Создает кеш для пользователя.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="update">Обновление telegram.</param>
public static TCache CreateCacheData<TCache>(this Update update) where TCache : ITelegramCache
public static TCache CreateCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Получает существующий кэш или создает новый.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="update">Обновление telegram.</param>
/// <returns>Кэш.</returns>
/// <remarks>Если тип кэша отличается от существующего, будет создан кэш нового типа.</remarks>
public static TCache GetOrCreate<TCache>(this Update update) where TCache : ITelegramCache
public static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Получает кэш пользователя.
/// </summary>
/// <typeparam name="TCache">Тип кэша.</typeparam>
/// <param name="update">Обновление telegram.</param>
/// <returns>Кэш.</returns>
public static TCache GetCacheData<TCache>(this Update update) where TCache : ITelegramCache
public static TCache GetCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Очищает кеш пользователя.
/// </summary>
/// <param name="update">Обновление данных telegram.</param>
public static void ClearCacheData(this Update update)
public static void ClearCacheData(this IBotContext context)

/// <summary>
/// Проверяет существуют ли кеш данные пользователя.
/// </summary>
/// <param name="update">Обновление данных telegram.</param>
/// <returns>True - есть кэш, False - нет кэша.</returns>
public static bool HasCacheData(this Update update)
public static bool HasCacheData(this IBotContext context)

/// <summary>
/// Полностью удаляет кэш пользователя из словаря.
/// </summary>
/// <param name="update">Обновление данных telegram.</param>
public static void RemoveCacheData(this Update update)
public static void RemoveCacheData(this IBotContext context)
```
