---
description: Building inline keyboards, and getting past the 64-byte callback_data limit.
---

# Creating an inline menu

Two things build an inline menu:

* **`InlineCallback`** — creates an inline button carrying a callback.
* **TCommand** — a helper class holding the data that travels in that callback.

`InlineCallback` takes:

| Parameter | Meaning |
| --- | --- |
| `buttonName` | The caption. |
| `commandType` | The command, as a value of your `[InlineCommand]` enum. |
| `data` | Optional. The payload to carry in the callback. |

**TCommand** is the base class for that payload. Derive from it to carry fields of your own.

{% hint style="warning" %}
**Telegram allows at most 64 bytes of `callback_data`.** Everything below is about living within that, or stepping around it.
{% endhint %}

## Ready-made TCommand types

* `CalendarTCommand` — carries a `DateTime`.
* `EntityTCommand<T>` — carries an id or another small value.

## Getting past 64 bytes

There are two levers, and they solve different problems.

**A more compact serialiser.** [`ToonSerializerWrapper`](../../api/classes/toonserializerwrapper.md) implements [`IPRSerializer`](../../api/interfaces/iprserializer.md) and encodes the same data in fewer bytes than JSON. This buys headroom; it does not remove the ceiling.

<figure><img src="../../.gitbook/assets/изображение (1).png" alt="The same payload encoded by the JSON and the ToonNet serialisers, side by side"><figcaption>The same data, and how many bytes each serialiser spends on it</figcaption></figure>

**A different converter.** [`FileInlineConverter`](../../api/classes/fileinlineconverter.md) implements [`IInlineMenuConverter`](../../api/interfaces/iinlinemenuconverter.md) and removes the limit altogether: the payload is written to a local JSON file named `{bot id}-{user id}-{command id}`, and only that short key travels in `callback_data`.

```csharp
var telegram = new PRBotBuilder("token")
                    .SetInlineMenuConverter(new FileInlineConverter())
                    .Build();
```

<figure><img src="../../.gitbook/assets/изображение (2).png" alt="The InlineCallbacks folder holding one JSON file per button"><figcaption>What <code>FileInlineConverter</code> writes: one file per button, named by bot, user and command</figcaption></figure>

The trade is that the data now lives on disk next to the bot: it does not survive a wiped directory, and it is not shared between instances if you run several.

Both interfaces support DI — register them as dependencies and the bot picks them up. See [Component resolution priorities](../../dependency-injection/resolution-priorities.md).

## Building a menu by hand

```csharp
public class Commands
{
    /// <summary>
    /// Write "Test" in the chat.
    /// </summary>
    [ReplyMenuHandler("Test")]
    public static async Task ExampleReply(IBotContext context)
    {
        // A button with a callback and no payload.
        var exampleItemOne = new InlineCallback("Example 1", CustomTHeader.ExampleOne);

        // A button carrying an id.
        var exampleItemTwo = new InlineCallback<EntityTCommand<long>>(
            "Example 2", CustomTHeader.ExampleTwo, new EntityTCommand<long>(2));

        var exampleItemThree = new InlineCallback<EntityTCommand<long>>(
            "Example 3", CustomTHeader.ExampleThree, new EntityTCommand<long>(3));

        // A button that opens a link.
        var url = new InlineURL("Google", "https://google.com");

        // A button that opens a WebApp.
        var webdata = new InlineWebApp("WA", "https://prethink.github.io/telegram/webapp.html");

        // Every inline button implements IInlineContent.
        List<IInlineContent> menu = new();

        menu.Add(exampleItemOne);
        menu.Add(exampleItemTwo);
        menu.Add(exampleItemThree);
        menu.Add(url);
        menu.Add(webdata);

        // One column.
        var testMenu = MenuGenerator.InlineKeyboard(1, menu);

        var option = new OptionMessage();
        option.MenuInlineKeyboardMarkup = testMenu;

        await MessageSender.Send(context, "Menu example", option);
    }
}
```

## Building a menu with the builder

[`InlineKeyboardBuilder`](../../api/classes/inlinekeyboardbuilder.md) gives explicit control over rows.

The example also shows captions coming from a configuration file, so button text can be changed without a rebuild — the same idea as [dynamic reply commands](../reply-commands/dynamic-reply-commands.md).

```csharp
[ReplyMenuHandler("InlineMenu")]
public static async Task InlineMenu(IBotContext context)
{
    /*
     * In Program.cs the bot is created with a config path:
     *
     *   var telegram = new PRBotBuilder(string.Empty)
     *       .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
     *       .Build();
     *
     * GetConfigValue<BotConfigJsonProvider, string>(key, "IN_EXAMPLE_ONE")
     * then reads the caption out of buttons.json.
     */
    var exampleItemOne = new InlineCallback(
        context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE"),
        CustomTHeaderTwo.ExampleOne);

    // A large number still fits.
    var exampleItemTwo = new InlineCallback<EntityTCommand<long>>(
        "Example with a large number",
        CustomTHeaderTwo.ExampleTwo,
        new EntityTCommand<long>(2_000_000_000_000_000_000));

    // A long text does not — this one only works with FileInlineConverter.
    var exampleItemThree = new InlineCallback<EntityTCommand<string>>(
        "Example with a long text",
        CustomTHeaderTwo.ExampleThree,
        new EntityTCommand<string>("...several paragraphs of text..."));

    var inlineStep = new InlineCallback("Inline Step", CustomTHeader.InlineWithStep);

    // Commands registered after the bot started.
    var exampleAddCommand = new InlineCallback("Dynamically added command 1", AddCustomTHeader.TestAddCommand);
    var exampleAddCommandTwo = new InlineCallback("Dynamically added command 2", AddCustomTHeader.TestAddCommandTwo);

    var url = new InlineURL("Google", "https://google.com");
    var webdata = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

    var keyboard = new InlineKeyboardBuilder()
        .AddButton(exampleItemOne)
        .AddButton(exampleItemTwo, newRow: true)
        .AddButton(exampleItemThree, newRow: true)
        .AddButton(exampleAddCommand, newRow: true)
        .AddRow()
        .AddButton(exampleAddCommandTwo)
        .AddButton(inlineStep)
        .AddRow()
        .AddButton(url)
        .AddButton(webdata)
        .Build();

    var option = new OptionMessage();
    option.MenuInlineKeyboardMarkup = keyboard;

    await MessageSender.Send(context, "Menu example", option);
}
```

<figure><img src="../../.gitbook/assets/изображение (3).png" alt="The resulting inline keyboard, with buttons laid out across several rows"><figcaption>The builder version, with the rows the chain above describes</figcaption></figure>

The "example with a long text" button is the point of the `FileInlineConverter` section above: with the default converter that payload does not fit in 64 bytes, and the button will not work.

## Buttons that do nothing

Bot API 10.3 added a third state for a button. `InlineDisabled` is drawn greyed out, and pressing it sends nothing at all — the handler is never reached.

```csharp
var keyboard = new InlineKeyboardBuilder()
    .AddButton(new InlineCallback("Step 1 — done", MyHeader.StepOne))
    .AddRowWithButton(new InlineDisabled("Step 2 — finish step 1 first"))
    .AddRowWithButton(new InlineDisabled("Step 3 — locked"))
    .Build();
```

The reason to reach for it is layout. An option that is temporarily unavailable used to leave two choices: drop the button, and the menu shifts under the user's finger between one message and the next, or keep it live and explain the refusal after the tap. A disabled button keeps its place and says why in its own label.

It carries no payload — the label is the whole button — so there is nothing to route and no header to declare.
