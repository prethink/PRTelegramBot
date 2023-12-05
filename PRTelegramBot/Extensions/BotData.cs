using Microsoft.VisualBasic;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    public static class BotData
    {
        static Dictionary<long, PRBot> _botHandlerData = new();

        /// <summary>
        /// Создает или обновляет данные для бота
        /// </summary>
        /// <param name="botClient">бот клиент</param>
        /// <param name="botData">Экземпляр класса PRBot</param>
        public static void CreateOrUpdateBotData(this ITelegramBotClient botClient, PRBot botData) 
        {
            if(botClient.BotId == null)
            {
                return;
            }

            var data = _botHandlerData.FirstOrDefault(x => x.Key == botClient.BotId);
            if (data.Equals(default(KeyValuePair<long, PRBot>)))
            {
                _botHandlerData.Add(botClient.BotId.Value, botData);
            }
            else
            {
                _botHandlerData.Remove(botClient.BotId.Value);
                _botHandlerData.Add(botClient.BotId.Value, botData);
            }
        }


        public static PRBot GetBotDataOrNull(this ITelegramBotClient botClient)
        {
            if (botClient.BotId == null)
            {
                return null;
            }

            var data = _botHandlerData.FirstOrDefault(x => x.Key == botClient.BotId);
            if (data.Equals(default(KeyValuePair<long, PRBot>)))
            {
                return null;
            }
            else
            {
                return data.Value;
            }
        }


        /// <summary>
        /// Проверяет есть ли данные бота
        /// </summary>
        public static bool HasBotData(this ITelegramBotClient botClient)
        {
            if(botClient.BotId == null)
            {
                return false;
            }
            return _botHandlerData.ContainsKey(botClient.BotId.Value);
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
            if(botData == null)
            {
                return false;
            }

            return botData.Config.Admins.Contains(userId);
        }
    }
}
