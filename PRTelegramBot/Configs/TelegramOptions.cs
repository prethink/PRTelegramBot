namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Конфигурация telegram бота
    /// </summary>
    public class TelegramOptions
    {
        /// <summary>
        /// Токен telegram бота.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Список администраторов бота
        /// Идентификатор telegram пользователя
        /// </summary>
        public List<long> Admins { get; set; } = new List<long>();

        /// <summary>
        /// Список разрешенных пользователей бота
        /// Если есть хоть 1 идентификатор telegram пользователя, могут пользоваться только эти пользователи
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

        /// <summary>
        /// Путь для конфигурационного файла
        /// </summary>
        public string ConfigPath { get; set; }
    }
}
