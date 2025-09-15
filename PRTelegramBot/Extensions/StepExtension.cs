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
        /// Список шагов для пользователя.
        /// </summary>
        static ConcurrentDictionary<string, IExecuteStep> step = new();

        #endregion

        #region Методы

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <param name="command">Следующая команда которая должна быть выполнена.</param>
        public static void RegisterStepHandler(this Update update, IExecuteStep command)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            update.ClearStepUserHandler();
            step.AddOrUpdate(userKey, command, (_, existingData) => command);
        }

        /// <summary>
        /// Получает обработчик или null пользователя.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>обработчик или null.</returns>
        public static TExecuteStep? GetStepHandler<TExecuteStep>(this Update update) where TExecuteStep : IExecuteStep
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return step.TryGetValue(userKey, out var data) && data is TExecuteStep stepHandler
                ? stepHandler
                : default(TExecuteStep);
        }

        /// <summary>
        /// Получить текущий обработчик шага.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Обработчик или null.</returns>
        public static IExecuteStep? GetStepHandler(this Update update)
        {
            return GetStepHandler<IExecuteStep>(update);
        }

        /// <summary>
        /// Очищает шаги пользователя.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        public static void ClearStepUserHandler(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            if (update.HasStepHandler())
                step.Remove(userKey, out _);
        }

        /// <summary>
        /// Проверяет есть ли шаг у пользователя.
        /// </summary>
        /// <param name="update">Обновление полученное с telegram</param>
        /// <returns>True - есть обработчик, False - нет обработчика.</returns>
        public static bool HasStepHandler(this Update update)
        {
            string userKey = update.GetKeyMappingUserTelegram();
            return step.ContainsKey(userKey);
        }

        #endregion
    }
}
