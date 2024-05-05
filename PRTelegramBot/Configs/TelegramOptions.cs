namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Конфигурация telegram бота
    /// </summary>
    public class TelegramOptions : ICloneable
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
        /// Дополнительные конфигурационные файлы.
        /// </summary>
        public Dictionary<string, string> ReplyDynamicCommands { get; set; } = new();

        /// <summary>
        /// Дополнительные конфигурационные файлы.
        /// </summary>
        public Dictionary<string, string> ConfigPaths { get; set; } = new();

        public object Clone()
        {
            var cloneOptions = new TelegramOptions();
            cloneOptions.Token = Token;
            cloneOptions.ClearUpdatesOnStart = ClearUpdatesOnStart;
            cloneOptions.BotId = BotId;
            cloneOptions.WhiteListUsers = WhiteListUsers.ToList();
            cloneOptions.Admins = Admins.ToList();
            cloneOptions.ReplyDynamicCommands = new Dictionary<string, string>(ReplyDynamicCommands);
            cloneOptions.ConfigPaths = new Dictionary<string, string>(ConfigPaths);
            return cloneOptions;
        }
    }
}
