---
description: Interface of the bot context.
---

# IBotContext

Interface of the bot context.

## Properties

| Property | Description |
| --- | --- |
| `IEnumerable<PRBotBase> Bots { get; }` | All bot instances. |
| `PRBotBase Current { get; }` | Bot instance. |
| `ITelegramBotClient BotClient { get; }` | The Telegram.Bot client. |
| `Update Update { get; }` | Update. |
| `UpdateType CurrentUpdateType { get; }` | The current update type. |
| `CancellationToken CancellationToken { get; }` | Cancellation token. |

## Methods

| Method | Description |
| --- | --- |
| `bool TryGetCustomValue<T>(out T? value)` | Tries to get a custom value. |

