using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    public static class BotData
    {
        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Обновление из telegram.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static bool IsAdmin(this ITelegramBotClient botClient, Update update)
        {
            return IsAdmin(botClient, update.GetChatId());
        }

        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static bool IsAdmin(this ITelegramBotClient botClient, long userId)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null && botData.Options.Admins.Contains(userId);
        }

        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static bool IsAdmin(this PRBot botClient, long userId)
        {
            return botClient.Options.Admins.Contains(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Обновление из telegram.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static bool InWhiteList(this ITelegramBotClient botClient, Update update)
        {
            return InWhiteList(botClient, update.GetChatId());
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static bool InWhiteList(this ITelegramBotClient botClient, long userId)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null && botData.Options.WhiteListUsers.Contains(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static bool InWhiteList(this PRBot botClient, long userId)
        {
            return botClient.Options.WhiteListUsers.Contains(userId);
        }

        /// <summary>
        /// Возращает список администраторов бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static List<long> GetAdminsIds(this ITelegramBotClient botClient)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null ? botData.Options.Admins : new List<long>();
        }

        /// <summary>
        /// Возращает список администраторов бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static List<long> GetAdminsIds(this PRBot botClient)
        {
            return botClient.Options.Admins;
        }

        /// <summary>
        /// Возращает белый список пользователей.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static List<long> GetWhiteList(this ITelegramBotClient botClient)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null ? botData.Options.Admins : new List<long>();
        }

        /// <summary>
        /// Возращает белый список пользователей.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static List<long> GetWhiteList(this PRBot botClient)
        {
            return botClient.Options.Admins;
        }

        /// <summary>
        /// Получить экземпляр класса бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Экземпляр класса или null.</returns>
        public static PRBot GetBotDataOrNull(this ITelegramBotClient botClient)
        {
            return BotCollection.Instance.GetBotByTelegramIdOrNull(botClient.BotId);
        }

        /// <summary>
        /// Логирование простых логов
        /// </summary>
        public static void InvokeCommonLog(this ITelegramBotClient botClient, string msg, Enum? typeEvent = null, ConsoleColor color = ConsoleColor.Blue)
        {
            var bot = GetBotDataOrNull(botClient);

            if (bot != null)
                bot?.InvokeCommonLog(msg, typeEvent, color);
        }

        /// <summary>
        /// Логирование ошибок
        /// </summary>
        public static void InvokeErrorLog(this ITelegramBotClient botClient, Exception ex, long? id = null)
        {
            var bot = GetBotDataOrNull(botClient);

            if (bot != null)
                bot?.InvokeErrorLog(ex, id);
        }
    }
}
