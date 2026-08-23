---
description: Extension methods for PRBotBase.
---

# PRBotBaseExtension

Extension methods for PRBotBase.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<bool> IsAdmin(this PRBotBase botClient, long userId)` | Checks whether the user is an administrator of the bot. |
| `static async Task<bool> InWhiteList(this PRBotBase botClient, long userId)` | Checks whether the user is present in the bot's white list. |
| `static async Task<List<long>> GetAdminsIds(this PRBotBase botClient)` | Returns the list of the bot's administrators. |
| `static async Task<List<long>> GetWhiteListIds(this PRBotBase botClient)` | Returns the white list of users. |
| `static IBotContext CreateContext(this PRBotBase botClient)` | Creates the bot context. |

