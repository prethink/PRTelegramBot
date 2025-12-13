using PRTelegramBot.Interfaces.Managers;

namespace PRTelegramBot.Managers
{
    /// <summary>
    /// Менеджер управления администраторами.
    /// </summary>
    public class AdminListManager : IAdminManager
    {
        #region Поля и свойства

        /// <summary>
        /// Пользователи.
        /// </summary>
        private List<long> users = new List<long>();

        #endregion

        #region IAdminManager

        /// <inheritdoc />
        public long Count => users.Count;

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
        public Task<bool> Initialize()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public  Task<bool> Reload()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public Task<bool> RemoveUser(long userId)
        {
            return Task.FromResult(users.Remove(userId));
        }

        #endregion
    }
}
