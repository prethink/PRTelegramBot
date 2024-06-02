using PRTelegramBot.Configs;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core
{
    public sealed class PRBot
    {
        #region Поля и свойства

        /// <summary>
        /// Имя бота.
        /// </summary>
        public string BotName { get; private set; }

        /// <summary>
        /// Клиент для telegram бота.
        /// </summary>
        public ITelegramBotClient botClient { get; private set; }

        /// <summary>
        /// Идетификатор бота в telegram.
        /// </summary>
        public long? TelegramId => botClient.BotId;

        /// <summary>
        /// Обработчик для telegram бота
        /// </summary>
        public Handler Handler;

        /// <summary>
        /// Токен.
        /// </summary>
        private CancellationTokenSource cts;

        /// <summary>
        /// Настройки telegram бота
        /// </summary>
        private ReceiverOptions receiverOptions;

        /// <summary>
        /// Сигнатура для записи ошибок
        /// </summary>
        /// <param name="ex">Эксекшен</param>
        /// <param name="id">Идентификатор пользователя</param>
        public delegate void ErrorEvent(Exception ex, long? id);

        /// <summary>
        /// Сигнатура для записи обычных логов
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="typeEvent">Тип событий</param>
        /// <param name="color">Цвет</param>
        public delegate void CommonEvent(string msg, string type, ConsoleColor color);

        /// <summary>
        /// События записи ошибок
        /// </summary>
        public event ErrorEvent OnLogError;

        /// <summary>
        /// Событие записи обычного лога
        /// </summary>
        public event CommonEvent OnLogCommon;

        /// <summary>
        /// 
        /// </summary>
        private IServiceProvider serviceProvider;

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; private set; }

        /// <summary>
        /// Параметры бота.
        /// </summary>
        public TelegramOptions Options { get; init; } = new TelegramOptions();

        /// <summary>
        /// Идентификатор бота.
        /// </summary>
        public long BotId => Options.BotId;

        /// <summary>
        /// События.
        /// </summary>
        public TEvents Events { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Запуск бота
        /// </summary>
        public async Task Start()
        {
            try
            {
                if (Options.ClearUpdatesOnStart)
                    await ClearUpdates();

                botClient.StartReceiving(Handler, receiverOptions);

                var client = await botClient.GetMeAsync();
                BotName = client?.Username;
                this.InvokeCommonLog($"Bot {BotName} is running.", "Initialization", ConsoleColor.Yellow);
                IsWork = true;
            }
            catch (Exception ex)
            {
                IsWork = false;
                this.InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Остановка бота
        /// </summary>
        public async Task Stop()
        {
            try
            {
                cts.Cancel();

                await Task.Delay(3000);
                IsWork = false;
            }
            catch (Exception ex)
            {
                this.InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Очистка очереди команд перед запуском
        /// </summary>
        private async Task ClearUpdates()
        {
            try
            {
                var update = await botClient.GetUpdatesAsync();
                foreach (var item in update)
                {
                    var offset = item.Id + 1;
                    await botClient.GetUpdatesAsync(offset);
                }
            }
            catch (Exception ex)
            {
                InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Вызов события записи в обычный лог
        /// </summary>
        /// <param name="msg">Текст</param>
        /// <param name="typeEvent">Тип события</param>
        /// <param name="color">Цвет</param>
        public void InvokeCommonLog(string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)
        {
            OnLogCommon?.Invoke(msg, typeEvent, color);
        }

        /// <summary>
        /// Вызов события записи в лог ошибок
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="id">Идентификатор</param>
        public void InvokeErrorLog(Exception ex, long? id = null)
        {
            OnLogError?.Invoke(ex, id);
        }

        /// <summary>
        /// Регистрация Slash command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool RegisterSlashCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            return Handler.MessageFacade.SlashHandler.AddCommand(command, method);
        }

        /// <summary>
        /// Регистрация Reply command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool RegisterReplyCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            return Handler.MessageFacade.ReplyHandler.AddCommand(command, method);
        }

        /// <summary>
        /// Регистрация inline command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool RegisterInlineCommand(Enum command, Func<ITelegramBotClient, Update, Task> method)
        {
            return Handler.InlineUpdateHandler.AddCommand(command, method);
        }

        /// <summary>
        /// Удаление Reply команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveReplyCommand(string command)
        {
            return Handler.MessageFacade.ReplyHandler.RemoveCommand(command);
        }

        /// <summary>
        /// Удаление slash команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveSlashCommand(string command)
        {
            return Handler.MessageFacade.SlashHandler.RemoveCommand(command);
        }

        /// <summary>
        /// Удаление inline команды
        /// </summary>
        /// <param name="command">перечисление команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveInlineCommand(Enum command)
        {
            return Handler.InlineUpdateHandler.RemoveCommand(command);
        }

        #endregion

        #region Конструкторы

        public PRBot(Action<TelegramOptions> options)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), null) { }

        public PRBot(Action<TelegramOptions> options, IServiceProvider serviceProvider)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions)
            : this(options, null, receiverOptions, new CancellationTokenSource(), null) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions, IServiceProvider serviceProvider)
            : this(options, null, receiverOptions, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(Action<TelegramOptions> options, CancellationTokenSource cancellationToken)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, null) { }

        public PRBot(Action<TelegramOptions> options, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(options, null, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, serviceProvider) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken)
            : this(options, null, receiverOptions, cancellationToken, null) { }

        public PRBot(Action<TelegramOptions> options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(options, null, receiverOptions, cancellationToken, serviceProvider) { }

        public PRBot(TelegramOptions options)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), null) { }

        public PRBot(TelegramOptions options, IServiceProvider serviceProvider)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions)
            : this(null, options, receiverOptions, new CancellationTokenSource(), null) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions, IServiceProvider serviceProvider)
            : this(null, options, receiverOptions, new CancellationTokenSource(), serviceProvider) { }

        public PRBot(TelegramOptions options, CancellationTokenSource cancellationToken)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, null) { }

        public PRBot(TelegramOptions options, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(null, options, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken, serviceProvider) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken)
            : this(null, options, receiverOptions, cancellationToken, null) { }

        public PRBot(TelegramOptions options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
            : this(null, options, receiverOptions, cancellationToken, serviceProvider) { }

        private PRBot(Action<TelegramOptions> optionsBuilder, TelegramOptions options, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider)
        {
            if (optionsBuilder != null)
                optionsBuilder.Invoke(Options);
            else
                Options = options;

            if (string.IsNullOrEmpty(Options.Token))
                throw new Exception("Bot token is empty");

            if (Options.BotId < 0)
                throw new Exception("Bot ID cannot be less than zero");

            botClient = new TelegramBotClient(Options.Token);
            BotCollection.Instance.AddBot(this);
            this.receiverOptions = receiverOptions;
            this.serviceProvider = serviceProvider;
            Events = new TEvents();
            Handler = new Handler(this, this.serviceProvider);
            cts = cancellationToken;

        }

        #endregion
    }
}