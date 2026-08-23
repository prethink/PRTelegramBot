---
description: Creates a button that copies the given text to the clipboard when it is pressed.
---

# InlineCopyText

Creates a button that copies the given text to the clipboard when it is pressed.

Inherits `InlineBase`, `IInlineContent`.

## Properties

| Property | Description |
| --- | --- |
| `string CopyText { get; set; }` | Text that is copied to the clipboard. |

## Methods

| Method | Description |
| --- | --- |
| `object GetContent()` |  |
| `override InlineKeyboardButton GetInlineButton()` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `InlineCopyText(string buttonName, string copyText)` | Constructor. |

