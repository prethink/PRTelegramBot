using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс контекста бота.
    /// </summary>
    public interface IBotContext
    {
        /// <summary>
        /// Все экземпляры ботов.
        /// </summary>
        public IEnumerable<PRBotBase> Bots { get; }

        /// <summary>
        /// Экземпляр бота.
        /// </summary>
        public PRBotBase Current { get; }

        /// <summary>
        /// Клиент Telegram.Bot.
        /// </summary>
        public ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Обновление.
        /// </summary>
        public Update Update { get; }

        /// <summary>
        /// Текущий тип обновления.
        /// </summary>
        public UpdateType CurrentUpdateType { get; }

        /// <summary>
        /// Токен отмены.
        /// </summary>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// Попытаться кастомное значение пользовательское значение.
        /// </summary>
        /// <typeparam name="T">Тип/</typeparam>
        /// <returns>Значение или null</returns>
        public bool TryGetCustomValue<T>(out T? value);

        /// <summary>
        /// Установить кастомные данные.
        /// </summary>
        /// <param name="data">Данные.</param>
        public void SetCustomData(object data); 
    }
}
