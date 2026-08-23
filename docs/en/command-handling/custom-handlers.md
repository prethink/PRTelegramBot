---
description: Replacing or extending how the framework routes message and callbackQuery updates.
---

# Custom command handlers

The command kinds described elsewhere in this section are not special cases baked into the framework — they are handlers, and you can add your own alongside them.

Already implemented for `message`:

* **Reply** — text commands
* **ReplyDynamic** — text commands whose captions come from a file
* **Slash** — text commands beginning with `/`

And for `callbackQuery`:

* **InlineCallback** — inline button presses

The earlier pages describe how to use those. This page is what is underneath them.

Since version 0.7 you can register handlers of your own, and **they run before the built-in ones**.

## The contract

A handler returns an [`UpdateResult`](../api/enums/updateresult.md):

* `UpdateResult.Handled` — this update is dealt with; stop.
* `UpdateResult.Continue` — not mine; try the next handler.

## Message handlers

Implement [`IMessageCommandHandler`](../api/interfaces/imessagecommandhandler.md):

```csharp
public class MessageTestHandler : IMessageCommandHandler
{
    public async Task<UpdateResult> Handle(IBotContext context, Message updateType)
    {
        /* If this is what you were looking for and you have dealt with it, return Handled.
         * The remaining handlers are then skipped. */
        if (updateType.Text == "The data I want")
            return UpdateResult.Handled;

        // Not handled — let the next handler try.
        return UpdateResult.Continue;
    }
}
```

Register it on the builder:

```csharp
var bot = new PRBotBuilder("Token")
    .AddMessageCommandHandlers(new MessageTestHandler())
    .Build();
```

From then on a `message` update reaches your handler first. If it returns `Continue`, the built-in handlers run in their usual order: **slash, reply, replydynamic**.

## CallbackQuery handlers

Implement [`ICallbackQueryCommandHandler`](../api/interfaces/icallbackquerycommandhandler.md):

```csharp
public class CallbackQueryTestHandler : ICallbackQueryCommandHandler
{
    public async Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
    {
        if (updateType.Data == "The data I want")
            return UpdateResult.Handled;

        return UpdateResult.Continue;
    }
}
```

```csharp
var bot = new PRBotBuilder("Token")
    .AddCallbackQueryCommandHandlers(new CallbackQueryTestHandler())
    .Build();
```

## When to reach for this

Rarely — but there are cases the attributes cannot express:

* **A different matching rule.** Commands matched by regular expression, by prefix, or against a list held in a database.
* **Callback data not produced by this framework.** Buttons built by another system, or by an older version of your bot, whose `callback_data` the inline converter cannot parse.
* **Catching something before anything else sees it.** A maintenance mode that answers every message with a notice, ahead of all routing.

{% hint style="warning" %}
A handler that returns `Handled` too eagerly silently disables every command after it. If commands stop working after adding one, that is the first thing to check: return `Continue` for everything you did not actually deal with.
{% endhint %}

For work that should happen around handling rather than instead of it, see [middleware](../middleware.md) or the `OnPreUpdate` [event](../events/update-events.md).
