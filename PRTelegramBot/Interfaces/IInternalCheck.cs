﻿using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс проверки команд перед их выполнением.
    /// </summary>
    public interface IInternalCheck
    {
        /// <summary>
        /// Выполнить проверку перед выполнение команды.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Update.</param>
        /// <param name="handler">Команда обработчик.</param>
        /// <returns>Результат выполнения.</returns>
        /// <returns></returns>
        Task<InternalCheckResult> Check(PRBotBase bot, Update update, CommandHandler handler);
    }
}
