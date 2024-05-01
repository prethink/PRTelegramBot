using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    public interface IExecuteStep
    {
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
        Task<ResultExecuteStep> ExecuteStep(ITelegramBotClient botClient, Update update);
    }
}
