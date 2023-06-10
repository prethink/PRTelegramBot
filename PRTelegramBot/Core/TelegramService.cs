using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Polling;
using PRTelegramBot.Configs;

namespace PRTelegramBot.Core
{
    public class TelegramService
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
        /// Экземпляр для singleton
        /// </summary>
        public static TelegramService Instance { get; private set; }

        /// <summary>
        /// Токен бота
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; private set; }

        /// <summary>
        /// Приватный конструктор
        /// </summary>
        /// <param name="token">Токен</param>
        private TelegramService(string token)
        {
            Token = token;
        }

        /// <summary>
        /// Singleton получение экземпляров
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Эксепшн если нет токен</exception>
        public static TelegramService GetInstance()
        {
            if (Instance == null)
            {
                var telegramConfig = ConfigApp.GetSettingsTelegram<TelegramConfig>();
                if (string.IsNullOrEmpty(telegramConfig.Token))
                {
                    throw new Exception($"Пустой {nameof(telegramConfig.Token)} при создание объекта {typeof(TelegramService)}\nПроверьте файл ../Configs/telegram.json");
                }
                Instance = new TelegramService(telegramConfig.Token);
            }

            return Instance;
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        public async Task Start()
        {
            try
            {
                botClient = new TelegramBotClient(Token);
                Handler = new Handler(botClient);
                _cts = new CancellationTokenSource();
                _options = new ReceiverOptions { AllowedUpdates = { } };

                bool isClearUpdate = ConfigApp.GetSettingsTelegram<TelegramConfig>().ClearUpdatesOnStart;

                if(isClearUpdate)
                {
                    await ClearUpdates();
                }


                botClient.StartReceiving(
                            Handler.HandleUpdateAsync,
                            Handler.HandleErrorAsync,
                            _options,
                            cancellationToken: _cts.Token);


                var client = await botClient.GetMeAsync();
                BotName = client.Username;
                GetInstance().InvokeCommonLog($"Бот {client.Username} запущен.", TelegramEvents.Initialization, ConsoleColor.Yellow);

                IsWork = true;
            }
            catch (Exception ex)
            {
                IsWork = false;
                GetInstance().InvokeErrorLog(ex);
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
                GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Смена токена
        /// </summary>
        /// <param name="token">Токен</param>
        public async Task ChangeToken(string token)
        {
            await Stop();
            Token = token;
            await Start();
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