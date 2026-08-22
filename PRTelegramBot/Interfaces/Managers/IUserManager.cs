namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the user management manager.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Number of users.
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// Reloads the users.
        /// </summary>
        /// <returns>True if the reload succeeded; False if it did not.</returns>
        public Task<bool> Reload();

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        /// <returns>True if initialization succeeded.</returns>
        public Task<bool> Initialize();

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="userId">Identifier.</param>
        /// <returns>True on success; False on failure.</returns>
        public Task<bool> AddUser(long userId);

        /// <summary>
        /// Adds users.
        /// </summary>
        /// <param name="userIds">User identifiers.</param>
        /// <returns>True on success; False on failure.</returns>
        public Task<bool> AddUsers(params long[] userIds);

        /// <summary>
        /// Gets the user identifiers.
        /// </summary>
        /// <returns>Identifiers.</returns>
        public Task<List<long>> GetUsersIds();

        /// <summary>
        /// Removes a user from the list.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>True on success; False on failure.</returns>
        public Task<bool> RemoveUser(long userId);

        /// <summary>
        /// Checks whether the user is in the list.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if present; False if not.</returns>
        public Task<bool> HasUser(long userId);
    }
}
