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

        #region IBaseQueryAttribute

        public long BotId { get; set; }

        public CommandComparison CommandComparison { get; protected set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        public BaseQueryAttribute(long botId = 0)
            :this (botId, CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public BaseQueryAttribute(long botId, CommandComparison commandComparison)
        {
            BotId = botId;
            this.CommandComparison = commandComparison;
        }

        #endregion
    }
}
