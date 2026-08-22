using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Class that stores a command type together with its checks.
    /// </summary>
    public class InternalChecker
    {
        #region Fields and properties

        /// <summary>
        /// Which command type the check applies to.
        /// </summary>
        public List<CommandType> CommandTypes { get; private set; } = new();

        #endregion

        #region IInternalCheck

        /// <summary>
        /// The class that will perform the check.
        /// </summary>
        public IInternalCheck Checker { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandType">Command type.</param>
        /// <param name="checker">Checker.</param>
        public InternalChecker(CommandType commandType, IInternalCheck checker)
        {
            this.CommandTypes.Add(commandType);
            this.Checker = checker;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandTypes">Command types.</param>
        /// <param name="checker">Checker.</param>
        public InternalChecker(List<CommandType> commandTypes, IInternalCheck checker)
        {
            this.CommandTypes.AddRange(commandTypes);
            this.Checker = checker;
        }

        #endregion
    }
}
