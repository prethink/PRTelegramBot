using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// A stub bot instance.
    /// </summary>
    public class PRBotDummy : PRBotBase
    {

        #region Base class

        /// <inheritdoc />
        public override DataRetrievalMethod DataRetrieval => DataRetrievalMethod.Dummy;

        /// <inheritdoc />
        protected override bool addBotToCollection => false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="optionsBuilder">Builder.</param>
        /// <param name="options">Options.</param>
        public PRBotDummy(Action<TelegramOptions>? optionsBuilder, TelegramOptions? options) : base(optionsBuilder, options)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PRBotDummy()
            : this(opt =>
            {
                opt.Client = new TelegramBotClient("35425:token");
                opt.Token = "35425:token";
                opt.BotId = 9876;
            }, null)
        { }

        #endregion
    }
}
