using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Позволяет выполнить пользователю команды пошагово.
    /// </summary>
    public sealed class StepTelegram : IExecuteStep
    {
        #region Свойства и константы

        /// <summary>
        /// Ссылка на метод который должен быть выполнен.
        /// </summary>
        public Func<IBotContext, Task> CommandDelegate { get; set; }

        /// <summary>
        /// До какого времени команду можно выполнить.
        /// </summary>
        public DateTime? ExpiredTime { get; set; }

        /// <summary>
        /// Кэш данных.
        /// </summary>
        private ITelegramCache cache { get; set; }

        #endregion

        #region IExecuteStep

        /// <inheritdoc/>
        public bool LastStepExecuted { get; set; }

        /// <inheritdoc/>
        public bool IgnoreBasicCommands { get; set; }

        /// <inheritdoc/>
        public async Task<ExecuteStepResult> ExecuteStep(IBotContext context)
        {
            if (ExpiredTime is not null && DateTime.Now > ExpiredTime)
            {
                context.Update.ClearStepUserHandler();
                return ExecuteStepResult.ExpiredTime;
            }

            try
            {
                await CommandDelegate.Invoke(context);
                return ExecuteStepResult.Success;
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
                return ExecuteStepResult.Failure;
            }
        }

        /// <inheritdoc/>
        public Func<IBotContext, Task> GetExecuteMethod()
        {
            return CommandDelegate;
        }

        /// <inheritdoc/>
        public bool CanExecute()
        {
            return ExpiredTime is null || DateTime.Now < ExpiredTime;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep)
        {
            RegisterNextStep(nextStep, null);
        }

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        /// <param name="addTime">До какого времени команду можно выполнить</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, TimeSpan addTime)
        {
            RegisterNextStep(nextStep, DateTime.Now.Add(addTime));
        }

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        /// <param name="expiriedTime"> До какого времени команду можно выполнить.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, DateTime? expiriedTime)
        {
            RegisterNextStep(nextStep, expiriedTime, false);
        }

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        /// <param name="expiriedTime"> До какого времени команду можно выполнить.</param>
        /// <param name="ignoreBasicCommands">Игнорировать базовые команды при выполнение шагов.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, DateTime? expiriedTime, bool ignoreBasicCommands)
        {
            CommandDelegate = nextStep;
            ExpiredTime = expiriedTime;
            IgnoreBasicCommands = ignoreBasicCommands;
        }

        /// <summary>
        /// Получение текущего кэша
        /// </summary>
        /// <typeparam name="T">Класс для хранения кэша</typeparam>
        /// <returns>Кэш</returns>
        public T GetCache<T>()
        {
            return cache is T resultCache 
                ? resultCache 
                : default;
        }

        #endregion

        #region Конструкторы класса

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Func<IBotContext, Task> command)
            : this(command, null, null) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="cache">Кэш.</param>
        public StepTelegram(Func<IBotContext, Task> command, ITelegramCache cache)
            : this(command, null, cache, false) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды после чего команда будет проигнорирована.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime expiriedTime)
            : this(command, expiriedTime, null, false) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды после чего команда будет проигнорирована.</param>
        /// <param name="cache">Кэш.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime? expiriedTime, ITelegramCache cache)
            : this(command, expiriedTime, cache, false) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды после чего команда будет проигнорирована.</param>
        /// <param name="cache">Кэш.</param>
        /// <param name="ignoreBasicCommands">Игнорировать базовые команды при выполнение шагов.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime? expiriedTime, ITelegramCache cache, bool ignoreBasicCommands)
        {
            this.cache = cache;
            IgnoreBasicCommands = ignoreBasicCommands;
            CommandDelegate = command;
            ExpiredTime = expiriedTime;
        }

        #endregion
    }
}
