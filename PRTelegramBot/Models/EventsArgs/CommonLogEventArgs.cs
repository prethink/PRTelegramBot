using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы события простых логов.
    /// </summary>
    public class CommonLogEventArgs : BotEventArgs
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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст.</param>
        public CommonLogEventArgs(IBotContext context, CommonLogEventArgsCreator e) : base(context)
        {
            this.Message = e.Message;
            this.Type = e.Type;
            this.Color = e.Color;
        }

        #endregion
    }
}
