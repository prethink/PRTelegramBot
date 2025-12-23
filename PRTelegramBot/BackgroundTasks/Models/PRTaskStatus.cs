namespace PRTelegramBot.BackgroundTasks.Models
{
    /// <summary>
    /// Статус фоновой задачи.
    /// </summary>
    public enum PRTaskStatus
    {
        /// <summary>
        /// Создана, но не стартовала.
        /// </summary>
        Pending,     
        /// <summary>
        /// Ждёт времени старта.
        /// </summary>
        Scheduled,        
        /// <summary>
        /// Начала выполнение.
        /// </summary>
        Started,
        /// <summary>
        /// Инициализация.
        /// </summary>
        Initialize,
        /// <summary>
        /// Задача выполняется.
        /// </summary>
        Executing,          
        /// <summary>
        /// Временно приостановлена.
        /// </summary>
        Paused,           
        /// <summary>
        /// Попытка повтора после ошибки.
        /// </summary>
        Retrying,         
        /// <summary>
        /// Между повторными запусками.
        /// </summary>
        WaitingNextRun,
        /// <summary>
        /// Выполнение пропущено.
        /// </summary>
        Skipped,
        /// <summary>
        /// Возникла ошибка.
        /// </summary>
        Error,
        /// <summary>
        /// Отменена/
        /// </summary>
        Complete
    }

    /// <summary>
    /// Статусы завершения задачи.
    /// </summary>
    public enum PRTaskCompletionResult
    {
        /// <summary>
        /// Нет статуса.
        /// </summary>
        None,
        /// <summary>
        /// Успешно завершена.
        /// </summary>
        Success,
        /// <summary>
        /// Завершилась с ошибкой.
        /// </summary>
        Failed,
        /// <summary>
        /// Отменена.
        /// </summary>
        Canceled
    }
}
