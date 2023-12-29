using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Конфигурация telegram бота
    /// </summary>
    public class TelegramConfig
    {
        /// <summary>
        /// Токен telegram бота берется из BotFather
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Список администраторов бота
        /// Идентификатор телеграм пользователя
        /// </summary>
        public List<long> Admins { get; set; } = new List<long>();

        /// <summary>
        /// Список разрешенных пользователей бота
        /// Если есть хоть 1 идентификатор телеграм пользователя, могут пользоваться только эти пользователи
        /// </summary>
        public List<long> WhiteListUsers { get; set; } = new List<long>();

        /// <summary>
        /// Перед запуском очищает список обновлений, которые накопились когда бот не работал.
        /// </summary>
        public bool ClearUpdatesOnStart { get; set; }

        /// <summary>
        /// Уникальных идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов
        /// </summary>
        public long BotId { get; set; }
    }
}
