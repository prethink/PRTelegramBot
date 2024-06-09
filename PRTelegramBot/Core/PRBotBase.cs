using PRTelegramBot.Configs;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    public abstract class PRBotBase
    {
        #region Поля и свойства

        /// <summary>
        /// Имя бота.
        /// </summary>
        public string BotName { get; protected set; }

        /// <summary>
        /// Клиент для telegram бота.
        /// </summary>
        public ITelegramBotClient botClient { get; protected set; }

        /// <summary>
        /// Идетификатор бота в telegram.
        /// </summary>
        public long? TelegramId { get { return botClient.BotId; } }

        /// <summary>
        /// Обработчик для telegram бота
        /// </summary>
        public Handler Handler { get; protected set; }

        /// <summary>
        /// Работает бот или нет
        /// </summary>
        public bool IsWork { get; protected set; }

        /// <summary>
        /// Параметры бота.
        /// </summary>
        public TelegramOptions Options { get; protected set; }

        /// <summary>
        /// Идентификатор бота.
        /// </summary>
        public long BotId => Options.BotId;

        /// <summary>
        /// События.
        /// </summary>
        public TEvents Events { get; protected set; }

        /// <summary>
        /// Регистрация команд.
        /// </summary>
        public RegisterCommands Register { get; protected set; }

        /// <summary>
        /// Тип получения обновления.
        /// </summary>
        public abstract DataRetrievalMethod DataRetrieval { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Очистка очереди команд перед запуском.
        /// </summary>
        protected async Task ClearUpdates()
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
                this.Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Запустить бота.
        /// </summary>
        public abstract Task Start();


        /// <summary>
        /// Остановка бота
        /// </summary>
        public abstract Task Stop();

        #endregion

        #region Конструкторы

        protected PRBotBase()
        {
            Options = new TelegramOptions();
        }

        #endregion
    }
}
