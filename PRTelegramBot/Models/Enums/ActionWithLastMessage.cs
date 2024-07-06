namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Действие с последним сообщения для inline кнопок.
    /// </summary>
    public enum ActionWithLastMessage
    {
        /// <summary>
        /// Ничего не делать.
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// Редактировать.
        /// </summary>
        Edit,
        /// <summary>
        /// Удалить.
        /// </summary>
        Delete
    }
}
