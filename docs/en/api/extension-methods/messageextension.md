---
description: Extension methods for working with a message
---

# MessageExtension

Extension methods for working with a message

## Methods

| Method | Description |
| --- | --- |
| `static void AutoDeleteMessage(this Message message, int seconds, IBotContext context)` | Automatically deletes the message after a given delay. |
| `static void AutoEditMessage(this Message message, string messageText, int seconds, IBotContext context)` | Automatically edits the message after a given delay. |
| `static void AutoEditMessageCycle(this Message message, List<string> messageTexts, int seconds, IBotContext context)` | Automatically edits the message after a given delay, in a loop. |

