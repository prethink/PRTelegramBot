using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Базовый атрибут для обработки команд.
    /// </summary>
    /// <typeparam name="T">Тип параметра.</typeparam>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class BaseQueryAttribute<T> : Attribute, IBotIdentifier, ICommandStore<T>
    {
        #region Поля и свойства

        /// <summary>
        /// Команды для методов.
        /// </summary>
        protected List<T> commands = new List<T>();

        #endregion

        #region ICommandStore

        public IEnumerable<T> Commands
        {
            get
            {
                return commands.ToList();
            }
        }

        #endregion

        #region IBotIdentifier

        /// <summary>
        /// Идентификатор бота
        /// </summary>
        public long BotId { get; set; }

        #endregion

        #region Конструкторы

        public BaseQueryAttribute(long botId = 0)
        {
            BotId = botId;
        }

        #endregion
    }
}
