using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    /// <summary>
    /// Пример работы с кэшем пользователей
    /// </summary>
    public class ExampleUserCache
    {
        /// <summary>
        /// Напишите в боте "cache"
        /// Функция записывает данные в кэш
        /// </summary>
        [ReplyMenuHandler("cache")]
        public static async Task GetCache(IBotContext context)
        {
            string msg = $"Запись в кэш пользователя данных: {context.GetChatId()}";
            //Записываем данные в кеш пользователя
            context.GetCacheData<UserCache>().Id = context.GetChatId();
            await Helpers.Message.Send(context, msg);
        }

        /// <summary>
        /// Напишите в боте "resultcache"
        /// Функция получает данные из кэша
        /// </summary>
        [ReplyMenuHandler("resultcache")]
        public static async Task CheckCache(IBotContext context)
        {
            //Получаем данные с кеша
            var cache = context.GetCacheData<UserCache>();
            string msg = "";
            if(cache.Id != null)
            {
                msg = $"Данные в кэше пользователя: {cache.Id}";
            }
            else
            {
                msg = $"Данные в кэше пользователя отсутствуют.";
            }
            await Helpers.Message.Send(context, msg);
        }

        /// <summary>
        /// Напишите в боте "clearcache"
        /// Функция очищает данные в кэше пользователя
        /// </summary>
        [ReplyMenuHandler("clearcache")]
        public static async Task ClearCache(IBotContext context)
        {
            string msg = "Очистка данных";
            //Очищаем кеш для пользователя
            context.GetCacheData<UserCache>().ClearData();
            await Helpers.Message.Send(context, msg);
        }
    }
}
