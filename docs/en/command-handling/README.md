---
description: The three ways the framework routes an incoming message to your code.
---

# Command handling

The library has three main kinds of command:

* **Reply** — handles the text a user sends.
* **Slash** — handles a message written as a slash command. Telegram renders these as clickable links: `/get`, `/get_1`.
* **Inline** — handles a press on an inline button, which happens in the background without the user sending a message.

Each kind has its own attribute and its own page:

* [Reply commands](reply-commands/)
* [Slash commands](slash-commands.md)
* [Inline commands](inline-commands/)

[Parameters](parameters.md) describes the arguments these attributes share — `botId`, `CommandComparison`, `StringComparison`.

## What a handler looks like

A handler is an ordinary method that takes an `IBotContext`. The framework finds it by reflection at startup — nothing registers it by hand.

### As a static method

```csharp
[HandlerAttribute]
public static async Task MethodName(IBotContext context)
{
    // Your code.
}
```

### As an instance method with dependency injection

Mark the class with `[BotHandler]` and the framework will resolve it through the service provider, so the constructor can take dependencies.

```csharp
[BotHandler]
public class BotHandler
{
    private readonly ILogger<BotHandler> _logger;

    public BotHandler(ILogger<BotHandler> logger)
    {
        _logger = logger;
    }

    [HandlerAttribute]
    public async Task MethodName(IBotContext context)
    {
        // Your code.
    }
}
```

Both forms are equivalent as far as routing is concerned. Use the static one for simple commands and the instance one when the handler needs services — see [Dependency injection](../dependency-injection/).
