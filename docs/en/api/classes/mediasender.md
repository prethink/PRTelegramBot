---
description: Sends media: photos, photo groups, files and media by URL.
---

# MediaSender

Sends media: photos, photo groups, files and media by URL.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, Stream stream, OptionMessage? option = null)` | Sends a message with a photo. |
| `static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, string filePath, OptionMessage? option = null)` | Sends a message with a photo. |
| `static async Task<Message> SendPhotoWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage? option = null)` | Sends a message with a photo. |
| `static async Task<Message> SendMediaWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage? option = null)` | Sends a message with a photo. |
| `static async Task<Message[]> SendPhotoGroup(IBotContext context, long chatId, string text, List<string> filepaths, OptionMessage? option = null)` | Sends a group of photos. |
| `static async Task<Message> SendFile(IBotContext context, long chatId, string text, string filePath, OptionMessage? option = null)` | Sends a file. |

