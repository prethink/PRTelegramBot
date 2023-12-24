using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Polling;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using System;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core
{
    public class PRBot
    {
        /// <summary>
        /// События для логов
        /// </summary>
        public enum TelegramEvents
        {
            [Description(nameof(Initialization))]
            Initialization,
            [Description(nameof(Register))]
            Register,
            [Description(nameof(Message))]
            Message,
            [Description(nameof(Server))]
            Server,
            [Description(nameof(BlockedBot))]
            BlockedBot,
            [Description(nameof(CommandExecute))]
            CommandExecute,
            [Description(nameof(GroupAction))]
            GroupAction,
        }

        /// <summary>
        /// Имя бота
        /// </summary>
        public string BotName { get; private set; }

        /// <summary>
        /// Клиент для telegram бота
        /// </summary>
        public ITelegramBotClient botClient { get; private set; }

        /// <summary>
        /// Обработчик для telegram бота
        /// </summary>
        public Handler Handler;

        /// <summary>
        /// Токен 
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// Настройки telegram бота
        /// </summary>
        private ReceiverOptions _options;

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
        public delegate void CommonEvent(string msg, TelegramEvents typeEvent, ConsoleColor color);

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
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; private set; }

        public TelegramConfig Config { get; init; } = new TelegramConfig();

        public ReceiverOptions ReceiverOptions { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public PRBot(Action<TelegramConfig> configOptions, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(Action<TelegramConfig> configOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(Action<TelegramConfig> configOptions, ReceiverOptions receiverOptions, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(Action<TelegramConfig> configOptions, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(TelegramConfig config, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(TelegramConfig config, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(TelegramConfig config, ReceiverOptions receiverOptions, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            InitBot();
        }

        public PRBot(TelegramConfig config, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
            InitBot();
        }

        private void InitBot()
        {
            Handler = new Handler(this, Config, _serviceProvider);
            _cts = CancellationTokenSource;
            _options = ReceiverOptions;
        }
            
        /// <summary>
        /// Запуск бота
        /// </summary>
        public async Task Start()
        {
            try
            {
                if(string.IsNullOrEmpty(Config.Token))
                {
                    throw new Exception("Bot token is empty");
                }

                botClient = new TelegramBotClient(Config.Token);

                if (Config.ClearUpdatesOnStart)
                {
                    await ClearUpdates();
                }

                botClient.StartReceiving(Handler , _options);


                var client = await botClient.GetMeAsync();
                BotName = client?.Username;
                this.InvokeCommonLog($"Bot {BotName} is running.", TelegramEvents.Initialization, ConsoleColor.Yellow);
                botClient.CreateOrUpdateBotData(this);
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
                _cts.Cancel();

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
        public async Task ClearUpdates()
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
        public void InvokeCommonLog(string msg, TelegramEvents typeEvent = TelegramEvents.Message, ConsoleColor color = ConsoleColor.Blue)
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
            return Handler.Router.RegisterSlashCommand(command,method);
        }

        /// <summary>
        /// Регистрация Reply command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool RegisterReplyCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            return Handler.Router.RegisterReplyCommand(command, method);
        }

        /// <summary>
        /// Регистрация inline command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool RegisterInlineCommand(Enum command, Func<ITelegramBotClient, Update, Task> method)
        {
            return Handler.Router.RegisterInlineCommand(command, method);
        }

        /// <summary>
        /// Удаление Reply команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveReplyCommand(string command)
        {
            return Handler.Router.RemoveReplyCommand(command);
        }

        /// <summary>
        /// Удаление slash команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveSlashCommand(string command)
        {
            return Handler.Router.RemoveSlashCommand(command);
        }

        /// <summary>
        /// Удаление inline команды
        /// </summary>
        /// <param name="command">перечисление команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveInlineCommand(Enum command)
        {
            return Handler.Router.RemoveInlineCommand(command);
        }
    }
}