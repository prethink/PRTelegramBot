using PRTelegramBot.Core;

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
        /// <param name="bot">Бот.</param>
        /// <param name="e">Событие.</param>
        public CommonLogEventArgs(PRBotBase bot, CommonLogEventArgsCreator e) : base(bot, e.Update)
        {
            this.Message = e.Message;
            this.Type = e.Type;
            this.Color = e.Color;
        }

        #endregion
    }
}
