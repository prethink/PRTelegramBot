using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут позволяет игнорировать настройки белого списка.
    /// </summary>
    public class WhiteListAnonymousAttribute
        : Attribute, IBotIdentificatorAttribute
    {
        #region IBaseQueryAttribute

        public long BotId { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        public WhiteListAnonymousAttribute(long botId)
        {
            this.BotId = botId;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WhiteListAnonymousAttribute() : this(0) { }


        #endregion
    }
}
