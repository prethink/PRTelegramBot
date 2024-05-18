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
        #region Поля и свойства

        /// <summary>
        /// Как сравнивать строку.
        /// </summary>
        public StringComparison StringComparison { get; protected set; }

        #endregion

        #region Конструкторы

        public StringQueryAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison) 
            : base(botId, commandComparison) 
        {
            this.StringComparison = stringComparison;
        }

        #endregion
    }
}
