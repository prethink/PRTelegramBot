using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Interface;

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
        public Func<ITelegramBotClient, Update, CustomParameters, Task> CommandDelegate { get; set; }
        /// <summary>
        /// Срок когда команда еще актуальна для выполнения
        /// </summary>
        public DateTime? ExpiriedTime { get; set; }

        /// <summary>
        /// Дополнительные параметры
        /// </summary>
        public CustomParameters Args { get; set; } 

        /// <summary>
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Func<ITelegramBotClient,Update, CustomParameters, Task> command)
        {
            CommandDelegate = command;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
        public StepTelegram(Func<ITelegramBotClient, Update, CustomParameters, Task> command, DateTime expiriedTime)
        {
            CommandDelegate = command;
            ExpiriedTime = expiriedTime;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="args">Дополнительные аргументы</param>
        public StepTelegram(Func<ITelegramBotClient, Update, CustomParameters, Task> command, CustomParameters args)
        {
            CommandDelegate = command;
            Args = args;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
        /// <param name="args">Дополнительные аргументы</param>
        public StepTelegram(Func<ITelegramBotClient, Update, CustomParameters, Task> command, DateTime expiriedTime, CustomParameters args)
        {
            CommandDelegate = command;
            ExpiriedTime = expiriedTime;
            Args = args;
        }
    }
}
