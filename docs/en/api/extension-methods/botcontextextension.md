---
description: Extension methods for the bot context.
---

# BotContextExtension

Extension methods for the bot context.

## Methods

| Method | Description |
| --- | --- |
| `static long GetChatId(this IBotContext context)` | Gets the chat identifier depending on the message type. |
| `static ChatId GetChatIdClass(this IBotContext context)` | Gets the identifier as a class. |
| `static bool TryGetChatId(this IBotContext context, out long chatId)` | Tries to get the chat identifier. |
| `static int GetMessageId(this IBotContext context)` | Gets the message identifier. |
| `static bool IsUserChatId(this IBotContext context)` | Whether the identifier belongs to a private user chat. |
| `static string GetInfoUser(this IBotContext context)` | Information about the user. |
| `static long GetUserId(this IBotContext context)` | Gets the user identifier from the Telegram update. |
| `static TCache CreateCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache` | Creates a cache for the user. |
| `static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache` | Gets the existing cache, or creates a new one. |
| `static TCache GetCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache` | Gets the user's cache. |
| `static void ClearCacheData(this IBotContext context)` | Clears the user's cache. |
| `static bool HasCacheData(this IBotContext context)` | Checks whether cached data exists for the user. |
| `static void RemoveCacheData(this IBotContext context)` | Removes the user's cache from the dictionary entirely. |
| `static void RegisterStepHandler(this IBotContext context, IExecuteStep command)` | Registers the next step. |
| `static TExecuteStep? GetStepHandler<TExecuteStep>(this IBotContext context) where TExecuteStep : IExecuteStep` | Gets the user's handler, or null. |
| `static IExecuteStep? GetStepHandler(this IBotContext context)` | Gets the current step handler. |
| `static void ClearStepUserHandler(this IBotContext context)` | Clears the user's steps. |
| `static bool HasStepHandler(this IBotContext context)` | Checks whether the user has a step registered. |
| `static InlineCallback GetCommandByCallbackOrNull(this IBotContext context)` | Gets the inline command from the callback data using the bot context. |
| `static InlineCallback<T> GetCommandByCallbackOrNull<T>(this IBotContext context)` | Gets the inline command from the callback data using the bot context. |
| `static List<string> GetSlashArgs(this IBotContext context)` | Gets the arguments of the slash command. |
| `static List<T> GetSlashArgs<T>(this IBotContext context, bool throwOnError = false)` | Gets the arguments of a slash command of a specific type. |

