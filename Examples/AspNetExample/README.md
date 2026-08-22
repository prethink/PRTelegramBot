# ASP.NET example (dependency injection)

**English** | [Русский](README.ru.md)

A bot living inside an ASP.NET Core application, where everything — handlers, middleware and background tasks — is resolved through the DI container.

Target framework: **net8.0** · Update delivery: **polling**

For the webhook variant see [AspNetWebHookExample](../AspNetWebHookExample/README.md).

## Running it

1. Get a bot token from [@BotFather](https://t.me/BotFather).
2. Put the token into `Program.cs`:
   ```csharp
   var prBotInstance = new PRBotBuilder("token")
   ```
3. Run the project. The web application and the bot start together.

No webhook or public URL is needed here — the bot polls Telegram itself.

## How the bot is wired into DI

The important call is `builder.Services.AddBotHandlers()`. It scans the assembly for classes marked with `[BotHandler]` and registers them, so their constructor dependencies are injected.

The bot then receives the container:

```csharp
var serviceProvaider = app.Services.GetService<IServiceProvider>();
var prBotInstance = new PRBotBuilder("token")
    .SetServiceProvider(serviceProvaider)
    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(BotInlineHandlerWithDependency))
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata())
    .Build();
```

## What is demonstrated

| Area | Where to look |
| --- | --- |
| Handler with injected dependencies | `BotController/BotHandlerWithDependency.cs` |
| Handler without dependencies | `BotController/BotHandlerWithoutDependency.cs` |
| Handler with static methods only | `BotController/BotHandlerOnlyStatic.cs` |
| Instance-based inline handler through DI | `BotController/BotInlineHandlerWithDependency.cs` |
| Middleware through DI | `MiddleWares/DIMiddleware.cs`, `MiddleWares/UserMiddleware.cs` |
| Background tasks through DI | `BackgroundTasks/` |
| Service lifetimes side by side | `Services/ServiceTransient.cs`, `ServiceScoped.cs`, `ServiceSingleton.cs` |
| EF Core (in-memory) inside a handler | `AppDbContext.cs` |
| Step-by-step commands with a cache | `Models/StepCache.cs` |

## Service lifetimes

Three services with different lifetimes are registered on purpose:

```csharp
builder.Services.AddTransient<ServiceTransient>();
builder.Services.AddScoped<ServiceScoped>();
builder.Services.AddSingleton<ServiceSingleton>();
```

Each update is handled inside its own scope, so a `Scoped` service lives exactly as long as one update is being processed. That is what makes `AddDbContext` safe to use directly in a handler.

## Worth noting

`IInlineMenuConverter` is registered in the container rather than on the builder:

```csharp
builder.Services.AddSingleton<IInlineMenuConverter>(new FileInlineConverter());
```

Both ways work. The builder wins if a converter is set in both places — the resolution order is: builder, then DI, then the default implementation.

---

See also: [main README](../../README.md) · [documentation](https://prethink.gitbook.io/prtelegrambot/)
