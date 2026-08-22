# ASP.NET webhook example

**English** | [Русский](README.ru.md)

Two bots receiving updates over a webhook: Telegram posts each update to your endpoint instead of the bot polling for them.

Target framework: **net8.0** · Update delivery: **webhook**

For the polling variant see [AspNetExample](../AspNetExample/README.md).

## Requirements

A webhook needs an address Telegram can reach: a public **HTTPS** URL with a valid certificate. During development a tunnel (ngrok, Cloudflare Tunnel and the like) will do.

## Running it

1. Get a bot token from [@BotFather](https://t.me/BotFather).
2. In `Program.cs` set the token and your public address:
   ```csharp
   new PRBotBuilder("5623652365:Token")
       .UseFactory(new PRBotWebHookFactory())
       .SetUrlWebHook("https://domain.ru/botendpoint")
       .SetClearUpdatesOnStart(true)
       .Build();
   ```
   `SetUrlWebHook` must match the route registered below — `/botendpoint` by default.
3. Run the project. `BotHostedService` starts the bots and registers the webhook with Telegram.

## How it is put together

Three pieces matter, and all three are required.

**Controllers and Newtonsoft.Json.** Without them the update will not deserialize:
```csharp
builder.Services.AddControllers().AddNewtonsoftJson();
```

**The route.** `MapBotWebhookRoute<BotController>` binds the endpoint to the controller action:
```csharp
app.MapBotWebhookRoute<BotController>("/botendpoint");
app.MapControllers();
```

**Startup.** `BotHostedService` runs on application start: it hands the bots the `IServiceProvider`, calls `ReloadHandlers()`, starts them, and then checks with Telegram that the webhook was accepted — reporting `LastErrorMessage` through the bot's error event if it was not.

## Several bots on one endpoint

Both bots share the route `/botendpoint`. They are told apart by the secret token: Telegram sends it in the `X-Telegram-Bot-Api-Secret-Token` header, and `BotController` compares it against `bot.Options.WebHookOptions.SecretToken` to pick the right bot.

`ValidateTelegramBotAttribute` rejects requests without a valid header before the action runs, so a random POST to your endpoint gets nothing.

Bot instances are available anywhere through `BotCollection.Instance.GetBots()`.

## What is demonstrated

| Area | Where to look |
| --- | --- |
| Receiving and dispatching an update | `Controllers/BotController.cs` |
| Validating the secret token | `Filter/ValidateTelegramBotAttribute.cs` |
| Registering the webhook route | `WebHookExtensions.cs` |
| Starting the bots as a hosted service | `Services/BotHostedService.cs` |
| Two bots side by side | `Program.cs` |

## Worth noting

The example builds the bots before `app.Build()` and only starts them from the hosted service. That order matters: the bots have to exist in `BotCollection` by the time the first update arrives, but they must not talk to Telegram until the application is ready to serve the endpoint.

Set a secret token in production — without it, anyone who learns your URL can post fake updates:
```csharp
.SetSecretTokenWebHook("your-secret")
```

---

See also: [main README](../../README.md) · [documentation](https://prethink.gitbook.io/prtelegrambot/)
