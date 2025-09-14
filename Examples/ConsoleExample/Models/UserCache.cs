﻿using PRTelegramBot.Interfaces;

namespace ConsoleExample.Models
{
    /// <summary>
    /// Пример кэша.
    /// </summary>
    public class UserCache : ITelegramCache
    {
        public long Id { get; set; }
        /// <summary>
        /// Временные данные
        /// </summary>
        public string Data { get; set; }

        public bool ClearData()
        {
            Data = string.Empty;
            return true;
        }
    }
}
