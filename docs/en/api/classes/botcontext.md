---
description: Bot context.
---

# BotContext

Bot context.

Inherits `IBotContext`.

## Properties

| Property | Description |
| --- | --- |
| `PRBotBase Current { get; }` |  |
| `Update Update { get; }` |  |
| `CancellationToken CancellationToken { get; }` |  |

## Fields

| Field | Description |
| --- | --- |
| `ITelegramBotClient BotClient => Current.BotClient` |  |
| `UpdateType CurrentUpdateType => Update.Type` |  |

## Methods

| Method | Description |
| --- | --- |
| `IEnumerable<PRBotBase> Bots => BotCollection.Instance.GetBots()` |  |
| `static IBotContext CreateEmpty()` | Creates a stub context. |
| `bool TryGetCustomValue<T>(out T? value)` |  |
| `void SetCustomData(object data)` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `BotContext(PRBotBase bot, Update update, CancellationToken cancellationToken)` | Constructor. |
| `BotContext(PRBotBase bot) : this(bot, new Update(), CancellationToken.None) {}` | Constructor. |
| `BotContext(PRBotBase bot, Update update) : this(bot, update, CancellationToken.None) { }` | Constructor. |
| `BotContext(PRBotBase bot, CancellationToken cancellationToken) : this(bot, new Update(), cancellationToken) { }` | Constructor. |

