using PRTelegramBot.BackgroundTasks.Models;

namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Интерфейс данных запущенной задачи.
    /// </summary>
    public interface IRunningBackgroundTaskData
    {
        /// <summary>
        /// Задача.
        /// </summary>
        Task Task { get; }

        /// <summary>
        /// Метаданные.
        /// </summary>
        IPRBackgroundTaskMetadata Metadata { get; }

        /// <summary>
        /// Ошибки
        /// </summary>
        IReadOnlyList<Exception> Errors { get; }

        /// <summary>
        /// Количество ошибок.
        /// </summary>
        int ErrorCount { get; }

        /// <summary>
        /// Количество выполнений
        /// </summary>
        int ExecutedCount { get; }

        /// <summary>
        /// Дата и время начала задачи.
        /// </summary>
        DateTime? StartDate { get; }

        /// <summary>
        /// Дата и время завершения задачи.
        /// </summary>
        DateTime? EndDate { get; }

        /// <summary>
        /// Статус задачи.
        /// </summary>
        PRTaskStatus Status { get; }

        /// <summary>
        /// Статус завершения задачи.
        /// </summary>
        PRTaskCompletionResult CompleteStatus { get; }

        /// <summary>
        /// Выполнить инкремент выполнения задачи.
        /// </summary>
        void IncrementExecutionCount();

        /// <summary>
        /// Добавить ошибку.
        /// </summary>
        /// <param name="ex">Исключение.</param>
        void AddError(Exception ex);

        /// <summary>
        /// Установить статус задачи.
        /// </summary>
        /// <param name="status">Статус.</param>
        void SetStatus(PRTaskStatus status);

        /// <summary>
        /// Установить статус завершения задачи.
        /// </summary>
        /// <param name="status">Статус.</param>
        void SetCompleteStatus(PRTaskCompletionResult status);

        /// <summary>
        /// Запуск задачи.
        /// </summary>
        void StartTask();

        /// <summary>
        /// Завершение задачи.
        /// </summary>
        void EndTask();

        /// <summary>
        /// Исходник токена отмены.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; }
    }
}
