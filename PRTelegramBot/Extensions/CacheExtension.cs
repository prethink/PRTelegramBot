using PRTelegramBot.Interfaces;
using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Класс для работы с временными данными
    /// </summary>
    public static class CacheExtension
    {
        #region Поля и свойства

        /// <summary>
        /// Словарь для работы который хранит идентификатор пользователя и его кеш.
        /// </summary>
        static ConcurrentDictionary<string, ITelegramCache> userHandlerData = new();

        #endregion

        #region Методы

        /// <summary>
        /// Создает кеш для пользователя.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Кэш.</returns>
        public static TCache CreateCacheData<TCache>(this Update update) where TCache : ITelegramCache
        {
            string userKey = update.GetKeyMappingUserTelegram();
            var newData = Activator.CreateInstance<TCache>();
            userHandlerData.AddOrUpdate(userKey, newData, (_, existingData) => newData);
            return newData;
        }

        /// <summary>
        /// Получает существующий кэш или создает новый.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Кэш.</returns>
        /// <remarks>Если тип кэша отличается от существующего, будет создан кэш нового типа.</remarks>
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
        /// Получает кэш пользователя.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Кэш.</returns>
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
        /// Очищает кеш пользователя.
        /// </summary>
        /// <param name="update">Обновление данных telegram.</param>
        public static void ClearCacheData(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (userHandlerData.TryGetValue(userKey, out var data))
                data.ClearData();

        }

        /// <summary>
        /// Проверяет существуют ли кеш данные пользователя.
        /// </summary>
        /// <param name="update">Обновление данных telegram.</param>
        /// <returns>True - есть кэш, False - нет кэша.</returns>
        public static bool HasCacheData(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return userHandlerData.ContainsKey(userKey);
        }

        /// <summary>
        /// Полностью удаляет кэш пользователя из словаря.
        /// </summary>
        /// <param name="update">Обновление данных telegram.</param>
        public static void RemoveCacheData(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            userHandlerData.TryRemove(userKey, out _);
        }

        #endregion
    }
}
