using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

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
        /// <param name="bot">Бот.</param>
        /// <param name="update">Update.</param>
        /// <returns>True - продолжить, False - нет.</returns>
        public static bool IsContinueHandle(this UpdateResult result, PRBotBase bot, Update update)
        {
            if (result == UpdateResult.Error)
            {
                bot.Events.OnErrorCommandInvoke(new BotEventArgs(bot, update));
                return false;
            }

            if (result == UpdateResult.Handled || result == UpdateResult.Stop)
                return false;

            return true;
        }
    }
}
