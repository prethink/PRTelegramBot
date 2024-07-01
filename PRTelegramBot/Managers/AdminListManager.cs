using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Managers
{
    /// <summary>
    /// Менеджер управления администраторами.
    /// </summary>
    public class AdminListManager : IUserManager
    {
        #region Поля и свойства

        /// <summary>
        /// Пользователи.
        /// </summary>
        private List<long> users = new List<long>();

        #endregion

        #region IUserManager

        public long Count => users.Count;

        public async Task<bool> AddUser(long userId)
        {
            users.Add(userId);
            return true;
        }

        public async Task<bool> AddUsers(params long[] userIds)
        {
            users.AddRange(userIds);
            return true;
        }

        public async Task<List<long>> GetUsersIds()
        {
            return users.ToList();
        }

        public async Task<bool> HasUser(long userId)
        {
            return users.Contains(userId);
        }

        public async Task<bool> Reload()
        {
            return true;
        }

        public async Task<bool> RemoveUser(long userId)
        {
            return users.Remove(userId);
        }

        #endregion
    }
}
