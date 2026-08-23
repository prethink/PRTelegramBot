---
description: Pointing a bot at your own Bot API server instead of Telegram's.
---

# Local Server API

Telegram publishes the [Bot API server](https://github.com/tdlib/telegram-bot-api) as software you can run yourself. A bot connected to your own instance has higher limits — notably file uploads and downloads, which go from 50 MB to 2 GB — and the traffic stays on your infrastructure.

Point a bot at it by building the Telegram client yourself and handing it to the builder:

```csharp
// The token, and the address the bot should connect to.
var telegramOptions = new TelegramBotClientOptions("Token", "http://baseurl");

// Pass the client when creating the bot.
var telegram = new PRBotBuilder(new TelegramBotClient(telegramOptions)).Build();
```

Everything else works unchanged — the same handlers, the same menus. Only the endpoint differs.

{% hint style="info" %}
A bot must be moved to a local server with [`logOut`](https://core.telegram.org/bots/api#logout) against the official API first, and moved back with `close` before it can return. Doing this in the wrong order leaves the bot unable to receive updates from either.
{% endhint %}

Since the client is yours to construct, this is also where any other `TelegramBotClientOptions` setting goes — a custom `HttpClient`, a proxy, your own timeouts.
