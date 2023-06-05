using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Core;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Позволяет выполнить пользователю команды пошагово
    /// </summary>
    public class StepTelegram
    {
        /// <summary>
        /// Ссылка на метод который должен быть выполнен
        /// </summary>
        public Router.TelegramCommand CommandDelegate { get; set; }

        /// <summary>
        /// Срок когда команда еще актуальна для выполнения
        /// </summary>
        public DateTime? ExpiriedTime { get; set; }

        /// <summary>
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Router.TelegramCommand command)
        {
            CommandDelegate = command;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
        public StepTelegram(Router.TelegramCommand command, DateTime expiriedTime)
        {
            CommandDelegate = command;
            ExpiriedTime = expiriedTime;
        }
    }
}
