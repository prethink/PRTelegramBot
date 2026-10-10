---
description: "Sends media: photos, photo groups, files and media by URL."
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

## Telegram.Bot media interfaces in 1.1.2

When calling Telegram.Bot directly, `IAlbumInputMedia` and `IInputRichMedia` expose `Type`, `Media`, `Caption`, `ParseMode` and `CaptionEntities`. JSON serialization supports live photos in albums and documents in rich messages.

`IInputMediaSettings` lets you set caption placement and spoilers for photos, videos, animations and live photos through a shared interface:

```csharp
using Telegram.Bot.Types;

IAlbumInputMedia media = new InputMediaPhoto(InputFile.FromFileId("existing-photo"));
IInputMediaSettings settings = (IInputMediaSettings)media;
settings.ShowCaptionAboveMedia = true;
settings.HasSpoiler = true;
```

{% hint style="warning" %}
If you implement `IAlbumInputMedia` or `IInputRichMedia` yourself, provide the new getters: `InputMediaType Type`, `InputFile Media`, `string? Caption`, `ParseMode ParseMode` and `MessageEntity[]? CaptionEntities`. `IInputRichMedia` already required `Type`. The enums are in `Telegram.Bot.Types.Enums`; the other types are in `Telegram.Bot.Types`. Built-in `InputMedia*` classes already implement these members.
{% endhint %}
