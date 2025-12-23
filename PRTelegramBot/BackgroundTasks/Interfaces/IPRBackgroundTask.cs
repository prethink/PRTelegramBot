using PRTelegramBot.Core;

namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Интерфейс фоновой задачи.
    /// </summary>
    public interface IPRBackgroundTask
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Проверяет, может ли фоновая задача быть выполнена в текущий момент.
        /// Метод вызывается фреймворком перед каждой попыткой выполнения.
        /// Возврат false означает, что выполнение будет пропущено и
        /// повторная проверка произойдёт при следующем плановом запуске.
        /// </summary>
        Task<bool> CanExecute();

        /// <summary>
        /// Запустить выполнение фоновой задачи.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task ExecuteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Устанавливает экземпляр бота для доступа к его контексту и сервисам.
        /// Метод вызывается фреймворком при инициализации фоновой задачи.
        /// </summary>
        /// <param name="bot">Экземпляр базового класса бота.</param>
        Task Initialize(PRBotBase bot);
    }
}
