---
description: One method serving every bot, and the flags that control a step sequence.
---

# Working with commands

## One method for every bot

When a project runs several bots and a command should be available on all of them, use `-1` as the `BotId` instead of listing them.

```csharp
/// <summary>
/// Runs for every bot, whatever its botId.
/// Runs when the user writes "Command for all bots".
/// </summary>
[ReplyMenuHandler(-1, "Command for all bots")]
public static async Task ReplyExampleAllBots(IBotContext context)
{
    await MessageSender.Send(context, nameof(ReplyExampleAllBots));
}
```

Works with:

* `ReplyMenuHandlerAttribute`
* `ReplyMenuDynamicHandlerAttribute`
* `SlashHandlerAttribute`
* `InlineCallbackHandlerAttribute`

Useful for the commands every bot needs regardless of what it is for — `/start`, `/help`, a support contact.

## Ending a step sequence on the last step

Since version 0.6 a step can announce that it is the last one, so the sequence finishes without an explicit clear.

```csharp
var handler = context.GetStepHandler<StepTelegram>();
handler.LastStepExecuted = true;
```

This does the same as calling `context.ClearStepUserHandler()`, but reads better in the step that knows it is the end — see [Step-by-step commands](../step-by-step-commands.md).

## Ignoring ordinary commands during a step sequence

Also since 0.6: a flag telling the framework that while this sequence is running, everything except the next step should be ignored.

```csharp
var handler = context.GetStepHandler<StepTelegram>();
handler.IgnoreBasicCommands = true;
```

{% hint style="warning" %}
This is the difference between a wizard and a trap. With the flag set, the user cannot leave by typing a command — not even `/start`. Give them a way out: a cancel button, or a deadline on the step, so an abandoned sequence expires by itself.
{% endhint %}
