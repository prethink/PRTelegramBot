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
        /// Попытаться получить кастомное значени.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <returns>True если значение есть.</returns>
        public bool TryGetCustomValue<T>(out T? value);
    }
}
