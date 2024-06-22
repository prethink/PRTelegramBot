using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Параметры telegram бота.
    /// </summary>
    public class TelegramOptions 
    {
        #region Поля и свойства

        /// <summary>
        /// Клиент телеграма.
        /// </summary>
        public TelegramBotClient? Client { get; set; }

        /// <summary>
        /// Токен telegram бота.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Список администраторов бота.
        /// Идентификатор telegram пользователя.
        /// </summary>
        public List<long> Admins { get; set; } = new List<long>();

        /// <summary>
        /// Список разрешенных пользователей бота
        /// Если есть хоть 1 идентификатор telegram пользователя, могут пользоваться только эти пользователи.
        /// </summary>
        public List<long> WhiteListUsers { get; set; } = new List<long>();

        /// <summary>
        /// Перед запуском очищает список обновлений, которые накопились когда бот не работал.
        /// </summary>
        public bool ClearUpdatesOnStart { get; set; }

        /// <summary>
        /// Уникальных идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов.
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

        /// <summary>
        /// Токен отмены.
        /// </summary>
        public CancellationTokenSource CancellationToken { get; set; } = new CancellationTokenSource();

        /// <summary>
        /// Настройки telegram бота.
        /// </summary>
        public ReceiverOptions ReceiverOptions { get; set; } = new ReceiverOptions { AllowedUpdates = { } } ;

        /// <summary>
        /// Сервис провайдер.
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Обработчик обновлений Telegram.
        /// </summary>
        public IPRUpdateHandler UpdateHandler { get; set; }

        /// <summary>
        /// Регистратор команд.
        /// </summary>
        public IRegisterCommand RegisterCommand { get; set; }

        #endregion
    }
}
