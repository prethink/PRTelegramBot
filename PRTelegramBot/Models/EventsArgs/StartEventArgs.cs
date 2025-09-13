using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы события при написание пользователем /start_data.
    /// </summary>
    public class StartEventArgs : BotEventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Данные.
        /// </summary>
        public string Data;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="data">Данные.</param>
        public StartEventArgs(IBotContext context, string data)
            : base(context)
        {
            Data = data;
        }

        #endregion
    }
}
