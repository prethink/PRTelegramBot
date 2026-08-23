---
description: Keeping short-lived per-user data between messages.
---

# User cache

PRTelegramBot provides a basic per-user cache. It is the simplest way to remember something about a user between two messages without reaching for a database.

The cache is keyed by the user, and what it stores is a type you define. Implement [`ITelegramCache`](https://prethink.gitbook.io/prtelegrambot/ru/api/interfeisy/itelegramcache) and the framework takes care of the rest.

## Define your cache type

```csharp
public class UserCache : ITelegramCache
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Temporary data.
    /// </summary>
    public string Data { get; set; }

    public bool ClearData()
    {
        Id = 0;
        Data = "";
        return true;
    }
}
```

`ClearData` is what the framework calls when the cache is cleared, so reset every field there.

## Extension methods

```csharp
/// <summary>
/// Creates the cache for the user.
/// </summary>
public static TCache CreateCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Returns the existing cache, or creates one.
/// </summary>
/// <remarks>If the requested type differs from the stored one, a cache of the new type is created.</remarks>
public static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Returns the user's cache.
/// </summary>
public static TCache GetCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache

/// <summary>
/// Clears the user's cache by calling ClearData on it.
/// </summary>
public static void ClearCacheData(this IBotContext context)

/// <summary>
/// Reports whether the user has any cached data.
/// </summary>
public static bool HasCacheData(this IBotContext context)

/// <summary>
/// Removes the user's cache entirely.
/// </summary>
public static void RemoveCacheData(this IBotContext context)
```

Note the difference between the last two: `ClearCacheData` keeps the entry and resets its fields, `RemoveCacheData` drops the entry altogether.

The same methods also exist as extensions on `Update`, for code that has the update but not the context.

## Example

```csharp
/// <summary>
/// Send "cache" to the bot.
/// Writes data into the cache.
/// </summary>
[ReplyMenuHandler("cache")]
public static async Task GetCache(IBotContext context)
{
    string msg = $"Writing into the user's cache: {context.GetChatId()}";

    context.GetCacheData<UserCache>().Id = context.GetChatId();

    await MessageSender.Send(context, msg);
}

/// <summary>
/// Send "resultcache" to the bot.
/// Reads the data back.
/// </summary>
[ReplyMenuHandler("resultcache")]
public static async Task CheckCache(IBotContext context)
{
    var cache = context.GetCacheData<UserCache>();

    string msg = cache.Id != 0
        ? $"Data in the user's cache: {cache.Id}"
        : "There is no data in the user's cache.";

    await MessageSender.Send(context, msg);
}

/// <summary>
/// Send "clearcache" to the bot.
/// Clears the user's cached data.
/// </summary>
[ReplyMenuHandler("clearcache")]
public static async Task ClearCache(IBotContext context)
{
    context.GetCacheData<UserCache>().ClearData();

    await MessageSender.Send(context, "Cache cleared");
}
```

{% hint style="warning" %}
The cache lives in memory, in the bot process. It does not survive a restart, and it is not shared between instances if you run several. Use it for a wizard's intermediate state or a short-lived flag — not as storage.
{% endhint %}
