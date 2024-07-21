using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Исполнительно динамических reply команд.
    /// </summary>
    internal sealed class ExecutorReplyDynamicCommand : ExecutorMessageCommand
    {
        #region Базовый класс

        public override CommandType CommandType => CommandType.ReplyDynamic;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ExecutorReplyDynamicCommand(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
