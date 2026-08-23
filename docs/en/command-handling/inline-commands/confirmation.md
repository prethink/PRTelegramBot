---
description: Asking the user to confirm before an inline button actually does anything.
---

# InlineCallback with confirmation

Some actions should not happen on a single tap. `InlineCallbackWithConfirmation` wraps an ordinary `InlineCallback` so the user is asked first, and the wrapped action runs only after they agree.

You build the button you actually want, then wrap it.

## With the default "no"

```csharp
/// <summary>
/// Runs for the bot with botId 0.
/// Runs when the user writes InlineConfirm.
/// </summary>
[ReplyMenuHandler("InlineConfirm")]
public static async Task InlineConfirm(IBotContext context)
{
    // The button that needs a confirmation.
    var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>(
        "Button with confirmation",
        CustomTHeaderTwo.ExampleTwo,
        new EntityTCommand<long>(3, ActionWithLastMessage.Delete));

    // The wrapper.
    var exampleWithConfirmation = new InlineCallbackWithConfirmation(
        exampleInlineCallback,
        ActionWithLastMessage.Delete);

    // A new menu.
    List<IInlineContent> menu = new() { exampleWithConfirmation };
    var testMenu = MenuGenerator.InlineKeyboard(1, menu);

    var option = new OptionMessage();
    option.MenuInlineKeyboardMarkup = testMenu;

    await MessageSender.Send(context, "InlineCallback with confirmation", option);
}
```

Pressing the button replaces it with a Yes/No pair. Yes runs the wrapped callback; No, with no handler of its own, simply deletes the message.

<figure><img src="../../.gitbook/assets/изображение (18).png" alt="The message with the single button that needs confirming"><figcaption>The button as it first appears</figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (19).png" alt="After pressing it, the confirmation text with Yes and No buttons"><figcaption>After the press: the confirmation, with Yes and No</figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (20).png" alt="After pressing Yes, the wrapped handler has run and answered"><figcaption>After Yes: the wrapped handler has run</figcaption></figure>

{% hint style="warning" %}
The header on the wrapped button must be one whose handler reads the same `EntityTCommand<T>` the button carries. A button holding `EntityTCommand<long>` routed to a handler that reads `EntityTCommand<string>` throws a `JsonException` when the user presses Yes — the converter logs it, returns `null`, and the handler silently does nothing. Nothing appears to happen and nothing is reported to the user.
{% endhint %}

## With a "back" button, or your own handling of "no"

Pass an `InlineCallback` as the third argument and it replaces the default No.

```csharp
/// <summary>
/// Runs for the bot with botId 0.
/// Runs when the user writes InlineConfirmWithBack.
/// </summary>
[ReplyMenuHandler("InlineConfirmWithBack")]
[InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleBack)]
public static async Task InlineConfirmWithBack(IBotContext context)
{
    // The button that needs a confirmation.
    var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>(
        "Button with confirmation",
        CustomTHeaderTwo.ExampleTwo,
        new EntityTCommand<long>(3, ActionWithLastMessage.Delete));

    // The "back" button, or any handling of your own.
    var exampleBack = new InlineCallback("Back", CustomTHeaderTwo.ExampleBack);

    // The wrapper.
    var exampleWithConfirmation = new InlineCallbackWithConfirmation(
        exampleInlineCallback,
        ActionWithLastMessage.Edit,
        exampleBack);

    List<IInlineContent> menu = new() { exampleWithConfirmation };
    var testMenu = MenuGenerator.InlineKeyboard(1, menu);

    var option = new OptionMessage();
    option.MenuInlineKeyboardMarkup = testMenu;

    string msg = "InlineCallback with confirmation and a back or custom button handler";

    if (context.Update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        await MessageEditor.Edit(context, msg, option);
    else
        await MessageSender.Send(context, msg, option);
}
```

The method carries two attributes on purpose: `ReplyMenuHandler` opens the menu when the user types the command, and `InlineCallbackHandler` re-enters the same method when Back is pressed. That is what makes Back return to this screen instead of leaving a dead end.

The check on `context.Update.Type` is what lets one method serve both entry points: a typed command sends a new message, a button press edits the existing one.

<figure><img src="../../.gitbook/assets/изображение (14).png" alt="The message with the button that needs confirming"><figcaption>The starting screen</figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (16).png" alt="The confirmation, this time with Yes and a Back button instead of No"><figcaption>Here the second button is Back rather than No</figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (17).png" alt="After pressing Back, the original message has been restored in place"><figcaption>After Back: the same message, edited back to where it started</figcaption></figure>

## How long a confirmation waits

A pending confirmation is remembered in memory from the moment the button is built. Since 1.0.0 an unanswered one is discarded an hour later, and the entry is dropped as soon as No is pressed.

Two consequences worth knowing:

* the state does not survive a bot restart — a button built before a restart reports that something went wrong;
* the state is per process, so it is not shared between instances if you run several.

Constructors also let you set the wording — the Yes and No captions and the confirmation text — see [`InlineCallbackWithConfirmation`](../../api/classes/inlinecallbackwithconfirmation.md).
