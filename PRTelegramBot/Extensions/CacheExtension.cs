using PRTelegramBot.Interfaces;
using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Class for working with temporary data
    /// </summary>
    public static class CacheExtension
    {
        #region Fields and properties

        /// <summary>
        /// The working dictionary that maps a user identifier to that user's cache.
        /// </summary>
        static ConcurrentDictionary<string, ITelegramCache> userHandlerData = new();

        #endregion

        #region Methods

        /// <summary>
        /// Creates a cache for the user.
        /// </summary>
        /// <typeparam name="TCache">Cache type.</typeparam>
        /// <param name="update">Telegram update.</param>
        /// <returns>Cache.</returns>
        public static TCache CreateCacheData<TCache>(this Update update) where TCache : ITelegramCache
        {
            string userKey = update.GetKeyMappingUserTelegram();
            var newData = Activator.CreateInstance<TCache>();
            userHandlerData.AddOrUpdate(userKey, newData, (_, existingData) => newData);
            return newData;
        }

        /// <summary>
        /// Gets the existing cache, or creates a new one.
        /// </summary>
        /// <typeparam name="TCache">Cache type.</typeparam>
        /// <param name="update">Telegram update.</param>
        /// <returns>Cache.</returns>
        /// <remarks>If the cache type differs from the existing one, a cache of the new type is created.</remarks>
        public static TCache GetOrCreate<TCache>(this Update update) where TCache : ITelegramCache
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (userHandlerData.TryGetValue(userKey, out var data))
            {
                if (data is TCache cache)
                    return cache;

                var newData = Activator.CreateInstance<TCache>();
                userHandlerData.AddOrUpdate(userKey, newData, (_, existingData) => newData);
                return newData;
            }
            else
            {
                var newData = Activator.CreateInstance<TCache>();
                userHandlerData.AddOrUpdate(userKey, newData, (_, existingData) => newData);
                return newData;
            }
        }

        /// <summary>
        /// Gets the user's cache.
        /// </summary>
        /// <typeparam name="TCache">Cache type.</typeparam>
        /// <param name="update">Telegram update.</param>
        /// <returns>Cache.</returns>
        public static TCache GetCacheData<TCache>(this Update update) where TCache : ITelegramCache
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (!userHandlerData.TryGetValue(userKey, out var data))
            {
                update.GetOrCreate<TCache>();
                return (TCache)userHandlerData[userKey];
            }
            return (TCache)data;
        }

        /// <summary>
        /// Clears the user's cache.
        /// </summary>
        /// <param name="update">Telegram data update.</param>
        public static void ClearCacheData(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (userHandlerData.TryGetValue(userKey, out var data))
                data.ClearData();

        }

        /// <summary>
        /// Checks whether cached data exists for the user.
        /// </summary>
        /// <param name="update">Telegram data update.</param>
        /// <returns>True if a cache exists; False if it does not.</returns>
        public static bool HasCacheData(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return userHandlerData.ContainsKey(userKey);
        }

        /// <summary>
        /// Removes the user's cache from the dictionary entirely.
        /// </summary>
        /// <param name="update">Telegram data update.</param>
        public static void RemoveCacheData(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            userHandlerData.TryRemove(userKey, out _);
        }

        #endregion
    }
}
