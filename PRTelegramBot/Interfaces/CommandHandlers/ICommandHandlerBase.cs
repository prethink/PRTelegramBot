using PRTelegramBot.Models.Enums;

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
        /// <param name="context">Контекст бота.</param>
        /// <param name="updateType">Конкретный класс update.</param>
        /// <returns>Результат обновления.</returns>
        public Task<UpdateResult> Handle(IBotContext context, T updateType);
    }
}
