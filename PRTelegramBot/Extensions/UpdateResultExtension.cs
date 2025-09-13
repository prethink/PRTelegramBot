using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для обработки результатов update.
    /// </summary>
    internal static class UpdateResultExtension
    {
        /// <summary>
        /// Продолжить обработку.
        /// </summary>
        /// <param name="result">Результат update.</param>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - продолжить, False - нет.</returns>
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
