using Microsoft.Extensions.Options;
using PRTelegramBot.Configs;
using PRTelegramBot.Core.Factory;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Билдер для создания нового экземпляра класса PRBot.
    /// </summary>
    public sealed class PRBotBuilder
    {
        #region Поля и свойства

        private TelegramOptions _options;
        private PRBotFactoryBase _factory;
        private List<long> _admins = [];
        private List<long> _whiteList = [];
        private WhiteListSettings _whiteListSettings = WhiteListSettings.OnPreUpdate;

        #endregion

        #region Методы

        /// <summary>
        /// Сбилдить новый экземпляр класса PRBot.
        /// </summary>
        /// <returns>Экземпляр класса PRBot.</returns>
        public PRBotBase Build()
        {
            _options.AdminManager.AddUsers(_admins.ToArray());
            _options.WhiteListManager.AddUsers(_whiteList.ToArray());
            _options.WhiteListManager.SetSettings(_whiteListSettings);
            return _factory.CreateBot(_options);
        }

        /// <summary>
        /// Сбросить параметры.
        /// </summary>
        /// <param name="token">Токен.</param>
        public void ClearOptions(string token)
        {
            _admins.Clear();
            _whiteList.Clear();
            _options = new TelegramOptions();
            SetToken(token);
        }

        /// <summary>
        /// Сбросить параметры.
        /// </summary>
        /// <param name="client">Клиент телеграм бота.</param>
        public void ClearOptions(TelegramBotClient client)
        {
            _options = new TelegramOptions();
            SetTelegramClient(client);
        }

        /// <summary>
        /// Установить обработчик обновлений.
        /// </summary>
        /// <param name="updateHandler">Обработчик обновлений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetUpdateHandler(IPRUpdateHandler updateHandler)
        {
            _options.UpdateHandler = updateHandler;
            return this;
        }

        /// <summary>
        /// Установить менеджер управления администраторами.
        /// </summary>
        /// <param name="adminManager">Менеджер управления администраторами.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetAdminManager(IUserManager adminManager)
        {
            _options.AdminManager = adminManager;
            return this;
        }

        /// <summary>
        /// Установить менеджер управления белым списком.
        /// </summary>
        /// <param name="whiteListManager">Менеджер управления белым списком.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetWhiteListManager(IWhiteListManager whiteListManager)
        {
            _options.WhiteListManager = whiteListManager;
            return this;
        }

        /// <summary>
        /// Установить новые настройки для белого списка.
        /// </summary>
        /// <param name="settings">Настройки для белого списка.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetWhiteListSettings(WhiteListSettings settings)
        {
            _whiteListSettings = settings;
            return this;
        }

        /// <summary>
        /// Добавить промежуточный обработчик.
        /// </summary>
        /// <param name="middleware">Промежуточный обработчик.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMiddlewares(MiddlewareBase middleware)
        {
            _options.Middlewares.Add(middleware);
            return this;
        }

        /// <summary>
        /// Добавить промежуточные обработчики.
        /// </summary>
        /// <param name="middlewares">Промежуточные обработчики.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMiddlewares(params MiddlewareBase[] middlewares)
        {
            _options.Middlewares.AddRange(middlewares);
            return this;
        }

        /// <summary>
        /// Добавить чекер перед выполнением команд.
        /// </summary>
        /// <param name="checker">Чекер.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCommandChecker(InternalChecker checker)
        {
            _options.CommandCheckers.Add(checker);
            return this;
        }

        /// <summary>
        /// Добавить чекеры перед выполнением команд.
        /// </summary>
        /// <param name="checkers">Чекеры.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCommandChecker(List<InternalChecker> checkers)
        {
            _options.CommandCheckers.AddRange(checkers);
            return this;
        }

        /// <summary>
        /// Установить регистратор команд.
        /// </summary>
        /// <param name="registerCommand">Регистратор команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetRegisterCommand(IRegisterCommand registerCommand)
        {
            _options.CommandOptions.RegisterCommand = registerCommand;
            return this;
        }

        /// <summary>
        /// Установить токен в билдере.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetToken(string token)
        {
            _options.Token = token;
            return this;
        }

        /// <summary>
        /// Установить идентификатор бота.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetBotId(long botId)
        {
            _options.BotId = botId;
            return this;
        }

        /// <summary>
        /// Сбрасывать все обновление при запуске бота.
        /// </summary>
        /// <param name="flag">True - да, False - нет.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetClearUpdatesOnStart(bool flag)
        {
            _options.ClearUpdatesOnStart = flag;
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
            _options.ReplyDynamicCommands.Add(key, value);
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
                _options.ReplyDynamicCommands.Add(command.Key, command.Value);
            return this;
        }

        /// <summary>
        /// Добавить администратора бота.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmin(params long[] telegramId)
        {
            _admins.AddRange(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить администраторов бота.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmins(List<long> telegramIds)
        {
            _admins.AddRange(telegramIds.ToArray());
            return this;
        }

        /// <summary>
        /// Добавить пользователя в белый список.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUserWhiteList(params long[] telegramId)
        {
            _whiteList.AddRange(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить пользователей в белый список.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUsersWhiteList(List<long> telegramIds)
        {
            _whiteList.AddRange(telegramIds.ToArray());
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
            _options.ConfigPaths.Add(key, path);
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
                _options.ConfigPaths.Add(configPath.Key, configPath.Value);
            return this;
        }

        /// <summary>
        /// Добавить сервис провайдер в бот.
        /// </summary>
        /// <param name="serviceProvider">Сервис провайдер для DI.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetServiceProvider(IServiceProvider serviceProvider)
        {
            _options.ServiceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// Добавить параметры приемника.
        /// </summary>
        /// <param name="recevierOptions">Параметры приемника.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddRecevingOptions(ReceiverOptions recevierOptions)  // TODO: исправить опечатку в названии на AddReceivingOptions.
        {
            _options.ReceiverOptions = recevierOptions;
            return this;
        }

        /// <summary>
        /// Использовать фабрику для создания бота.
        /// </summary>
        /// <param name="factory">Фабрика.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder UseFactory(PRBotFactoryBase factory)
        {
            _factory = factory;
            return this;
        }

        /// <summary>
        /// Установить URL для вебхука.
        /// </summary>
        /// <param name="url">URL вебхука.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetUrlWebHook(string url)
        {
            _options.WebHookOptions.Url = url;
            return this;
        }

        /// <summary>
        /// Установить секретный токен для вебхука.
        /// </summary>
        /// <param name="secretToken">Секретный токен.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetSecretTokenWebHook(string secretToken)
        {
            _options.WebHookOptions.SecretToken = secretToken;
            return this;
        }

        /// <summary>
        /// Установить IP-адрес для вебхука.
        /// </summary>
        /// <param name="ipAddress">IP-адрес.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetIpAddresWebHook(string ipAddress)  // TODO: исправить опечатку в названии на SetIpAddressWebHook.
        {
            _options.WebHookOptions.IpAddress = ipAddress;
            return this;
        }

        /// <summary>
        /// Установить флаг сброса отложенных обновлений для вебхука.
        /// </summary>
        /// <param name="flag">Флаг сброса отложенных обновлений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetDropPendingUpdates(bool flag)
        {
            _options.WebHookOptions.DropPendingUpdates = flag;
            return this;
        }

        /// <summary>
        /// Установить максимальное количество подключений для вебхука.
        /// </summary>
        /// <param name="maxConnections">Максимальное количество подключений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetMaxConnectionsWebHook(int maxConnections)
        {
            _options.WebHookOptions.MaxConnections = maxConnections;
            return this;
        }

        /// <summary>
        /// Установить клиент Telegram.
        /// </summary>
        /// <param name="client">Клиент Telegram.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetTelegramClient(TelegramBotClient client)
        {
            _options.Client = client;
            return this;
        }

        /// <summary>
        ///  Установить сертификат для вебхука.
        /// </summary>
        /// <param name="certificate">Сертификат.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetCertificateWebHook(InputFileStream certificate)
        {
            _options.WebHookOptions.Certificate = certificate;
            return this;
        }

        /// <summary>
        /// Добавить новы(й/е) обработчик(и) команд для callbackQuery (inline).
        /// </summary>
        /// <param name="handlers">Обработчики для callbackQuery команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCallbackQueryCommandHandlers(params ICallbackQueryCommandHandler[] handlers)
        {
            _options.CallbackQueryHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Добавить новые обработчики команд для callbackQuery (inline).
        /// </summary>
        /// <param name="handlers">Обработчики для callbackQuery команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddCallbackQueryCommandHandlers(List<ICallbackQueryCommandHandler> handlers)
        {
            _options.CallbackQueryHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Добавить новы(й/е) обработчик(и) команд для message.
        /// </summary>
        /// <param name="handlers">Обработчик(и) для message команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMessageCommandHandlers(params IMessageCommandHandler[] handlers)
        {
            _options.MessageHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Добавить новые обработчики команд для message.
        /// </summary>
        /// <param name="handlers">Обработчики для message команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddMessageCommandHandlers(List<IMessageCommandHandler> handlers)
        {
            _options.MessageHandlers.AddRange(handlers);
            return this;
        }

        /// <summary>
        /// Установить параметр ограничения спама в логах ошибок.
        /// </summary>
        /// <param name="minute">Количество минут.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetAntiSpamErrorMinute(int minute)
        {
            _options.AntiSpamErrorMinute = minute;
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
                _options.CommandOptions.InlineClassHandlers.Add(@enum, type);
            else
                throw new ArgumentException($"{type} must implement the {typeof(ICallbackQueryCommandHandler)} interface.");

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
            AddRecevingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            _factory = new PRBotFactory();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="client">Клиент.</param>
        public PRBotBuilder(TelegramBotClient client)
            : this()
        {

            _options.Client = client;
            AddRecevingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            _factory = new PRBotFactory();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        private PRBotBuilder()
        {
            _options = new TelegramOptions();
        }

        #endregion
    }
}
