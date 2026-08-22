using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Base store class for message-type commands.
    /// </summary>
    public abstract class MessageCommandStore : BaseCommandStore<string>
    {
        #region Methods

        /// <summary>
        /// Adds a new command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="delegate">Method that handles the command.</param>
        /// <returns>True if the command was added; False if it could not be added.</returns>
        public override bool AddCommand(string command, Func<IBotContext, Task> @delegate)
        {
            try
            {
                Commands.Add(command, new CommandHandler(@delegate, bot));
                return true;
            }
            catch (Exception ex)
            {
                bot.GetLogger<MessageCommandStore>().LogErrorInternal(ex);
                return false;
            }
        }

        /// <summary>
        /// Removes the command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>True if the command was removed; False if it could not be removed.</returns>
        public override bool RemoveCommand(string command)
        {
            try
            {
                Commands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.GetLogger<MessageCommandStore>().LogErrorInternal(ex);
                return false;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public MessageCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
