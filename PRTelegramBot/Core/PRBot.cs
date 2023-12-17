using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Polling;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using System;

namespace PRTelegramBot.Core
{
    public class PRBot
    {
        /// <summary>
        /// События для логов
        /// </summary>
        public enum TelegramEvents
        {
            [Description("Initialization")]
            Initialization,
            [Description("Register")]
            Register,
            [Description("Message")]
            Message,
            [Description("Server")]
            Server,
            [Description("BlockedBot")]
            BlockedBot,
            [Description("CommandExecute")]
            CommandExecute,
            [Description("GroupAction")]
            GroupAction,
        }

        /// <summary>
        /// Имя бота
        /// </summary>
        public string BotName { get; private set; }

        /// <summary>
        /// Клиент для телеграм бота
        /// </summary>
        public ITelegramBotClient botClient { get; private set; }
        
        /// <summary>
        /// Обработчик для телеграм бота
        /// </summary>
        public Handler Handler;

        /// <summary>
        /// Токен 
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// Настройки телеграм бота
        /// </summary>
        private ReceiverOptions _options;

        /// <summary>
        /// Сигнатура для записи ошибок
        /// </summary>
        /// <param name="ex">Эксекшен</param>
        /// <param name="id">Идентиификатор пользователе</param>
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
        }

        public PRBot(Action<TelegramConfig> configOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
        }

        public PRBot(Action<TelegramConfig> configOptions, ReceiverOptions receiverOptions, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
        }

        public PRBot(Action<TelegramConfig> configOptions, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            configOptions.Invoke(Config);
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
        }

        public PRBot(TelegramConfig config, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
        }

        public PRBot(TelegramConfig config, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
        }

        public PRBot(TelegramConfig config, ReceiverOptions receiverOptions, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
        }

        public PRBot(TelegramConfig config, ReceiverOptions receiverOptions, CancellationTokenSource cancellationToken, IServiceProvider serviceProvider = null)
        {
            Config = config;
            ReceiverOptions = receiverOptions;
            CancellationTokenSource = cancellationToken;
            _serviceProvider = serviceProvider;
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
                Handler = new Handler(this, Config, _serviceProvider);
                _cts = CancellationTokenSource;
                _options = ReceiverOptions;

                if(Config.ClearUpdatesOnStart)
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
    }
}