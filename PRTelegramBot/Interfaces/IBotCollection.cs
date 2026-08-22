using PRTelegramBot.Core;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface for working with the bot collection.
    /// Lets you look bots up by various criteria and manage the collection.
    /// </summary>
    public interface IBotCollection
    {
        /// <summary>
        /// Number of bots in the collection.
        /// </summary>
        long BotCount { get; }

        /// <summary>
        /// Gets a bot by its Telegram Id.
        /// </summary>
        /// <param name="telegramId">Telegram identifier.</param>
        /// <returns>The bot instance, or null if it was not found.</returns>
        PRBotBase? GetBotByTelegramIdOrNull(long? telegramId);

        /// <summary>
        /// Gets a bot by its internal Id.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <returns>The bot instance, or null if it was not found.</returns>
        PRBotBase? GetBotOrNull(long botId);

        /// <summary>
        /// Gets a bot matching a filter condition.
        /// </summary>
        /// <param name="predicate">Filter function.</param>
        /// <returns>The bot instance, or null.</returns>
        PRBotBase? GetBotOrNull(Func<PRBotBase, bool> predicate);

        /// <summary>
        /// Gets a bot by its name or login.
        /// </summary>
        /// <param name="botName">Bot name / login.</param>
        /// <returns>The bot instance, or null.</returns>
        PRBotBase? GetBotOrNull(string botName);

        /// <summary>
        /// Gets all bots.
        /// </summary>
        /// <returns>The list of all bots.</returns>
        IEnumerable<PRBotBase> GetBots();

        /// <summary>
        /// Gets all bots matching a filter condition.
        /// </summary>
        /// <param name="predicate">Filter function.</param>
        /// <returns>The list of bots that match the condition.</returns>
        IEnumerable<PRBotBase> GetBots(Func<PRBotBase, bool> predicate);

        /// <summary>
        /// Adds a new bot to the collection.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        void AddBot(PRBotBase bot);

        /// <summary>
        /// Removes a bot from the collection.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        void RemoveBot(PRBotBase bot);

        /// <summary>
        /// Clears the entire bot collection.
        /// </summary>
        void ClearBots();

        /// <summary>
        /// Gets the next unique identifier for a new bot.
        /// </summary>
        /// <returns>The next Id.</returns>
        long GetNextId();
    }
}
