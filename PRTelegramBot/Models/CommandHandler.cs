using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Общий обработчик команд.
    /// </summary>
    public class CommandHandler 
    {
        #region Поля и свойства

        /// <summary>
        /// Сравнение команд.
        /// </summary>
        public CommandComparison CommandComparison { get;}

        /// <summary>
        /// Сервис провайдер.
        /// </summary>
        private IServiceProvider serviceProvider { get; set; }

        /// <summary>
        /// Информация о методе.
        /// </summary>
        public MethodInfo Method { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить команду.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public async Task ExecuteCommand(IBotContext context)
        {
            if (Method is null)
                return;

            if (Method.IsStatic)
            {
                Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), Method, false);
                await ((Func<IBotContext, Task>)serverMessageHandler).Invoke(context);
            }
            else
            {
                if (CurrentScope.Services != null)
                {
                    var instance = CurrentScope.Services.GetRequiredService(Method.DeclaringType);
                    var instanceMethod = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), instance, Method);
                    await (((Func<IBotContext, Task>)instanceMethod)).Invoke(context);
                }
                else
                {
                    var instance = ReflectionUtils.CreateInstanceWithNullArguments(Method.DeclaringType);
                    var instanceMethod = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), instance, Method);
                    await (((Func<IBotContext, Task>)instanceMethod)).Invoke(context);
                }
            }
        }

        #endregion

        #region Конструкторы класса

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        public CommandHandler(MethodInfo method)
            : this(method, null , CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(MethodInfo method, CommandComparison commandComparison)
            : this(method, null, commandComparison) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        public CommandHandler(MethodInfo method, IServiceProvider ServiceProvider)
            : this(method, ServiceProvider , CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        public CommandHandler(Func<IBotContext, Task> command) 
            : this (command, null, CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        public CommandHandler(Func<IBotContext, Task> command, IServiceProvider ServiceProvider)
            : this(command, ServiceProvider, CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(Func<IBotContext, Task> command, CommandComparison commandComparison)
            : this(command, null, commandComparison) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(Func<IBotContext, Task> command, IServiceProvider ServiceProvider, CommandComparison commandComparison)
            : this(command.Method, ServiceProvider, commandComparison) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(MethodInfo method, IServiceProvider ServiceProvider, CommandComparison commandComparison)
        {
            this.serviceProvider = ServiceProvider;
            this.CommandComparison = commandComparison;
            this.Method = method;
        }

        #endregion
    }
}
