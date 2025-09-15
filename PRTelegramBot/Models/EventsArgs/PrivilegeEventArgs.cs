using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы при проверки привилегий.
    /// </summary>
    public class PrivilegeEventArgs : CommandEventsArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Маска доступа.
        /// </summary>
        public int? Mask { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="executeMethod">Метод для выполнения.</param>
        /// <param name="mask">Маска доступа.</param>
        public PrivilegeEventArgs(IBotContext context, Func<IBotContext, Task> executeMethod, int? mask)
            : base(context, executeMethod)
        {
            Mask = mask;
        }

        #endregion
    }
}
