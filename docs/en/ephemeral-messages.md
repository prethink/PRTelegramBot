| | Bot must be admin? | Window | Works in a private chat? |
| --- | --- | --- | --- |
| Answering a callback query, quoting its id | no | 15 seconds | yes |
| Replying to an incoming ephemeral message | no | 15 seconds | yes |
| Writing to any non-bot member of the chat | **yes** | any time | no — there are no admins in a private chat |
---
description: Answering one person in a group, without the rest of the chat reading along.
---

# Ephemeral messages

An ephemeral message is drawn as an overlay for a single member of a chat. Nobody else in the chat sees it, and it never enters the chat history — there is no message to delete afterwards, because there was never a message in the chat.

Bot API 10.3 introduced them. Before that, a bot with something private to say had three awkward options: post it in the group and let everyone read it, write to the user privately and hope they had started the bot first, or cram the answer into a callback alert that fits a couple of lines.

{% hint style="info" %}
They were designed for shared chats, but the everyday case — answering a button press — works in a private chat with the bot too. What a private chat cannot offer is administrator rights, so the routes that need them are closed there. The table below says which is which.
{% endhint %}

## When Telegram accepts one

This is the part that decides whether your code works, so it comes before the examples. Telegram allows an ephemeral message in exactly three situations:

| | Bot must be admin? | Window | Works in a private chat? |
| --- | --- | --- | --- |
| Answering a callback query, quoting its id | no | 15 seconds | yes |
| Replying to an incoming ephemeral message | no | 15 seconds | yes |
| Writing to any non-bot member of the chat | **yes** | any time | no — a private chat has no admins |

The first two are how a bot *continues* an exchange the user started. The third is the only way to *begin* one — and it needs the bot to be an administrator of the chat.

Get this wrong and Telegram answers:

```
Bad Request: BOT_NOT_ADMIN
```

That error means the bot tried the third route without the rights for it — most often by sending an ephemeral message from a plain command handler rather than from a button press.

## Answering a button press

This is the common case, and it needs no special rights.

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

[InlineCallbackHandler<MyHeader>(MyHeader.ShowBalance)]
public static async Task ShowBalance(IBotContext context)
{
    await MessageSender.SendEphemeral(context, "Your balance is 42 ⭐.");
}
```

`SendEphemeral` takes what it needs from the current update: the user who will see the message, and the callback query it is answering, so Telegram knows which tap it belongs to and that it falls inside the 15-second window.

## Continuing inside the overlay

A reply to an ephemeral message must itself be ephemeral, and it also has 15 seconds. The framework fills the reply target in from the update, so a follow-up needs no extra code:

```csharp
[ReplyMenuHandler("Yes")]
public static async Task Confirm(IBotContext context)
{
    // If this arrived from inside an ephemeral overlay, the reply stays in it.
    await MessageSender.SendEphemeral(context, "Done.");
}
```

An incoming ephemeral message carries `Message.EphemeralMessageId`, which is what the framework copies into [`OptionMessage.ReplyToEphemeralMessageId`](api/classes/optionmessage.md). Setting that property yourself takes precedence.

## Starting an exchange — admin only

Sending from a plain command handler, or writing to somebody who did not trigger the update, means there is no callback query to quote. That is the third route, and it needs the bot to be an administrator of the group:

```csharp
[ReplyMenuHandler("Notify")]
public static async Task Notify(IBotContext context)
{
    // Requires the bot to be an administrator of this chat.
    await MessageSender.SendEphemeral(context, moderatorId, "A quiet word, just for you.");
}
```

## Replacing the message instead of covering it

By default the overlay appears over the original message, which stays where it is. Passing `replaceCallbackQueryMessage` puts the ephemeral message in its place instead — for this user only. Everyone else in the chat still sees the original untouched.

```csharp
await MessageSender.SendEphemeral(context, "The menu is gone — for you.", replaceCallbackQueryMessage: true);
```

{% hint style="danger" %}
**Replacing needs a group.** In a private chat Telegram refuses it with `MESSAGE_ID_INVALID` — there is no shared timeline to replace anything on, and a one-to-one message is edited with the edit methods instead. The plain ephemeral reply above works in both, so this is a limit of the flag rather than of ephemeral messages.

The Bot API does not spell this out. It is what the API answers, checked both ways: refused in a private chat, accepted in a group.
{% endhint %}

{% hint style="warning" %}
The flag only applies to a button press. When the update is not a callback query the framework leaves it off, because Telegram rejects the combination.

It must also stay off for a button press that came from an ephemeral message — those are edited with the ephemeral edit methods rather than replaced.
{% endhint %}

## Through OptionMessage

`SendEphemeral` is a convenience over one property. Anything that accepts an [`OptionMessage`](api/classes/optionmessage.md) can be made ephemeral by setting it directly, which is how photos, files and [rich messages](rich-messages.md) are sent this way:

```csharp
var option = new OptionMessage
{
    EphemeralMessageParameters = new EphemeralMessageParameters { ReceiverUserId = userId }
};

await MediaSender.SendPhotoWithUrl(context, chatId, "Your receipt", receiptUrl, option);
```

A plain user id converts to the parameters implicitly, so `EphemeralMessageParameters = userId` is enough when there is nothing else to set. Note that this route sets no callback query id, so the same admin requirement applies unless you fill one in.

The property is forwarded by [`MessageSender`](api/classes/messagesender.md) and by [`MediaSender`](api/classes/mediasender.md) for photos, files and media by URL. Media groups do not accept it — that is a Bot API limit, not a gap in the framework.

{% hint style="info" %}
Delivery is not guaranteed. Telegram will not queue an ephemeral message for a user who is offline, so do not use one to deliver anything the user must not miss. For that, send an ordinary message.
{% endhint %}
