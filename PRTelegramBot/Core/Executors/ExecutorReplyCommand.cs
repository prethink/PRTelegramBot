using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Исполнитель reply команд.
    /// </summary>
    internal sealed class ExecutorReplyCommand : ExecutorMessageCommand
    {
        #region Базовый класс

        /// <inheritdoc />
        public override CommandType CommandType => CommandType.Reply;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ExecutorReplyCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
