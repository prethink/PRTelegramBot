using Telegram.Bot.Types;
using PRTelegramBot.Models;
using System.Collections.Concurrent;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Позволяет пользователю выполнять команды пошагово
    /// </summary>
    public static class StepExtension
    {
        /// <summary>
        /// Список шагов для пользователя
        /// </summary>
        static ConcurrentDictionary<long, IExecuteStep> _step = new();

        /// <summary>
        /// Регистрация следующего шага
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <param name="command">Следующая команда которая должна быть выполнена</param>
        public static void RegisterStepHandler(this Update update, IExecuteStep command)
        {
            long userId = update.GetChatId();
            update.ClearStepUserHandler();
            _step.AddOrUpdate(userId, command, (_, existingData) => command);
        }

        /// <summary>
        /// Получает обработчик или null пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <returns>обработчик или null</returns>
        public static T? GetStepHandler<T>(this Update update) where T :  IExecuteStep
        {
            long userId = update.GetChatId();

            return _step.TryGetValue(userId, out var data) && data is T stepHandler 
                ? stepHandler 
                : default(T);
        }

        /// <summary>
        /// Получить текущий
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <returns>обработчик или null</returns>
        public static IExecuteStep? GetStepHandler(this Update update)
        {
            return GetStepHandler<IExecuteStep>(update);
        }

        /// <summary>
        /// Очищает шаги пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        public static void ClearStepUserHandler(this Update update)
        {
            long userId = update.GetChatId();

            if (update.HasStepHandler())
                _step.Remove(userId, out _);
        }

        /// <summary>
        /// Проверяет есть ли шаг у пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <returns>True/false</returns>
        public static bool HasStepHandler(this Update update)
        {
            long userId = update.GetChatId();
            return _step.ContainsKey(userId) ;
        }
    }


}
