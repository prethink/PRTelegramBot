using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Базовый интерфейс обработки команд.
    /// </summary>
    /// <typeparam name="T">Тип update для проверки.</typeparam>
    public interface ICommandHandlerBase<T>
    {
        /// <summary>
        /// Обработка.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Update.</param>
        /// <param name="updateType">Конкретный класс update.</param>
        /// <returns>Результат обновления.</returns>
        public Task<UpdateResult> Handle(PRBotBase bot, Update update, T updateType);
    }
}
