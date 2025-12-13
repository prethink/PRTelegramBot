using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Managers
{
    /// <summary>
    /// Менеджер управления белым списком.
    /// </summary>
    public class WhiteListManager : IWhiteListManager
    {
        #region Поля и свойства

        /// <summary>
        /// Пользователи.
        /// </summary>
        private List<long> users = new List<long>();

        #endregion

        #region IUserManager

        /// <inheritdoc />
        public long Count => users.Count;

        /// <inheritdoc />
        private WhiteListSettings settings = WhiteListSettings.OnPreUpdate;

        /// <inheritdoc />
        public WhiteListSettings Settings
        {
            get
            {
                return settings;
            }
        }

        /// <inheritdoc />
        public Task<bool> AddUser(long userId)
        {
            users.Add(userId);
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public Task<bool> AddUsers(params long[] userIds)
        {
            users.AddRange(userIds);
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public Task<List<long>> GetUsersIds()
        {
            return Task.FromResult(users.ToList());
        }

        /// <inheritdoc />
        public Task<bool> HasUser(long userId)
        {
            return Task.FromResult(users.Contains(userId));
        }

        /// <inheritdoc />
        public Task<bool> Reload()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public Task<bool> RemoveUser(long userId)
        {
            return Task.FromResult(users.Remove(userId));
        }

        /// <inheritdoc />
        public void SetSettings(WhiteListSettings whiteListSettings)
        {
            settings = whiteListSettings;
        }

        /// <inheritdoc />
        public Task<bool> Initialize()
        {
            return Task.FromResult(true);
        }

        #endregion
    }
}
