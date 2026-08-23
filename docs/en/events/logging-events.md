---
description: Writing to the bot's own log from your code.
---

# Logging events

A bot raises two logging events, and your code can raise them too:

* **`OnCommonLog`** — ordinary messages;
* **`OnErrorLog`** — errors.

These are the fallback logging mechanism. If you have configured an `ILogger`, prefer that — see [Logging](../logging.md), where the resolution order is described. The events remain useful when you want a console bot to print something without pulling in a logging framework.

## Subscribing

```csharp
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .Build();

// Ordinary log messages.
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
// Errors.
telegram.Events.OnErrorLog += Telegram_OnLogError;
```

The handlers take the matching argument type:

```csharp
async Task Telegram_OnLogCommon(CommonLogEventArgs e)
{
    Console.WriteLine(e.Message);
}

async Task Telegram_OnLogError(ErrorLogEventArgs e)
{
    Console.WriteLine(e.Exception);
}
```

## Raising them yourself

Both are available as extensions on the context, so a handler can write into the same log the framework uses.

```csharp
public static void InvokeCommonLog(this IBotContext context, string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)
public static void InvokeErrorLog(this IBotContext context, Exception ex)
```

An ordinary message:

```csharp
context.InvokeCommonLog("Write this to the ordinary log");
```

With a category and a colour, which a console subscriber can use:

```csharp
context.InvokeCommonLog("Payment received", "payments", ConsoleColor.Green);
```

An error:

```csharp
context.InvokeErrorLog(new Exception("something went wrong"));
```

{% hint style="info" %}
`InvokeErrorLog` takes the exception and nothing else. The context it is called on already knows which bot, chat and user the update belongs to, so there is no user id to pass.
{% endhint %}

## What the framework itself logs here

These are not only for your code. The framework raises them for its own diagnostics: a command throwing, a faulty event subscriber, a webhook reporting an error at startup, a configuration file that could not be read. Subscribing to `OnErrorLog` in even the smallest bot is worth it — without a subscriber and without an `ILogger`, those messages have nowhere to go.
