using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for ITelegramBotClient.
    /// </summary>
    public static class ITelegramBotClientExtension
    {
        #region Methods

        /// <summary>
        /// Checks whether the user is an administrator of the bot.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>True if the user is an administrator; False otherwise.</returns>
        public static async Task<bool> IsAdmin(this IBotContext context)
        {
            return await IsAdmin(context, context.Update.GetChatId());
        }

        /// <summary>
        /// Checks whether the user is an administrator of the bot.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if the user is an administrator; False otherwise.</returns>
        public static async Task<bool> IsAdmin(this IBotContext context, long userId)
        {
            return await context.Current.GetAdminManager().HasUser(userId);
        }

        /// <summary>
        /// Checks whether the user is present in the bot's white list.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>True if present in the list; False if not.</returns>
        public static async Task<bool> InWhiteList(this IBotContext context)
        {
            return await InWhiteList(context, context.Update.GetChatId());
        }

        /// <summary>
        /// Checks whether the user is present in the bot's white list.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if present in the list; False if not.</returns>
        public static async Task<bool> InWhiteList(this IBotContext context, long userId)
        {
            return await context.Current.GetWhiteListManager().HasUser(userId);
        }

        /// <summary>
        /// Returns the list of the bot's administrators.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>List of identifiers.</returns>
        public static async Task<List<long>> GetAdminsIds(this IBotContext context)
        {
            return await context.Current.GetAdminManager().GetUsersIds();
        }

        /// <summary>
        /// Returns the white list of users.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>List of identifiers.</returns>
        public static async Task<List<long>> GetWhiteListIds(this IBotContext context)
        {
            return await context.Current.GetWhiteListManager().GetUsersIds();
        }

        /// <summary>
        /// Raises the plain log event.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="msg">Message.</param>
        /// <param name="typeEvent">Event type.</param>
        /// <param name="color">Color.</param>
        public static void InvokeCommonLog(this IBotContext context, string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)
        {
            context.Current.Events.OnCommonLogInvoke(msg, typeEvent, color);
        }

        /// <summary>
        /// Raises the error logging event.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="ex">Exception.</param>
        public static void InvokeErrorLog(this IBotContext context, Exception ex)
        {
            context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
        }

        /// <summary>
        /// Generates a referral link.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="refLink">Text of the referral link.</param>
        /// <returns>The generated referral link https://t.me/{bot.Username}?start={refLink}.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the text is empty.</exception>
        public async static Task<string> GetGeneratedRefLink(this IBotContext context, string refLink)
        {
            if (string.IsNullOrEmpty(refLink))
                throw new ArgumentNullException(nameof(refLink));

            var bot = await context.BotClient.GetMe();
            return $"https://t.me/{bot.Username}?start={refLink}";
        }

        /// <summary>
        /// Gets a value from the config file by key
        /// </summary>
        /// <typeparam name="TBotProvider">The provider that works with files.</typeparam>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="context">Bot context.</param>
        /// <param name="configKey">Config key.</param>
        /// <param name="key">Key of the value.</param>
        /// <returns>The value from the config file.</returns>
        public static TReturn GetConfigValue<TBotProvider, TReturn>(this IBotContext context, string configKey, string key)
            where TBotProvider : IBotConfigProvider
        {
            string configPath = context.Current.Options.ConfigPaths[configKey];
            var botConfiguration = Activator.CreateInstance(typeof(TBotProvider)) as IBotConfigProvider;
            botConfiguration.SetConfigPath(configPath);
            return botConfiguration.GetValue<TReturn>(key);
        }

        /// <summary>
        /// Tries to get a value from the config file by key
        /// </summary>
        /// <typeparam name="TBotProvider">The provider that works with files.</typeparam>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="context">Bot context.</param>
        /// <param name="configKey">Config key.</param>
        /// <param name="key">Key of the value.</param>
        /// <param name="result">Value.</param>
        /// <returns>True if the value was retrieved; False if it could not be retrieved.</returns>
        public static bool TryGetConfigValue<TBotProvider, TReturn>(this IBotContext context, string configKey, string key, out TReturn result)
            where TBotProvider : IBotConfigProvider, new()
        {
            result = default(TReturn);
            try
            {
                var botConfiguration = new TBotProvider(); // Create the configuration provider instance
                string configPath = context.Current.Options?.ConfigPaths?.GetValueOrDefault(configKey);

                if (configPath is null)
                {
                    // If the configuration path is not found, return false
                    return false;
                }

                botConfiguration.SetConfigPath(configPath); // Set the configuration path
                result = botConfiguration.GetValue<TReturn>(key); // Get the configuration value
                return true; // The configuration value was retrieved successfully
            }
            catch (Exception ex)
            {
                context.Current.GetLogger(typeof(ITelegramBotClientExtension)).LogErrorInternal(ex);
                return false;
            }
        }

        #endregion
    }
}
