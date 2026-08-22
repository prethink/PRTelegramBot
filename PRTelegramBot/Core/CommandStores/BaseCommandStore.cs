using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Registrars;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Base command store class.
    /// </summary>
    /// <typeparam name="TKey">Type of the command key.</typeparam>
    public abstract class BaseCommandStore<TKey>
        where TKey : notnull
    {
        #region Fields and properties

        /// <summary>
        /// Number of commands.
        /// </summary>
        public long CommandCount => Commands.Count;

        /// <summary>
        /// Commands.
        /// </summary>
        public Dictionary<TKey, CommandHandler> Commands { get; private set; } = new();

        /// <summary>
        /// Command registration service.
        /// </summary>
        protected MethodRegistrar registerService = new MethodRegistrar();

        /// <summary>
        /// Telegram bot.
        /// </summary>
        protected PRBotBase bot;

        #endregion

        #region Methods

        /// <summary>
        /// Clears the command list.
        /// </summary>
        public void ClearCommands()
        {
            Commands.Clear();
        }

        /// <summary>
        /// Registers the commands.
        /// </summary>
        public abstract void RegisterCommand();

        /// <summary>
        /// Adds a new command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="delegate">Method that handles the command.</param>
        /// <returns>True if the command was added; False if it could not be added.</returns>
        public abstract bool AddCommand(TKey command, Func<IBotContext, Task> @delegate);

        /// <summary>
        /// Removes the command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>True if the command was removed; False if it could not be removed.</returns>
        public abstract bool RemoveCommand(TKey command);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        protected BaseCommandStore(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
