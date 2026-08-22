using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// Events for the commands.
    /// </summary>
    public sealed class CommandsEvents
    {
        #region Events

        /// <summary>
        /// Event raised before a reply command is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreReplyCommandHandle;

        /// <summary>
        /// Event raised after a reply command has been handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostReplyCommandHandle;

        /// <summary>
        /// Event raised before a dynamic reply command is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreDynamicReplyCommandHandle;

        /// <summary>
        /// Event raised after a dynamic reply command has been handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostDynamicReplyCommandHandle;

        /// <summary>
        /// Event raised before a slash command is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreSlashCommandHandle;

        /// <summary>
        /// Event raised after a slash command has been handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostSlashCommandHandle;

        /// <summary>
        /// Event raised before an inline command is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreInlineCommandHandle;

        /// <summary>
        /// Event raised after an inline command has been handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostInlineCommandHandle;

        /// <summary>
        /// Event raised before a next step command is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreNextStepCommandHandle;

        /// <summary>
        /// Event raised after a next step command has been handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostNextStepCommandHandle;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="OnPreReplyCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPreReplyCommandHandleInvoke(BotEventArgs e) => OnPreReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPostReplyCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPostReplyCommandHandleInvoke(BotEventArgs e) => OnPostReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPreDynamicReplyCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPreDynamicReplyCommandHandleInvoke(BotEventArgs e) => OnPreDynamicReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPostDynamicReplyCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPostDynamicReplyCommandHandleInvoke(BotEventArgs e) => OnPostDynamicReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPreSlashCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPreSlashCommandHandleInvoke(BotEventArgs e) => OnPreSlashCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPostSlashCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPostSlashCommandHandleInvoke(BotEventArgs e) => OnPostSlashCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPreInlineCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPreInlineCommandHandleInvoke(BotEventArgs e) => OnPreInlineCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPostInlineCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPostInlineCommandHandleInvoke(BotEventArgs e) => OnPostInlineCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPreNextStepCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPreNextStepCommandHandleInvoke(BotEventArgs e) => OnPreNextStepCommandHandle?.Invoke(e);

        /// <summary>
        /// Raises the <see cref="OnPostNextStepCommandHandle"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal void OnPostNextStepCommandHandleInvoke(BotEventArgs e) => OnPostNextStepCommandHandle?.Invoke(e);

        #endregion
    }
}
