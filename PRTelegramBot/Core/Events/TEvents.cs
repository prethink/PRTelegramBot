using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

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
        /// Событие ошибки.
        /// </summary>
        public event Func<ErrorLogEventArgs, Task>? OnErrorLog;

        /// <summary>
        /// Событие общих логов.
        /// </summary>
        public event Func<CommonLogEventArgs, Task>? OnCommonLog;

        #endregion

        #region Методы

        internal void OnUserStartWithArgsInvoke(StartEventArgs e) => OnUserStartWithArgs?.Invoke(e);

        internal void OnMissingCommandInvoke(BotEventArgs e) => OnMissingCommand?.Invoke(e);

        internal void OnAccessDeniedInvoke(BotEventArgs e) => OnAccessDenied?.Invoke(e);

        internal void OnCheckPrivilegeInvoke(PrivilegeEventArgs e) => OnCheckPrivilege?.Invoke(e);

        internal void OnWrongTypeMessageInvoke(BotEventArgs e) => OnWrongTypeMessage?.Invoke(e);

        internal void OnWrongTypeChatInvoke(BotEventArgs e) => OnWrongTypeChat?.Invoke(e);

        internal void OnErrorLogInvoke(ErrorLogEventArgsCreator e) => OnErrorLog?.Invoke(new ErrorLogEventArgs(Bot, e));

        public void OnErrorLogInvoke(Exception exception, Update update) => OnErrorLogInvoke(new ErrorLogEventArgsCreator(exception, update));
  
        public void OnErrorLogInvoke(Exception exception) => OnErrorLogInvoke(new ErrorLogEventArgsCreator(exception));

        public void OnCommonLogInvoke(CommonLogEventArgsCreator e) => OnCommonLog?.Invoke(new CommonLogEventArgs(Bot, e));

        public void OnCommonLogInvoke(string message) => OnCommonLogInvoke(new CommonLogEventArgsCreator(message, "Common"));

        public void OnCommonLogInvoke(string message, string type) => OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        public void OnCommonLogInvoke(string message, string type, Update update) => OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, update));

        public void OnCommonLogInvoke(string message, string type, ConsoleColor color) => OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color));

        public void OnCommonLogInvoke(string message, string type, ConsoleColor color, Update update) => OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color, update));

        public void OnCommonLogInvokeInvoke(string message, string type) => OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

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
        }

        #endregion
    }
}
