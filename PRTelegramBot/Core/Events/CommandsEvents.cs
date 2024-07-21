using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// События для команд.
    /// </summary>
    public sealed class CommandsEvents
    {
        #region События

        /// <summary>
        /// Событие до обработки reply команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreReplyCommandHandle;

        /// <summary>
        /// Событие после обработки reply команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostReplyCommandHandle;

        /// <summary>
        /// Событие до обработки dynamic reply команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreDynamicReplyCommandHandle;

        /// <summary>
        /// Событие после обработки dynamic reply команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostDynamicReplyCommandHandle;

        /// <summary>
        /// Событие до обработки slash команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreSlashCommandHandle;

        /// <summary>
        /// Событие после обработки slash команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostSlashCommandHandle;

        /// <summary>
        /// Событие до обработки inline команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreInlineCommandHandle;

        /// <summary>
        /// Событие после обработки inline команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostInlineCommandHandle;

        /// <summary>
        /// Событие до обработки next step команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreNextStepCommandHandle;

        /// <summary>
        /// Событие после обработки next step команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostNextStepCommandHandle;

        #endregion

        #region Методы

        internal void OnPreReplyCommandHandleInvoke(BotEventArgs e) => OnPreReplyCommandHandle?.Invoke(e);

        internal void OnPostReplyCommandHandleInvoke(BotEventArgs e) => OnPostReplyCommandHandle?.Invoke(e);

        internal void OnPreDynamicReplyCommandHandleInvoke(BotEventArgs e) => OnPreDynamicReplyCommandHandle?.Invoke(e);

        internal void OnPostDynamicReplyCommandHandleInvoke(BotEventArgs e) => OnPostDynamicReplyCommandHandle?.Invoke(e);

        internal void OnPreSlashCommandHandleInvoke(BotEventArgs e) => OnPreSlashCommandHandle?.Invoke(e);

        internal void OnPostSlashCommandHandleInvoke(BotEventArgs e) => OnPostSlashCommandHandle?.Invoke(e);

        internal void OnPreInlineCommandHandleInvoke(BotEventArgs e) => OnPreInlineCommandHandle?.Invoke(e);

        internal void OnPostInlineCommandHandleInvoke(BotEventArgs e) => OnPostInlineCommandHandle?.Invoke(e);

        internal void OnPreNextStepCommandHandleInvoke(BotEventArgs e) => OnPreNextStepCommandHandle?.Invoke(e);

        internal void OnPostNextStepCommandHandleInvoke(BotEventArgs e) => OnPostNextStepCommandHandle?.Invoke(e);

        #endregion
    }
}
