---
description: The factories that decide how a bot receives updates.
---

# Bot factories

A factory decides what kind of bot `PRBotBuilder.Build()` produces, chiefly how it receives updates.

Pass one with `UseFactory`:

```csharp
var bot = new PRBotBuilder("Token")
    .UseFactory(new PRBotWebHookFactory())
    .Build();
```

| Factory | Produces |
| --- | --- |
| `PRBotFactory` | The default. Creates a polling bot, which is what you get when `UseFactory` is not called at all. |
| `PRBotPollingFactory` | A polling bot, stated explicitly. |
| `PRBotWebHookFactory` | A webhook bot — see [Webhook](../../getting-started/webhook/). |

All three derive from `PRBotFactoryBase`.

## Writing your own

Derive from `PRBotFactoryBase` when a bot has to be constructed differently — wrapped in your own type, or built against a client you configured yourself. In most cases the simpler route is enough: build the `TelegramBotClient` and hand it to the builder, as [Local Server API](../../local-server-api.md) shows.
