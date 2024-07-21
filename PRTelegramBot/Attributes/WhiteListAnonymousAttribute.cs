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

        public List<long> BotIds { get; set; } = new();

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        public WhiteListAnonymousAttribute(long botId)
        {
            this.BotIds.Add(botId);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы бота.</param>
        public WhiteListAnonymousAttribute(List<long> botIds)
        {
            this.BotIds.AddRange(botIds);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WhiteListAnonymousAttribute() : this(0) { }


        #endregion
    }
}
