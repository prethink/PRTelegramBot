using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Configs;
using PRTelegramBot.Converters.Inline;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.CommandHandlers;
using PRTelegramBot.Core.Events;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Interfaces.Managers;
using PRTelegramBot.Managers;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.Logger;
using PRTelegramBot.Registrars;
using PRTelegramBot.Wrappers;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Base class of a bot instance.
    /// </summary>
    public abstract class PRBotBase : IHostedService
    {
        #region Fields and properties

        /// <summary>
        /// Bot name.
        /// </summary>
        public string BotName { get; protected set; }

        /// <summary>
        /// The client for the Telegram bot.
        /// </summary>
        public ITelegramBotClient BotClient { get; protected set; }

        /// <summary>
        /// The bot's identifier in Telegram.
        /// </summary>
        public long? TelegramId => BotClient.BotId;

        /// <summary>
        /// Handler for the Telegram bot
        /// </summary>
        public IPRUpdateHandler Handler { get; protected set; }

        /// <summary>
        /// Whether the bot is running
        /// </summary>
        public bool IsWork { get; protected set; }

        /// <summary>
        /// Bot options.
        /// </summary>
        public TelegramOptions Options { get; protected set; }

        /// <summary>
        /// Bot identifier.
        /// </summary>
        public long BotId => Options.BotId;

        /// <summary>
        /// Events.
        /// </summary>
        public TEvents Events { get; protected set; }

        /// <summary>
        /// Registers the commands.
        /// </summary>
        public IRegisterCommand Register { get; protected set; }

        /// <summary>
        /// The class instances created for inline commands.
        /// </summary>
        public Dictionary<Enum, ICallbackQueryCommandHandler> InlineClassHandlerInstances { get; protected set; } = new();

        /// <summary>
        /// How updates are received.
        /// </summary>
        public abstract DataRetrievalMethod DataRetrieval { get; }

        /// <summary>
        /// Whether to add the bot to the collection when it is created.
        /// </summary>
        protected abstract bool addBotToCollection { get; }

        /// <summary>
        /// The local administrator manager.
        /// </summary>
        protected readonly IAdminManager localAdminManager = new AdminListManager();

        /// <summary>
        /// The local white list manager.
        /// </summary>
        protected readonly IWhiteListManager localWhiteListManager = new WhiteListManager();

        /// <summary>
        /// Background task runner.
        /// </summary>
        public IPRBackgroundTaskRunner BackgroundTaskRunner { get; protected set; }

        /// <summary>
        /// Whether the bot has been initialized.
        /// </summary>
        private bool isInitialized;

        #endregion

        #region Methods

        /// <summary>
        /// Reloads the handlers.
        /// </summary>
        /// <returns>True on success; False on failure.</returns>
        public bool ReloadHandlers()
        {
            try
            {
                InitializeHandlers();
                Handler.HotReload();
                return true;
            }
            catch(Exception ex)
            {
                GetLogger<PRBotBase>().LogErrorInternal(ex);
                return false;
            }
        }

        /// <summary>
        /// Creates a scope for the serviceProvider.
        /// </summary>
        /// <returns>A disposable object that holds the serviceProvider.</returns>
        public DisposableScope CreateServiceScope()
        {
           var scope = Options?.ServiceProvider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
           return new DisposableScope(scope);
        }

        /// <summary>
        /// Gets the serviceProvider.
        /// </summary>
        /// <returns>The IServiceProvider, or null.</returns>
        public IServiceProvider? GetServiceProvider()
        {
            return Options?.ServiceProvider;
        }

        /// <summary>
        /// Sets the service provider on the bot instance.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            Options.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Indicates that the bot has a service provider.
        /// </summary>
        public bool HasServiceProvider => Options?.ServiceProvider != null;

        /// <summary>
        /// Initializes the handlers.
        /// </summary>
        /// <returns>True if initialization succeeded; False if it did not.</returns>
        private bool InitializeHandlers()
        {
            try
            {
                if (Handler is null)
                {
                    Handler = Options.UpdateHandler ?? new Handler(this);
                    if(Handler is Handler baseHandler)
                    {
                        Options.MessageHandlers.Add(new SlashCommandHandler());
                        Options.MessageHandlers.Add(new ReplyCommandHandler());
                        Options.MessageHandlers.Add(new ReplyDynamicCommandHandler());

                        Options.CallbackQueryHandlers.Add(new InlineClassInstanceHandler());
                        Options.CallbackQueryHandlers.Add(new InlineCommandHandler());
                    }
                }
                if (Register is null)
                {
                    Register = Options.CommandOptions.RegisterCommand ?? new RegisterCommand();
                    Register.Init(this);
                }

                return true;
            }
            catch (Exception ex)
            {
                GetLogger<PRBotBase>().LogErrorInternal(ex);
                return false;
            }
        }

        /// <summary>
        /// Initializes the administrator manager.
        /// </summary>
        private async Task InitializeAdminManager()
        {
            try
            {
                var adminManager = GetAdminManager();
                await adminManager.AddUsers(Options.AdminIds.ToArray());
                await adminManager.Initialize();
            }
            catch(Exception ex)
            {
                GetLogger<PRBotBase>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Initializes the white list manager.
        /// </summary>
        private async Task InitializeWhiteListManager()
        {
            try
            {
                var whiteList = GetWhiteListManager();
                await whiteList.AddUsers(Options.WhiteListIds.ToArray());
                whiteList.SetSettings(Options.WhiteListSettings);
                await whiteList.Initialize();
            }
            catch (Exception ex)
            {
                GetLogger<PRBotBase>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Initializes the bot.
        /// </summary>
        public async Task Initialize()
        {
            if(isInitialized)
                return;

            InitializeHandlers();
            await InitializeAdminManager();
            await InitializeWhiteListManager();

            if (GetInlineConverter().GetType() == typeof(TelegramInlineConverter))
                GetLogger<PRBotBase>().LogWarning($"\nThe inline menu is being generated with the default converter, which limits callback_data to 64 bytes (a Telegram restriction). \nTo work around this limit when creating the bot through the builder, use:\n.SetInlineMenuConverter(new FileInlineConverter())\nFor more on how converters work, see the documentation at {PRConstants.DOCUMENTATION_URL}");

            Options?.InitializeAction?.Invoke();
            BackgroundTaskRunner.Initialize(Options.BackgroundTaskMetadata, Options.BackgroundTasks);
            isInitialized = true;
        }

        /// <summary>
        /// Clears the command queue before startup.
        /// </summary>
        protected async Task ClearUpdatesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var updates = await BotClient.GetUpdates(cancellationToken: cancellationToken);
                foreach (var item in updates)
                {
                    var offset = item.Id + 1;
                    await BotClient.GetUpdates(offset, cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                GetLogger<PRBotBase>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Starts the bot.
        /// </summary>
        public virtual async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await Initialize();
        }

        /// <summary>
        /// The method executed after the bot has started.
        /// </summary>
        protected virtual Task OnPostStart()
        {
            _ = BackgroundTaskRunner.StartAsync();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the bot's current serializer.
        /// </summary>
        /// <returns>Serializer.</returns>
        public IPRSerializer GetSerializer()
        {
            return this.PriorityResolve(Options.PRSerializer, () => new JsonSerializerWrapper());
        }

        /// <summary>
        /// Gets the bot's current inline converter.
        /// </summary>
        /// <returns>Inline converter.</returns>
        public IInlineMenuConverter GetInlineConverter()
        {
            return this.PriorityResolve(Options.InlineConverter, () => new TelegramInlineConverter());
        }

        /// <summary>
        /// Gets the bot's current admin manager.
        /// </summary>
        /// <returns>The admin manager.</returns>
        public IAdminManager GetAdminManager()
        {
            return this.PriorityResolve(Options.AdminManager, () => this.localAdminManager);
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <typeparam name="T">Logger type.</typeparam>
        /// <returns>Logger.</returns>
        public ILogger<T> GetLogger<T>()
        {
            if(Options.LoggerFactory == null && CurrentScope.Services != null)
            {
                var currentLogger = CurrentScope.Services?.GetService<ILogger<T>>();
                if (currentLogger != null)
                    return currentLogger;
            }

            return this.GetLoggerFactory().CreateLogger<T>();
        }

        /// <summary>
        /// Gets the logger by Type.
        /// </summary>
        public ILogger GetLogger(Type type)
        {
            if (type == null) 
                throw new ArgumentNullException(nameof(type));

            if (Options.LoggerFactory == null && CurrentScope.Services != null)
            {
                var loggerType = typeof(ILogger<>).MakeGenericType(type);
                var diLogger = CurrentScope.Services.GetService(loggerType) as ILogger;
                if (diLogger != null)
                    return diLogger;
            }

            return this.GetLoggerFactory().CreateLogger(type);
        }

        /// <summary>
        /// Gets the logger factory.
        /// </summary>
        /// <returns>The logger factory.</returns>
        public ILoggerFactory GetLoggerFactory()
        {
            return this.PriorityResolve(Options.LoggerFactory, () => new PRLoggerEventsFactory(this));
        }

        /// <summary>
        /// Gets the bot's current white list manager.
        /// </summary>
        /// <returns>The white list manager.</returns>
        public IWhiteListManager GetWhiteListManager()
        {
            return this.PriorityResolve(Options.WhiteListManager, () => this.localWhiteListManager);
        }

        /// <summary>
        /// Resolves a dependency, honouring the priority of the sources.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="optionValue">
        /// The value set directly in the bot options (it has the highest priority).
        /// </param>
        /// <param name="fallback">
        /// A factory that produces the default value, used when the service
        /// is found neither in the settings nor in the DI container.
        /// </param>
        /// <returns>
        /// The service instance, resolved in the following order of priority:
        /// <list type="number">
        /// <item><description>The value from <paramref name="optionValue"/>.</description></item>
        /// <item><description>The service from the DI container.</description></item>
        /// <item><description>The result of calling <paramref name="fallback"/>.</description></item>
        /// </list>
        /// </returns>
        private T PriorityResolve<T>(T? optionValue, Func<T> fallback)
            where T : class
        {
            return optionValue 
                ?? CurrentScope.Services?.GetService<T>()
                ?? fallback();
        }

        /// <summary>
        /// Stops the bot.
        /// </summary>
        public virtual async Task StopAsync(CancellationToken cancellationToken = default)
        {
            isInitialized = false;
            await BackgroundTaskRunner.StopAsync();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="optionsBuilder">
        /// A configuration delegate that lets the bot options be set up in code.
        /// May be <c>null</c>.  
        /// If supplied, it runs before the <paramref name="options"/> object is applied.
        /// </param>
        /// <param name="options">
        /// A <see cref="TelegramOptions"/> options object holding the bot settings.  
        /// May be <c>null</c>.  
        /// If both <paramref name="optionsBuilder"/> and <paramref name="options"/> are supplied,
        /// a combination of the two is used: <paramref name="optionsBuilder"/> is called first,
        /// and then the parameters from <paramref name="options"/> extend or override them.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when, after the delegate has run and the parameters have been merged,
        /// a valid <see cref="TelegramOptions"/> instance could not be produced.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the configuration parameters contain invalid values
        /// (for example, the bot token is missing or incompatible options are set).
        /// </exception>
        protected PRBotBase(Action<TelegramOptions>? optionsBuilder, TelegramOptions? options)
        {
            Options = new TelegramOptions();
            if (optionsBuilder is not null)
                optionsBuilder.Invoke(Options);
            else
                Options = options ?? throw new ArgumentNullException($"The arguments to the designer are incorrectly transferred, both arguments ({nameof(options)} and {nameof(optionsBuilder)}) cannot be null.");

            if (string.IsNullOrEmpty(Options.Token))
                throw new ArgumentException("Bot token is empty");

            if (Options.BotId < 0)
                throw new ArgumentException("Bot ID cannot be less than zero");

            if(addBotToCollection)
                BotCollection.Instance.AddBot(this);

            BackgroundTaskRunner = new PRBackgroundTaskRunner(this);

            BotClient = Options.Client ?? new TelegramBotClient(Options.Token);
            Events = new TEvents(this);
            InlineClassRegistrar.Register(this);
            InitializeHandlers();
        }

        #endregion
    }
}
