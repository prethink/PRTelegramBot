using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Исполнитель slash команд.
    /// </summary>
    internal sealed class ExecutorSlashCommand : ExecutorMessageCommand
    {
        #region Базовый класс

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.Slash;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ExecutorSlashCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
