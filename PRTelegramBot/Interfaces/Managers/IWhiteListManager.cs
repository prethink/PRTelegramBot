using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the user white list manager.
    /// </summary>
    public interface IWhiteListManager : IUserManager
    {
        /// <summary>
        /// Settings that control how the white list works.
        /// </summary>
        public WhiteListSettings Settings { get; }

        /// <summary>
        /// Sets the white list settings.
        /// </summary>
        /// <param name="settings">Settings.</param>
        public void SetSettings(WhiteListSettings settings);
    }
}
