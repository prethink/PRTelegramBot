using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для событий.
    /// </summary>
    public static class EventExtension
    {
        /// <summary>
        /// Создать базовые аргументы событий для контекста.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Аргументы.</returns>
        public static BotEventArgs CreateBotEventArgs(this IBotContext context)
        {
            return new BotEventArgs(context);
        }
    }
}