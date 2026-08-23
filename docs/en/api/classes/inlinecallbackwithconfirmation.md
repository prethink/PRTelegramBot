---
description: Lets an inlineCallBack be executed with a confirmation.
---

# InlineCallbackWithConfirmation

Lets an inlineCallBack be executed with a confirmation.

Inherits `InlineCallback<EntityTCommand<string>>`, `IInlineContent`.

## Properties

| Property | Description |
| --- | --- |
| `InlineCallback YesCallback { get; set; }` | Handler invoked when "yes" is pressed. |
| `InlineCallback NoCallback { get; set; }` | Handler invoked when "no" is pressed. |

## Fields

| Field | Description |
| --- | --- |
| `string YesButton = "Yes"` | Name of the "yes" button. |
| `string NoButton = "No"` | Name of the "no" button. |
| `string BaseMessage = "Confirm the action"` | Text of the confirmation message. |

## Methods

| Method | Description |
| --- | --- |
| `override object GetContent()` |  |
| `override InlineKeyboardButton GetInlineButton()` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, InlineCallback noCallBack)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, InlineCallback noCallBack)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, InlineCallback noCallBack)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string noButton, string messageText)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string noButton, string messageText)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string noButton, string messageText)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string messageText, InlineCallback noCallBack)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string messageText, InlineCallback noCallBack)` | Constructor. |
| `InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string messageText, InlineCallback noCallBack)` | Constructor. |

