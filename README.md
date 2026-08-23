<img alt="PRTelegramBot" src="https://raw.githubusercontent.com/prethink/PRTelegramBot/master/LogoBot.png" width="96"/>

# PRTelegramBot

![Static Badge](https://img.shields.io/badge/version-v1.0.0-brightgreen) [![Static Badge](https://img.shields.io/badge/Telegram_Bot_API-10.2-blue)](https://core.telegram.org/bots/api) ![Static Badge](https://img.shields.io/badge/telegram.bot-22.10.2.1-blue) ![NuGet Downloads](https://img.shields.io/nuget/dt/prtelegrambot) ![NuGet Version](https://img.shields.io/nuget/v/prtelegrambot) [![License: MIT](https://img.shields.io/badge/license-MIT-green)](https://github.com/prethink/PRTelegramBot/blob/master/LICENSE)

**English** | [Русский](https://github.com/prethink/PRTelegramBot/blob/master/README.ru.md)


> If this project has been useful to you, you can support its development on Boosty:
> https://boosty.to/prethink
> A ⭐ on the repository is great support too.

[https://prethink.gitbook.io/prtelegrambot/](https://prethink.gitbook.io/prtelegrambot/) - documentation. Also available [in Russian](https://prethink.gitbook.io/prtelegrambot/ru/).
[https://www.nuget.org/packages/PRTelegramBot/](https://www.nuget.org/packages/PRTelegramBot/) - NuGet.
[https://t.me/prethinkdev](https://t.me/prethinkdev) - chat for questions.
[CHANGELOG.md](https://github.com/prethink/PRTelegramBot/blob/master/CHANGELOG.md) - release history.

# 📰 About

A .NET framework for building Telegram bots on top of Telegram.Bot: attribute-based command routing, menus, middleware, DI and background tasks.

In development since 2023, currently tracking **Bot API 10.2** through [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot). The wrapper is not hidden: every method and type it gives you stays available. What the framework adds on top is the layer that otherwise gets rewritten by hand in every project — command routing, menus, state between messages, access control, configuration and background work.

Handlers are ordinary methods marked with an attribute. There is no registration table to keep in sync — the framework finds them by reflection at startup, so adding a command means adding a method:

```csharp
[SlashHandler("/start")]
public static async Task Start(IBotContext context)
{
    await MessageSender.Send(context, "Hello, World!");
}
```

# 🚀 Getting started

### Prerequisites

The library targets **.NET 6.0** and runs on any newer version, so you only need the [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed. You also need a bot token from [BotFather](https://t.me/botfather) — the [official tutorial](https://core.telegram.org/bots/tutorial#obtain-your-bot-token) walks through it.

### Installation

Create a console application and add the package:

```sh
dotnet new console -o MyBot
cd MyBot
dotnet add package PRTelegramBot
```

### Hello world

Put this in `Program.cs`:

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

var bot = new PRBotBuilder("YOUR_BOT_TOKEN").Build();
await bot.StartAsync();

// Keeps the console application alive.
await Task.Delay(Timeout.Infinite);

public static class Commands
{
    // Runs when the user sends /start.
    [SlashHandler("/start")]
    public static async Task Start(IBotContext context)
    {
        await MessageSender.Send(context, "Hello, World!");
    }

    // Runs when the message text is exactly "Ping", ignoring case.
    [ReplyMenuHandler("Ping")]
    public static async Task Ping(IBotContext context)
    {
        await MessageSender.Send(context, "Pong");
    }
}
```

Run it with `dotnet run` and send `/start` to your bot. Note that `Commands` is never referenced from `Program.cs` — the framework discovers both handlers on startup.

By default the bot receives updates through [polling](https://core.telegram.org/bots/faq#how-do-i-get-updates), which needs no public address and is the quickest way to start. Webhooks are configured on the same builder.

> [!WARNING]
> Keep your bot token out of version control. Use [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets), environment variables or a configuration file that is excluded from the repository.

From here the documentation covers the rest:

| | |
| --- | --- |
| [Getting started](https://prethink.gitbook.io/prtelegrambot/getting-started) | the same walkthrough in more detail, plus [webhooks](https://prethink.gitbook.io/prtelegrambot/getting-started/webhook) |
| [Command handling](https://prethink.gitbook.io/prtelegrambot/command-handling) | reply, slash and inline commands, menus and keyboards |
| [PRBotBuilder](https://prethink.gitbook.io/prtelegrambot/prbotbuilder) | everything a bot can be configured with |
| [Dependency injection](https://prethink.gitbook.io/prtelegrambot/dependency-injection) | handlers resolved from a container |
| [Migrating to 1.0](https://prethink.gitbook.io/prtelegrambot/migrating-to-1.0) | what to change when coming from 0.9.x |
| [F.A.Q.](https://prethink.gitbook.io/prtelegrambot/faq) | the problems people hit most often |

# 🧩 Examples

| Example | What it shows |
| --- | --- |
| [Console](https://github.com/prethink/PRTelegramBot/tree/master/Examples/ConsoleExample) | Most of the framework in one place: commands of every kind, menus, events, middleware, background tasks. Start here. |
| [ASP.NET](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetExample) | A bot inside ASP.NET Core with everything resolved through dependency injection. Polling. |
| [ASP.NET webhook](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetWebHookExample) | Two bots on a single webhook endpoint, told apart by their secret token. |

There is also a [quick-start template](https://github.com/prethink/PRTelegramBot/tree/master/Templates/FastBotTemplate) for a new console bot.

# 💎 Features

### Commands and routing

 - **Reply commands.** Support for simple text commands.
 - **Dynamic reply commands.** Text commands loaded from a configuration file, with no recompilation needed.
 - **Commands with parameters.** Support for commands that carry parameters in brackets, for example "Test (1)".
 - **Slash commands.** Handling of commands such as /get_1, /users and other text commands, with a configurable argument separator, typed argument access through `context.GetSlashArgs<T>()`, and /start deeplinks.
 - **Flexible inline commands.** A generator and a parser for inline commands.
 - **Step-by-step command execution.** Run sequential sets of reply commands.
 - **Dynamic command management.** Add and remove commands at runtime, with the option to implement your own command registrar.
 - **Pre-execution checks.** Internal checks for reply, dynamicreply, nextstep, slash and inline commands.
 - **Custom handlers for message and callbackQuery updates.** Implement your own handlers, just like reply, slash and inlineCallback.

### Menus, keyboards and messages

 - **Menu creation.** Simple and flexible creation of reply and inline menus.
 - **Keyboard builders.** `ReplyKeyboardBuilder` and `InlineKeyboardBuilder` for building keyboards fluently, with rows, columns, filler buttons and request buttons (contact, location, poll, chat, users, WebApp).
 - **Message builder.** `MessageBuilder` composes texts from a template with positional arguments and named tokens such as `{QA}`, including lazily resolved values.
 - **Inline confirmations.** `InlineCallbackWithConfirmation` wraps a button so the user is asked to confirm before the action runs.
 - **Paginated messages.** Message management with page-by-page navigation.
 - **Waiting messages.** `MessageAwaiter` posts a placeholder message while the data is being processed and removes it afterwards.
 - **Built-in calendar.** Working with dates and calendars.
 - **Media helpers.** `MediaSender` and `MediaEditor` for photos, photo groups, files and media by URL; `MessageCopier` for copying messages.

### Hosting and infrastructure

 - **Polling and webhook bots.** Support for different ways of running a bot.
 - **Hosted service.** A bot is an `IHostedService`, so it plugs straight into ASP.NET Core and the Generic Host.
 - **Multi-bot system.** Run several bots in a single project.
 - **Connecting to your own servers.** Run bots against your own servers.
 - **Dropping stale updates.** Drop every pending update before the bot starts.
 - **Background tasks.** Recurring tasks with metadata, retry and run limits, and DI support.
 - **Dependency injection.** Dependency injection support.
 - **Execution scope.** `CurrentScope` gives you the current bot, its context and its services anywhere in code that was invoked by a Telegram update.
 - **Logging.** Works with `ILogger` / `ILoggerFactory`, resolved from the builder or from DI, with a built-in fallback.

### Users and access control

 - **Admin manager.** Manage the bot's administrators, with the option to implement your own admin manager.
 - **User white list manager.** Flexible white list management: mark methods to be ignored by the white list, or implement your own white list manager.
 - **Method access control.** Restrict access to specific methods.
 - **User cache storage.** Working with a per-user cache.
 - **Group utilities.** `GroupUtils` checks whether a user is a member, an administrator or the creator of a group.

### Extensibility

 - **Middleware system.** Add your own handlers before and after an update, similar to middleware in ASP.NET.
 - **Event system.** A flexible event handling system.
 - **Event bus.** `PREventBus` and global subscribers for broadcasting events across the application.
 - **Update handling.** Implement your own update handler.
 - **Inline data converters.** `IInlineMenuConverter` lets you choose how `callback_data` is built; the bundled `FileInlineConverter` stores the payload on disk to work around Telegram's 64-byte limit.
 - **Interchangeable serializers.** `JsonSerializerWrapper` or `ToonSerializerWrapper` for inline button data — ToonNet produces a more compact `callback_data`.
 - **Configuration files.** Per-bot configuration files, with the option to implement your own configuration provider. JSON is used by default.
 - **Parsing from configuration files.** Parse messages, commands and buttons from configuration files.
 - **Everything telegram.bot provides.**

# 🧱 Integrated packages
 - CalendarPicker | karb0f0s   https://github.com/karb0f0s/CalendarPicker
 - ToonNet   https://www.nuget.org/packages/ToonNet

# 🛡️ Versioning

Version 1.0.0 is the first stable release. From this point the public API follows [semantic versioning](https://semver.org/): breaking changes land only in major versions, new functionality in minor ones, fixes in patches. Members that are going to be removed are marked `[Obsolete]` first, so an upgrade warns you at compile time before anything breaks.

Every release is described in the [changelog](https://github.com/prethink/PRTelegramBot/blob/master/CHANGELOG.md), breaking changes first.

# 🤝 Contributing and feedback

Pull requests for bug fixes, features and documentation are welcome — please open an issue first for anything large, so the design can be agreed before the work is done. [CONTRIBUTING.md](https://github.com/prethink/PRTelegramBot/blob/master/CONTRIBUTING.md) covers building, testing and the conventions this project follows.

This project has adopted the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/about/policies/code-of-conduct) — see [CODE_OF_CONDUCT.md](https://github.com/prethink/PRTelegramBot/blob/master/CODE_OF_CONDUCT.md).

If you have a question about using the framework, ask in the [Telegram chat](https://t.me/prethinkdev). For bugs and feature requests, open a [GitHub issue](https://github.com/prethink/PRTelegramBot/issues). For a security problem, please do not open a public issue — follow [SECURITY.md](https://github.com/prethink/PRTelegramBot/blob/master/SECURITY.md) instead.

# 📄 License

Distributed under the [MIT License](https://github.com/prethink/PRTelegramBot/blob/master/LICENSE).
