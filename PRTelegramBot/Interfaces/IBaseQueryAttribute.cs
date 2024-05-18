using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    public interface IBaseQueryAttribute
    {
        /// <summary>
        /// Идентификатор бота.
        /// </summary>
        public long BotId { get; }

        /// <summary>
        /// 
        /// </summary>
        public CommandComparison CommandComparison { get; }
    }
}
