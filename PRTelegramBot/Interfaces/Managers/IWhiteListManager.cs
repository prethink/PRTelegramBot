using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс белого менеджера белого списка пользователей.
    /// </summary>
    public interface IWhiteListManager : IUserManager
    {
        /// <summary>
        /// Настройки работы с белым списком.
        /// </summary>
        public WhiteListSettings Settings { get; }

        /// <summary>
        /// Установить настройки белого списка.
        /// </summary>
        /// <param name="settings">Настройки.</param>
        public void SetSettings(WhiteListSettings settings);
    }
}
