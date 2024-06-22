using PRTelegramBot.Configs;
using PRTelegramBot.Core.Factory;
using PRTelegramBot.Interfaces;
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

        private WebHookTelegramOptions options;
        private PRBotFactoryBase factory;

        #endregion

        #region Методы

        /// <summary>
        /// Сбилдить новый экземпляр класса PRBot.
        /// </summary>
        /// <returns>Экземпляр класса PRBot.</returns>
        public PRBotBase Build()
        {
            var createOptions = options;
            return factory.CreateBot(createOptions);
        }

        /// <summary>
        /// Сбросить параметры.
        /// </summary>
        /// <param name="token">Токен.</param>
        public void ClearOptions(string token)
        {
            options = new WebHookTelegramOptions();
            SetToken(token);
        }

        /// <summary>
        /// Сбросить параметры.
        /// </summary>
        /// <param name="client">Клиент телеграм бота.</param>
        public void ClearOptions(TelegramBotClient client)
        {
            options = new WebHookTelegramOptions();
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
        /// Установить регистратор команд.
        /// </summary>
        /// <param name="registerCommand">Регистратор команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetRegisterCommand(IRegisterCommand registerCommand)
        {
            options.RegisterCommand = registerCommand;
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
        public PRBotBuilder AddAdmin(long telegramId)
        {
            options.Admins.Add(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить администраторов бота.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmins(List<long> telegramIds)
        {
            options.Admins.AddRange(telegramIds);
            return this;
        }

        /// <summary>
        /// Добавить пользователя в белый список.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUserWhiteList(long telegramId)
        {
            options.WhiteListUsers.Add(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить пользователей в белый список.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUsersWhiteList(List<long> telegramIds)
        {
            options.WhiteListUsers.AddRange(telegramIds);
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
            this.options.ServiceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// Добавить параметры приемника.
        /// </summary>
        /// <param name="recevierOptions">параметры приемника.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddRecevingOptions(ReceiverOptions recevierOptions)
        {
            this.options.ReceiverOptions = recevierOptions;
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
            this.options.Url = url;
            return this;
        }

        /// <summary>
        /// Установить секретный токен для вебхука.
        /// </summary>
        /// <param name="secretToken">Секретный токен.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetSecretTokenWebHook(string secretToken)
        {
            this.options.SecretToken = secretToken;
            return this;
        }

        /// <summary>
        /// Установить IP-адрес для вебхука.
        /// </summary>
        /// <param name="ipAddres">IP-адрес.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetIpAddresWebHook(string ipAddres)
        {
            this.options.IpAddress = ipAddres;
            return this;
        }

        /// <summary>
        /// Установить флаг сброса отложенных обновлений для вебхука.
        /// </summary>
        /// <param name="flag">Флаг сброса отложенных обновлений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetDropPendingUpdates(bool flag)
        {
            this.options.DropPendingUpdates = flag;
            return this;
        }

        /// <summary>
        /// Установить максимальное количество подключений для вебхука.
        /// </summary>
        /// <param name="maxConnections">Максимальное количество подключений.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetMaxConnectionsWebHook(int maxConnections)
        {
            this.options.MaxConnections = maxConnections;
            return this;
        }

        /// <summary>
        /// Установить клиент Telegram.
        /// </summary>
        /// <param name="client">Клиент Telegram.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetTelegramClient(TelegramBotClient client)
        {
            this.options.Client = client;
            return this;
        }

        /// <summary>
        ///  Установить сертификат для вебхука.
        /// </summary>
        /// <param name="certificate">Сертификат.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetCertificateWebHook(InputFileStream certificate)
        {
            this.options.Certificate = certificate;
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
            AddRecevingOptions(new ReceiverOptions() { AllowedUpdates = { } });
            factory = new PRBotFactory();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        private PRBotBuilder()
        {
            options = new WebHookTelegramOptions();
        }

        #endregion
    }
}
