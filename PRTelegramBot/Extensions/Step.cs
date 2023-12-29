using Telegram.Bot.Types;
using PRTelegramBot.Models;
using System.Collections.Concurrent;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Позволяет пользователю выполнять команды пошагово
    /// </summary>
    public static class Step
    {
        /// <summary>
        /// Список шагов для пользователя
        /// </summary>
        static ConcurrentDictionary<long, StepTelegram> _step = new();

        /// <summary>
        /// Регистрация следующего шага
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <param name="command">Следующая команда которая должна быть выполнена</param>
        public static void RegisterNextStep(this Update update, StepTelegram command)
        {
            long userId = update.GetChatId();
            update.ClearStepUser();
            _step.AddOrUpdate(userId, command, (_, existingData) => command);
        }

        /// <summary>
        /// Получает шаг или null пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>шаг или null</returns>
        public static StepTelegram? GetStepOrNull(this Update update)
        {
            long userId = update.GetChatId();
            return (_step.TryGetValue(userId, out var data)) ? data : null;
        }

        /// <summary>
        /// Очищает шаги пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        public static void ClearStepUser(this Update update)
        {
            long userId = update.GetChatId();
            if (update.HasStep())
            {
                StepTelegram deleteData;
                _step.Remove(userId,out deleteData);
            }
        }

        /// <summary>
        /// Проверяет есть ли шаг у пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>True/false</returns>
        public static bool HasStep(this Update update)
        {
            long userId = update.GetChatId();
            if (!_step.ContainsKey(userId))
                return false;

            var data = update.GetStepOrNull();
            if (data.ExpiriedTime == null)
                return true;

            if (DateTime.Now > data.ExpiriedTime)
            {
                data.ExpiriedTime = null;
                StepTelegram deleteData;
                _step.Remove(userId, out deleteData);
                return false;
            }
            return true;
        }
    }


}
