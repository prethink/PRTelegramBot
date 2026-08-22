using PRTelegramBot.Core;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the command registrar.
    /// </summary>
    public interface IRegisterCommand
    {
        /// <summary>
        /// Registers a slash command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="method">Method</param>
        /// <returns>True if the method was registered; false on error / not registered</returns>
        public bool AddSlashCommand(string command, Func<IBotContext, Task> method);

        /// <summary>
        /// Registers a reply command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="method">Method</param>
        /// <returns>True if the method was registered; false on error / not registered</returns>
        public bool AddReplyCommand(string command, Func<IBotContext, Task> method);

        /// <summary>
        /// Registers an inline command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="method">Method</param>
        /// <returns>True if the method was registered; false on error / not registered</returns>
        public bool AddInlineCommand(Enum command, Func<IBotContext, Task> method);

        /// <summary>
        /// Removes a reply command
        /// </summary>
        /// <param name="command">Command name</param>
        /// <returns>True if the method was removed; false on error</returns>
        public bool RemoveReplyCommand(string command);

        /// <summary>
        /// Removes a slash command
        /// </summary>
        /// <param name="command">Command name</param>
        /// <returns>True if the method was removed; false on error</returns>
        public bool RemoveSlashCommand(string command);

        /// <summary>
        /// Removes an inline command
        /// </summary>
        /// <param name="command">command enum value</param>
        /// <returns>True if the method was removed; false on error</returns>
        public bool RemoveInlineCommand(Enum command);

        /// <summary>
        /// Initialization.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public void Init(PRBotBase bot);
    }
}
