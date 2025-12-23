using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Factory;
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
    /// Билдер для создания нового экземпляра класса PRBot.
    /// </summary>
    public sealed class PRBotBuilder
    {
        #region Поля и свойства

        private TelegramOptions options;
        private PRBotFactoryBase factory;
        private List<long> adminIds = [];
        private List<long> whiteListIds = [];
        private Action<IServiceCollection> configureServicesAction;

        #endregion

        #region Методы

        /// <summary>
        /// Сбилдить новый экземпляр класса PRBot.
        /// </summary>
        /// <returns>Экземпляр класса PRBot.</returns>
        public PRBotBase Build()
        {
            foreach (var adminId in adminIds)
                options.AdminIds.Add(adminId);

            foreach (var whiteListUserId in whiteListIds)
                options.WhiteListIds.Add(whiteListUserId);

            return factory.CreateBot(options);
        }

        /// <summary>
        /// Сбросить параметры.
        /// </summary>
        /// <param name="token">Токен.</param>
        public void ClearOptions(string token)
        {
            adminIds.Clear();
            whiteListIds.Clear();
            options = new TelegramOptions();
            SetToken(token);
        }

        /// <summary>
        /// Сбросить параметры.
        /// </summary>
        /// <param name="client">Клиент telegram бота.</param>
        public void ClearOptions(TelegramBotClient client)
        {
            options = new TelegramOptions();
            SetTelegramClient(client);
        }

        /// <summary>
        /// Установить обработчик обновлений.
        /// </summary>
        /// <param name="updateHandler">Обработчик обновлений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetUpdateHandler(IPRUpdateHandler updateHandler)
        {
            options.UpdateHandler = updateHandler;
            return this;
        }

        /// <summary>
        /// Установить менеджер управления администраторами.
        /// </summary>
        /// <param name="adminManager">Менеджер управления администраторами.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetAdminManager(IAdminManager adminManager)
        {
            options.AdminManager = adminManager;
            return this;
        }

        /// <summary>
        /// Установить менеджер управления белым списком.
        /// </summary>
        /// <param name="whiteListManager">Менеджер управления белым списком.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetWhiteListManager(IWhiteListManager whiteListManager)
        {
            options.WhiteListManager = whiteListManager;
            return this;
        }

        /// <summary>
        /// Установить новые настройки для белого списка.
        /// </summary>
        /// <param name="settings">Настройки для белого списка.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetWhiteListSettings(WhiteListSettings settings)
        {
            options.WhiteListSettings = settings;
            return this;
        }

        /// <summary>
        /// Добавить промежуточный обработчик.
        /// </summary>
        /// <param name="middleware">Промежуточный обработчик.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMiddlewares(MiddlewareBase middleware)
        {
            options.Middlewares.Add(middleware);
            return this;
        }

        /// <summary>
        /// Добавить промежуточные обработчики.
        /// </summary>
        /// <param name="middlewares">Промежуточные обработчики.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMiddlewares(params MiddlewareBase[] middlewares)
        {
            options.Middlewares.AddRange(middlewares);
            return this;
        }

        /// <summary>
        /// Добавить чекер перед выполнением команд.
        /// </summary>
        /// <param name="checker">Чекер.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCommandChecker(InternalChecker checker)
        {
            options.CommandCheckers.Add(checker);
            return this;
        }

        /// <summary>
        /// Добавить чекеры перед выполнением команд.
        /// </summary>
        /// <param name="checkers">Чекеры.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCommandChecker(List<InternalChecker> checkers)
        {
            options.CommandCheckers.AddRange(checkers);
            return this;
        }

        /// <summary>
        /// Установить регистратор команд.
        /// </summary>
        /// <param name="registerCommand">Регистратор команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetRegisterCommand(IRegisterCommand registerCommand)
        {
            options.CommandOptions.RegisterCommand = registerCommand;
            return this;
        }

        /// <summary>
        /// Установить токен в билдере.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetToken(string token)
        {
            options.Token = token;
            return this;
        }

        /// <summary>
        /// Установить идентификатор бота.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetBotId(long botId)
        {
            options.BotId = botId;
            return this;
        }

        /// <summary>
        /// Сбрасывать все обновление при запуске бота.
        /// </summary>
        /// <param name="flag">True - да, False - нет.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetClearUpdatesOnStart(bool flag)
        {
            options.ClearUpdatesOnStart = flag;
            return this;
        }

        /// <summary>
        /// Добавить динамическую команду.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReplyDynamicCommand(string key, string value)
        {
            options.ReplyDynamicCommands.Add(key, value);
            return this;
        }

        /// <summary>
        /// Добавить динамические команды.
        /// </summary>
        /// <param name="dynamicCommands">Коллекция динамических команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReplyDynamicCommands(Dictionary<string, string> dynamicCommands)
        {
            foreach (var command in dynamicCommands)
                options.ReplyDynamicCommands.Add(command.Key, command.Value);
            return this;
        }

        /// <summary>
        /// Добавить администратора бота.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmin(params long[] telegramId)
        {
            adminIds.AddRange(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить администраторов бота.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmins(List<long> telegramIds)
        {
            adminIds.AddRange(telegramIds.ToArray());
            return this;
        }

        /// <summary>
        /// Добавить пользователя в белый список.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUserWhiteList(params long[] telegramId)
        {
            whiteListIds.AddRange(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить пользователей в белый список.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUsersWhiteList(List<long> telegramIds)
        {
            whiteListIds.AddRange(telegramIds.ToArray());
            return this;
        }

        /// <summary>
        /// Добавить путь до конфигурационного файла.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="path">Путь.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddConfigPath(string key, string path)
        {
            options.ConfigPaths.Add(key, path);
            return this;
        }

        /// <summary>
        /// Добавить путь до конфигурационных файлов.
        /// </summary>
        /// <param name="configPaths">Коллекция путей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddConfigPaths(Dictionary<string, string> configPaths)
        {
            foreach (var configPath in configPaths)
                options.ConfigPaths.Add(configPath.Key, configPath.Value);
            return this;
        }

        /// <summary>
        /// Добавить сервис провайдер в бот.
        /// </summary>
        /// <param name="serviceProvider">Сервис провайдер для DI.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetServiceProvider(IServiceProvider serviceProvider)
        {
            options.ServiceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// Добавить параметры приемника.
        /// </summary>
        /// <param name="receiverOptions">Параметры приемника.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReceivingOptions(ReceiverOptions receiverOptions)
        {
            options.ReceiverOptions = receiverOptions;
            return this;
        }

        /// <summary>
        /// Использовать фабрику для создания бота.
        /// </summary>
        /// <param name="factory">Фабрика.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder UseFactory(PRBotFactoryBase factory)
        {
            this.factory = factory;
            return this;
        }

        /// <summary>
        /// Установить URL для вебхука.
        /// </summary>
        /// <param name="url">URL вебхука.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetUrlWebHook(string url)
        {
            options.WebHookOptions.Url = url;
            return this;
        }

        /// <summary>
        /// Установить секретный токен для вебхука.
        /// </summary>
        /// <param name="secretToken">Секретный токен.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetSecretTokenWebHook(string secretToken)
        {
            options.WebHookOptions.SecretToken = secretToken;
            return this;
        }

        /// <summary>
        /// Установить IP-адрес для вебхука.
        /// </summary>
        /// <param name="ipAddress">IP-адрес.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetIpAddressWebHook(string ipAddress)
        {
            options.WebHookOptions.IpAddress = ipAddress;
            return this;
        }

        /// <summary>
        /// Установить флаг сброса отложенных обновлений для вебхука.
        /// </summary>
        /// <param name="flag">Флаг сброса отложенных обновлений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetDropPendingUpdates(bool flag)
        {
            options.WebHookOptions.DropPendingUpdates = flag;
            return this;
        }

        /// <summary>
        /// Установить максимальное количество подключений для вебхука.
        /// </summary>
        /// <param name="maxConnections">Максимальное количество подключений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetMaxConnectionsWebHook(int maxConnections)
        {
            options.WebHookOptions.MaxConnections = maxConnections;
            return this;
        }

        /// <summary>
        /// Установить клиент Telegram.
        /// </summary>
        /// <param name="client">Клиент Telegram.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetTelegramClient(TelegramBotClient client)
        {
            options.Client = client;
            return this;
        }

        /// <summary>
        ///  Установить сертификат для вебхука.
        /// </summary>
        /// <param name="certificate">Сертификат.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetCertificateWebHook(InputFileStream certificate)
        {
            options.WebHookOptions.Certificate = certificate;
            return this;
        }

        /// <summary>
        /// Добавить новы(й/е) обработчик(и) команд для callbackQuery (inline).
        /// </summary>
        /// <param name="handlers">Обработчики для callbackQuery команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCallbackQueryCommandHandlers(params ICallbackQueryCommandHandler[] handlers)
        {
            options.CallbackQueryHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Добавить новые обработчики команд для callbackQuery (inline).
        /// </summary>
        /// <param name="handlers">Обработчики для callbackQuery команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCallbackQueryCommandHandlers(List<ICallbackQueryCommandHandler> handlers)
        {
            options.CallbackQueryHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Добавить новы(й/е) обработчик(и) команд для message.
        /// </summary>
        /// <param name="handlers">Обработчик(и) для message команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMessageCommandHandlers(params IMessageCommandHandler[] handlers)
        {
            options.MessageHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Добавить новые обработчики команд для message.
        /// </summary>
        /// <param name="handlers">Обработчики для message команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMessageCommandHandlers(List<IMessageCommandHandler> handlers)
        {
            options.MessageHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Установить параметр ограничения спама в логах ошибок.
        /// </summary>
        /// <param name="minute">Количество минут.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetAntiSpamErrorMinute(int minute)
        {
            options.AntiSpamErrorMinute = minute;
            return this;
        }

        /// <summary>
        /// Добавить обработчик экземпляра класса для inline команды.
        /// </summary>
        /// <param name="enum">Заголовок команды.</param>
        /// <param name="type">Тип класса. Тип должен реализовывать интерфейс <see cref="ICallbackQueryCommandHandler"/>.</param>
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
        /// Установить сериализатор данных для inline кнопок.
        /// </summary>
        /// <param name="serializator">Сериализатор.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetInlineSerializer(IPRSerializer serializator)
        {
            options.PRSerializer = serializator;
            return this;
        }

        /// <summary>
        /// Установить конвертер для inline меню.
        /// </summary>
        /// <param name="inlineMenuConverter">Конвертер.</param>
        /// <remarks>Конвертер можно так же добавить через DI.
        /// Важное уточнение приоритет установки конвертера идет следующим образом:
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
        /// Установить действие при инициализации бота.
        /// </summary>
        /// <param name="action">Действие которое должно быть выполнено при инициализации бота.</param>
        /// <remarks>Инициализация бота происходит во время его старта.</remarks>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetInitializeAction(Action action)
        {
            options.InitializeAction = action;
            return this;
        }

        /// <summary>
        /// Добавить фоновую задачу.
        /// ВАЖНО: backgroundTask должен реализовывать <see cref="IPRBackgroundTaskMetadata"/> или использовать атрибут на классе <see cref="PRBackgroundTaskAttribute"/>.
        /// </summary>
        /// <param name="backgroundTask">Фоновая задача.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddBackgroundTask(IPRBackgroundTask backgroundTask)
        {
            options.BackgroundTasks.Add(backgroundTask);
            return this;
        }

        /// <summary>
        /// Добавить фоновую задачу.
        /// </summary>
        /// <param name="backgroundTask">Фоновая задача.</param>
        /// <param name="metadata">Метаданные фоновой задачи.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddBackgroundTask(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)
        {
            options.BackgroundTasks.Add(backgroundTask);
            options.BackgroundTaskMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Добавить метаданные фоновой задачи.
        /// </summary>
        /// <param name="metadata">Метаданные.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddBackgroundTaskMetadata(IPRBackgroundTaskMetadata metadata)
        {
            options.BackgroundTaskMetadata.Add(metadata);
            return this;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="token">Токен.</param>
        public PRBotBuilder(string token)
            : this()
        {
            SetToken(token);
            AddReceivingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            factory = new PRBotFactory();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="client">Клиент.</param>
        public PRBotBuilder(TelegramBotClient client)
            : this()
        {

            options.Client = client;
            AddReceivingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            factory = new PRBotFactory();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        private PRBotBuilder()
        {
            options = new TelegramOptions();
        }

        #endregion
    }
}
