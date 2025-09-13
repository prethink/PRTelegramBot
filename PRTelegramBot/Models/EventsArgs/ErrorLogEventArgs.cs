﻿using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// Аргументы событий логирования ошибок.
    /// </summary>
    public class ErrorLogEventArgs : BotEventArgs
    {
        #region Поля и свойства

        /// <summary>
        /// Исключение.
        /// </summary>
        public Exception Exception { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Создать аргументы событий с ошибкой.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        /// <param name="exception">Исключение.</param>
        /// <returns></returns>
        public static ErrorLogEventArgs Create(PRBotBase bot, Exception exception)
        {
            return new ErrorLogEventArgs(new BotContext(bot), exception);
        }

        /// <summary>
        /// Создать аргументы событий с ошибкой.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        /// <param name="exception">Исключение.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        public static ErrorLogEventArgs Create(PRBotBase bot, Exception exception, CancellationToken cancellationToken)
        {
            return new ErrorLogEventArgs(new BotContext(bot, cancellationToken), exception);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="exception">Исключение.</param>
        public ErrorLogEventArgs(IBotContext context, Exception exception)
            : base(context)
        {
            this.Exception = exception;
        }

        #endregion
    }
}
