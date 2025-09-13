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

        /// <inheritdoc />
        public IEnumerable<T> Commands => commands.ToList();

        #endregion

        #region IBaseQueryAttribute

        /// <inheritdoc />
        public List<long> BotIds { get; set; } = new();

        /// <inheritdoc />
        public CommandComparison CommandComparison { get; protected set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public BaseQueryAttribute(long[] botIds, CommandComparison commandComparison)
        {
            BotIds.AddRange(botIds);

            this.CommandComparison = commandComparison;
        }

        #endregion
    }
}
