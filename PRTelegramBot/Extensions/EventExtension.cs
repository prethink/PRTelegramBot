using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for events.
    /// </summary>
    public static class EventExtension
    {
        /// <summary>
        /// Creates the base event arguments for the context.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Arguments.</returns>
        public static BotEventArgs CreateBotEventArgs(this IBotContext context)
        {
            return new BotEventArgs(context);
        }
    }
}