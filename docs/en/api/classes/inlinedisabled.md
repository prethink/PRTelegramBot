---
description: Creates a button that is shown but does nothing when it is pressed.
---

# InlineDisabled

Creates a button that is shown but does nothing when it is pressed.

Added in version 1.1.0. Requires Bot API 10.3.

Inherits `InlineBase`, `IInlineContent`.

Telegram draws the button greyed out and ignores taps on it, so no callback ever arrives. Useful for a menu that has to keep its shape while an action is unavailable: a step the user has not reached yet, an option their plan does not include, or a button that is busy while a long operation runs.

Lives in `PRTelegramBot.Models.InlineButtons` and wraps `InlineKeyboardButton.WithDisabled`.

## Methods

| Method | Description |
| --- | --- |
| `object GetContent()` | Returns the button label. A disabled button carries no payload, so its label is all there is. |
| `override InlineKeyboardButton GetInlineButton()` | Builds the disabled button. |

## Constructors

| Constructor | Description |
| --- | --- |
| `InlineDisabled(string buttonName)` | Constructor. |

## Example

```csharp
var keyboard = new InlineKeyboardBuilder()
    .AddButton(new InlineCallback("Step 1 — done", MyHeader.StepOne))
    .AddRowWithButton(new InlineDisabled("Step 2 — finish step 1 first"))
    .Build();
```

See [Creating an inline menu](../../command-handling/inline-commands/inline-menu.md) for where this fits.
