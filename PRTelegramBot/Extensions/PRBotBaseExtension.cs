using PRTelegramBot.Core;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для PRBotBase.
    /// </summary>
    public static class PRBotBaseExtension
    {
        #region Методы

        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static bool IsAdmin(this PRBotBase botClient, long userId)
        {
            return botClient.Options.Admins.Contains(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static bool InWhiteList(this PRBotBase botClient, long userId)
        {
            return botClient.Options.WhiteListUsers.Contains(userId);
        }

        /// <summary>
        /// Возращает список администраторов бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static List<long> GetAdminsIds(this PRBotBase botClient)
        {
            return botClient.Options.Admins.ToList();
        }

        /// <summary>
        /// Возращает белый список пользователей.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static List<long> GetWhiteListIds(this PRBotBase botClient)
        {
            return botClient.Options.WhiteListUsers.ToList();
        }

        #endregion
    }
}
