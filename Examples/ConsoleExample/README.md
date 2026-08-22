# Console example

**English** | [Русский](README.ru.md)

A console bot that walks through most of the framework: commands of every kind, menus, events, middleware, background tasks and the helper utilities.

Target framework: **net7.0**

## Running it

1. Get a bot token from [@BotFather](https://t.me/BotFather).
2. Put the token into `Program.cs`:
   ```csharp
   var telegram = new PRBotBuilder("token")
   ```
3. Optionally add your Telegram id to `.AddAdmin(...)` so the admin-only examples work for you.
4. Run the project and send `/start` to the bot.

The console stays open after the bot starts — type `exit` to shut it down.

## Configuration files

`Configs/` holds the JSON files the bot reads at startup. They are wired up with `.AddConfigPaths(Initializer.GetConfigPaths())`.

| File | What it holds |
| --- | --- |
| `telegram.json` | Bot token, administrators, white list, startup options |
| `commands.json` | Texts of the dynamic reply commands |
| `buttons.json` | Button captions |
| `messages.json` | Message texts |

`commands.json` is what makes dynamic commands work: the value stored under a key becomes the trigger text, so a command can be renamed without recompiling.

## What is demonstrated

| Area | Where to look |
| --- | --- |
| Reply commands, comparison modes, commands with parameters | `Examples/Commands/ExampleReplyCommands.cs` |
| Slash commands, arguments, `/start` with a deeplink | `Examples/Commands/ExampleSlashCommands.cs` |
| Inline commands and menu generation | `Examples/Commands/ExampleInlineCommands.cs` |
| Inline buttons with a confirmation step | `Examples/Commands/ExampleInlineConfirmation.cs` |
| Step-by-step command execution | `Examples/Commands/ExampleStepCommand.cs` |
| Keyboard builders | `Examples/Builders/InlineKeyboard.cs` |
| Instance-based inline handler | `Examples/InlineClassHandlers/InlineDefaultClassHandler.cs` |
| Events: updates, messages, logs, privileges | `Examples/Events/` |
| Middleware before and after an update | `Middlewares/` |
| Background tasks, with and without an attribute | `BackgroundTasks/` |
| Custom checks before a command runs | `Checkers/` |
| Custom attribute for access control | `Attributes/AdminOnlyExampleAttribute.cs` |
| Calendar | `Examples/ExampleCalendar.cs` |
| Paginated output | `Examples/ExamplePage.cs` |
| User cache | `Examples/ExampleUserCache.cs` |
| White list | `Examples/ExampleWhiteList.cs` |
| Admin check | `Examples/ExampleAdminCheck.cs` |
| Auto-delete / auto-edit, waiting messages | `Examples/ExampleUtils.cs` |
| WebApp button | `Examples/WebApp.html` |

Everything is registered in `Services/Initializer.cs` — that is the place to start reading.

## Worth noting

`Program.cs` sets `.SetInlineMenuConverter(new FileInlineConverter())`. This stores inline payloads on disk instead of packing them into `callback_data`, which works around Telegram's 64-byte limit. Without it, large payloads such as the "Example with a long text" button would not fit.

---

See also: [main README](../../README.md) · [documentation](https://prethink.gitbook.io/prtelegrambot/)
