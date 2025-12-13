using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;

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
            return await botClient.GetAdminManager().HasUser(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static async Task<bool> InWhiteList(this PRBotBase botClient, long userId)
        {
            return await botClient.GetWhiteListManager().HasUser(userId);
        }

        /// <summary>
        /// Возвращает список администраторов бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetAdminsIds(this PRBotBase botClient)
        {
            return await botClient.GetAdminManager().GetUsersIds();
        }

        /// <summary>
        /// Возвращает белый список пользователей.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetWhiteListIds(this PRBotBase botClient)
        {
            return await botClient.GetWhiteListManager().GetUsersIds();
        }

        /// <summary>
        /// Создать контекст бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Контекст бота.</returns>
        public static IBotContext CreateContext(this PRBotBase botClient)
        {
            return new BotContext(botClient);
        }

        #endregion
    }
}
