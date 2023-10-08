using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Polling;
using PRTelegramBot.Configs;

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

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; private set; }

        public TelegramConfig Config { get; init; } = new TelegramConfig();

        public PRBot(Action<TelegramConfig> configOptions)
        {
             configOptions.Invoke(Config);
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        public async Task Start()
        {
            try
            {
                botClient = new TelegramBotClient(Config.Token);
                Handler = new Handler(this, Config);
                _cts = new CancellationTokenSource();
                _options = new ReceiverOptions { AllowedUpdates = { } };

                if(Config.ClearUpdatesOnStart)
                {
                    await ClearUpdates();
                }


                botClient.StartReceiving(Handler , _options);


                var client = await botClient.GetMeAsync();
                BotName = client.Username;
                this.InvokeCommonLog($"Бот {client.Username} запущен.", TelegramEvents.Initialization, ConsoleColor.Yellow);

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