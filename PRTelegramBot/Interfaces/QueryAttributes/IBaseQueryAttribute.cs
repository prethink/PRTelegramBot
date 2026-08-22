using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Contract of a command attribute: the commands it is bound to and how they are compared.
    /// </summary>
    public interface IBaseQueryAttribute : IBotIdentificatorAttribute
    {
        /// <summary>
        /// Command comparison.
        /// </summary>
        public CommandComparison CommandComparison { get; }
    }
}
