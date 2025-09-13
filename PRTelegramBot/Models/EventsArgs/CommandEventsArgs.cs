using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Событие с сылкой на команду.
    /// </summary>
    public class CommandEventsArgs : BotEventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Метод для выполнения.
        /// </summary>
        public Func<IBotContext, Task> ExecuteMethod { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="executeMethod"></param>
        public CommandEventsArgs(IBotContext context, Func<IBotContext, Task> executeMethod)
            : base(context)
        {
            this.ExecuteMethod = executeMethod;
        }

        #endregion
    }
}
