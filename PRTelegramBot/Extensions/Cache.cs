using Telegram.Bot.Types;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Interface;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Класс для работы с временными данными
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// Словарь для работы который хранит идентификатор пользователя и его кеш
        /// </summary>
        static Dictionary<long, TelegramCache> _userHandlerData = new();


        /// <summary>
        /// Создает кеш для пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        public static void CreateCacheData<T>(this Update update) where T : TelegramCache
        {
            long userId = update.GetChatId();
            update.ClearCacheData();
            if (!HasCacheData(update))
            {
                _userHandlerData.Add(userId, Activator.CreateInstance<T>());
            }
        }

        /// <summary>
        /// Получает кеш пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        /// <returns>Кеш пользователя</returns>
        public static T GetCacheData<T>(this Update update) where T : TelegramCache
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
            if (update.HasCacheData())
            {
                _userHandlerData.FirstOrDefault(x => x.Key == userId).Value.ClearData();
            }

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
    }


}
