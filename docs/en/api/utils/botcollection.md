---
description: Class that holds all bots.
---

# BotCollection

Class that holds all bots.

Inherits `IBotCollection`.

## Fields

| Field | Description |
| --- | --- |
| `static IBotCollection Instance => instance.Value` | The global settings instance. |
| `long BotCount => botList.Count` |  |

## Methods

| Method | Description |
| --- | --- |
| `long GetNextId() => botList.LastOrDefault().Key + 1` |  |
| `void AddBot(PRBotBase bot)  => botList.Add(bot.BotId, bot)` |  |
| `void RemoveBot(PRBotBase bot) => botList.Remove(bot.BotId)` |  |
| `void ClearBots() => botList.Clear()` |  |
| `PRBotBase? GetBotByTelegramIdOrNull(long? telegramId)  => botList.Values.SingleOrDefault(x => x.TelegramId == telegramId)` |  |
| `PRBotBase GetBotOrNull(long botId) => botList.Values.SingleOrDefault(x => x.BotId == botId)` |  |
| `PRBotBase? GetBotOrNull(Func<PRBotBase, bool> predicate) => botList.Values.SingleOrDefault(predicate)` |  |
| `IEnumerable<PRBotBase> GetBots() => botList.Select(x => x.Value).ToList()` |  |
| `IEnumerable<PRBotBase> GetBots(Func<PRBotBase, bool> predicate) => botList.Values.Where(predicate).ToList()` |  |
| `PRBotBase? GetBotOrNull(string botName) => botList.Values.SingleOrDefault(x => x.BotName.Contains(botName, StringComparison.OrdinalIgnoreCase))` |  |

