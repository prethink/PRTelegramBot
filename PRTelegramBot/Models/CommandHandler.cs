using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Core;
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
        /// Бот.
        /// </summary>
        private PRBotBase bot { get; set; }

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
                if (bot.HasServiceProvider)
                {
                    using(var scope = bot.CreateServiceScope())
                    {
                        var instance = scope.ServiceProvider.GetRequiredService(Method.DeclaringType);
                        var instanceMethod = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), instance, Method);
                        await (((Func<IBotContext, Task>)instanceMethod)).Invoke(context);
                    }
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
        /// <param name="bot">Бот.</param>
        /// <param name="bot">Бот.</param>
        public CommandHandler(MethodInfo method, PRBotBase bot)
            : this(method, bot , CommandComparison.Equals) { }

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
        /// <param name="bot">Бот.</param>
        public CommandHandler(Func<IBotContext, Task> command, PRBotBase bot)
            : this(command, bot, CommandComparison.Equals) { }

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
        /// <param name="bot">Бот.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(Func<IBotContext, Task> command, PRBotBase bot, CommandComparison commandComparison)
            : this(command.Method, bot, commandComparison) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="method">Метод.</param>
        /// <param name="bot">Бот.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(MethodInfo method, PRBotBase bot, CommandComparison commandComparison)
        {
            this.bot = bot;
            this.CommandComparison = commandComparison;
            this.Method = method;
        }

        #endregion
    }
}
