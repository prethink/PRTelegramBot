﻿using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using System.Reflection;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Строковый обработчик команд.
    /// </summary>
    public sealed class StringCommandHandler : CommandHandler
    {
        #region Поля и свойства

        /// <summary>
        /// Сравнение строк.
        /// </summary>
        public StringComparison StringComparison { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        public StringCommandHandler(MethodInfo method)
            : this(method, null, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public StringCommandHandler(MethodInfo method, CommandComparison commandComparison)
            : this(method, null, commandComparison, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        public StringCommandHandler(MethodInfo method, IServiceProvider ServiceProvider)
            : this(method, ServiceProvider, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        public StringCommandHandler(Func<IBotContext, Task> command)
            : this(command, null, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        public StringCommandHandler(Func<IBotContext, Task> command, IServiceProvider ServiceProvider)
            : this(command, ServiceProvider, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public StringCommandHandler(Func<IBotContext, Task> command, CommandComparison commandComparison) 
            : this(command,null, commandComparison, StringComparison.OrdinalIgnoreCase) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        /// <param name="stringComparison">Сравнение строк.</param>
        public StringCommandHandler(MethodInfo method, IServiceProvider ServiceProvider, CommandComparison commandComparison, StringComparison stringComparison) 
            : base(method, ServiceProvider, commandComparison)
        {
            this.StringComparison = stringComparison;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        /// <param name="stringComparison">Сравнение строк.</param>
        public StringCommandHandler(Func<IBotContext, Task> command, IServiceProvider ServiceProvider, CommandComparison commandComparison, StringComparison stringComparison)
            : base(command, ServiceProvider, commandComparison)
        {
            this.StringComparison = stringComparison;
        }

        #endregion
    }
}
