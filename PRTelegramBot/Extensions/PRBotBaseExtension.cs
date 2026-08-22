using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for PRBotBase.
    /// </summary>
    public static class PRBotBaseExtension
    {
        #region Methods

        /// <summary>
        /// Checks whether the user is an administrator of the bot.
        /// </summary>
        /// <param name="botClient">Bot.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if the user is an administrator; False otherwise.</returns>
        public static async Task<bool> IsAdmin(this PRBotBase botClient, long userId)
        {
            return await botClient.GetAdminManager().HasUser(userId);
        }

        /// <summary>
        /// Checks whether the user is present in the bot's white list.
        /// </summary>
        /// <param name="botClient">Bot.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if present in the list; False if not.</returns>
        public static async Task<bool> InWhiteList(this PRBotBase botClient, long userId)
        {
            return await botClient.GetWhiteListManager().HasUser(userId);
        }

        /// <summary>
        /// Returns the list of the bot's administrators.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <returns>List of identifiers.</returns>
        public static async Task<List<long>> GetAdminsIds(this PRBotBase botClient)
        {
            return await botClient.GetAdminManager().GetUsersIds();
        }

        /// <summary>
        /// Returns the white list of users.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <returns>List of identifiers.</returns>
        public static async Task<List<long>> GetWhiteListIds(this PRBotBase botClient)
        {
            return await botClient.GetWhiteListManager().GetUsersIds();
        }

        /// <summary>
        /// Creates the bot context.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <returns>Bot context.</returns>
        public static IBotContext CreateContext(this PRBotBase botClient)
        {
            return new BotContext(botClient);
        }

        #endregion
    }
}
