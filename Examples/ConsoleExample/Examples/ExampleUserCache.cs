using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples
{
    /// <summary>
    /// Example of working with the user cache
    /// </summary>
    public class ExampleUserCache
    {
        /// <summary>
        /// Send "cache" to the bot
        /// Writes the data into the cache
        /// </summary>
        [ReplyMenuHandler("cache")]
        public static async Task GetCache(IBotContext context)
        {
            string msg = $"Writing data into the user's cache: {context.GetChatId()}";
            //Write the data into the user's cache
            context.GetCacheData<UserCache>().Id = context.GetChatId();
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Send "resultcache" to the bot
        /// Reads the data from the cache
        /// </summary>
        [ReplyMenuHandler("resultcache")]
        public static async Task CheckCache(IBotContext context)
        {
            //Read the data from the cache
            var cache = context.GetCacheData<UserCache>();
            string msg = string.Empty;
            if (cache.Id != 0)
            {
                msg = $"Data in the user's cache: {cache.Id}";
            }
            else
            {
                msg = $"There is no data in the user's cache.";
            }
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Send "clearcache" to the bot
        /// Clears the data in the user's cache
        /// </summary>
        [ReplyMenuHandler("clearcache")]
        public static async Task ClearCache(IBotContext context)
        {
            string msg = "Clearing the data";
            //Clear the user's cache
            context.GetCacheData<UserCache>().ClearData();
            await MessageSender.Send(context, msg);
        }
    }
}
