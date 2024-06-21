namespace PRTelegramBot.Models
{
    /// <summary>
    /// Маппинг бота и пользователя.
    /// </summary>
    internal sealed class UserBotMapping
    {
        #region Поля и свойства

        /// <summary>
        /// Идентификатор бота.
        /// </summary>
        private long botId;

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        private long userId;

        /// <summary>
        /// Получить уникальное сочетание ключа для бота и пользователя.
        /// </summary>
        public string GetKey => $"{botId}-{userId}";

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="UserId">Идентификатор пользователя.</param>
        public UserBotMapping(long botId, long UserId)
        {
            this.botId = botId;
            this.userId = UserId;
        }

        #endregion
    }
}
