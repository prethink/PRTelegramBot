using Microsoft.Extensions.Logging;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Factories;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Interfaces.Managers;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace PRTelegramBot.Builders
{
    /// <summary>
    /// Builder that creates a new instance of the PRBot class.
    /// </summary>
    public sealed class PRBotBuilder
    {
        #region Fields and properties

        private TelegramOptions options;
        private PRBotFactoryBase factory;
        private List<long> adminIds = [];
        private List<long> whiteListIds = [];

        #endregion

        #region Methods

        /// <summary>
        /// Builds a new instance of the PRBot class.
        /// </summary>
        /// <returns>An instance of the PRBot class.</returns>
        public PRBotBase Build()
        {
            foreach (var adminId in adminIds)
                options.AdminIds.Add(adminId);

            foreach (var whiteListUserId in whiteListIds)
                options.WhiteListIds.Add(whiteListUserId);

            return factory.CreateBot(options);
        }

        /// <summary>
        /// Resets the options.
        /// </summary>
        /// <param name="token">Token.</param>
        public void ClearOptions(string token)
        {
            adminIds.Clear();
            whiteListIds.Clear();
            options = new TelegramOptions();
            SetToken(token);
        }

        /// <summary>
        /// Resets the options.
        /// </summary>
        /// <param name="client">Telegram bot client.</param>
        public void ClearOptions(TelegramBotClient client)
        {
            options = new TelegramOptions();
            SetTelegramClient(client);
        }

        /// <summary>
        /// Sets the update handler.
        /// </summary>
        /// <param name="updateHandler">Update handler.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetUpdateHandler(IPRUpdateHandler updateHandler)
        {
            options.UpdateHandler = updateHandler;
            return this;
        }

        /// <summary>
        /// Sets the administrator manager.
        /// </summary>
        /// <param name="adminManager">Administrator manager.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetAdminManager(IAdminManager adminManager)
        {
            options.AdminManager = adminManager;
            return this;
        }

        /// <summary>
        /// Sets the white list manager.
        /// </summary>
        /// <param name="whiteListManager">White list manager.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetWhiteListManager(IWhiteListManager whiteListManager)
        {
            options.WhiteListManager = whiteListManager;
            return this;
        }

        /// <summary>
        /// Sets new white list settings.
        /// </summary>
        /// <param name="settings">White list settings.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetWhiteListSettings(WhiteListSettings settings)
        {
            options.WhiteListSettings = settings;
            return this;
        }

        /// <summary>
        /// Adds a middleware handler.
        /// </summary>
        /// <param name="middleware">Middleware handler.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMiddlewares(MiddlewareBase middleware)
        {
            options.Middlewares.Add(middleware);
            return this;
        }

        /// <summary>
        /// Adds middleware handlers.
        /// </summary>
        /// <param name="middlewares">Middleware handlers.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMiddlewares(params MiddlewareBase[] middlewares)
        {
            options.Middlewares.AddRange(middlewares);
            return this;
        }

        /// <summary>
        /// Adds a checker that runs before commands are executed.
        /// </summary>
        /// <param name="checker">Checker.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCommandChecker(InternalChecker checker)
        {
            options.CommandCheckers.Add(checker);
            return this;
        }

        /// <summary>
        /// Adds checkers that run before commands are executed.
        /// </summary>
        /// <param name="checkers">Checkers.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCommandChecker(List<InternalChecker> checkers)
        {
            options.CommandCheckers.AddRange(checkers);
            return this;
        }

        /// <summary>
        /// Sets the command registrar.
        /// </summary>
        /// <param name="registerCommand">Command registrar.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetRegisterCommand(IRegisterCommand registerCommand)
        {
            options.CommandOptions.RegisterCommand = registerCommand;
            return this;
        }

        /// <summary>
        /// Sets the token on the builder.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetToken(string token)
        {
            options.Token = token;
            return this;
        }

        /// <summary>
        /// Sets the bot identifier.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetBotId(long botId)
        {
            options.BotId = botId;
            return this;
        }

        /// <summary>
        /// Drop all pending updates when the bot starts.
        /// </summary>
        /// <param name="flag">True for yes; False for no.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetClearUpdatesOnStart(bool flag)
        {
            options.ClearUpdatesOnStart = flag;
            return this;
        }

        /// <summary>
        /// Adds a dynamic command.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReplyDynamicCommand(string key, string value)
        {
            options.ReplyDynamicCommands.Add(key, value);
            return this;
        }

        /// <summary>
        /// Adds dynamic commands.
        /// </summary>
        /// <param name="dynamicCommands">Collection of dynamic commands.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReplyDynamicCommands(Dictionary<string, string> dynamicCommands)
        {
            foreach (var command in dynamicCommands)
                options.ReplyDynamicCommands.Add(command.Key, command.Value);
            return this;
        }

        /// <summary>
        /// Adds an administrator to the bot.
        /// </summary>
        /// <param name="telegramId">User identifier.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmin(params long[] telegramId)
        {
            adminIds.AddRange(telegramId);
            return this;
        }

        /// <summary>
        /// Adds administrators to the bot.
        /// </summary>
        /// <param name="telegramIds">Collection of user identifiers.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmins(List<long> telegramIds)
        {
            adminIds.AddRange(telegramIds.ToArray());
            return this;
        }

        /// <summary>
        /// Adds a user to the white list.
        /// </summary>
        /// <param name="telegramId">User identifier.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUserWhiteList(params long[] telegramId)
        {
            whiteListIds.AddRange(telegramId);
            return this;
        }

        /// <summary>
        /// Adds users to the white list.
        /// </summary>
        /// <param name="telegramIds">Collection of user identifiers.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUsersWhiteList(List<long> telegramIds)
        {
            whiteListIds.AddRange(telegramIds.ToArray());
            return this;
        }

        /// <summary>
        /// Adds the path to a configuration file.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="path">Path.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddConfigPath(string key, string path)
        {
            options.ConfigPaths.Add(key, path);
            return this;
        }

        /// <summary>
        /// Adds the paths to configuration files.
        /// </summary>
        /// <param name="configPaths">Collection of paths.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddConfigPaths(Dictionary<string, string> configPaths)
        {
            foreach (var configPath in configPaths)
                options.ConfigPaths.Add(configPath.Key, configPath.Value);
            return this;
        }

        /// <summary>
        /// Adds a service provider to the bot.
        /// </summary>
        /// <param name="serviceProvider">The service provider used for DI.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetServiceProvider(IServiceProvider serviceProvider)
        {
            options.ServiceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// Adds the receiver options.
        /// </summary>
        /// <param name="receiverOptions">Receiver options.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReceivingOptions(ReceiverOptions receiverOptions)
        {
            options.ReceiverOptions = receiverOptions;
            return this;
        }

        /// <summary>
        /// Use a factory to create the bot.
        /// </summary>
        /// <param name="factory">Factory.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder UseFactory(PRBotFactoryBase factory)
        {
            this.factory = factory;
            return this;
        }

        /// <summary>
        /// Sets the URL for the webhook.
        /// </summary>
        /// <param name="url">Webhook URL.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetUrlWebHook(string url)
        {
            options.WebHookOptions.Url = url;
            return this;
        }

        /// <summary>
        /// Sets the secret token for the webhook.
        /// </summary>
        /// <param name="secretToken">Secret token.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetSecretTokenWebHook(string secretToken)
        {
            options.WebHookOptions.SecretToken = secretToken;
            return this;
        }

        /// <summary>
        /// Sets the IP address for the webhook.
        /// </summary>
        /// <param name="ipAddress">IP address.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetIpAddressWebHook(string ipAddress)
        {
            options.WebHookOptions.IpAddress = ipAddress;
            return this;
        }

        /// <summary>
        /// Sets the drop-pending-updates flag for the webhook.
        /// </summary>
        /// <param name="flag">Flag that drops pending updates.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetDropPendingUpdates(bool flag)
        {
            options.WebHookOptions.DropPendingUpdates = flag;
            return this;
        }

        /// <summary>
        /// Sets the maximum number of connections for the webhook.
        /// </summary>
        /// <param name="maxConnections">Maximum number of connections.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetMaxConnectionsWebHook(int maxConnections)
        {
            options.WebHookOptions.MaxConnections = maxConnections;
            return this;
        }

        /// <summary>
        /// Sets the Telegram client.
        /// </summary>
        /// <param name="client">Telegram client.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetTelegramClient(TelegramBotClient client)
        {
            options.Client = client;
            return this;
        }

        /// <summary>
        ///  Sets the certificate for the webhook.
        /// </summary>
        /// <param name="certificate">Certificate.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetCertificateWebHook(InputFileStream certificate)
        {
            options.WebHookOptions.Certificate = certificate;
            return this;
        }

        /// <summary>
        /// Adds new command handler(s) for callbackQuery (inline).
        /// </summary>
        /// <param name="handlers">Handlers for callbackQuery commands.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCallbackQueryCommandHandlers(params ICallbackQueryCommandHandler[] handlers)
        {
            options.CallbackQueryHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Adds new command handlers for callbackQuery (inline).
        /// </summary>
        /// <param name="handlers">Handlers for callbackQuery commands.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCallbackQueryCommandHandlers(List<ICallbackQueryCommandHandler> handlers)
        {
            options.CallbackQueryHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Adds new command handler(s) for message.
        /// </summary>
        /// <param name="handlers">Handler(s) for message commands.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMessageCommandHandlers(params IMessageCommandHandler[] handlers)
        {
            options.MessageHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Adds new command handlers for message.
        /// </summary>
        /// <param name="handlers">Handlers for message commands.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMessageCommandHandlers(List<IMessageCommandHandler> handlers)
        {
            options.MessageHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Sets the spam-limiting parameter for the error logs.
        /// </summary>
        /// <param name="minute">Number of minutes.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetAntiSpamErrorMinute(int minute)
        {
            options.AntiSpamErrorMinute = minute;
            return this;
        }

        /// <summary>
        /// Adds a class instance handler for an inline command.
        /// </summary>
        /// <param name="enum">Command header.</param>
        /// <param name="type">Class type. The type must implement the <see cref="ICallbackQueryCommandHandler"/> interface.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddInlineClassHandler(Enum @enum, Type type)
        {
            if (type.IsAssignableTo(typeof(ICallbackQueryCommandHandler)))
                options.CommandOptions.InlineClassHandlers.Add(@enum, type);
            else
                throw new ArgumentException($"{type} must implement the {typeof(ICallbackQueryCommandHandler)} interface.");

            return this;
        }

        /// <summary>
        /// Sets the data serializer used for inline buttons.
        /// </summary>
        /// <param name="serializer">Serializer.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetInlineSerializer(IPRSerializer serializer)
        {
            options.PRSerializer = serializer;
            return this;
        }

        /// <summary>
        /// Sets the converter for the inline menu.
        /// </summary>
        /// <param name="inlineMenuConverter">Converter.</param>
        /// <remarks>The converter can also be added through DI.
        /// An important note: the converter is resolved in the following order of priority:
        /// 1. SetInlineMenuConverter
        /// 2. DI
        /// 3. defualt</remarks>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetInlineMenuConverter(IInlineMenuConverter inlineMenuConverter)
        {
            options.InlineConverter = inlineMenuConverter;
            return this;
        }

        /// <summary>
        /// Sets the action to run when the bot is initialized.
        /// </summary>
        /// <param name="action">The action to run when the bot is initialized.</param>
        /// <remarks>The bot is initialized while it starts up.</remarks>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetInitializeAction(Action action)
        {
            options.InitializeAction = action;
            return this;
        }

        /// <summary>
        /// Adds a background task.
        /// IMPORTANT: backgroundTask must implement <see cref="IPRBackgroundTaskMetadata"/> or carry the <see cref="PRBackgroundTaskAttribute"/> attribute on the class.
        /// </summary>
        /// <param name="backgroundTask">Background task.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddBackgroundTask(IPRBackgroundTask backgroundTask)
        {
            options.BackgroundTasks.Add(backgroundTask);
            return this;
        }

        /// <summary>
        /// Adds a background task.
        /// </summary>
        /// <param name="backgroundTask">Background task.</param>
        /// <param name="metadata">Background task metadata.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddBackgroundTask(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)
        {
            options.BackgroundTasks.Add(backgroundTask);
            options.BackgroundTaskMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds background task metadata.
        /// </summary>
        /// <param name="metadata">Metadata.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddBackgroundTaskMetadata(IPRBackgroundTaskMetadata metadata)
        {
            options.BackgroundTaskMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Sets the logger factory.
        /// Used when no DI container is supplied, or logging is configured manually.
        /// </summary>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetLoggerFactory(ILoggerFactory loggerFactory)
        {
            options.LoggerFactory = loggerFactory;
            return this;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="token">Token.</param>
        public PRBotBuilder(string token)
            : this()
        {
            SetToken(token);
            AddReceivingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            factory = new PRBotFactory();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">Client.</param>
        public PRBotBuilder(TelegramBotClient client)
            : this()
        {

            options.Client = client;
            AddReceivingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            factory = new PRBotFactory();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        private PRBotBuilder()
        {
            options = new TelegramOptions();
        }

        #endregion
    }
}
