# PRBotBuilder - создание ботов

PRBotBuilder - позволяет гибко создавать новых ботов с использованием fluent build.

Пример создания бота через Builder:

```csharp
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .Build();
```

API:

<pre class="language-csharp"><code class="lang-csharp">/// &#x3C;summary>
/// Сбилдить новый экземпляр класса PRBot.
/// &#x3C;/summary>
/// &#x3C;returns>Экземпляр класса PRBot.&#x3C;/returns>
public PRBotBase Build()

/// &#x3C;summary>
/// Сбросить параметры.
/// &#x3C;/summary>
/// &#x3C;param name="token">Токен.&#x3C;/param>
public void ClearOptions(string token)

/// &#x3C;summary>
/// Сбросить параметры.
/// &#x3C;/summary>
/// &#x3C;param name="client">Клиент телеграм бота.&#x3C;/param>
public void ClearOptions(TelegramBotClient client)

/// &#x3C;summary>
/// Установить обработчик обновлений.
/// &#x3C;/summary>
/// &#x3C;param name="updateHandler">Обработчик обновлений.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetUpdateHandler(IPRUpdateHandler updateHandler)

/// &#x3C;summary>
/// Установить регистратор команд.
/// &#x3C;/summary>
/// &#x3C;param name="registerCommand">Регистратор команд.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetRegisterCommand(IRegisterCommand registerCommand)

/// &#x3C;summary>
/// Установить обработчик обновлений.
/// &#x3C;/summary>
/// &#x3C;param name="updateHandler">Обработчик обновлений.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetUpdateHandler(IPRUpdateHandler updateHandler)

/// &#x3C;summary>
/// Установить менеджер управления администраторами.
/// &#x3C;/summary>
/// &#x3C;param name="adminManager">Менеджер управления администраторами.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetAdminManager(IUserManager adminManager)

/// &#x3C;summary>
/// Установить менеджер управления белым списком.
/// &#x3C;/summary>
/// &#x3C;param name="whiteListManager">Менеджер управления белым списком.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetWhiteListManager(IUserManager whiteListManager)

/// &#x3C;summary>
/// Установить новые настройки для белого списка.
/// &#x3C;/summary>
/// &#x3C;param name="settings">Настройки для белого списка.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetWhiteListSettings(WhiteListSettings settings)

/// &#x3C;summary>
/// Добавить промежуточный обработчик.
/// &#x3C;/summary>
/// &#x3C;param name="middleware">Промежуточный обработчик.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddMiddlewares(MiddlewareBase middleware)

/// &#x3C;summary>
/// Добавить промежуточные обработчики.
/// &#x3C;/summary>
/// &#x3C;param name="middlewares">Промежуточные обработчики.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddMiddlewares(params MiddlewareBase[] middlewares)

/// &#x3C;summary>
/// Установить регистратор команд.
/// &#x3C;/summary>
/// &#x3C;param name="registerCommand">Регистратор команд.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetRegisterCommand(IRegisterCommand registerCommand)

/// &#x3C;summary>
/// Добавить чекер перед выполнением команд.
/// &#x3C;/summary>
/// &#x3C;param name="checker">Чекер.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddCommandChecker(InternalChecker checker)

/// &#x3C;summary>
/// Добавить чекеры перед выполнением команд.
/// &#x3C;/summary>
/// &#x3C;param name="checkers">Чекеры.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddCommandChecker(List&#x3C;InternalChecker> checkers)

/// &#x3C;summary>
/// Установить токен в билдере.
/// &#x3C;/summary>
/// &#x3C;param name="token">Токен.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetToken(string token)

/// &#x3C;summary>
/// Установить идентификатор бота.
/// &#x3C;/summary>
/// &#x3C;param name="botId">Идентификатор бота.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetBotId(long botId)

/// &#x3C;summary>
/// Сбрасывать все обновление при запуске бота.
/// &#x3C;/summary>
/// &#x3C;param name="flag">True - да, False - нет.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetClearUpdatesOnStart(bool flag)

/// &#x3C;summary>
/// Добавить динамическую команду.
/// &#x3C;/summary>
/// &#x3C;param name="key">Ключ.&#x3C;/param>
/// &#x3C;param name="value">Значение.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddReplyDynamicCommand(string key, string value)

/// &#x3C;summary>
/// Добавить динамические команды.
/// &#x3C;/summary>
/// &#x3C;param name="dynamicCommands">Коллекция динамических команд.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddReplyDynamicCommands(Dictionary&#x3C;string, string> dynamicCommands)

/// &#x3C;summary>
/// Добавить администратора бота.
/// &#x3C;/summary>
/// &#x3C;param name="telegramId">Идентификатор пользователя.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddAdmin(long telegramId)

/// &#x3C;summary>
/// Добавить администраторов бота.
/// &#x3C;/summary>
/// &#x3C;param name="telegramIds">Коллекция идентификаторов пользователей.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddAdmins(List&#x3C;long> telegramIds)

/// &#x3C;summary>
/// Добавить пользователя в белый список.
/// &#x3C;/summary>
/// &#x3C;param name="telegramId">Идентификатор пользователя.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddUserWhiteList(long telegramId)

/// &#x3C;summary>
/// Добавить пользователей в белый список.
/// &#x3C;/summary>
/// &#x3C;param name="telegramIds">Коллекция идентификаторов пользователей.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddUsersWhiteList(List&#x3C;long> telegramIds)

/// &#x3C;summary>
/// Добавить путь до конфигурационного файла.
/// &#x3C;/summary>
/// &#x3C;param name="key">Ключ.&#x3C;/param>
/// &#x3C;param name="path">Путь.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddConfigPath(string key, string path)

/// &#x3C;summary>
/// Добавить путь до конфигурационных файлов.
/// &#x3C;/summary>
/// &#x3C;param name="configPaths">Коллекция путей.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddConfigPaths(Dictionary&#x3C;string, string> configPaths)

/// &#x3C;summary>
/// Добавить сервис провайдер в бот.
/// &#x3C;/summary>
/// &#x3C;param name="serviceProvider">Сервис провайдер для DI.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetServiceProvider(IServiceProvider serviceProvider)

/// &#x3C;summary>
/// Добавить параметры приемника.
/// &#x3C;/summary>
/// &#x3C;param name="receiverOptions">параметры приемника.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddReceivingOptions(ReceiverOptions receiverOptions)

/// &#x3C;summary>
/// Использовать фабрику для создания бота.
/// &#x3C;/summary>
/// &#x3C;param name="factory">Фабрика.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder UseFactory(PRBotFactoryBase factory)

/// &#x3C;summary>
/// Установить URL для вебхука.
/// &#x3C;/summary>
/// &#x3C;param name="url">URL вебхука.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetUrlWebHook(string url)

/// &#x3C;summary>
/// Установить секретный токен для вебхука.
/// &#x3C;/summary>
/// &#x3C;param name="secretToken">Секретный токен.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetSecretTokenWebHook(string secretToken)

/// &#x3C;summary>
/// Установить IP-адрес для вебхука.
/// &#x3C;/summary>
/// &#x3C;param name="ipAddress">IP-адрес.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetIpAddressWebHook(string ipAddress)

/// &#x3C;summary>
/// Установить флаг сброса отложенных обновлений для вебхука.
/// &#x3C;/summary>
/// &#x3C;param name="flag">Флаг сброса отложенных обновлений.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetDropPendingUpdates(bool flag)

/// &#x3C;summary>
/// Установить максимальное количество подключений для вебхука.
/// &#x3C;/summary>
/// &#x3C;param name="maxConnections">Максимальное количество подключений.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetMaxConnectionsWebHook(int maxConnections)

/// &#x3C;summary>
/// Установить клиент Telegram.
/// &#x3C;/summary>
/// &#x3C;param name="client">Клиент Telegram.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetTelegramClient(TelegramBotClient client)

/// &#x3C;summary>
///  Установить сертификат для вебхука.
/// &#x3C;/summary>
/// &#x3C;param name="certificate">Сертификат.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetCertificateWebHook(InputFileStream certificate)

/// &#x3C;summary>
/// Добавить новый обработчик команд для callbackQuery (inline).
/// &#x3C;/summary>
/// &#x3C;param name="handler">Обработчик.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddCallbackQueryCommandHandlers(params ICallbackQueryCommandHandler[] handlers)

<strong>/// &#x3C;summary>
</strong>/// Добавить новые обработчики команд для callbackQuery (inline).
/// &#x3C;/summary>
/// &#x3C;param name="handlers">Обработчик.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddCallbackQueryCommandHandlers(List&#x3C;ICallbackQueryCommandHandler> handlers)
		
/// &#x3C;summary>
/// Добавить новый обработчик команд для message.
/// &#x3C;/summary>
/// &#x3C;param name="handler">Обработчик.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddMessageCommandHandlers(params IMessageCommandHandler[] handlers)

/// &#x3C;summary>
/// Добавить новые обработчики команд для message.
/// &#x3C;/summary>
/// &#x3C;param name="handlers">Обработчик.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddMessageCommandHandlers(List&#x3C;IMessageCommandHandler> handlers)

/// &#x3C;summary>
/// Конструктор.
/// &#x3C;/summary>
/// &#x3C;param name="token">Токен.&#x3C;/param>
public PRBotBuilder(string token)

/// &#x3C;summary>
/// Конструктор.
/// &#x3C;/summary>
/// &#x3C;param name="client">Клиент.&#x3C;/param>
public PRBotBuilder(TelegramBotClient client)

/// &#x3C;summary>
/// Установить параметр ограничения спама в логах ошибок.
/// &#x3C;/summary>
/// &#x3C;param name="minute">Количество минут.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetAntiSpamErrorMinute(int minute)

/// &#x3C;summary>
/// Установить сериализатор данных для inline кнопок.
/// &#x3C;/summary>
/// &#x3C;param name="serializator">Сериализатор.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetInlineSerializer(IPRSerializer serializator)

/// &#x3C;summary>
/// Установить конвертер для inline меню.
/// &#x3C;/summary>
/// &#x3C;param name="inlineMenuConverter">Конвертер.&#x3C;/param>
/// &#x3C;remarks>Конвертер можно так же добавить через DI.
/// Важное уточнение приоритет установки конвертера идет следующим образом:
/// 1. SetInlineMenuConverter
/// 2. DI
/// 3. defualt&#x3C;/remarks>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetInlineMenuConverter(IInlineMenuConverter inlineMenuConverter)

/// &#x3C;summary>
/// Установить действие при инициализации бота.
/// &#x3C;/summary>
/// &#x3C;param name="action">Действие которое должно быть выполнено при инициализации бота.&#x3C;/param>
/// &#x3C;remarks>Инициализация бота происходит во время его старта.&#x3C;/remarks>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetInitializeAction(Action action)

/// &#x3C;summary>
/// Добавить фоновую задачу.
/// ВАЖНО: backgroundTask должен реализовывать &#x3C;see cref="IPRBackgroundTaskMetadata"/> или использовать атрибут на классе &#x3C;see cref="PRBackgroundTaskAttribute"/>.
/// &#x3C;/summary>
/// &#x3C;param name="backgroundTask">Фоновая задача.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddBackgroundTask(IPRBackgroundTask backgroundTask)

/// &#x3C;summary>
/// Добавить фоновую задачу.
/// &#x3C;/summary>
/// &#x3C;param name="backgroundTask">Фоновая задача.&#x3C;/param>
/// &#x3C;param name="metadata">Метаданные фоновой задачи.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddBackgroundTask(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)

/// &#x3C;summary>
/// Добавить метаданные фоновой задачи.
/// &#x3C;/summary>
/// &#x3C;param name="metadata">Метаданные.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder AddBackgroundTaskMetadata(IPRBackgroundTaskMetadata metadata)

/// &#x3C;summary>
/// Установить фабрику логгеров.
/// Используется, если DI-контейнер не передан или логирование настраивается вручную.
/// &#x3C;/summary>
/// &#x3C;param name="loggerFactory">Фабрика логгеров.&#x3C;/param>
/// &#x3C;returns>Builder.&#x3C;/returns>
public PRBotBuilder SetLoggerFactory(ILoggerFactory loggerFactory)
</code></pre>
