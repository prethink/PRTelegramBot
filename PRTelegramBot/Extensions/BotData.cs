﻿using Microsoft.VisualBasic;
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
            
            if (HasBotData(botClient))
            {
                _botHandlerData[botClient.BotId.Value] = botData; 
            }
            else
            {
                _botHandlerData.AddOrUpdate(botClient.BotId.Value, botData, (_, existingData) => botData);
            }
        }

        /// <summary>
        /// Получает данные бота или null
        /// </summary>
        public static PRBot? GetBotDataOrNull(this ITelegramBotClient botClient)
        {
            if (botClient.BotId == null) return null;

            if (_botHandlerData.TryGetValue(botClient.BotId.Value, out var botData))
            {
                return botData;
            }
            else
            {
                return null;
            }
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
    }
}