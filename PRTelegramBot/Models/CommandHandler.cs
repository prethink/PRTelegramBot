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
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Update.</param>
        public async Task ExecuteCommand(ITelegramBotClient botClient, Update update)
        {
            if (Method == null)
                return;

            if (Method.IsStatic)
            {
                Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), Method, false);
                await ((Func<ITelegramBotClient, Update, Task>)serverMessageHandler).Invoke(botClient, update);
            }
            else
            {
                if (serviceProvider != null)
                {
                    var factory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                    using (var scope = factory.CreateScope())
                    {
                        var instance = scope.ServiceProvider.GetRequiredService(Method.DeclaringType);
                        var instanceMethod = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), instance, Method);
                        await (((Func<ITelegramBotClient, Update, Task>)instanceMethod)).Invoke(botClient, update);
                    }
                }
                else
                {
                    var instance = ReflectionUtils.CreateInstanceWithNullArguments(Method.DeclaringType);
                    var instanceMethod = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), instance, Method);
                    await (((Func<ITelegramBotClient, Update, Task>)instanceMethod)).Invoke(botClient, update);
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
