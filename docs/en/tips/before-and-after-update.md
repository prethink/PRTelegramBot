---
description: Running your own logic before and after every update the bot handles.
---

# Before and after an update

## OnPreUpdate — before everything

Sometimes something has to happen before an incoming message is handled at all. Checking whether the user is registered, for instance: if they are not, send them to registration and stop the update going any further.

`OnPreUpdate` runs ahead of the main pipeline and can do exactly that.

```csharp
// Create the bot.
var telegram = new PRBotBuilder("Token").SetBotId(0).Build();

// Subscribe.
telegram.Events.UpdateEvents.OnPreUpdate += Handler_OnUpdate;

// The handler.
async Task<UpdateResult> Handler_OnUpdate(BotEventArgs e)
{
    /*
     * For example, is the user registered?
     *
     *   registered     -> return UpdateResult.Continue;
     *                     handling proceeds as usual
     *
     *   not registered -> RegisterMethod();
     *                     return UpdateResult.Handled;
     *                     the update stops here and the user is sent to registration
     */
    return UpdateResult.Continue;
}
```

The return value is the whole point: `Continue` lets the update through, `Handled` ends it.

## OnPostUpdate — after everything

Sometimes something has to happen after any user action, whatever it was. Recording when the user was last active, for example.

`OnPostUpdate` runs once the main handling has finished, and cannot affect it.

```csharp
// Create the bot.
var telegram = new PRBotBuilder("Token").SetBotId(0).Build();

// Subscribe.
telegram.Events.UpdateEvents.OnPostUpdate += Handler_OnPostUpdate;

// The handler.
async Task Handler_OnPostUpdate(BotEventArgs e)
{
    // For example: record the user's last activity — the date and time.
}
```

## When to use middleware instead

These two events and [middleware](../middleware.md) cover overlapping ground. The difference:

* the events are **two independent hooks** — nothing carries over from one to the other;
* middleware is **one component wrapping the update**, so it can hold state across both halves, and several of them nest in a defined order.

Measuring how long an update took needs middleware, because the stopwatch has to survive from one end to the other. Deciding whether an update should proceed at all needs only `OnPreUpdate`.
