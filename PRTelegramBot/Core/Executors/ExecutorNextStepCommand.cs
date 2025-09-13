using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Исполнитель для пошагового выполнения команд.
    /// </summary>
    internal sealed class ExecutorNextStepCommand : ExecutorMessageCommand
    {
        #region Базовый класс

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.NextStep;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ExecutorNextStepCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
