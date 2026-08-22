using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for the bot context.
    /// </summary>
    public static class BotContextExtension
    {
        #region UpdateExtension

        /// <summary>
        /// Gets the chat identifier depending on the message type.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Chat identifier.</returns>
        /// <exception cref="NotImplementedException">Thrown when handling of the update is not implemented.</exception>
        public static long GetChatId(this IBotContext context)
        {
            return context.Update.GetChatId();
        }

        /// <summary>
        /// Gets the identifier as a class.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>The identifier as a class</returns>
        public static ChatId GetChatIdClass(this IBotContext context)
        {
            return context.Update.GetChatIdClass();
        }

        /// <summary>
        /// Tries to get the chat identifier.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <returns>True if it was retrieved; false otherwise.</returns>
        public static bool TryGetChatId(this IBotContext context, out long chatId)
        {
            return context.Update.TryGetChatId(out chatId);
        }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Message identifier.</returns>
        /// <exception cref="NotImplementedException">Thrown when handling of the update is not implemented.</exception>
        public static int GetMessageId(this IBotContext context)
        {
            return context.Update.GetMessageId();
        }

        /// <summary>
        /// Whether the identifier belongs to a private user chat.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>True for yes; False for no.</returns>
        public static bool IsUserChatId(this IBotContext context)
        {
            return context.Update.IsUserChatId();
        }

        /// <summary>
        /// Information about the user.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Information about the user.</returns>
        public static string GetInfoUser(this IBotContext context)
        {
            return context.Update.GetInfoUser();
        }

        /// <summary>
        /// Gets the user identifier from the Telegram update.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>The user identifier (UserId).</returns>
        public static long GetUserId(this IBotContext context)
        {
            return context.Update.GetUserId();
        }

        #endregion

        #region CacheExtension

        /// <summary>
        /// Creates a cache for the user.
        /// </summary>
        /// <typeparam name="TCache">Cache type.</typeparam>
        /// <param name="context">Bot context.</param>
        /// <returns>Cache.</returns>
        public static TCache CreateCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache
        {
            return context.Update.CreateCacheData<TCache>();
        }

        /// <summary>
        /// Gets the existing cache, or creates a new one.
        /// </summary>
        /// <typeparam name="TCache">Cache type.</typeparam>
        /// <param name="context">Bot context.</param>
        /// <returns>Cache.</returns>
        /// <remarks>If the cache type differs from the existing one, a cache of the new type is created.</remarks>
        public static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache
        {
            return context.Update.GetOrCreate<TCache>();
        }

        /// <summary>
        /// Gets the user's cache.
        /// </summary>
        /// <typeparam name="TCache">Cache type.</typeparam>
        /// <param name="context">Bot context.</param>
        /// <returns>Cache.</returns>
        public static TCache GetCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache
        {
            return context.Update.GetCacheData<TCache>();
        }

        /// <summary>
        /// Clears the user's cache.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public static void ClearCacheData(this IBotContext context)
        {
            context.Update.ClearCacheData();
        }

        /// <summary>
        /// Checks whether cached data exists for the user.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>True if a cache exists; False if it does not.</returns>
        public static bool HasCacheData(this IBotContext context)
        {
            return context.Update.HasCacheData();
        }

        /// <summary>
        /// Removes the user's cache from the dictionary entirely.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public static void RemoveCacheData(this IBotContext context)
        {
            context.Update.RemoveCacheData();
        }

        #endregion

        #region StepExtension

        /// <summary>
        /// Registers the next step.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="command">The next command that has to be executed.</param>
        public static void RegisterStepHandler(this IBotContext context, IExecuteStep command)
        {
            context.Update.RegisterStepHandler(command);
        }

        /// <summary>
        /// Gets the user's handler, or null.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>the handler, or null.</returns>
        public static TExecuteStep? GetStepHandler<TExecuteStep>(this IBotContext context) where TExecuteStep : IExecuteStep
        {
            return context.Update.GetStepHandler<TExecuteStep>();
        }

        /// <summary>
        /// Gets the current step handler.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>The handler, or null.</returns>
        public static IExecuteStep? GetStepHandler(this IBotContext context)
        {
            return context.Update.GetStepHandler();
        }

        /// <summary>
        /// Clears the user's steps.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public static void ClearStepUserHandler(this IBotContext context)
        {
            context.Update.ClearStepUserHandler();
        }

        /// <summary>
        /// Checks whether the user has a step registered.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>True if a handler exists; False if it does not.</returns>
        public static bool HasStepHandler(this IBotContext context)
        {
            return context.Update.HasStepHandler();
        }

        #endregion

        #region Other

        /// <summary>
        /// Gets the inline command from the callback data using the bot context.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>The command, or null.</returns>
        public static InlineCallback GetCommandByCallbackOrNull(this IBotContext context)
        {
            return new InlineCallback(context).GetCommandByCallbackOrNull();
        }

        /// <summary>
        /// Gets the inline command from the callback data using the bot context.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="context">Context.</param>
        /// <returns>The command, or null.</returns>
        public static InlineCallback<T> GetCommandByCallbackOrNull<T>(this IBotContext context) 
            where T : TCommandBase
        {
            return new InlineCallback<T>(context).GetCommandByCallbackOrNull();
        }

        /// <summary>
        /// Gets the arguments of the slash command.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Collection of arguments.</returns>
        public static List<string> GetSlashArgs(this IBotContext context)
        {
            if(context.TryGetCustomValue<List<string>>(out var args))
                return args;

            return new List<string>();
        }

        /// <summary>
        /// Gets the arguments of a slash command of a specific type.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="context">Bot context.</param>
        /// <param name="throwOnError">Indicates that an exception has to be thrown.</param>
        /// <returns>Collection of arguments.</returns>
        /// <exception cref="FormatException">Exception.</exception>
        public static List<T> GetSlashArgs<T>(this IBotContext context, bool throwOnError = false)
        {
            var args = context.GetSlashArgs();
            var result = new List<T>();

            if (args.Count == 0)
                return result;

            foreach (var arg in args)
            {
                try
                {
                    object? converted = Convert.ChangeType(arg, typeof(T));
                    if (converted is T value)
                        result.Add(value);
                }
                catch (Exception ex)
                {
                    if (throwOnError)
                        throw new FormatException($"Could not convert '{arg}' to type {typeof(T).Name}.", ex);
                }
            }

            return result;
        }

        #endregion
    }
}
