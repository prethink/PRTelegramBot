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
| `static async Task<Message> SendEphemeral(IBotContext context, string text, OptionMessage? option = null, bool replaceCallbackQueryMessage = false)` | Sends an ephemeral message to the user the current update came from. |
| `static async Task<Message> SendEphemeral(IBotContext context, long receiverUserId, string text, OptionMessage? option = null, bool replaceCallbackQueryMessage = false)` | Sends an ephemeral message to a specific user. |
| `static async Task<Message> SendRichMessage(IBotContext context, string html, OptionMessage? option = null)` | Sends a rich message described with HTML. |
| `static async Task<Message> SendRichMessage(IBotContext context, long chatId, string html, OptionMessage? option = null)` | Sends a rich message described with HTML to a specific chat. |
| `static async Task<Message> SendRichMessage(IBotContext context, InputRichMessage richMessage, OptionMessage? option = null)` | Sends a rich message that was built by hand. |
| `static async Task<Message> SendRichMessage(IBotContext context, long chatId, InputRichMessage richMessage, OptionMessage? option = null)` | Sends a rich message that was built by hand to a specific chat. |

