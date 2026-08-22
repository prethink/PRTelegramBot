using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Base attribute for handling commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public abstract class BaseQueryAttribute<T> 
        : Attribute, IBaseQueryAttribute, ICommandStore<T>
    {
        #region Fields and properties

        /// <summary>
        /// Commands bound to the methods.
        /// </summary>
        protected List<T> commands = new List<T>();

        #endregion

        #region ICommandStore

        /// <inheritdoc />
        public IEnumerable<T> Commands => commands.ToList();

        #endregion

        #region IBaseQueryAttribute

        /// <inheritdoc />
        public List<long> BotIds { get; set; } = new();

        /// <inheritdoc />
        public CommandComparison CommandComparison { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">Command comparison.</param>
        public BaseQueryAttribute(long[] botIds, CommandComparison commandComparison)
        {
            BotIds.AddRange(botIds);

            this.CommandComparison = commandComparison;
        }

        #endregion
    }
}
