---
description: Edits media that has already been sent, and its caption.
---

# MediaEditor

Edits media that has already been sent, and its caption.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage? option = null)` | Edits a photo. |
| `static async Task<Message> EditWithPhoto(IBotContext context, long chatId, int messageId, string text, InputMedia media, OptionMessage? option = null)` | Edits the inline menu together with the photo. |
| `static async Task<Message> EditPhoto(IBotContext context, long chatId, int messageId, string photoPath, OptionMessage? option = null)` | Edits a photo. |
| `static async Task<Message> EditCaption(IBotContext context, long chatId, int messageId, string text, OptionMessage? option = null)` | Edits the caption under the photo. |

