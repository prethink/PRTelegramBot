---
description: Sends messages to Telegram.
---

# MessageSender

Sends messages to Telegram.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<Message> AwaitAnswerBot(IBotContext context, long chatId, string message = "⏳ Generating a reply...", OptionMessage? option = null)` | The waiting message shown while the message is processed. |
| `static async Task<Message> Send(IBotContext context, Update update, string text, OptionMessage? option = null)` | Sends a message. |
| `static async Task<Message> Send(IBotContext context, string text, OptionMessage? option = null)` | Sends a message. |
| `static async Task<Message> Send(IBotContext context, long chatId, string text, OptionMessage? option = null)` | Sends a message. |

