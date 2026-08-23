# PageExtension

```csharp
/// <summary>
/// Вывод данных постранично.
/// </summary>
/// <typeparam name="T">Тип.</typeparam>
/// <param name="query">Коллекция данных.</param>
/// <param name="page">Страница.</param>
/// <param name="pageSize">Размер страницы.</param>
/// <returns>Страница данных с доп информацией.</returns>
public static Task<PagedResult<T>> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize) where T : class
```
