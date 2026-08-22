using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Base interface for command handling.
    /// </summary>
    /// <typeparam name="T">The update type to check.</typeparam>
    public interface ICommandHandlerBase<T>
    {
        /// <summary>
        /// Handling.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="updateType">The concrete update class.</param>
        /// <returns>The update result.</returns>
        public Task<UpdateResult> Handle(IBotContext context, T updateType);
    }
}
