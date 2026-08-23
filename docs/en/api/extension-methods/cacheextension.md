---
description: Class for working with temporary data
---

# CacheExtension

Class for working with temporary data

## Methods

| Method | Description |
| --- | --- |
| `static TCache CreateCacheData<TCache>(this Update update) where TCache : ITelegramCache` | Creates a cache for the user. |
| `static TCache GetOrCreate<TCache>(this Update update) where TCache : ITelegramCache` | Gets the existing cache, or creates a new one. |
| `static TCache GetCacheData<TCache>(this Update update) where TCache : ITelegramCache` | Gets the user's cache. |
| `static void ClearCacheData(this Update update)` | Clears the user's cache. |
| `static bool HasCacheData(this Update update)` | Checks whether cached data exists for the user. |
| `static void RemoveCacheData(this Update update)` | Removes the user's cache from the dictionary entirely. |

