using Telegram.Bot.Types;
using PRTelegramBot.Models;

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
        static Dictionary<long, UserCache> _userHandlerData = new();


        /// <summary>
        /// Создает кеш для пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        public static void CreateCacheData(this Update update)
        {
            long userId = update.GetChatId();
            update.ClearCacheData();
            _userHandlerData.Add(userId, new UserCache());
        }

        /// <summary>
        /// Получает кеш пользователя
        /// </summary>
        /// <param name="update">Обновление данных telegram</param>
        /// <returns>Кеш пользователя</returns>
        public static UserCache GetCacheData(this Update update)
        {
            long userId = update.GetChatId();
            var data = _userHandlerData.FirstOrDefault(x => x.Key == userId);
            if (data.Equals(default(KeyValuePair<long, UserCache>)))
            {
                update.CreateCacheData();
                return _userHandlerData.FirstOrDefault(x => x.Key == userId).Value;
            }
            else
            {
                return data.Value;
            }
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
