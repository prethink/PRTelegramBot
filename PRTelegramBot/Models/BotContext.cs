using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Контекст бота.
    /// </summary>
    public class BotContext : IBotContext
    {
        #region IIBotContext

        /// <inheritdoc />
        public IEnumerable<PRBotBase> Bots => BotCollection.Instance.GetBots();

        /// <inheritdoc />
        public PRBotBase Current { get; }

        /// <inheritdoc />
        public ITelegramBotClient BotClient => Current.BotClient;

        /// <inheritdoc />
        public Update Update { get; }

        /// <inheritdoc />
        public UpdateType CurrentUpdateType => Update.Type;

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; }

        /// <inheritdoc />
        public IPRSerializator Serializator => Current.Serializator;

        #endregion

        #region Методы

        /// <summary>
        /// Создать заглушку контекста.
        /// </summary>
        /// <returns>Заглушка.</returns>
        public static IBotContext CreateEmpty()
        {
            return new BotContext(new PRBotDummy());
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        /// <param name="update">Обновление telegram.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public BotContext(PRBotBase bot, Update update, CancellationToken cancellationToken)
        {
            Current = bot;
            Update = update;
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        public BotContext(PRBotBase bot) : this(bot, new Update(), CancellationToken.None) {}

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        /// <param name="update">Обновление telegram.</param>
        public BotContext(PRBotBase bot, Update update) : this(bot, update, CancellationToken.None) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public BotContext(PRBotBase bot, CancellationToken cancellationToken) : this(bot, new Update(), cancellationToken) { }

        #endregion
    }
}
