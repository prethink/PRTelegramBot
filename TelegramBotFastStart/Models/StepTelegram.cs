using Telegram.Bot.Types;
using Telegram.Bot;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Позволяет выполнить пользователю команды пошагово
    /// </summary>
    public class StepTelegram
    {
        /// <summary>
        /// Делегат с сигнатурой метода
        /// </summary>
        public delegate Task Command(ITelegramBotClient botClient, Update update);

        /// <summary>
        /// Ссылка на метод который должен быть выполнен
        /// </summary>
        public Command CommandDelegate { get; set; }

        /// <summary>
        /// Срок когда команда еще актуальна для выполнения
        /// </summary>
        public DateTime? ExpiriedTime { get; set; }

        /// <summary>
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Command command)
        {
            CommandDelegate = command;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
        public StepTelegram(Command command, DateTime expiriedTime)
        {
            CommandDelegate = command;
            ExpiriedTime = expiriedTime;
        }
    }
}
