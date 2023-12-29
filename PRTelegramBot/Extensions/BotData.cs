using Microsoft.VisualBasic;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using static PRTelegramBot.Core.PRBot;

namespace PRTelegramBot.Extensions
{
    public static class BotData
    {
        static ConcurrentDictionary<long, PRBot> _botHandlerData = new();

        /// <summary>
        /// Создает или обновляет данные для бота
        /// </summary>
        public static void CreateOrUpdateBotData(this ITelegramBotClient botClient, PRBot botData) 
        {
            if(botClient.BotId == null) return;

            _ = HasBotData(botClient) 
                ? _botHandlerData[botClient.BotId.Value] = botData 
                : _botHandlerData.AddOrUpdate(botClient.BotId.Value, botData, (_, existingData) => botData);
        }

        /// <summary>
        /// Получает данные бота или null
        /// </summary>
        public static PRBot? GetBotDataOrNull(this ITelegramBotClient botClient)
        {
            if (botClient.BotId == null) return null;

            return (_botHandlerData.TryGetValue(botClient.BotId.Value, out var botData)) 
                ?  botData 
                : null;
        }

        /// <summary>
        /// Проверяет есть ли данные бота
        /// </summary>
        public static bool HasBotData(this ITelegramBotClient botClient)
        {
            return botClient.BotId.HasValue && _botHandlerData.ContainsKey(botClient.BotId.Value);
        }

        /// <summary>
        /// Проверяет является ли админом данного бота пользователь по update
        /// </summary>
        public static bool IsAdmin(this ITelegramBotClient botClient, Update update)
        {
            return IsAdmin(botClient, update.GetChatId());
        }

        /// <summary>
        /// Проверяет является ли админом данного бота пользователь по userId
        /// </summary>
        public static bool IsAdmin(this ITelegramBotClient botClient, long userId)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null && botData.Config.Admins.Contains(userId);
        }

        /// <summary>
        /// Возвращает список администраторов бота
        /// </summary>
        public static List<long> GetBotAdminIds(this ITelegramBotClient botClient)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null ? botData.Config.Admins : new List<long>();
        }

        /// <summary>
        /// Логирование простых логов
        /// </summary>
        public static void InvokeCommonLog(this ITelegramBotClient botClient, string msg, Enum? typeEvent = null, ConsoleColor color = ConsoleColor.Blue)
        {
            GetBotDataOrNull(botClient)?.InvokeCommonLog(msg, typeEvent, color);
        }


        /// <summary>
        /// Логирование ошибок
        /// </summary>
        public static void InvokeErrorLog(this ITelegramBotClient botClient, Exception ex, long? id = null)
        {
            GetBotDataOrNull(botClient)?.InvokeErrorLog(ex, id);
        }


    }
}
