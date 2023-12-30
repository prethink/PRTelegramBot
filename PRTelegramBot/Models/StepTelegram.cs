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
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Func<ITelegramBotClient,Update, Task> command)
        {
            CommandDelegate = command;
        }

        /// <summary>
        ///  Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="steps">Коллекция шагов</param>
        public StepTelegram(Func<ITelegramBotClient, Update, Task> command, List<StepTelegram> steps)
        {
            CommandDelegate = command;
            Steps = steps;
        }

        /// <summary>
        /// Создает новый шаг
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды</param>
        public StepTelegram(Func<ITelegramBotClient, Update, Task> command, DateTime expiriedTime)
        {
            CommandDelegate = command;
            ExpiredTime = expiriedTime;
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
                botClient.GetBotDataOrNull().InvokeErrorLog(ex);
                return ResultExecuteStep.Failure;
            }
        }

        public Func<ITelegramBotClient, Update, Task> GetExecuteMethod()
        {
            return CommandDelegate;
        }
    }
}
