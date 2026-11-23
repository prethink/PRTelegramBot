---
description: Edits messages that have already been sent.
---

# MessageEditor

Edits messages that have already been sent.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<Telegram.Bot.Types.Message> EditInline(IBotContext context, long chatId, int messageId, OptionMessage? option = null)` | Edits the inline menu. |
| `static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, long chatId, int messageId, string text, OptionMessage? option = null)` | Edits a message. |
| `static async Task<Telegram.Bot.Types.Message?> Edit(IBotContext context, string text, OptionMessage? option = null)` | Edits the message identified by the update. Returns null after an edit by inline message ID. |
| `static async Task EditInline(IBotContext context, InlineKeyboardMarkup? keyboard, CancellationToken cancellationToken = default)` | Replaces the keyboard using the chat message or inline message ID. Pass null to remove it. |

