![Static Badge](https://img.shields.io/badge/version-v0.9.11-brightgreen) ![Static Badge](https://img.shields.io/badge/telegram.bot-22.10.2.1-blue)  ![NuGet Downloads](https://img.shields.io/nuget/dt/prtelegrambot) ![NuGet Version](https://img.shields.io/nuget/v/prtelegrambot)

**English** | [Русский](https://github.com/prethink/PRTelegramBot/blob/master/README.ru.md)


> If this project has been useful to you, you can support its development on Boosty:
> https://boosty.to/prethink
> A ⭐ on the repository is great support too.

[https://prethink.gitbook.io/prtelegrambot/](https://prethink.gitbook.io/prtelegrambot/) - up-to-date documentation.
[https://www.nuget.org/packages/PRTelegramBot/](https://www.nuget.org/packages/PRTelegramBot/) - NuGet.
[https://t.me/prethinkdev](https://t.me/prethinkdev) - chat for questions.
[CHANGELOG.md](https://github.com/prethink/PRTelegramBot/blob/master/CHANGELOG.md) - release history.

# ⚛️ Framework core
TelegramBot v22.10.2.1 https://github.com/TelegramBots/Telegram.Bot

# 📰 Description
An open-source framework with flexible and simple functionality for creating Telegram bots.
Examples: https://github.com/prethink/PRTelegramBot/tree/master/Examples
Video examples: [https://github.com/prethink/PRTelegramYoutube](https://github.com/prethink/PRTelegramYoutubeOld)

# 💎 Features

 - **Reply commands.** Support for simple text commands.
 - **Dynamic reply commands.** Text commands loaded from a configuration file, with no recompilation needed.
 - **Commands with parameters.** Support for commands that carry parameters in brackets, for example "Test (1)".
 - **Slash commands.** Handling of commands such as /get_1, /users and other text commands, with a configurable argument separator, typed argument access through `context.GetSlashArgs<T>()`, and /start deeplinks.
 - **Flexible inline commands.** A generator and a parser for inline commands.
 - **Inline data converters.** `IInlineMenuConverter` lets you choose how `callback_data` is built; the bundled `FileInlineConverter` stores the payload on disk to work around Telegram's 64-byte limit.
 - **Interchangeable serializers.** `JsonSerializerWrapper` or `ToonSerializerWrapper` for inline button data — ToonNet produces a more compact `callback_data`.
 - **Inline confirmations.** `InlineCallbackWithConfirmation` wraps a button so the user is asked to confirm before the action runs.
 - **Menu creation.** Simple and flexible creation of reply and inline menus.
 - **Keyboard builders.** `ReplyKeyboardBuilder` and `InlineKeyboardBuilder` for building keyboards fluently, with rows, columns, filler buttons and request buttons (contact, location, poll, chat, users, WebApp).
 - **Message builder.** `MessageBuilder` composes texts from a template with positional arguments and named tokens such as `{QA}`, including lazily resolved values.
 - **Configuration files.** Per-bot configuration files, with the option to implement your own configuration provider. JSON is used by default.
 - **Admin manager.** Manage the bot's administrators, with the option to implement your own admin manager.
 - **User white list manager.** Flexible white list management: mark methods to be ignored by the white list, or implement your own white list manager.
 - **Update handling.** Implement your own update handler.
 - **Event system.** A flexible event handling system.
 - **Event bus.** `PREventBus` and global subscribers for broadcasting events across the application.
 - **Multi-bot system.** Run several bots in a single project.
 - **Middleware system.** Add your own handlers before and after an update, similar to middleware in ASP.NET.
 - **Pre-execution checks.** Internal checks for reply, dynamicreply, nextstep, slash and inline commands.
 - **Custom handlers for message and callbackQuery updates.** Implement your own handlers, just like reply, slash and inlineCallback.
 - **Dynamic command management.** Add and remove commands at runtime, with the option to implement your own command registrar.
 - **Dropping stale updates.** Drop every pending update before the bot starts.
 - **Step-by-step command execution.** Run sequential sets of reply commands.
 - **Connecting to your own servers.** Run bots against your own servers.
 - **Polling and webhook bots.** Support for different ways of running a bot.
 - **Hosted service.** A bot is an `IHostedService`, so it plugs straight into ASP.NET Core and the Generic Host.
 - **Built-in calendar.** Working with dates and calendars.
 - **Paginated messages.** Message management with page-by-page navigation.
 - **Waiting messages.** `MessageAwaiter` posts a placeholder message while the data is being processed and removes it afterwards.
 - **Media helpers.** `MediaSender` and `MediaEditor` for photos, photo groups, files and media by URL; `MessageCopier` for copying messages.
 - **User cache storage.** Working with a per-user cache.
 - **Method access control.** Restrict access to specific methods.
 - **Group utilities.** `GroupUtils` checks whether a user is a member, an administrator or the creator of a group.
 - **Dependency injection.** Dependency injection support.
 - **Execution scope.** `CurrentScope` gives you the current bot, its context and its services anywhere in code that was invoked by a Telegram update.
 - **Parsing from configuration files.** Parse messages, commands and buttons from configuration files.
 - **Background tasks.** Recurring tasks with metadata, retry and run limits, and DI support.
 - **Logging.** Works with `ILogger` / `ILoggerFactory`, resolved from the builder or from DI, with a built-in fallback.
 - **Everything telegram.bot provides.**

# 🧱 Integrated packages
 - CalendarPicker | karb0f0s   https://github.com/karb0f0s/CalendarPicker
 - ToonNet   https://www.nuget.org/packages/ToonNet
