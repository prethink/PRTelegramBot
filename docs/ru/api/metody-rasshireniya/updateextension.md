# UpdateExtension

```csharp
/// <summary>
/// Получает идентификатор чата в зависимости от типа сообщений.
/// </summary>
/// <param name="update">Обновление telegram.</param>
/// <returns>Идентификатор чата.</returns>
/// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
public static long GetChatId(this Update update)
public static long GetChatId(this IBotContext context)

/// <summary>
/// Получает идентификатор сообщения.
/// </summary>
/// <param name="update">Обновление telegram.</param>
/// <returns>Идентификатор сообщения.</returns>
/// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
public static int GetMessageId(this Update update)
public static ChatId GetChatIdClass(this IBotContext context)

/// <summary>
/// Информация о пользователе.
/// </summary>
/// <param name="update">Обновление telegram.</param>
/// <returns>Информация о пользователе.</returns>
public static string GetInfoUser(this Update update)
public static string GetInfoUser(this IBotContext context)

/// <summary>
/// Попытаться получить идентификатор чата.
/// </summary>
/// <param name="update">Update.</param>
/// <param name="chatId">Идентификатор чата.</param>
/// <returns>True - удалось получить, false - нет.</returns>
public static bool TryGetChatId(this Update update, out long chatId)
public static bool TryGetChatId(this IBotContext context, out long chatId)

/// <summary>
/// Является ли идентификатор пользователским чатом.
/// </summary>
/// <param name="update">Update.</param>
/// <returns>True - да, False - нет.</returns>
public static bool IsUserChatId(this Update update)
public static bool IsUserChatId(this IBotContext context)

/// <summary>
/// Получает идентификатор пользователя из обновления Telegram.
/// </summary>
/// <param name="update">Объект обновления Telegram.</param>
/// <returns>Идентификатор пользователя (UserId).</returns>
public static long GetUserId(this Update update)
public static long GetUserId(this IBotContext context)
```
