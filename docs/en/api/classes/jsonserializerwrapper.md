---
description: Json data serializer.
---

# JsonSerializerWrapper

Json data serializer.

Inherits `IPRSerializer`.

## Methods

| Method | Description |
| --- | --- |
| `T Deserialize<T>(string data)` |  |
| `string Serialize<T>(T data)` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `JsonSerializerWrapper(JsonSerializerOptions? options = null)` | Constructor. |
| `JsonSerializerWrapper()` | Constructor. |

## Telegram keyboards

In 1.1.2, Telegram.Bot's JSON converter restores the concrete keyboard type when deserializing `ReplyMarkup`. It supports `InlineKeyboardMarkup`, `ReplyKeyboardMarkup`, `ReplyKeyboardRemove` and `ForceReplyMarkup`, including when these are serialized through `JsonSerializerWrapper`:

```csharp
using PRTelegramBot.Wrappers;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

var serializer = new JsonSerializerWrapper(JsonBotAPI.Options);
ReplyMarkup markup = new InlineKeyboardMarkup(
    InlineKeyboardButton.WithCallbackData("Open", "open"));
string json = serializer.Serialize<ReplyMarkup>(markup);
ReplyMarkup restored = serializer.Deserialize<ReplyMarkup>(json);
```

Use `JsonBotAPI.Options` for Telegram types so JSON uses Bot API property names such as `keyboard`. Here `restored` is an `InlineKeyboardMarkup` with the original callback data.
