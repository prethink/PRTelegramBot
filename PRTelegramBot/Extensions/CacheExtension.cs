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
        /// Словарь для работы который хранит идентификатор пользователя и его кеш
        /// </summary>
        static ConcurrentDictionary<long, ITelegramCache> _userHandlerData = new();

        #endregion

        #region Методы

        /// <summary>
        /// Создает кеш для пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        public static void CreateCacheData<T>(this Update update) where T : ITelegramCache
        {
            long userId = update.GetChatId();
            if (_userHandlerData.TryGetValue(userId, out var data))
            {
                data?.ClearData();
            }
            else
            {
                var newData = Activator.CreateInstance<T>();
                _userHandlerData.AddOrUpdate(userId, newData, (_, existingData) => newData);
            }
        }

        /// <summary>
        /// Получает кеш пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        /// <returns>Кеш пользователя</returns>
        public static T GetCacheData<T>(this Update update) where T : ITelegramCache
        {
            long userId = update.GetChatId();
            if (!_userHandlerData.TryGetValue(userId, out var data))
            {
                update.CreateCacheData<T>();
                return (T)_userHandlerData[userId];
            }
            return (T)data;
        }

        /// <summary>
        /// Очищает кеш пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        public static void ClearCacheData(this Update update)
        {
            long userId = update.GetChatId();
            if (_userHandlerData.TryGetValue(userId, out var data))
                data.ClearData();

        }

        /// <summary>
        /// Проверяет существуют ли кеш данные пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        /// <returns>true/false</returns>
        public static bool HasCacheData(this Update update)
        {
            long userId = update.GetChatId();
            return _userHandlerData.ContainsKey(userId);
        }

        #endregion
    }
}
