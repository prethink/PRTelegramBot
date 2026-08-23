---
description: Extension methods for ITelegramBotClient.
---

# ITelegramBotClientExtension

Extension methods for ITelegramBotClient.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<bool> IsAdmin(this IBotContext context)` | Checks whether the user is an administrator of the bot. |
| `static async Task<bool> IsAdmin(this IBotContext context, long userId)` | Checks whether the user is an administrator of the bot. |
| `static async Task<bool> InWhiteList(this IBotContext context)` | Checks whether the user is present in the bot's white list. |
| `static async Task<bool> InWhiteList(this IBotContext context, long userId)` | Checks whether the user is present in the bot's white list. |
| `static async Task<List<long>> GetAdminsIds(this IBotContext context)` | Returns the list of the bot's administrators. |
| `static async Task<List<long>> GetWhiteListIds(this IBotContext context)` | Returns the white list of users. |
| `static void InvokeCommonLog(this IBotContext context, string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)` | Raises the plain log event. |
| `static void InvokeErrorLog(this IBotContext context, Exception ex)` | Raises the error logging event. |
| `async static Task<string> GetGeneratedRefLink(this IBotContext context, string refLink)` | Generates a referral link. |
| `static TReturn GetConfigValue<TBotProvider, TReturn>(this IBotContext context, string configKey, string key)` | Gets a value from the config file by key |
| `static bool TryGetConfigValue<TBotProvider, TReturn>(this IBotContext context, string configKey, string key, out TReturn result)` | Tries to get a value from the config file by key |

