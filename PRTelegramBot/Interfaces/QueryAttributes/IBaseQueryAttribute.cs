using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    public interface IBaseQueryAttribute : IBotIdentificatorAttribute
    {
        /// <summary>
        /// Сравнение команды.
        /// </summary>
        public CommandComparison CommandComparison { get; }
    }
}
