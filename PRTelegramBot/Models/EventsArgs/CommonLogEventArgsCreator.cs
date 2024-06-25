using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы события простых логов.
    /// </summary>
    public class CommonLogEventArgsCreator : EventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Цвет.
        /// </summary>
        public ConsoleColor Color { get; private set; }

        /// <summary>
        /// Обновление.
        /// </summary>
        public Update Update { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        public CommonLogEventArgsCreator(string message, string type)
            : this(message, type, ConsoleColor.White, new Update()) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        /// <param name="update">Обновление.</param>
        public CommonLogEventArgsCreator(string message, string type, Update update)
            : this(message, type, ConsoleColor.White, update) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        /// <param name="color">Цвет.</param>
        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color)
            : this(message, type, color, new Update()) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="update">Обновление.</param>
        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color, Update update)
        {
            this.Message = message;
            this.Type = type;
            this.Color = color;
            this.Update = update;
        }

        #endregion
    }
}
