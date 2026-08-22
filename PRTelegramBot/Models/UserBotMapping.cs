namespace PRTelegramBot.Models
{
    /// <summary>
    /// Mapping between a bot and a user.
    /// </summary>
    internal sealed class UserBotMapping
    {
        #region Fields and properties

        /// <summary>
        /// Bot identifier.
        /// </summary>
        private long botId;

        /// <summary>
        /// User identifier.
        /// </summary>
        private long userId;

        /// <summary>
        /// Gets the unique key combination for the bot and the user.
        /// </summary>
        public string GetKey => $"{botId}-{userId}";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="UserId">User identifier.</param>
        public UserBotMapping(long botId, long UserId)
        {
            this.botId = botId;
            this.userId = UserId;
        }

        #endregion
    }
}
