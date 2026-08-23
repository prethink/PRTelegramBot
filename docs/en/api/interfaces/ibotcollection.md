---
description: Interface for working with the bot collection. Lets you look bots up by various criteria and manage the collection.
---

# IBotCollection

Interface for working with the bot collection. Lets you look bots up by various criteria and manage the collection.

## Properties

| Property | Description |
| --- | --- |
| `long BotCount { get; }` | Number of bots in the collection. |

## Methods

| Method | Description |
| --- | --- |
| `PRBotBase? GetBotByTelegramIdOrNull(long? telegramId)` | Gets a bot by its Telegram Id. |
| `PRBotBase? GetBotOrNull(long botId)` | Gets a bot by its internal Id. |
| `PRBotBase? GetBotOrNull(Func<PRBotBase, bool> predicate)` | Gets a bot matching a filter condition. |
| `PRBotBase? GetBotOrNull(string botName)` | Gets a bot by its name or login. |
| `IEnumerable<PRBotBase> GetBots()` | Gets all bots. |
| `IEnumerable<PRBotBase> GetBots(Func<PRBotBase, bool> predicate)` | Gets all bots matching a filter condition. |
| `void AddBot(PRBotBase bot)` | Adds a new bot to the collection. |
| `void RemoveBot(PRBotBase bot)` | Removes a bot from the collection. |
| `void ClearBots()` | Clears the entire bot collection. |
| `long GetNextId()` | Gets the next unique identifier for a new bot. |

