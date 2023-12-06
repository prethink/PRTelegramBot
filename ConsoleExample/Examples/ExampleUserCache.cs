using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;
using Helpers = PRTelegramBot.Helpers;
using CallbackId = PRTelegramBot.Models.Enums.THeader;
using ConsoleExample.Models;

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
        public static async Task GetCache(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Запись в кэш пользователя данных: {update.GetChatId()}";
            //Записываем данные в кеш пользователя
            update.GetCacheData<UserCache>().Id = update.GetChatId();
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в боте "resultcache"
        /// Функция получает данные из кэша
        /// </summary>
        [ReplyMenuHandler("resultcache")]
        public static async Task CheckCache(ITelegramBotClient botClient, Update update)
        {
            //Получаем данные с кеша
            var cache = update.GetCacheData<UserCache>();
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
        [ReplyMenuHandler("clearcache")]
        public static async Task ClearCache(ITelegramBotClient botClient, Update update)
        {
            string msg = "Очистка данных";
            //Очищаем кеш для пользователя
            update.GetCacheData<UserCache>().ClearData();
            await Helpers.Message.Send(botClient, update, msg);
        }
    }
}
