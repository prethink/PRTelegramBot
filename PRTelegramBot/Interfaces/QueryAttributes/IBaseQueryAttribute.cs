using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    public interface IBaseQueryAttribute : IBotIdentificatorAttribute
    {
        /// <summary>
        /// Command comparison.
        /// </summary>
        public CommandComparison CommandComparison { get; }
    }
}
