using PRTelegramBot.Interfaces;
using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Позволяет пользователю выполнять команды пошагово
    /// </summary>
    public static class StepExtension
    {
        #region Поля и свойства

        /// <summary>
        /// Список шагов для пользователя
        /// </summary>
        static ConcurrentDictionary<string, IExecuteStep> _step = new();

        #endregion

        #region Методы

        /// <summary>
        /// Регистрация следующего шага
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <param name="command">Следующая команда которая должна быть выполнена</param>
        public static void RegisterStepHandler(this Update update, IExecuteStep command)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            update.ClearStepUserHandler();
            _step.AddOrUpdate(userKey, command, (_, existingData) => command);
        }

        /// <summary>
        /// Получает обработчик или null пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <returns>обработчик или null</returns>
        public static T? GetStepHandler<T>(this Update update) where T : IExecuteStep
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return _step.TryGetValue(userKey, out var data) && data is T stepHandler
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
            string userKey = update.GetKeyMappingUserTelegram();
            if (update.HasStepHandler())
                _step.Remove(userKey, out _);
        }

        /// <summary>
        /// Проверяет есть ли шаг у пользователя
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <returns>True/false</returns>
        public static bool HasStepHandler(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return _step.ContainsKey(userKey);
        }

        #endregion
    }
}
