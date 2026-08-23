---
description: >-
  Since version 0.5.0 handlers can be resolved through a dependency injection
  container.
---

# Dependency injection

This page shows how to build a Telegram bot with dependency injection inside ASP.NET Core. The full example is [AspNetExample](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetExample).

## 1. Register the handlers

In `Program.cs`, call `AddBotHandlers()`:

```csharp
builder.Services.AddBotHandlers();

// or, to control the lifetime:
builder.Services.AddTransientBotHandlers();
builder.Services.AddSingletonBotHandlers();
builder.Services.AddScopedBotHandlers();
```

This scans the assembly for every class marked with `[BotHandler]`, registers it, and lets the container inject whatever those classes ask for in their constructors.

## 2. Hand the service provider to the bot

After `builder.Build()`, create the bot and pass it the `IServiceProvider`:

```csharp
var app = builder.Build();
var serviceProvider = app.Services.GetService<IServiceProvider>();

var telegram = new PRBotBuilder("Token")
                    .SetServiceProvider(serviceProvider)
                    .Build();
```

Without this the framework has nowhere to resolve handlers from, and they fall back to being treated as static.

### Program.cs in full

```csharp
using PRTelegramBot.Builders;
using PRTelegramBot.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ... register your own dependencies here

// Register the classes marked with [BotHandler].
builder.Services.AddBotHandlers();

var app = builder.Build();
var serviceProvider = app.Services.GetService<IServiceProvider>();

var telegram = new PRBotBuilder("Token")
                    .SetServiceProvider(serviceProvider)
                    .Build();

await telegram.StartAsync();

app.Run();
```

## 3. Write the handler class

Mark the class with `[BotHandler]`, take the dependencies you need in the constructor, and write the handlers as instance methods:

```csharp
[HandlerAttribute]
public async Task MethodName(IBotContext context)
{
    // Your code.
}
```

### A full handler class

```csharp
namespace TestDI.BotController
{
    [BotHandler]
    public class BotController
    {
        private readonly ILogger<BotController> _logger;

        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethod(IBotContext context)
        {
            await MessageSender.Send(context, $"{nameof(TestMethod)} {_logger != null}");
        }

        [SlashHandler("/test")]
        public async Task Slash(IBotContext context)
        {
            await MessageSender.Send(context, nameof(Slash));
        }

        [ReplyMenuHandler("inline")]
        public async Task InlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var menuItems = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", THeader.CurrentPage),
                new InlineCallback("TestStatic", THeader.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItems);
            await MessageSender.Send(context, nameof(InlineTest), options);
        }

        [InlineCallbackHandler<THeader>(THeader.CurrentPage)]
        public async Task InlineHandler(IBotContext context)
        {
            await MessageSender.Send(context, nameof(InlineHandler));
        }

        // Static methods work inside a [BotHandler] class too — they simply
        // cannot use the injected fields.
        [InlineCallbackHandler<THeader>(THeader.NextPage)]
        public static async Task InlineHandlerStatic(IBotContext context)
        {
            await MessageSender.Send(context, nameof(InlineHandlerStatic));
        }
    }
}
```

## Which component wins

When a bot needs a logger, a serializer or a manager, and one is available from more than one place, the framework follows a fixed order. See [Component resolution priorities](resolution-priorities.md).
