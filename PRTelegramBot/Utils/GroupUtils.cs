using PRTelegramBot.Interfaces;
using Telegram.Bot;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utilities for working with groups.
    /// </summary>
    public static class GroupUtils
    {
        #region Methods

        /// <summary>
        /// Checks whether the user is a member of the group.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="groupId">Group identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if present; otherwise false.</returns>
        public static async Task<bool> IsGroupMember(IBotContext context, long groupId, long userId)
        {
            var data = await context.BotClient.GetChatMember(groupId, userId);
            return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Member ||
                    data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator ||
                    data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Creator;
        }

        /// <summary>
        /// Checks whether the user is an administrator of the group.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="groupId">Group identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if the user is an administrator; otherwise false.</returns>
        public static async Task<bool> IsGroupAdmin(IBotContext context, long groupId, long userId)
        {
            var data = await context.BotClient.GetChatMember(groupId, userId);
            return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator;
        }

        /// <summary>
        /// Checks whether the user is the creator of the group.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="groupId">Group identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if the user is the creator; otherwise false.</returns>
        public static async Task<bool> IsGroupCreator(IBotContext context, long groupId, long userId)
        {
            var data = await context.BotClient.GetChatMember(groupId, userId);
            return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Creator;
        }

        #endregion
    }
}
