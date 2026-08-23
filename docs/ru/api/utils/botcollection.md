---
description: Singleton класс хранящий всех ботов.
---

# BotCollection

```csharp
/// <summary>
/// Количество ботов.
/// </summary>
public long BotCount

/// <summary>
/// Singleton экземпляр.
/// </summary>
public static BotCollection Instance

/// <summary>
/// Получить следующий идентификатор для бота.
/// </summary>
/// <returns>Идентификатор бота.</returns>
public static long GetNextId()

/// <summary>
/// Добавить бота в коллекцию.
/// </summary>
/// <param name="bot">Бот.</param>
public void AddBot(PRBotBase bot)

/// <summary>
/// Удалить бота из коллекции.
/// </summary>
/// <param name="bot">Бот.</param>
public void RemoveBot(PRBotBase bot)

/// <summary>
/// Очистить всех ботов.
/// </summary>
public void ClearBots()

/// <summary>
/// Получить бота по telegram id.
/// </summary>
/// <param name="telegramId">Идентификатор telegram.</param>
/// <returns>Экземпляр класса бота или null.</returns>
public PRBotBase GetBotByTelegramIdOrNull(long? telegramId)

/// <summary>
/// Получить экземпляр бота.
/// </summary>
/// <param name="botId">Идентификатор бота.</param>
/// <returns>Экземпляр класса бота или null.</returns>
public PRBotBase GetBotOrNull(long botId)

/// <summary>
/// Получить экземпляр бота.
/// </summary>
/// <param name="predicate">Выражение для фильтрации.</param>
/// <returns>Экземпляр класса бота или null.</returns>
public PRBotBase GetBotOrNull(Func<PRBotBase, bool> predicate)

/// <summary>
/// Получить всех ботов.
/// </summary>
/// <returns>Коллекция ботов.</returns>
public List<PRBotBase> GetBots()

/// <summary>
/// Получить всех ботов.
/// </summary>
/// <param name="predicate">Выражение для фильтрации.</param>
/// <returns>Коллекция ботов.</returns>
public List<PRBotBase> GetBots(Func<PRBotBase, bool> predicate)

/// <summary>
/// Получить экземпляр бота.
/// </summary>
/// <param name="botName">Название/логин бота.</param>
/// <returns>Экземпляр класса бота или null.</returns>
public PRBotBase GetBotOrNull(string botName)
```
