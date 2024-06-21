using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс пошагового выполнения команд.
    /// </summary>
    public interface IExecuteStep
    {
        /// <summary>
        /// Игнорировать базовые команды при выполнение шагов.
        /// </summary>
        public bool IgnoreBasicCommands { get; set; }

        /// <summary>
        /// Это последний шаг завершен.
        /// </summary>
        public bool LastStepExecuted { get; set; }

        /// <summary>
        /// Получить ссылку на метод, который нужно выполнить.
        /// </summary>
        /// <returns>Метод для выполнения.</returns>
        Func<ITelegramBotClient, Update, Task> GetExecuteMethod();

        /// <summary>
        /// Выполнить команду.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Обновления telegram.</param>
        /// <returns>Результат выполнения команды.</returns>
        Task<ExecuteStepResult> ExecuteStep(ITelegramBotClient botClient, Update update);
    }
}
