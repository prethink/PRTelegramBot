---
description: Reacting to things the framework notices but no command handles.
---

# Events

A command handles a message the user deliberately sent. Events cover everything else: a photo arriving, a command not being found, access being denied, an update passing through the pipeline.

Because PRTelegramBot can run several bots at once, events belong to a bot instance rather than being global. They all hang off `bot.Events`.

## The four groups

| Group | Covers |
| --- | --- |
| `bot.Events` | the bot's own events — access, privileges, missing commands, logs |
| `bot.Events.MessageEvents` | [message types](message-events.md) — photo, document, location, contact… |
| `bot.Events.UpdateEvents` | [update types](update-events.md) — pre/post update, chat membership, polls… |
| `bot.Events.CommandsEvents` | [commands](command-events.md) — before and after a command runs |

## Events on the bot itself

```csharp
/// <summary>
/// Raised when access is denied.
/// </summary>
public event Func<BotEventArgs, Task>? OnAccessDenied;

/// <summary>
/// Raised when the user sent /start with an argument.
/// </summary>
public event Func<StartEventArgs, Task>? OnUserStartWithArgs;

/// <summary>
/// Raised when privileges must be checked before a command runs.
/// </summary>
public event Func<PrivilegeEventArgs, Task>? OnCheckPrivilege;

/// <summary>
/// Raised when the message type is not the one the command expects.
/// </summary>
public event Func<BotEventArgs, Task>? OnWrongTypeMessage;

/// <summary>
/// Raised when the chat type is not the one the command expects.
/// </summary>
public event Func<BotEventArgs, Task>? OnWrongTypeChat;

/// <summary>
/// Raised when no command matched.
/// </summary>
public event Func<BotEventArgs, Task>? OnMissingCommand;

/// <summary>
/// Raised when a command threw.
/// </summary>
public event Func<BotEventArgs, Task>? OnErrorCommand;

/// <summary>
/// Raised on an error.
/// </summary>
public event Func<ErrorLogEventArgs, Task>? OnErrorLog;

/// <summary>
/// Raised on an ordinary log message.
/// </summary>
public event Func<CommonLogEventArgs, Task>? OnCommonLog;
```

`OnMissingCommand` is the one most bots want first: it is where "I did not understand that" lives.

`OnCheckPrivilege` is different from the rest — it does not just notify you, it **decides**. See [Restricted access to commands](../restricted-access.md).

## Writing handlers

An event handler is an ordinary method matching the delegate:

```csharp
public static async Task OnUserStartWithArgs(StartEventArgs args)
{
    await MessageSender.Send(args.Context, "The user sent start with an argument");
}

public static async Task OnWrongTypeMessage(BotEventArgs e)
{
    await MessageSender.Send(e.Context, "Wrong message type");
}
```

Every argument type carries a `Context`, so a handler can answer the user exactly as a command would.

## Subscribing

```csharp
// Dynamic commands parsed from a key:value JSON file.
var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
var dynamicCommands = botJsonProvider.GetKeysAndValues();

var telegram = new PRBotBuilder("")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();

// Ordinary log messages.
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
// Errors.
telegram.Events.OnErrorLog += Telegram_OnLogError;

await telegram.StartAsync();

InitEvents(telegram);

void InitEvents(PRBotBase bot)
{
    // Before every update.
    bot.Events.UpdateEvents.OnPreUpdate += Handler_OnUpdate;
    // After every update.
    bot.Events.UpdateEvents.OnPostUpdate += Handler_OnPostUpdate;

    // The message type was not the expected one.
    bot.Events.OnWrongTypeMessage += ExampleEvent.OnWrongTypeMessage;
    // The chat type was not the expected one.
    bot.Events.OnWrongTypeChat += ExampleEvent.OnWrongTypeChat;
    // /start carried a deeplink argument.
    bot.Events.OnUserStartWithArgs += ExampleEvent.OnUserStartWithArgs;
    // Privileges have to be checked.
    bot.Events.OnCheckPrivilege += ExampleEvent.OnCheckPrivilege;
    // No command matched.
    bot.Events.OnMissingCommand += ExampleEvent.OnMissingCommand;
    // The user was refused.
    bot.Events.OnAccessDenied += ExampleEvent.OnAccessDenied;

    // Message types.
    bot.Events.MessageEvents.OnLocationHandle  += ExampleEvent.OnLocationHandle;
    bot.Events.MessageEvents.OnContactHandle   += ExampleEvent.OnContactHandle;
    bot.Events.MessageEvents.OnPollHandle      += ExampleEvent.OnPollHandle;
    bot.Events.MessageEvents.OnWebAppsHandle   += ExampleEvent.OnWebAppsHandle;
    bot.Events.MessageEvents.OnDocumentHandle  += ExampleEvent.OnDocumentHandle;
    bot.Events.MessageEvents.OnAudioHandle     += ExampleEvent.OnAudioHandle;
    bot.Events.MessageEvents.OnVideoHandle     += ExampleEvent.OnVideoHandle;
    bot.Events.MessageEvents.OnPhotoHandle     += ExampleEvent.OnPhotoHandle;
    bot.Events.MessageEvents.OnStickerHandle   += ExampleEvent.OnStickerHandle;
    bot.Events.MessageEvents.OnVoiceHandle     += ExampleEvent.OnVoiceHandle;
    bot.Events.MessageEvents.OnVenueHandle     += ExampleEvent.OnVenueHandle;
    bot.Events.MessageEvents.OnGameHandle      += ExampleEvent.OnGameHandle;
    bot.Events.MessageEvents.OnVideoNoteHandle += ExampleEvent.OnVideoNoteHandle;
    bot.Events.MessageEvents.OnDiceHandle      += ExampleEvent.OnDiceHandle;
    bot.Events.MessageEvents.OnUnknownHandle   += ExampleEvent.OnUnknownHandle;

    // The bot was added to or removed from a chat.
    bot.Events.UpdateEvents.OnMyChatMemberHandle += ExampleEvent.OnUpdateMyChatMember;
}

async Task<UpdateResult> Handler_OnUpdate(BotEventArgs e)
{
    return UpdateResult.Continue;
}
```

Note that `OnPreUpdate` returns an `UpdateResult`: returning `Continue` lets the update proceed, and returning `Handled` stops it there. That makes it a place to filter updates before anything else sees them.

{% hint style="info" %}
Handlers are invoked without being awaited, so a slow subscriber cannot hold up other updates. A failure inside one is logged rather than lost — but it will not propagate to you, so do not rely on an exception from an event handler reaching anything.
{% endhint %}

## Pages

* [Message events](message-events.md)
* [Update events](update-events.md)
* [Command events](command-events.md)
* [Logging events](logging-events.md)
