using Telegram.Bot.Types;
using PRTelegramBot.Models;
using System.Collections.Concurrent;
using PRTelegramBot.Interface;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Позволяет пользователю выполнять команды пошагово
    /// </summary>
    public static class StepService
    {
        /// <summary>
        /// Список шагов для пользователя
        /// </summary>
        static ConcurrentDictionary<long, IExecuteStep> _step = new();

        /// <summary>
        /// Регистрация следующего шага
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <param name="command">Следующая команда которая должна быть выполнена</param>
        public static void RegisterStepHandler(this Update update, IExecuteStep command)
        {
            long userId = update.GetChatId();
            update.ClearStepUserHandler();
            _step.AddOrUpdate(userId, command, (_, existingData) => command);
        }

        /// <summary>
        /// Получает шаг или null пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>шаг или null</returns>
        public static IExecuteStep? GetStepHandler(this Update update)
        {
            long userId = update.GetChatId();
            return (_step.TryGetValue(userId, out var data)) ? data : null;
        }

        /// <summary>
        /// Очищает шаги пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        public static void ClearStepUserHandler(this Update update)
        {
            long userId = update.GetChatId();

            if (update.HasStepHandler())
                _step.Remove(userId, out _);
            
        }

        /// <summary>
        /// Проверяет есть ли шаг у пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>True/false</returns>
        public static bool HasStepHandler(this Update update)
        {
            long userId = update.GetChatId();
            return _step.ContainsKey(userId);
        }
    }


}
