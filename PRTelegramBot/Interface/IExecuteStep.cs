using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interface
{
    public interface IExecuteStep
    {
        /// <summary>
        /// Возвращает делегат метод
        /// </summary>
        /// <returns>Возвращает делегат метод</returns>
        Func<ITelegramBotClient, Update, Task> GetExecuteMethod();

        /// <summary>
        /// Выполнять команду
        /// </summary>
        /// <param name="botClient">telegram bot</param>
        /// <param name="update">update пользователя</param>
        /// <returns>Результат выполнения команды</returns>
        Task<ResultExecuteStep> ExecuteStep(ITelegramBotClient botClient, Update update);
    }
}
