using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Core;
using PRTelegramBot.Interface;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Позволяет выполнить пользователю команды пошагово
    /// </summary>
    public class StepTelegram : IExecuteStep
    {
        /// <summary>
        /// Дочерние шаги
        /// </summary>
        public List<StepTelegram> Steps { get; private set; } = new List<StepTelegram>();

        /// <summary>
        /// Ссылка на метод который должен быть выполнен
        /// </summary>
        public Func<ITelegramBotClient, Update, Task> CommandDelegate { get; set; }

        /// <summary>
        /// Срок когда команда еще актуальна для выполнения
        /// </summary>
        public DateTime? ExpiredTime { get; set; }

        /// <summary>
        /// Кэш данных
        /// </summary>
        private ITelegramCache cache { get; set; }

        /// <summary>
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Func<ITelegramBotClient,Update, Task> command, ITelegramCache cache = null)
        {
            CommandDelegate = command;
            this.cache = cache;
        }

        /// <summary>
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="steps">Коллекция шагов</param>
        public StepTelegram(Func<ITelegramBotClient, Update, Task> command, List<StepTelegram> steps, ITelegramCache cache = null)
        {
            CommandDelegate = command;
            Steps = steps;
            this.cache = cache;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
        public StepTelegram(Func<ITelegramBotClient, Update, Task> command, DateTime expiriedTime, ITelegramCache cache = null)
        {
            CommandDelegate = command;
            ExpiredTime = expiriedTime;
            this.cache = cache;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
         /// <param name="steps">Коллекция шагов</param>
        public StepTelegram(Func<ITelegramBotClient, Update, Task> command, DateTime expiriedTime, List<StepTelegram> steps)
        {
            CommandDelegate = command;
            ExpiredTime = expiriedTime;
            Steps = steps;
        }

        /// <summary>
        /// Выполнение шага
        /// </summary>
        /// <param name="botClient">telegram клиент</param>
        /// <param name="update">Update пользователя</param>
        /// <returns>Результат обработки</returns>
        public async Task<ResultExecuteStep> ExecuteStep(ITelegramBotClient botClient, Update update)
        {
            if (ExpiredTime != null && DateTime.Now > ExpiredTime)
            {
                update.ClearStepUserHandler();
                return ResultExecuteStep.ExpiredTime;
            }

            try
            {
                await CommandDelegate.Invoke(botClient, update);
                return ResultExecuteStep.Success;
            }
            catch(Exception ex) 
            {
                botClient.GetBotDataOrNull()!.InvokeErrorLog(ex);
                return ResultExecuteStep.Failure;
            }
        }

        /// <summary>
        /// Получение текущей команды
        /// </summary>
        /// <returns>Ссылка на метод обработки</returns>
        public Func<ITelegramBotClient, Update, Task> GetExecuteMethod()
        {
            return CommandDelegate;
        }

        /// <summary>
        /// Регистрация следующего шага
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки</param>
        /// <param name="expiriedTime">До какого времени команду можно выполнить</param>
        public void RegisterNextStep(Func<ITelegramBotClient, Update, Task> nextStep, DateTime? expiriedTime = null)
        {
            CommandDelegate = nextStep;
            ExpiredTime = expiriedTime;
        }

        /// <summary>
        /// Регистрация следующего шага
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки</param>
        /// <param name="addTime">До какого времени команду можно выполнить</param>
        public void RegisterNextStep(Func<ITelegramBotClient, Update, Task> nextStep, TimeSpan addTime)
        {
            CommandDelegate = nextStep;
            ExpiredTime = DateTime.Now.Add(addTime);
        }

        /// <summary>
        /// Получение текущего кэша
        /// </summary>
        /// <typeparam name="T">Класс для хранения кэша</typeparam>
        /// <returns>Кэш</returns>
        public T GetCache<T>() where T : ITelegramCache
        {
            return cache is T resultCache ? resultCache : default(T);
        }
    }
}
