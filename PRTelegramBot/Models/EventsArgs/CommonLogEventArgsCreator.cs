using PRTelegramBot.Interfaces;

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
        /// Context.
        /// </summary>
        public IBotContext Context { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        public CommonLogEventArgsCreator(string message, string type)
            : this(message, type, ConsoleColor.White, BotContext.CreateEmpty()) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        /// <param name="context">Контекст бота.</param>
        public CommonLogEventArgsCreator(string message, string type, IBotContext context)
            : this(message, type, ConsoleColor.White, context) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        /// <param name="color">Цвет.</param>
        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color)
            : this(message, type, color, BotContext.CreateEmpty()) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="type">Тип.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="context">Контекст бота.</param>
        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color, IBotContext context)
        {
            this.Message = message;
            this.Type = type;
            this.Color = color;
            this.Context = context;
        }

        #endregion
    }
}
