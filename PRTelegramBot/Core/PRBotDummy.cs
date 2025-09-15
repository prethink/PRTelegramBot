using PRTelegramBot.Configs;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Экземпляр заглушки бота.
    /// </summary>
    public class PRBotDummy : PRBotBase
    {

        #region Базовый класс

        /// <inheritdoc />
        public override DataRetrievalMethod DataRetrieval => DataRetrievalMethod.Dummy;

        /// <inheritdoc />
        public override Task StopAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="optionsBuilder">Билдер.</param>
        /// <param name="options">Опции.</param>
        public PRBotDummy(Action<TelegramOptions>? optionsBuilder, TelegramOptions? options) : base(optionsBuilder, options)
        { }

        /// <summary>
        /// Конструктор.
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
