using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for handling update results.
    /// </summary>
    internal static class UpdateResultExtension
    {
        /// <summary>
        /// Continue processing.
        /// </summary>
        /// <param name="result">The update result.</param>
        /// <param name="context">Bot context.</param>
        /// <returns>True to continue; False otherwise.</returns>
        public static bool IsContinueHandle(this UpdateResult result, IBotContext context)
        {
            if (result == UpdateResult.Error)
            {
                context.Current.Events.OnErrorCommandInvoke(context.CreateBotEventArgs());
                return false;
            }

            if (result == UpdateResult.Handled || result == UpdateResult.Stop)
                return false;

            return true;
        }
    }
}
