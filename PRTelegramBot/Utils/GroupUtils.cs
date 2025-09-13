using PRTelegramBot.Interfaces;
using Telegram.Bot;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Утилиты для работы с группами.
    /// </summary>
    public static class GroupUtils
    {
        #region Методы

        /// <summary>
        /// Проверяет находится ли пользователь в группе.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть иначе false.</returns>
        public static async Task<bool> IsGroupMember(IBotContext context, long groupId, long userId)
        {
            var data = await context.BotClient.GetChatMember(groupId, userId);
            return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Member ||
                    data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator ||
                    data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Creator;
        }

        /// <summary>
        /// Проверяет является ли администратором группы.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - администратор иначе false.</returns>
        public static async Task<bool> IsGroupAdmin(IBotContext context, long groupId, long userId)
        {
            var data = await context.BotClient.GetChatMember(groupId, userId);
            return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator;
        }

        /// <summary>
        /// Проверяет является ли создателем группы.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - создатель иначе false.</returns>
        public static async Task<bool> IsGroupCreator(IBotContext context, long groupId, long userId)
        {
            var data = await context.BotClient.GetChatMember(groupId, userId);
            return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Creator;
        }

        #endregion
    }
}
