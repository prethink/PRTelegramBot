using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Базовый атрибут для обработки команд.
    /// </summary>
    /// <typeparam name="T">Тип параметра.</typeparam>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public abstract class BaseQueryAttribute<T> 
        : Attribute, IBaseQueryAttribute, ICommandStore<T>
    {
        #region Поля и свойства

        /// <summary>
        /// Команды для методов.
        /// </summary>
        protected List<T> commands = new List<T>();

        /// <summary>
        /// Как сравнивать команды.
        /// </summary>
        public CommandComparison CommandComparison { get; protected set; }

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
            :this (botId, CommandComparison.Equals) { }

        public BaseQueryAttribute(long botId, CommandComparison commandComparison)
        {
            BotId = botId;
            this.CommandComparison = CommandComparison;
        }

        #endregion
    }
}
