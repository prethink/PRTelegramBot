using PRTelegramBot.Models.Enums;

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
        Func<IBotContext, Task> GetExecuteMethod();

        /// <summary>
        /// Выполнить команду.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Результат выполнения команды.</returns>
        Task<ExecuteStepResult> ExecuteStep(IBotContext context);

        /// <summary>
        /// Может ли быть выполнен шаг
        /// </summary>
        /// <returns>True - да/False - нет.</returns>
        bool CanExecute();
    }
}
