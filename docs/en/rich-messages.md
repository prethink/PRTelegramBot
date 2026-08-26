---
description: Sending messages built from blocks — headings, lists, tables, quotations and media.
---

# Rich messages

A rich message is a different kind of message from a formatted one. A formatted message is a single run of text with entities laid over it — bold here, a link there. A rich message is built from **blocks**: headings, paragraphs, lists, tables, quotations, dividers, embedded photos and video, collapsible details. Telegram lays them out.

Bot API 10.1 introduced them; 10.3 added buttons inside them, attached documents, expandable quotations and compact tables.

## Sending one

The framework sends a rich message the same way it sends any other, so every [`OptionMessage`](api/classes/optionmessage.md) setting keeps working:

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

[ReplyMenuHandler("Report")]
public static async Task Report(IBotContext context)
{
    const string html = """
        <h1>Weekly report</h1>
        <p>Revenue is up <b>12%</b> on last week.</p>
        <ul>
            <li>New users: 1 204</li>
            <li>Churn: 0.8%</li>
        </ul>
        <blockquote>Growth held through the weekend.</blockquote>
        """;

    await MessageSender.SendRichMessage(context, html);
}
```

{% hint style="warning" %}
The HTML here is the **rich message dialect**, not the one `ParseMode.Html` accepts. `<h1>`, `<ul>` and `<table>` mean something in a rich message and nothing in a formatted one. The Bot API's rich message formatting options page has the tag list.

The framework passes the HTML through as HTML — Telegram does the parsing. Nothing is validated on the way out.
{% endhint %}

## Options

Everything a rich message can carry is mapped exactly as it is for an ordinary message: menus, reply parameters, thread id, protected content, silent delivery, business connection, message effect, paid broadcast, direct messages topic, suggested post parameters and the [ephemeral parameters](ephemeral-messages.md).

{% hint style="warning" %}
Ephemeral parameters on a rich message carry the same conditions as anywhere else: without a callback query to answer, Telegram needs the bot to be an administrator of the chat. See [Ephemeral messages](ephemeral-messages.md).
{% endhint %}

```csharp
var option = new OptionMessage
{
    MenuInlineKeyboardMarkup = keyboard,
    ProtectedContent = true,
    EphemeralMessageParameters = context.Update.GetUserId()
};

await MessageSender.SendRichMessage(context, html, option);
```

What has no counterpart here is `ParseMode`, `Entities` and `DisableWebPagePreview` — the blocks carry their own structure, so there is nothing for those to apply to.

## Building one by hand

When the content is assembled rather than authored, an overload takes an `InputRichMessage` directly:

```csharp
var rich = new InputRichMessage
{
    Blocks = new InputRichBlock[]
    {
        new InputRichBlockSectionHeading { Text = "Weekly report" },
        new InputRichBlockParagraph { Text = "Revenue is up 12% on last week." },
        new InputRichBlockDivider(),
    }
};

await MessageSender.SendRichMessage(context, rich);
```

There are 26 block types and around 30 text types in `Telegram.Bot.Types`, and the framework does not wrap them — they are used directly, exactly like every other Telegram.Bot type.

## Receiving one

An incoming rich message raises `OnRichMessageHandle`, and `MessageType.RichMessage` identifies it. See [Message events](events/message-events.md).

Round-tripping works: `msg.RichMessage.ToHtml()` gives you HTML with the media references intact, and passing that HTML back to `SendRichMessage` resolves them again, so a message can be read, edited and sent on without losing its pictures.
