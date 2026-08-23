---
description: Turning reply commands into a keyboard the user can tap.
---

# Creating a reply menu

A reply menu is the keyboard that replaces the user's normal one. Tapping a button sends its caption as an ordinary message — which means the same `[ReplyMenuHandler]` that answers typed text also answers the button.

Three things build one:

* [`OptionMessage`](../../api/classes/optionmessage.md) — the settings a message is sent with
* `MenuGenerator.ReplyKeyboard` or [`ReplyKeyboardBuilder`](../../api/classes/replykeyboardbuilder.md) — the keyboard itself
* [`MessageSender.Send`](../../api/classes/messagesender.md) — sending it

## OptionMessage

Configures the message before it goes out. The properties that matter here:

| Property | Effect |
| --- | --- |
| `ClearMenu` | When true, removes the current keyboard. |
| `MenuReplyKeyboardMarkup` | When not null, attaches a reply menu. |
| `MenuInlineKeyboardMarkup` | When not null, attaches an inline menu. |

{% hint style="warning" %}
Setting `MenuReplyKeyboardMarkup` and `MenuInlineKeyboardMarkup` at the same time does not give you both — only one takes effect.
{% endhint %}

## MenuGenerator.ReplyKeyboard

| Parameter | Meaning |
| --- | --- |
| `maxColumn` | Maximum number of columns. |
| `keyboardButtons` / `buttons` / `menu` | The buttons. |
| `resizeKeyboard` | Telegram's `resize` flag — lets the client shrink the keyboard to fit. |
| `mainMenu` | When not empty, appends a plain button at the very bottom. Handy for a permanent "Main menu". |

## MessageSender.Send

A wrapper over Telegram.Bot. It takes the text and, optionally, an `OptionMessage`. If the text runs past 4000 characters it is split across several messages rather than rejected.

## Example: building the list by hand

```csharp
[ReplyMenuHandler("Menu")]
public static async Task ExampleReplyMenu(IBotContext context)
{
    string msg = "Menu";

    // Message settings.
    var option = new OptionMessage();
    // The list of buttons.
    var menuList = new List<KeyboardButton>();

    // A plain text button.
    menuList.Add(new KeyboardButton("Button 1"));
    // Ask the user for their contact.
    menuList.Add(KeyboardButton.WithRequestContact("Send your contact"));
    // Ask the user for their location.
    menuList.Add(KeyboardButton.WithRequestLocation("Send your location"));
    // Ask the user to pick a chat and send it to the bot.
    menuList.Add(KeyboardButton.WithRequestChat("Send a group to the bot", new KeyboardButtonRequestChat() { RequestId = 2 }));
    // Ask the user to pick a user and send them to the bot.
    menuList.Add(KeyboardButton.WithRequestUser("Send a user to the bot", new KeyboardButtonRequestUser() { RequestId = 1 }));
    // Ask the user to create a poll.
    menuList.Add(KeyboardButton.WithRequestPoll("Send your poll"));
    // Open a WebApp.
    menuList.Add(KeyboardButton.WithWebApp("WebApp", new WebAppInfo() { Url = "https://prethink.github.io/telegram/webapp.html" }));

    // One column, the buttons, resize enabled, and a bottom button.
    var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Main menu");

    option.MenuReplyKeyboardMarkup = menu;
    await MessageSender.Send(context, msg, option);
}
```

<figure><img src="../../.gitbook/assets/изображение-16.png" alt="A reply keyboard with one button per row and a Main menu button at the bottom"><figcaption>One column, with the <code>mainMenu</code> button last</figcaption></figure>

## Example: using the builder

The builder says the same thing with less ceremony, and gives you explicit control over rows.

```csharp
[ReplyMenuHandler("Reply menu")]
public static async Task ExampleReplyMenuBuilder(IBotContext context)
{
    string msg = "Menu";
    var option = new OptionMessage();

    var keyboard = new ReplyKeyboardBuilder()
        .SetResizeKeyboard(true)
        .AddButton("Button 1")
        .AddRequestContact("Send your contact", newRow: true)
        .AddRequestLocation("Send your location")
        .AddRow()
        .AddRequestChat("Send a group to the bot", new KeyboardButtonRequestChat(2, true))
        .AddRequestUsers("Send a user to the bot", new KeyboardButtonRequestUsers() { RequestId = 1 })
        .AddRequestPoll("Send your poll", new KeyboardButtonPollType())
        .AddEmptyButton(3, newRow: true)
        .AddRow()
        .AddButtonWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html")
        .SetMainMenuButton("Main menu")
        .Build();

    option.MenuReplyKeyboardMarkup = keyboard;
    await MessageSender.Send(context, msg, option);
}
```

<figure><img src="../../.gitbook/assets/изображение.png" alt="The same keyboard built with the builder, with several buttons sharing a row"><figcaption>The builder version — note how the rows differ from the one-column example above</figcaption></figure>

`AddRow` starts a new row explicitly; `newRow: true` on a button does the same thing inline. `AddEmptyButton` inserts blank placeholders, which is how you centre a button or keep a grid aligned.
