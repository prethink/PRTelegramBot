using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// События для бота.
    /// </summary>
    public sealed class TEvents
    {
        #region Поля и свойства

        /// <summary>
        /// Бот для событий.
        /// </summary>
        public PRBotBase Bot { get; private set; }

        /// <summary>
        /// События для обновления типа сообщения.
        /// </summary>
        public MessageEvents MessageEvents { get; private set; }

        /// <summary>
        /// События обновлений.
        /// </summary>
        public UpdateEvents UpdateEvents { get; private set; }

        /// <summary>
        /// События команд.
        /// </summary>
        public CommandsEvents CommandsEvents { get; private set; }

        #endregion

        #region События

        /// <summary>
        /// Событие когда отказано в доступе.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAccessDenied;

        /// <summary>
        /// Событие когда пользователь написал start с аргументом.
        /// </summary>
        public event Func<StartEventArgs, Task>? OnUserStartWithArgs;

        /// <summary>
        /// Событие когда нужно проверить привилегии перед выполнением команды.
        /// </summary>
        public event Func<PrivilegeEventArgs, Task>? OnCheckPrivilege;

        /// <summary>
        /// Событие когда указан не верный тип сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeMessage;

        /// <summary>
        /// Событие когда указан не верный тип чат.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeChat;

        /// <summary>
        /// Событие когда не найдена команда.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMissingCommand;

        /// <summary>
        /// Событие когда произошла ошибка при обработке команды.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnErrorCommand;

        /// <summary>
        /// Событие ошибки.
        /// </summary>
        public event Func<ErrorLogEventArgs, Task>? OnErrorLog;

        /// <summary>
        /// Событие общих логов.
        /// </summary>
        public event Func<CommonLogEventArgs, Task>? OnCommonLog;

        #endregion

        #region Методы

        /// <summary>
        /// Вызвать событие <see cref="OnUserStartWithArgs"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnUserStartWithArgsInvoke(StartEventArgs e) => OnUserStartWithArgs?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnMissingCommand"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnMissingCommandInvoke(BotEventArgs e) => OnMissingCommand?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnErrorCommand"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnErrorCommandInvoke(BotEventArgs e) => OnErrorCommand?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnAccessDenied"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnAccessDeniedInvoke(BotEventArgs e) => OnAccessDenied?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnCheckPrivilege"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnCheckPrivilegeInvoke(PrivilegeEventArgs e) => OnCheckPrivilege?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnWrongTypeMessage"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnWrongTypeMessageInvoke(BotEventArgs e) => OnWrongTypeMessage?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnWrongTypeChat"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal void OnWrongTypeChatInvoke(BotEventArgs e) => OnWrongTypeChat?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnErrorLog"/>.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        public void OnErrorLogInvoke(ErrorLogEventArgs e) => OnErrorLog?.Invoke(e);

        /// <summary>
        /// Вызвать событие <see cref="OnCommonLog"/> через готовый объект аргументов.
        /// </summary>
        /// <param name="e">Создатель аргументов события.</param>
        public void OnCommonLogInvoke(CommonLogEventArgsCreator e) =>
            OnCommonLog?.Invoke(new CommonLogEventArgs(e.Context, e));

        /// <summary>
        /// Вызвать событие <see cref="OnCommonLog"/> с простым сообщением.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void OnCommonLogInvoke(string message) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, "Common"));

        /// <summary>
        /// Вызвать событие <see cref="OnCommonLog"/> с указанием типа лога.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="type">Тип лога.</param>
        public void OnCommonLogInvoke(string message, string type) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        /// <summary>
        /// Вызвать событие <see cref="OnCommonLog"/> с контекстом бота.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="type">Тип лога.</param>
        /// <param name="context">Контекст бота.</param>
        public void OnCommonLogInvoke(string message, string type, BotContext context) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, context));

        /// <summary>
        /// Вызвать событие <see cref="OnCommonLog"/> с цветом текста.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="type">Тип лога.</param>
        /// <param name="color">Цвет текста в консоли.</param>
        public void OnCommonLogInvoke(string message, string type, ConsoleColor color) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color));

        /// <summary>
        /// Вызвать событие <see cref="OnCommonLog"/> с цветом текста и контекстом бота.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="type">Тип лога.</param>
        /// <param name="color">Цвет текста в консоли.</param>
        /// <param name="context">Контекст бота.</param>
        public void OnCommonLogInvoke(string message, string type, ConsoleColor color, BotContext context) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color, context));

        /// <summary>
        /// Дополнительный метод вызова <see cref="OnCommonLog"/> с типом лога.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="type">Тип лога.</param>
        public void OnCommonLogInvokeInvoke(string message, string type) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
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
