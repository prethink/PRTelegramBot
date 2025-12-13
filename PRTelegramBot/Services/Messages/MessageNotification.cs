using PRTelegramBot.Interfaces;
using Telegram.Bot;

namespace PRTelegramBot.Services.Messages
{
    public class MessageNotification
    {
        #region Методы

        /// <summary>
        /// Вывод уведомления пользователю.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="callbackQueryId">Идентификатор callback.</param>
        /// <param name="text">Текст.</param>
        /// <param name="showAlert">Показывать уведомление.</param>
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
