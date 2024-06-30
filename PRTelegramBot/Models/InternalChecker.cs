using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Класс для хранения типа команд и их проверок.
    /// </summary>
    public class InternalChecker
    {
        #region Поля и свойства

        /// <summary>
        /// Для какого типа команд проверка.
        /// </summary>
        public List<CommandType> CommandTypes { get; private set; } = new();

        #endregion

        #region IInternalCheck

        /// <summary>
        /// Класс который будет выполнять проверку.
        /// </summary>
        public IInternalCheck Checker { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandType">Тип команды.</param>
        /// <param name="checker">Чекер.</param>
        public InternalChecker(CommandType commandType, IInternalCheck checker)
        {
            this.CommandTypes.Add(commandType);
            this.Checker = checker;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandTypes">Типы команд.</param>
        /// <param name="checker">Чекер.</param>
        public InternalChecker(List<CommandType> commandTypes, IInternalCheck checker)
        {
            this.CommandTypes.AddRange(commandTypes);
            this.Checker = checker;
        }

        #endregion
    }
}
