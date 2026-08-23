---
description: Plugging your own ILogger into the framework.
---

# Logging

Since version **0.9.2** the framework works with any logger built on **`Microsoft.Extensions.Logging.Abstractions`** — anything implementing `ILogger` and `ILoggerFactory`.

You can supply your implementation in two ways:

* through the bot builder;
* through the DI container.

## Why hand the logger to the bot at all?

**Question:** if the logger is already registered in DI, why pass it into the bot separately?

**Answer:** the framework logs from more places than your command handlers. Events, background tasks and internal operations all log too.

For the whole bot to use your logger, register `ILoggerFactory` / `ILogger` in DI, or pass the factory through the builder. Then every internal message, bot events included, goes through your implementation.

## Resolution order

{% hint style="warning" %}
Logging follows the [component resolution priorities](dependency-injection/resolution-priorities.md):

1. `ILoggerFactory` given through the builder
2. `ILogger` taken from the DI container
3. `ILoggerFactory` taken from the DI container
4. The built-in fallback logger based on events
{% endhint %}

The first one found wins, so a factory set on the builder overrides anything in DI.

### Through the DI container

```csharp
// Register your own logger factory in DI.
builder.Services.AddTransient<ILoggerFactory, YourLoggerFactory>();
```

### Through the bot builder

```csharp
var bot = new PRBotBuilder("YOUR_BOT_TOKEN")
    .SetLoggerFactory(new YourLoggerFactory())
    .Build();
```

## Using the logger

Ask the bot instance for a logger:

```csharp
var logger = bot.GetLogger<T>();
```

`T` is the type the logger is created for, and it becomes the logging category.

What you get back is an `ILogger<T>` from **Microsoft.Extensions.Logging.Abstractions**, so it is used exactly as anywhere else in .NET:

```csharp
logger.LogInformation("...");
logger.LogWarning("...");
logger.LogError(exception, "...");
```

Inside a handler you can also reach the logger through the execution scope, without holding a reference to the bot:

```csharp
context.Current.GetLogger<MyHandler>().LogWarning("Hello world");
```
