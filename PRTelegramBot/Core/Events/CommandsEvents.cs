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

        /// <summary>
        /// Вызвать событие <see cref="OnPreReplyCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPreReplyCommandHandleInvoke(BotEventArgs e) => OnPreReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPostReplyCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPostReplyCommandHandleInvoke(BotEventArgs e) => OnPostReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPreDynamicReplyCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPreDynamicReplyCommandHandleInvoke(BotEventArgs e) => OnPreDynamicReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPostDynamicReplyCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPostDynamicReplyCommandHandleInvoke(BotEventArgs e) => OnPostDynamicReplyCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPreSlashCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPreSlashCommandHandleInvoke(BotEventArgs e) => OnPreSlashCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPostSlashCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPostSlashCommandHandleInvoke(BotEventArgs e) => OnPostSlashCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPreInlineCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPreInlineCommandHandleInvoke(BotEventArgs e) => OnPreInlineCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPostInlineCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPostInlineCommandHandleInvoke(BotEventArgs e) => OnPostInlineCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPreNextStepCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPreNextStepCommandHandleInvoke(BotEventArgs e) => OnPreNextStepCommandHandle?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnPostNextStepCommandHandle"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnPostNextStepCommandHandleInvoke(BotEventArgs e) => OnPostNextStepCommandHandle?.Invoke(e);

        #endregion
    }
}
