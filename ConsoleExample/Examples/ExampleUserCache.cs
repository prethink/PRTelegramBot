using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.Examples
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
        [ReplyMenuHandler(true, "cache")]
        public static async Task GetCache(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Запись в кэш пользователя данных: {update.GetChatId()}";
            //Записываем данные в кеш пользователя
            update.GetCacheData().Id = update.GetChatId();
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в боте "resultcache"
        /// Функция получает данные из кэша
        /// </summary>
        [ReplyMenuHandler(true, "resultcache")]
        public static async Task CheckCache(ITelegramBotClient botClient, Update update)
        {
            //Получаем данные с кеша
            var cache = update.GetCacheData();
            string msg = "";
            if(cache.Id != null)
            {
                msg = $"Данные в кэше пользователя: {cache.Id}";
            }
            else
            {
                msg = $"Данные в кэше пользователя отсутствуют.";
            }
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в боте "clearcache"
        /// Функция очищает данные в кэше пользователя
        /// </summary>
        [ReplyMenuHandler(true, "clearcache")]
        public static async Task ClearCache(ITelegramBotClient botClient, Update update)
        {
            string msg = "Тестирование функции пошагового выполнения";
            //Очищаем кеш для пользователя
            update.GetCacheData().ClearData();
            await Helpers.Message.Send(botClient, update, msg);
        }
    }
}
