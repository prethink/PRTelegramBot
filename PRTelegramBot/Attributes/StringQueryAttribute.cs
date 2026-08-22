using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Common attribute for commands of type string.
    /// </summary>
    public abstract class StringQueryAttribute 
        : BaseQueryAttribute<string> , IStringQueryAttribute
    {
        #region IStringQueryAttribute

        /// <summary>
        /// How to compare the string.
        /// </summary>
        public StringComparison StringComparison { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        public StringQueryAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison)
            : base(botIds, commandComparison)
        {
            this.StringComparison = stringComparison;
        }

        #endregion
    }
}
