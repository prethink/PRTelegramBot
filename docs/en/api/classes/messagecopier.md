---
description: Copies messages between chats.
---

# MessageCopier

Copies messages between chats.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<List<MessageId>> CopyMessages(IBotContext context, List<Message> messages, long chatId, OptionMessage? option = null)` | Copies a collection of messages. |
| `static async Task<MessageId> CopyMessage(IBotContext context, Message message, long chatId, OptionMessage? option = null)` | Copies the message. |

