using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

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
        /// Команда.
        /// </summary>
        public Func<ITelegramBotClient, Update, Task> Command { get; }

        /// <summary>
        /// Сервис провайдер.
        /// </summary>
        private IServiceProvider serviceProvider { get; set; }

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
        /// <param name="method">Метод.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(MethodInfo method, IServiceProvider ServiceProvider, CommandComparison commandComparison)
        {
            this.serviceProvider = ServiceProvider;
            this.CommandComparison = commandComparison;

            if (method.IsStatic)
            {
                Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), method, false);
                Command = (Func<ITelegramBotClient, Update, Task>)serverMessageHandler;
            }
            else
            {
                object instance = null;
                if (this.serviceProvider != null)
                {
                    var factory = this.serviceProvider.GetRequiredService<IServiceScopeFactory>();
                    using (var scope = factory.CreateScope())
                    {
                        instance = scope.ServiceProvider.GetRequiredService(method.DeclaringType);
                    }
                }
                else
                {
                    instance = ReflectionUtils.CreateInstanceWithNullArguments(method.DeclaringType);
                }
                var instanceMethod = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), instance, method);
                Command = ((Func<ITelegramBotClient, Update, Task>)instanceMethod);
            }
            CommandComparison = commandComparison;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        public CommandHandler(Func<ITelegramBotClient, Update, Task> command) 
            : this (command, null, CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        public CommandHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider)
            : this(command, ServiceProvider, CommandComparison.Equals) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(Func<ITelegramBotClient, Update, Task> command, CommandComparison commandComparison)
            : this(command, null, commandComparison) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="ServiceProvider">Сервис провайдер.</param>
        /// <param name="commandComparison">Сравнение команд.</param>
        public CommandHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider, CommandComparison commandComparison)
        {
            this.serviceProvider = ServiceProvider;
            this.Command = command;
            this.CommandComparison = commandComparison;
        }

        #endregion
    }
}
