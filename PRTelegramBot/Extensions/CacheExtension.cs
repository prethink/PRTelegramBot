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
        static ConcurrentDictionary<string, ITelegramCache> _userHandlerData = new();

        #endregion

        #region Методы

        /// <summary>
        /// Создает кеш для пользователя.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="update">Обновление telegram.</param>
        public static void CreateCacheData<TCache>(this Update update) where TCache : ITelegramCache
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (_userHandlerData.TryGetValue(userKey, out var data))
            {
                data?.ClearData();
            }
            else
            {
                var newData = Activator.CreateInstance<TCache>();
                _userHandlerData.AddOrUpdate(userKey, newData, (_, existingData) => newData);
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
            if (!_userHandlerData.TryGetValue(userKey, out var data))
            {
                update.CreateCacheData<TCache>();
                return (TCache)_userHandlerData[userKey];
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
            if (_userHandlerData.TryGetValue(userKey, out var data))
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
            return _userHandlerData.ContainsKey(userKey);
        }

        #endregion
    }
}
