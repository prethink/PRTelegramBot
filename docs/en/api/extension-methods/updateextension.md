---
description: Extension methods for Telegram updates.
---

# UpdateExtension

Extension methods for Telegram updates.

## Methods

| Method | Description |
| --- | --- |
| `static long GetChatId(this Update update)` | Gets the chat identifier depending on the message type. |
| `static ChatId GetChatIdClass(this Update update)` | Gets the identifier as a class. |
| `static bool TryGetChatId(this Update update, out long chatId)` | Tries to get the chat identifier. |
| `static int GetMessageId(this Update update)` | Gets the message identifier. |
| `static bool IsUserChatId(this Update update)` | Whether the identifier belongs to a private user chat. |
| `static string GetInfoUser(this Update update)` | Information about the user. |
| `static bool TryGetBot(this Update update, out PRBotBase? bot)` | Tries to get the bot from the update. |
| `static long GetUserId(this Update update)` | Gets the user identifier from the Telegram update. |

