using Microsoft.Extensions.Logging;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Interfaces.Managers;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Telegram bot options.
    /// </summary>
    public class TelegramOptions 
    {
        #region Fields and properties

        /// <summary>
        /// The Telegram client.
        /// </summary>
        public ITelegramBotClient? Client { get; set; }

        /// <summary>
        /// Telegram bot token.
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// Before startup, clears the updates that piled up while the bot was down.
        /// </summary>
        public bool ClearUpdatesOnStart { get; set; }

        /// <summary>
        /// Unique identifier of the bot; it lets several bots run in a single application.
        /// </summary>
        public long BotId { get; set; }

        /// <summary>
        /// Additional configuration files.
        /// </summary>
        public Dictionary<string, string> ReplyDynamicCommands { get; set; } = new();

        /// <summary>
        /// Additional configuration files.
        /// </summary>
        public Dictionary<string, string> ConfigPaths { get; set; } = new();

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; set; } = new();

        /// <summary>
        /// Telegram bot settings.
        /// </summary>
        public ReceiverOptions ReceiverOptions { get; set; } = new ReceiverOptions { AllowedUpdates = { } } ;

        /// <summary>
        /// Service provider.
        /// </summary>
        public IServiceProvider? ServiceProvider { get; set; }

        /// <summary>
        /// Telegram update handler.
        /// </summary>
        public IPRUpdateHandler? UpdateHandler { get; set; }

        /// <summary>
        /// Administrator manager.
        /// </summary>
        public IAdminManager? AdminManager { get; set; }

        /// <summary>
        /// White list manager.
        /// </summary>
        public IWhiteListManager? WhiteListManager { get; set; }

        /// <summary>
        /// Middleware handlers that run before the update.
        /// </summary>
        public List<MiddlewareBase> Middlewares { get; set; } = [];

        /// <summary>
        /// Additional checks performed before commands are handled.
        /// </summary>
        public List<InternalChecker> CommandCheckers { get; set; } = [];

        /// <summary>
        /// Timeout for receiving updates in polling mode.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Handlers for callbackQuery (inline) commands.
        /// </summary>
        public List<ICallbackQueryCommandHandler> CallbackQueryHandlers { get; set; } = [];

        /// <summary>
        /// Handlers for message.
        /// </summary>
        public List<IMessageCommandHandler> MessageHandlers { get; set; } = [];

        /// <summary>
        /// This parameter prevents error spam when the network drops. The default is 1 minute and can be changed.
        /// </summary>
        public int AntiSpamErrorMinute { get; set; } = 1;
        
        /// <summary>
        /// Webhook options.
        /// </summary>
        public readonly WebHookOptions WebHookOptions = new();

        /// <summary>
        /// Command options.
        /// </summary>
        public readonly CommandOptions CommandOptions = new();

        /// <summary>
        /// Serializer.
        /// </summary>
        public IPRSerializer? PRSerializer { get; set; }
        
        /// <summary>
        /// Converter for the inline menu.
        /// </summary>
        public IInlineMenuConverter? InlineConverter { get; set; }

        /// <summary>
        /// Predefined administrator identifiers.
        /// </summary>
        public HashSet<long> AdminIds { get; set; } = new();

        /// <summary>
        /// Predefined identifiers of the users on the white list.
        /// </summary>
        public HashSet<long> WhiteListIds { get; set; } = new();

        /// <summary>
        /// White list settings.
        /// </summary>
        public WhiteListSettings WhiteListSettings { get; set; } = WhiteListSettings.OnPreUpdate;

        /// <summary>
        /// An additional action to run when the bot is initialized.
        /// </summary>
        public Action? InitializeAction { get; set; }

        /// <summary>
        /// Background task metadata.
        /// </summary>
        public HashSet<IPRBackgroundTaskMetadata> BackgroundTaskMetadata { get; set; } = new();

        /// <summary>
        /// Background task metadata.
        /// </summary>
        public HashSet<IPRBackgroundTask> BackgroundTasks { get; set; } = new();

        /// <summary>
        /// Logger factory.
        /// </summary>
        public ILoggerFactory? LoggerFactory { get; set; }

        #endregion
    }
}
