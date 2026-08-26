---
description: Events raised for each kind of update, and the two that wrap the whole pipeline.
---

# Update events

`bot.Events.UpdateEvents` covers updates rather than message contents: the kinds of thing Telegram sends that are not a message in a chat, plus two events wrapping every update the bot handles.

## Before and after every update

```csharp
/// <summary>
/// Raised before the update is handled. Handling can be stopped here.
/// </summary>
public event Func<BotEventArgs, Task<UpdateResult>>? OnPreUpdate;

/// <summary>
/// Raised after an update of type Message or CallbackQuery has been handled.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostUpdate;
```

`OnPreUpdate` is the only event that returns a value, and the value decides what happens next:

```csharp
bot.Events.UpdateEvents.OnPreUpdate += async e =>
{
    if (IsBanned(e.Context.GetUserId()))
        return UpdateResult.Handled;   // stop here, nothing else sees this update

    return UpdateResult.Continue;      // carry on
};
```

That makes it the natural place for a ban list, a rate limiter, or logging every incoming update.

{% hint style="info" %}
`OnPreUpdate` and [middleware](../middleware.md) overlap. The difference: middleware wraps the update, so it also runs *after* handling and can hold state across both halves; `OnPreUpdate` is a single notification with a verdict. Reach for middleware when you need both ends, and for `OnPreUpdate` when you only need to decide whether to proceed.
{% endhint %}

## Chats and members

| Event | Raised for |
| --- | --- |
| `OnMyChatMemberHandle` | the bot's own membership changed — this is how you learn it was added to or removed from a chat |
| `OnChatMemberHandle` | another member's status changed |
| `OnChatJoinRequestHandle` | someone asked to join |

## Messages elsewhere

| Event | Raised for |
| --- | --- |
| `OnEditedMessageHandle` | a message was edited |
| `OnChannelPostHandle` | a post in a channel |
| `OnEditedChannelPostHandle` | a channel post was edited |
| `OnCallbackQueryHandle` | an inline button was pressed |

## Inline mode

| Event | Raised for |
| --- | --- |
| `OnInlineQueryHandle` | the user typed the bot's name in another chat |
| `OnChosenInlineResultHandle` | they picked one of the results |

## Polls

| Event | Raised for |
| --- | --- |
| `OnPollHandle` | a poll's state changed |
| `OnPollAnswerHandle` | someone answered |

## Payments

| Event | Raised for |
| --- | --- |
| `OnShippingQueryHandle` | a shipping query |
| `OnPreCheckoutQueryHandle` | a pre-checkout query — answer this within 10 seconds or Telegram cancels the payment |
| `OnPurchasedPaidMediaHandle` | paid media was bought |
| `OnStoppedMessageGenerationHandle` | the user pressed stop on a message the bot was streaming — `Update.StoppedMessageGeneration.DraftId` says which draft |
| `OnSubscriptionHandle` | a subscription event |

## Business accounts

| Event | Raised for |
| --- | --- |
| `OnBusinessConnectionHandle` | a business connection changed |
| `OnBusinessMessageHandle` | a message in a business account |
| `OnEditedBusinessMessageHandle` | one was edited |
| `OnDeletedBusinessMessagesHandle` | messages were deleted |

## Reactions and boosts

| Event | Raised for |
| --- | --- |
| `OnMessageReactionHandle` | a reaction changed |
| `OnMessageReactionCountHandle` | the anonymous reaction count changed |
| `OnChatBoostHandle` | a chat was boosted |
| `OnRemovedChatBoostHandle` | a boost was removed |

## Everything else

| Event | Raised for |
| --- | --- |
| `OnManagedBotHandle` | a managed bot update |
| `OnGuestMessageHandle` | a guest message |
| `OnUnknownHandle` | an update type the framework does not know |
