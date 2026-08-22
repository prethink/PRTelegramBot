using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Bot context.
    /// </summary>
    public class BotContext : IBotContext
    {
        #region Fields and properties

        /// <summary>
        /// Store for custom data.
        /// </summary>
        protected object customData { get; set; }

        #endregion

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

        #endregion

        #region Methods

        /// <summary>
        /// Creates a stub context.
        /// </summary>
        /// <returns>A stub.</returns>
        public static IBotContext CreateEmpty()
        {
            return new BotContext(new PRBotDummy());
        }

        /// <inheritdoc />
        public bool TryGetCustomValue<T>(out T? value)
        {
            if (customData is T t)
            {
                value = t;
                return true;
            }

            value = default;
            return false;
        }

        /// <inheritdoc />
        public void SetCustomData(object data)
        {
            customData = data;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <param name="update">Telegram update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public BotContext(PRBotBase bot, Update update, CancellationToken cancellationToken)
        {
            Current = bot;
            Update = update;
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        public BotContext(PRBotBase bot) : this(bot, new Update(), CancellationToken.None) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <param name="update">Telegram update.</param>
        public BotContext(PRBotBase bot, Update update) : this(bot, update, CancellationToken.None) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public BotContext(PRBotBase bot, CancellationToken cancellationToken) : this(bot, new Update(), cancellationToken) { }

        #endregion
    }
}
