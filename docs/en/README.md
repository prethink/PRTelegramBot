---
description: >-
  A .NET framework for building Telegram bots on top of Telegram.Bot:
  attribute-based command routing, menus, middleware, DI and background tasks.
---

# PRTelegramBot

> #### <mark style="color:$info;">**If this project has been useful to you, you can support its development on Boosty:**</mark> [<mark style="color:orange;">**https://boosty.to/prethink**</mark>](https://boosty.to/prethink) <mark style="color:$info;">**A ⭐ on the**</mark> [<mark style="color:blue;">**repository**</mark>](https://github.com/prethink/PRTelegramBot) <mark style="color:$info;">**is great support too.**</mark>

{% hint style="info" %}
The [API reference](api/) is generated from the XML documentation comments in the library's source, so it cannot drift away from the code. The same documentation is also available [in Russian](https://prethink.gitbook.io/prtelegrambot/ru/).
{% endhint %}

## Source code

[https://github.com/prethink/PRTelegramBot](https://github.com/prethink/PRTelegramBot)

PRTelegramBot is listed in Telegram's official [Bot API library examples](https://core.telegram.org/bots/samples).

This documentation covers version 1.1.0.

## ⚛️ Framework core

PRTelegramBot is built on top of [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot), so every method and type that library gives you stays available. Its [documentation](https://telegrambots.github.io/book/) applies here too.

* Telegram.Bot v22.10.3 [https://github.com/TelegramBots/Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot)
* Supports [Bot API 10.3](https://core.telegram.org/bots/api)

The library targets .NET 6.0 and runs on any newer version.

## 🚀 Hello world

Create a console application, add the package, and put this in `Program.cs`:

```sh
dotnet new console -o MyBot
cd MyBot
dotnet add package PRTelegramBot
```

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

Handlers are ordinary methods marked with an attribute. There is no registration table to keep in sync — `Commands` is never referenced from `Program.cs`, the framework finds both handlers by reflection at startup.

By default the bot receives updates through [polling](https://core.telegram.org/bots/faq#how-do-i-get-updates), which needs no public address. Webhooks are configured on the same builder.

{% hint style="warning" %}
Keep your bot token out of version control. Use [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets), environment variables or a configuration file that is excluded from the repository.
{% endhint %}

## 💎 Features

### Commands and routing

* **Reply commands.** Support for simple text commands.
* **Dynamic reply commands.** Text commands loaded from a configuration file, with no recompilation needed.
* **Commands with parameters.** Support for commands that carry parameters in brackets, for example "Test (1)".
* **Slash commands.** Handling of commands such as /get\_1, /users and other text commands, with a configurable argument separator, typed argument access through `context.GetSlashArgs<T>()`, and /start deeplinks.
* **Flexible inline commands.** A generator and a parser for inline commands.
* **Step-by-step command execution.** Run sequential sets of reply commands.
* **Dynamic command management.** Add and remove commands at runtime, with the option to implement your own command registrar.
* **Pre-execution checks.** Internal checks for reply, dynamicreply, nextstep, slash and inline commands.
* **Custom handlers for message and callbackQuery updates.** Implement your own handlers, just like reply, slash and inlineCallback.

### Menus, keyboards and messages

* **Menu creation.** Simple and flexible creation of reply and inline menus.
* **Keyboard builders.** `ReplyKeyboardBuilder` and `InlineKeyboardBuilder` for building keyboards fluently, with rows, columns, filler buttons and request buttons (contact, location, poll, chat, users, WebApp).
* **Message builder.** `MessageBuilder` composes texts from a template with positional arguments and named tokens such as `{QA}`, including lazily resolved values.
* **Inline confirmations.** `InlineCallbackWithConfirmation` wraps a button so the user is asked to confirm before the action runs.
* **Disabled buttons.** `InlineDisabled` shows a button greyed out, so a menu keeps its shape while an option is unavailable instead of losing a button and jumping.
* **Ephemeral messages.** `MessageSender.SendEphemeral` answers one person in a group with a message only they see, which never enters the chat history.
* **Rich messages.** `MessageSender.SendRichMessage` sends a message built from blocks — headings, lists, tables, quotations and embedded media — with the same options as any other message.
* **Paginated messages.** Message management with page-by-page navigation.
* **Waiting messages.** `MessageAwaiter` posts a placeholder message while the data is being processed and removes it afterwards.
* **Built-in calendar.** Working with dates and calendars.
* **Media helpers.** `MediaSender` and `MediaEditor` for photos, photo groups, files and media by URL; `MessageCopier` for copying messages.

### Hosting and infrastructure

* **Polling and webhook bots.** Support for different ways of running a bot.
* **Hosted service.** A bot is an `IHostedService`, so it plugs straight into ASP.NET Core and the Generic Host.
* **Multi-bot system.** Run several bots in a single project.
* **Connecting to your own servers.** Run bots against your own servers.
* **Dropping stale updates.** Drop every pending update before the bot starts.
* **Background tasks.** Recurring tasks with metadata, retry and run limits, and DI support.
* **Dependency injection.** Dependency injection support.
* **Execution scope.** `CurrentScope` gives you the current bot, its context and its services anywhere in code that was invoked by a Telegram update.
* **Logging.** Works with `ILogger` / `ILoggerFactory`, resolved from the builder or from DI, with a built-in fallback.

### Users and access control

* **Admin manager.** Manage the bot's administrators, with the option to implement your own admin manager.
* **User white list manager.** Flexible white list management: mark methods to be ignored by the white list, or implement your own white list manager.
* **Method access control.** Restrict access to specific methods.
* **User cache storage.** Working with a per-user cache.
* **Group utilities.** `GroupUtils` checks whether a user is a member, an administrator or the creator of a group.

### Extensibility

* **Middleware system.** Add your own handlers before and after an update, similar to middleware in ASP.NET.
* **Event system.** A flexible event handling system.
* **Event bus.** `PREventBus` and global subscribers for broadcasting events across the application.
* **Update handling.** Implement your own update handler.
* **Inline data converters.** `IInlineMenuConverter` lets you choose how `callback_data` is built; the bundled `FileInlineConverter` stores the payload on disk to work around Telegram's 64-byte limit.
* **Interchangeable serializers.** `JsonSerializerWrapper` or `ToonSerializerWrapper` for inline button data — ToonNet produces a more compact `callback_data`.
* **Configuration files.** Per-bot configuration files, with the option to implement your own configuration provider. JSON is used by default.
* **Parsing from configuration files.** Parse messages, commands and buttons from configuration files.
* **Everything Telegram.Bot provides.**

## 🧱 Integrated packages

CalendarPicker | karb0f0s [https://github.com/karb0f0s/CalendarPicker](https://github.com/karb0f0s/CalendarPicker)

ToonNet [https://www.nuget.org/packages/ToonNet](https://www.nuget.org/packages/ToonNet)

## 🛡️ Versioning

Version 1.0.0 is the first stable release. From this point the public API follows [semantic versioning](https://semver.org/): breaking changes land only in major versions, new functionality in minor ones, fixes in patches.

Every release is described in the [changelog](https://github.com/prethink/PRTelegramBot/blob/master/CHANGELOG.md), breaking changes first.

## 🤝 Getting help

Ask in the [Telegram chat](https://t.me/prethinkdev), or open a [GitHub issue](https://github.com/prethink/PRTelegramBot/issues) for bugs and feature requests.
