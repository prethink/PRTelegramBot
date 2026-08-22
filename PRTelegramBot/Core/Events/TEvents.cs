using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// Events of the bot.
    /// </summary>
    public sealed class TEvents
    {
        #region Fields and properties

        /// <summary>
        /// The bot the events belong to.
        /// </summary>
        public PRBotBase Bot { get; private set; }

        /// <summary>
        /// Events for message-type updates.
        /// </summary>
        public MessageEvents MessageEvents { get; private set; }

        /// <summary>
        /// Update events.
        /// </summary>
        public UpdateEvents UpdateEvents { get; private set; }

        /// <summary>
        /// Command events.
        /// </summary>
        public CommandsEvents CommandsEvents { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when access is denied.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAccessDenied;

        /// <summary>
        /// Event raised when the user sends start with an argument.
        /// </summary>
        public event Func<StartEventArgs, Task>? OnUserStartWithArgs;

        /// <summary>
        /// Event raised when privileges have to be checked before a command runs.
        /// </summary>
        public event Func<PrivilegeEventArgs, Task>? OnCheckPrivilege;

        /// <summary>
        /// Event raised when the message type is invalid.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeMessage;

        /// <summary>
        /// Event raised when the chat type is invalid.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeChat;

        /// <summary>
        /// Event raised when no command was found.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMissingCommand;

        /// <summary>
        /// Event raised when an error occurs while handling a command.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnErrorCommand;

        /// <summary>
        /// Error event.
        /// </summary>
        public event Func<ErrorLogEventArgs, Task>? OnErrorLog;

        /// <summary>
        /// Event for general logs.
        /// </summary>
        public event Func<CommonLogEventArgs, Task>? OnCommonLog;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="OnUserStartWithArgs"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnUserStartWithArgsInvoke(StartEventArgs e) => OnUserStartWithArgs?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnMissingCommand"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnMissingCommandInvoke(BotEventArgs e) => OnMissingCommand?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnErrorCommand"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnErrorCommandInvoke(BotEventArgs e) => OnErrorCommand?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnAccessDenied"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnAccessDeniedInvoke(BotEventArgs e) => OnAccessDenied?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnCheckPrivilege"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnCheckPrivilegeInvoke(PrivilegeEventArgs e) => OnCheckPrivilege?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnWrongTypeMessage"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnWrongTypeMessageInvoke(BotEventArgs e) => OnWrongTypeMessage?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnWrongTypeChat"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnWrongTypeChatInvoke(BotEventArgs e) => OnWrongTypeChat?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnErrorLog"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        public void OnErrorLogInvoke(ErrorLogEventArgs e) => OnErrorLog?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnCommonLog"/> event using a prebuilt arguments object.
        /// </summary>
        /// <param name="e">Factory that creates the event arguments.</param>
        public void OnCommonLogInvoke(CommonLogEventArgsCreator e) =>
            OnCommonLog?.Invoke(new CommonLogEventArgs(e.Context, e));

        /// <summary>
        /// Raises the <see cref="OnCommonLog"/> event with a plain message.
        /// </summary>
        /// <param name="message">Message text.</param>
        public void OnCommonLogInvoke(string message) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, "Common"));

        /// <summary>
        /// Raises the <see cref="OnCommonLog"/> event with an explicit log type.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="type">Log type.</param>
        public void OnCommonLogInvoke(string message, string type) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        /// <summary>
        /// Raises the <see cref="OnCommonLog"/> event with the bot context.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="type">Log type.</param>
        /// <param name="context">Bot context.</param>
        public void OnCommonLogInvoke(string message, string type, BotContext context) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, context));

        /// <summary>
        /// Raises the <see cref="OnCommonLog"/> event with a text color.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="type">Log type.</param>
        /// <param name="color">Console text color.</param>
        public void OnCommonLogInvoke(string message, string type, ConsoleColor color) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color));

        /// <summary>
        /// Raises the <see cref="OnCommonLog"/> event with a text color and the bot context.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="type">Log type.</param>
        /// <param name="color">Console text color.</param>
        /// <param name="context">Bot context.</param>
        public void OnCommonLogInvoke(string message, string type, ConsoleColor color, BotContext context) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color, context));

        /// <summary>
        /// An additional overload that raises <see cref="OnCommonLog"/> with a log type.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="type">Log type.</param>
        public void OnCommonLogInvokeInvoke(string message, string type) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public TEvents(PRBotBase bot)
        {
            Bot = bot;
            MessageEvents = new MessageEvents();
            UpdateEvents = new UpdateEvents();
            CommandsEvents = new CommandsEvents();
        }

        #endregion
    }
}
