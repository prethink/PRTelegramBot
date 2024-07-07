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
        /// Добавить пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор.</param>
        /// <returns>True - удачно, False не удачно.</returns>
        public Task<bool> AddUser(long userId);

        /// <summary>
        /// Добавить пользователей.
        /// </summary>
        /// <param name="userIds">Идентификаторы пользователей.</param>
        /// <returns>True - удачно, False не удачно.</returns>
        public Task<bool> AddUsers(params long[] userIds);

        /// <summary>
        /// Получить идентификаторы пользователей.
        /// </summary>
        /// <returns>Идентификаторы.</returns>
        public Task<List<long>> GetUsersIds();

        /// <summary>
        /// Удалить пользователя из списка.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - удачно, False не удачно.</returns>
        public Task<bool> RemoveUser(long userId);

        /// <summary>
        /// Проверка есть ли пользователь в списке.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть, False - нет.</returns>
        public Task<bool> HasUser(long userId);
    }
}
