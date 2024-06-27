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
        public static async Task<bool> IsAdmin(this PRBotBase botClient, long userId)
        {
            return await botClient.Options.AdminManager.HasUser(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static async Task<bool> InWhiteList(this PRBotBase botClient, long userId)
        {
            return await botClient.Options.WhiteListManager.HasUser(userId);
        }

        /// <summary>
        /// Возращает список администраторов бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetAdminsIds(this PRBotBase botClient)
        {
            return await botClient.Options.AdminManager.GetUsersIds();
        }

        /// <summary>
        /// Возращает белый список пользователей.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetWhiteListIds(this PRBotBase botClient)
        {
            return await botClient.Options.WhiteListManager.GetUsersIds();
        }

        #endregion
    }
}
