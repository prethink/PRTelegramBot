---
description: Events raised immediately before and after each kind of command runs.
---

# Command events

`bot.Events.CommandsEvents` brackets each kind of command with a pair of events. Where [middleware](../middleware.md) wraps the whole update and `OnPreUpdate` sees everything, these fire only when a command of that particular kind has actually been matched.

```csharp
/// <summary>
/// Raised before a reply command is handled.
/// </summary>
public event Func<BotEventArgs, Task>? OnPreReplyCommandHandle;

/// <summary>
/// Raised after a reply command has been handled.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostReplyCommandHandle;
```

## The full set

| Command kind | Before | After |
| --- | --- | --- |
| Reply | `OnPreReplyCommandHandle` | `OnPostReplyCommandHandle` |
| Dynamic reply | `OnPreDynamicReplyCommandHandle` | `OnPostDynamicReplyCommandHandle` |
| Slash | `OnPreSlashCommandHandle` | `OnPostSlashCommandHandle` |
| Inline | `OnPreInlineCommandHandle` | `OnPostInlineCommandHandle` |
| Next step | `OnPreNextStepCommandHandle` | `OnPostNextStepCommandHandle` |

## What they are for

None of them can stop a command — they notify, they do not decide. Their value is that by the time they fire, the framework has already worked out *which* command this is, which the earlier hooks have not.

That makes them the right place for:

* **Usage metrics.** Counting which commands are actually used, without touching a single handler.
* **Timing.** Start a stopwatch in the pre event, read it in the post one, and find the slow commands.
* **Audit trails.** Recording that a user ran a command, uniformly, rather than remembering to log it in each handler.

To *prevent* a command from running, use [command checks](../command-handling/pre-execution-checks.md), the `OnCheckPrivilege` event, or [middleware](../middleware.md) — those run early enough to say no.

```csharp
bot.Events.CommandsEvents.OnPreSlashCommandHandle += async e =>
{
    metrics.Increment(e.Context.Update.Message?.Text ?? "unknown");
};
```
