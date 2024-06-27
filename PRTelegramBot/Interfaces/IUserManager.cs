namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс менеджера управления пользователем.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Количество пользователей.
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// Перезагрузить пользователей.
        /// </summary>
        /// <returns>True - удалсь выполнить перезагрузку, False - не удалось.</returns>
        public Task<bool> Reload();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> AddUser(long userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public Task<bool> AddUsers(params long[] userIds);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<long>> GetUsersIds();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> RemoveUser(long userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> HasUser(long userId);
    }
}
