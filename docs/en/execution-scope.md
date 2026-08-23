---
description: Reaching the current bot, context and services from anywhere in the call stack.
---

# Execution scope

Since version 0.9 every `update` is handled inside a dedicated **scope** that carries everything needed to serve it. Any code reached from that update — however deep in the call stack, and without passing anything down through parameters — can ask for:

```csharp
// The current context.
var currentContext = CurrentScope.Context;

// The current bot.
var currentBot = CurrentScope.Bot;

// The current service provider.
var services = CurrentScope.Services;
```

This is what makes it possible for a helper five calls away from the handler to send a message or resolve a service without the handler threading `IBotContext` through every signature.

## Example

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

public static class Commands
{
    [ReplyMenuHandler("Report")]
    public static async Task Report(IBotContext context)
    {
        // Nothing is passed along — BuildReport finds what it needs itself.
        await SendReport();
    }

    private static async Task SendReport()
    {
        var context = CurrentScope.Context;
        var logger = CurrentScope.Bot.GetLogger<object>();

        logger.LogInformation("Building the report");
        await MessageSender.Send(context, "Your report is ready.");
    }
}
```

## When it is available

The scope exists only for the duration of an update. Code that did not start from an update — a background task started by a timer, a request to your own ASP.NET controller — has no scope, and reading `CurrentScope.Context` there gives you nothing useful.

In those places take the dependency explicitly: background tasks receive what they need through DI, and controllers have their own `IServiceProvider`.
