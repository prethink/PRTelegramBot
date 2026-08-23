---
description: Handling one inline command with a class instead of a static method.
---

# Instance inline handler

Available since version 0.7.6.

Ordinary inline handlers are static methods, which means they cannot take dependencies. An **instance inline handler** is a class dedicated to one command, so it can have a constructor — and therefore be resolved from DI with whatever it needs.

The class implements [`ICallbackQueryCommandHandler`](../../api/interfaces/icallbackquerycommandhandler.md), whose `Handle` method returns the result of handling.

<figure><img src="../../.gitbook/assets/изображение (7).png" alt="A class implementing ICallbackQueryCommandHandler with its Handle method"><figcaption>The class implements <code>Handle</code> and returns an <code>UpdateResult</code></figcaption></figure>

## Registering it

Register the class on the builder, against the command it serves. **One class handles one command.**

<figure><img src="../../.gitbook/assets/изображение (8).png" alt="AddInlineClassHandler called on the builder, binding a command header to a class type"><figcaption>The header and the type are bound on the builder</figcaption></figure>

```csharp
var telegram = new PRBotBuilder("Token")
    .SetServiceProvider(serviceProvider)
    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(BotInlineHandlerWithDependency))
    .Build();
```

Because the type is registered rather than an instance, the framework constructs it — and with a service provider set, its constructor dependencies are resolved. That is the whole reason this exists.

## When to prefer it

| | Use |
| --- | --- |
| A short handler needing nothing external | a static method with `[InlineCallbackHandler]` |
| A handler needing a repository, a service, a logger | an instance handler |
| A handler with enough logic to deserve its own tests | an instance handler — a class is testable, a static method reached by reflection is less so |

## Examples

Working examples are in [AspNetExample](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetExample), which uses DI, and [ConsoleExample](https://github.com/prethink/PRTelegramBot/tree/master/Examples/ConsoleExample).

For handling that is not tied to a single command — matching by prefix, or by a rule of your own — see [Custom command handlers](../custom-handlers.md), which uses the same interface but is asked about every callback.
