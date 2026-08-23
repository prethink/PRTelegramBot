---
description: What you need to change in your own code when moving from 0.9.x to 1.0.0.
---

# Migrating to 1.0

Version 1.0.0 is the first stable release, and it gathers every breaking change that had been accumulating through 0.9.x. From here the public API follows [semantic versioning](https://semver.org/), so the next chance to break anything is 2.0.

Only the changes that require an edit on your side are listed below. The full list, bug fixes included, is in the [changelog](https://github.com/prethink/PRTelegramBot/blob/master/CHANGELOG.md).

## The `Helpers.Message` facade was removed

This one affects almost everybody. `PRTelegramBot.Helpers.Message` was marked obsolete back in 0.9.0 and only forwarded its calls. It is now gone.

The replacements have identical signatures — only the type name changes:

| Was | Now |
| --- | --- |
| `Helpers.Message.Send(...)` | `MessageSender.Send(...)` |
| `Helpers.Message.Edit(...)` | `MessageEditor.Edit(...)` |

```csharp
// before
using PRTelegramBot.Helpers;
await Message.Send(context, "Hello");

// after
using PRTelegramBot.Services.Messages;
await MessageSender.Send(context, "Hello");
```

## Namespaces were unified

Each of these had been split in two: the files sat in one folder while declaring two different namespaces. Now there is one namespace per folder.

| Was | Now |
| --- | --- |
| `PRTelegramBot.InlineButtons` | `PRTelegramBot.Models.InlineButtons` |
| `PRTelegramBot.Core.Factory` | `PRTelegramBot.Core.Factories` |
| `PRTelegramBot.Models.TCommands` | `PRTelegramBot.Models.CallbackCommands` |
| `PRTelegramBot.Core.UpdateHandlers` | `PRTelegramBot.Core.UpdateDispatchers` |

Fixed by updating `using` directives; the types themselves did not change.

## Two attributes were renamed

They expressed the same idea under different names, and put the words in the opposite order to the Telegram.Bot types they filter on.

| Was | Now |
| --- | --- |
| `[RequiredTypeChat(...)]` | `[RequireChatType(...)]` |
| its `TypesChat` property | `ChatTypes` |
| `[RequireTypeMessage(...)]` | `[RequireMessageType(...)]` |
| its `TypeMessages` property | `MessageTypes` |

## Typos in names were fixed

| Was | Now |
| --- | --- |
| `AutoEditMessageСycle` | `AutoEditMessageCycle` |
| `OptionMessage.thumbnail` | `OptionMessage.Thumbnail` |

The "С" in the old `AutoEditMessageСycle` was a Cyrillic letter, so the name looked right but never matched a search through the code.

## Things that never worked were removed

* `PRTelegramBot.Models.InlineButton` — used nowhere, and its `GetContent` always threw `NotImplementedException`. Putting such a button into a menu was impossible.
* `IInlineStorage` — an interface nothing ever implemented.
* The `PRTelegramBot.Workflow` namespace — unfinished, empty stubs.

If any of these appeared in your code, it was not working there either.

## Hidden from the public API

* `PRLoggerEvents<T>` and `PRLoggerEventsFactory` are now `internal`. They are the internal fallback that keeps event-based logging working when no `ILoggerFactory` is supplied; use `ILogger` instead.
* `InlineCallbackWithConfirmation.DataCollection` is no longer public. Pending confirmations are looked up by the framework itself — and they no longer accumulate forever, since anything unanswered is discarded after an hour.

## Behaviour changes

These compile fine but act differently at runtime.

**`GetChatId`, `GetMessageId` and `GetUserId`** now throw `InvalidOperationException` with a clear message instead of a `NullReferenceException` when the update carries no such data. If you were catching `NullReferenceException`, change the type.

**`UpdateExtension.TryGetBot`** declares its `out` parameter as `PRBotBase?`, because it is `null` when the bot is not found. The compiler will now point this out.

**`FileInlineConverter(string path)`** used to ignore the folder name it was given and always use a folder literally called `path`. The name is now honoured, so inline payloads move to the folder you asked for — and any confirmation still pending at the moment of the upgrade will not be found.

**`InlineUtils.GetInlineButton`** no longer switches over concrete button types; it calls `GetInlineButton()` on the button itself. Built-in buttons convert exactly as before, but a subclass that overrides the conversion is now honoured, and button types the switch did not list now work instead of throwing.

## What is new

Worth a look while you are here:

* `InlineCopyText` — a button that copies the given text to the clipboard.
* `MessageBuilder` — composes a message from a template.
* `ReplyKeyboardBuilder.AddRequestManagedBot` — a button that asks the user to pick a bot.
* `OptionMessage.ShowCaptionAboveMedia`, together with other sending parameters Telegram supports that the library was not passing through.
* Message and update events that had been missed across Telegram.Bot upgrades.
