using PRTelegramBot.Interfaces;
using Telegram.Bot;

namespace PRTelegramBot.Services.Messages
{
    /// <summary>
    /// Shows notifications and alerts in response to a callbackQuery.
    /// </summary>
    public class MessageNotification
    {
        #region Methods

        /// <summary>
        /// Shows a notification to the user.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="callbackQueryId">Callback identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="showAlert">Whether to show an alert.</param>
        /// <param name="url">.</param>
        /// <param name="cacheTime">.</param>
        /// <returns>Task</returns>
        public static async Task NotifyFromCallBack(
            IBotContext context,
            string callbackQueryId,
            string text,
            bool showAlert = true,
            string? url = null,
            int? cacheTime = null)
        {
            await context.BotClient.AnswerCallbackQuery(callbackQueryId, text, showAlert, url, cacheTime, context.CancellationToken);
        }

        #endregion
    }
}
