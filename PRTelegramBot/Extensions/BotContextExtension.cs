﻿using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для контекста бота.
    /// </summary>
    public static class BotContextExtension
    {
        #region UpdateExtension

        /// <summary>
        /// Получает идентификатор чата в зависимости от типа сообщений.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Идентификатор чата.</returns>
        /// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
        public static long GetChatId(this IBotContext context)
        {
            return context.Update.GetChatId();
        }

        /// <summary>
        /// Получает идентификатор в формате класса.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Идентификатор в формате класса</returns>
        public static ChatId GetChatIdClass(this IBotContext context)
        {
            return context.Update.GetChatIdClass();
        }

        /// <summary>
        /// Попытаться получить идентификатор чата.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <returns>True - удалось получить, false - нет.</returns>
        public static bool TryGetChatId(this IBotContext context, out long chatId)
        {
            return context.Update.TryGetChatId(out chatId);
        }

        /// <summary>
        /// Получает идентификатор сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Идентификатор сообщения.</returns>
        /// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
        public static int GetMessageId(this IBotContext context)
        {
            return context.Update.GetMessageId();
        }

        /// <summary>
        /// Является ли идентификатор пользователским чатом.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - да, False - нет.</returns>
        public static bool IsUserChatId(this IBotContext context)
        {
            return context.Update.IsUserChatId();
        }

        /// <summary>
        /// Информация о пользователе.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Информация о пользователе.</returns>
        public static string GetInfoUser(this IBotContext context)
        {
            return context.Update.GetInfoUser();
        }

        #endregion

        #region CacheExtension

        /// <summary>
        /// Создает кеш для пользователя.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Кэш.</returns>
        public static TCache CreateCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache
        {
            return context.Update.CreateCacheData<TCache>();
        }

        /// <summary>
        /// Получает существующий кэш или создает новый.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Кэш.</returns>
        /// <remarks>Если тип кэша отличается от существующего, будет создан кэш нового типа.</remarks>
        public static TCache GetOrCreate<TCache>(this IBotContext context) where TCache : ITelegramCache
        {
            return context.Update.GetOrCreate<TCache>();
        }

        /// <summary>
        /// Получает кэш пользователя.
        /// </summary>
        /// <typeparam name="TCache">Тип кэша.</typeparam>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Кэш.</returns>
        public static TCache GetCacheData<TCache>(this IBotContext context) where TCache : ITelegramCache
        {
            return context.Update.GetCacheData<TCache>();
        }

        /// <summary>
        /// Очищает кеш пользователя.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public static void ClearCacheData(this IBotContext context)
        {
            context.Update.ClearCacheData();
        }

        /// <summary>
        /// Проверяет существуют ли кеш данные пользователя.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - есть кэш, False - нет кэша.</returns>
        public static bool HasCacheData(this IBotContext context)
        {
            return context.Update.HasCacheData();
        }

        /// <summary>
        /// Полностью удаляет кэш пользователя из словаря.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public static void RemoveCacheData(this IBotContext context)
        {
            context.Update.RemoveCacheData();
        }

        #endregion

        #region StepExtension

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="command">Следующая команда которая должна быть выполнена.</param>
        public static void RegisterStepHandler(this IBotContext context, IExecuteStep command)
        {
            context.Update.RegisterStepHandler(command);
        }

        /// <summary>
        /// Получает обработчик или null пользователя.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>обработчик или null.</returns>
        public static TExecuteStep? GetStepHandler<TExecuteStep>(this IBotContext context) where TExecuteStep : IExecuteStep
        {
            return context.Update.GetStepHandler<TExecuteStep>();
        }

        /// <summary>
        /// Получить текущий обработчик шага.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Обработчик или null.</returns>
        public static IExecuteStep? GetStepHandler(this IBotContext context)
        {
            return context.Update.GetStepHandler();
        }

        /// <summary>
        /// Очищает шаги пользователя.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public static void ClearStepUserHandler(this IBotContext context)
        {
            context.Update.ClearStepUserHandler();
        }

        /// <summary>
        /// Проверяет есть ли шаг у пользователя.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - есть обработчик, False - нет обработчика.</returns>
        public static bool HasStepHandler(this IBotContext context)
        {
            return context.Update.HasStepHandler();
        }

        #endregion
    }
}
