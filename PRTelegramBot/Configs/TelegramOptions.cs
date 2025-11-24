using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Managers;
using PRTelegramBot.Models;
using PRTelegramBot.Wrappers;
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
        public ITelegramBotClient? Client { get; set; }

        /// <summary>
        /// Токен telegram бота.
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// Перед запуском очищает список обновлений, которые накопились когда бот не работал.
        /// </summary>
        public bool ClearUpdatesOnStart { get; set; }

        /// <summary>
        /// Уникальный идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов.
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
        /// Источник токена отмены.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; set; } = new();

        /// <summary>
        /// Настройки telegram бота.
        /// </summary>
        public ReceiverOptions ReceiverOptions { get; set; } = new ReceiverOptions { AllowedUpdates = { } } ;

        /// <summary>
        /// Сервис провайдер.
        /// </summary>
        public IServiceProvider? ServiceProvider { get; set; }

        /// <summary>
        /// Обработчик обновлений Telegram.
        /// </summary>
        public IPRUpdateHandler? UpdateHandler { get; set; }

        /// <summary>
        /// Менеджер управления администраторами.
        /// </summary>
        public IUserManager AdminManager { get; set; } = new AdminListManager();

        /// <summary>
        /// Менеджер управления белым списком.
        /// </summary>
        public IWhiteListManager WhiteListManager { get; set; } = new WhiteListManager();

        /// <summary>
        /// Промежуточные обработчики перед update.
        /// </summary>
        public List<MiddlewareBase> Middlewares { get; set; } = [];

        /// <summary>
        /// Дополнительные проверки перед обработкой команд.
        /// </summary>
        public List<InternalChecker> CommandCheckers { get; set; } = [];

        /// <summary>
        /// Таймаут для получения update в режиме polling.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Обработчики callbackQuery (inline) команд.
        /// </summary>
        public List<ICallbackQueryCommandHandler> CallbackQueryHandlers { get; set; } = [];

        /// <summary>
        /// Обработчики для message.
        /// </summary>
        public List<IMessageCommandHandler> MessageHandlers { get; set; } = [];

        /// <summary>
        /// Параметр предотвращает спам об ошибке, если пропала сеть. По умолчанию значение 1 минута, можно поменять.
        /// </summary>
        public int AntiSpamErrorMinute { get; set; } = 1;
        
        /// <summary>
        /// Параметры для webhook.
        /// </summary>
        public readonly WebHookOptions WebHookOptions = new();

        /// <summary>
        /// Параметры команд.
        /// </summary>
        public readonly CommandOptions CommandOptions = new();

        /// <summary>
        /// Сериалайзер данных.
        /// </summary>
        public IPRSerializator Serializator { get; set; } = new JsonSerializatorWrapper();

        #endregion
    }
}
