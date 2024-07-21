using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Общий атрибут для команд с типом string.
    /// </summary>
    public abstract class StringQueryAttribute 
        : BaseQueryAttribute<string> , IStringQueryAttribute
    {
        #region IStringQueryAttribute

        /// <summary>
        /// Как сравнивать строку.
        /// </summary>
        public StringComparison StringComparison { get; protected set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Как сравнивать команду.</param>
        /// <param name="stringComparison">Как сравнивать строку.</param>
        public StringQueryAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison)
            : base(botIds, commandComparison)
        {
            this.StringComparison = stringComparison;
        }

        #endregion
    }
}
