---
description: Handling presses on inline buttons, and the 64-byte limit behind them.
---

# Inline commands

An inline button carries `callback_data`, and **Telegram allows at most 64 bytes** of it. Everything about inline commands follows from that limit.

PRTelegramBot serialises the button's data into those bytes for you. JSON is the default; since 0.8.4 there is also a ToonNet serialiser that fits more into the same space, and you can supply your own by implementing `IPRSerializer`.

```csharp
var telegram = new PRBotBuilder("token")
                    .SetInlineSerializer(new ToonSerializerWrapper())
                    .Build();

var telegram = new PRBotBuilder("token")
                    .SetInlineSerializer(new JsonSerializerWrapper())
                    .Build();
```

When even that is not enough, [Creating an inline menu](inline-menu.md) explains how to sidestep the 64-byte limit entirely by keeping the payload outside `callback_data`.

See [Parameters](../parameters.md) for the arguments shared with the other handler attributes.

<figure><img src="../../.gitbook/assets/изображение (6).png" alt="A message carrying an inline keyboard beneath it"><figcaption>An inline keyboard: the buttons belong to the message, not to the chat</figcaption></figure>

## The three kinds of inline button

* **InlineCallback** — carries data back to a handler. This is the one you will use most.
* **InlineURL** — opens a link. No handler, nothing reaches the bot.
* **InlineWebApp** — opens a WebApp.

Since 1.0.0 there is also **InlineCopyText**, which copies text to the user's clipboard and likewise never reaches the bot.

## The attribute

```csharp
/// <param name="botId">Bot identifier.</param>
/// <param name="botIds">Bot identifiers.</param>
/// <param name="commands">Commands.</param>
public InlineCallbackHandlerAttribute(params T[] commands)
public InlineCallbackHandlerAttribute(long botId, params T[] commands)
public InlineCallbackHandlerAttribute(long[] botIds, params T[] commands)
```

Note the generic parameter: unlike the reply and slash attributes, this one matches on an **enum value** rather than on text. There is no `CommandComparison` or `StringComparison` here — there is nothing to compare loosely.

## Declaring the command enum

Before building a menu, declare an enum holding the set of commands. It **must** carry the `[InlineCommand]` attribute, and it is worth starting the numbering above 100 so your values cannot collide with the framework's own.

```csharp
[InlineCommand]
public enum CustomTHeader
{
    [Description("Example 1")]
    ExampleOne = 500,
    [Description("Example 2")]
    ExampleTwo,
    [Description("Example 3")]
    ExampleThree,
}
```

<figure><img src="../../.gitbook/assets/изображение (37).png" alt="An enum marked with the InlineCommand attribute and its numbered members"><figcaption>The <code>[InlineCommand]</code> attribute is what makes the enum usable as a command set</figcaption></figure>

{% hint style="warning" %}
The enum value travels inside `callback_data` as a number. Reordering the members, or inserting one in the middle, renumbers the rest — and every button already sitting in a user's chat history then points at the wrong handler. Append new values at the end.
{% endhint %}

## Pages

* [Creating an inline menu](inline-menu.md)
* [Handling inline commands](handling-inline-commands.md)
* [InlineCallback with confirmation](confirmation.md)
* [Instance inline handler](instance-handler.md)
