using PRTelegramBot.Interfaces;
using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Lets the user execute commands step by step
    /// </summary>
    public static class StepExtension
    {
        #region Fields and properties

        /// <summary>
        /// The list of steps for the user.
        /// </summary>
        static ConcurrentDictionary<string, IExecuteStep> step = new();

        #endregion

        #region Methods

        /// <summary>
        /// Registers the next step.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <param name="command">The next command that has to be executed.</param>
        public static void RegisterStepHandler(this Update update, IExecuteStep command)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            update.ClearStepUserHandler();
            step.AddOrUpdate(userKey, command, (_, existingData) => command);
        }

        /// <summary>
        /// Gets the user's handler, or null.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>the handler, or null.</returns>
        public static TExecuteStep? GetStepHandler<TExecuteStep>(this Update update) where TExecuteStep : IExecuteStep
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return step.TryGetValue(userKey, out var data) && data is TExecuteStep stepHandler
                ? stepHandler
                : default(TExecuteStep);
        }

        /// <summary>
        /// Gets the current step handler.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>The handler, or null.</returns>
        public static IExecuteStep? GetStepHandler(this Update update)
        {
            return GetStepHandler<IExecuteStep>(update);
        }

        /// <summary>
        /// Clears the user's steps.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        public static void ClearStepUserHandler(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (update.HasStepHandler())
                step.Remove(userKey, out _);
        }

        /// <summary>
        /// Checks whether the user has a step registered.
        /// </summary>
        /// <param name="update">The update received from Telegram</param>
        /// <returns>True if a handler exists; False if it does not.</returns>
        public static bool HasStepHandler(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return step.ContainsKey(userKey);
        }

        #endregion
    }
}
