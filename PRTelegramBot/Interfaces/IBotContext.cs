using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the bot context.
    /// </summary>
    public interface IBotContext
    {
        /// <summary>
        /// All bot instances.
        /// </summary>
        public IEnumerable<PRBotBase> Bots { get; }

        /// <summary>
        /// Bot instance.
        /// </summary>
        public PRBotBase Current { get; }

        /// <summary>
        /// The Telegram.Bot client.
        /// </summary>
        public ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Update.
        /// </summary>
        public Update Update { get; }

        /// <summary>
        /// The current update type.
        /// </summary>
        public UpdateType CurrentUpdateType { get; }

        /// <summary>
        /// Cancellation token.
        /// </summary>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// Tries to get a custom value.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns>True if the value is present.</returns>
        public bool TryGetCustomValue<T>(out T? value);
    }
}
