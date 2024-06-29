using PRTelegramBot.Core;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Core.UpdateHandlers;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
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

        /// <summary>
        /// Менеджер управления администраторами.
        /// </summary>
        public IUserManager AdminManager { get; set; } = new AdminListManager();

        /// <summary>
        /// Менеджер управления белым списком.
        /// </summary>
        public IUserManager WhiteListManager { get; set; } = new WhiteListManager();

        /// <summary>
        /// Промежуточные обработчики перед update.
        /// </summary>
        public List<MiddlewareBase> Middlewares { get; set; } = new();

        #endregion
    }
}
