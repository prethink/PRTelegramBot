---
description: Provides access to the current state of the context and the bot. Read-only. The stack is managed by BotDataScope.
---

# CurrentScope

Provides access to the current state of the context and the bot. Read-only. The stack is managed by BotDataScope.

## Fields

| Field | Description |
| --- | --- |
| `static IBotContext? Context => contextStack.Value?.Count > 0` | The current bot context (read-only). |
| `static PRBotBase? Bot => botStack.Value?.Count > 0` | The current bot (read-only). |
| `static IServiceProvider? Services => serviceProvider.Value` | Services of the current bot (read-only). |

