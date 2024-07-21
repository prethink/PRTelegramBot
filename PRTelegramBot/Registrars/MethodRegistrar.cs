using PRTelegramBot.Core;
using PRTelegramBot.Core.Factories;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Registrars
{
    /// <summary>
    /// Класс для регистрации методов для дальнейшей обработки команд.
    /// </summary>
    public sealed class MethodRegistrar
    {
        #region Методы

        /// <summary>
        /// Регистрация методов из классов.
        /// </summary>
        /// <typeparam name="Tkey">Тип для команды.</typeparam>
        /// <param name="bot">Бот.</param>
        /// <param name="attributetype">Тип атрибута.</param>
        /// <param name="methods">Методы.</param>
        /// <param name="commands">Команды.</param>
        /// <param name="serviceProvider">Сервис провайдер.</param>
        public void RegisterMethodFromClass<Tkey>(PRBotBase bot, Type attributetype, MethodInfo[] methods, Dictionary<Tkey, CommandHandler> commands, IServiceProvider serviceProvider)
        {
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);

                    if (attribute == null || !((IBaseQueryAttribute)attribute).BotIds.Contains(bot.Options.BotId) && !((IBaseQueryAttribute)attribute).BotIds.Contains(-1))
                        continue;

                    bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                    if (!isValidMethod)
                    {
                        bot.Events.OnErrorLogInvoke(new Exception($"The method {method.Name} has an invalid signature. " +
                            $"Required return {nameof(Task)} arg1 {nameof(ITelegramBotClient)} arg2 {nameof(Update)}"));
                        continue;
                    }

                    var telegramhandler = new HandlerFactory().CreateHandler((IBaseQueryAttribute)attribute, method, serviceProvider);
                    foreach (var command in ((ICommandStore<Tkey>)attribute).Commands)
                        commands.Add(command, telegramhandler);
                }
                catch (Exception ex)
                {
                    bot.Events.OnErrorLogInvoke(ex);
                }
            }
        }

        /// <summary>
        /// Регистрация статических команд.
        /// </summary>
        /// <typeparam name="Tkey">Тип для команды.</typeparam>
        /// <param name="bot">Бот.</param>
        /// <param name="attributetype">Тип атрибута.</param>
        /// <param name="methods">Методы.</param>
        /// <param name="commands">Команды.</param>
        public void RegisterStaticCommand<Tkey>(PRBotBase bot, Type attributetype, MethodInfo[] methods, Dictionary<Tkey, CommandHandler> commands)
        {
            foreach (var method in methods)
            {
                try
                {
                    if (!method.IsStatic)
                        continue;

                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);
                    if (attribute == null)
                        continue;

                    foreach (var command in ((ICommandStore<Tkey>)attribute).Commands)
                    {
                        bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                        if (!isValidMethod)
                        {
                            bot.Events.OnErrorLogInvoke(new Exception($"The method {method.Name} has an invalid signature for the {attribute.GetType()} attribute. The method will be ignored."));
                            continue;
                        }

                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), method, false);
                        var telegramCommand = new HandlerFactory().CreateHandler((IBaseQueryAttribute)attribute, (Func<ITelegramBotClient, Update, Task>)serverMessageHandler, null);
                        commands.Add(command, telegramCommand);
                    }
                }
                catch (Exception ex)
                {
                    bot.Events.OnErrorLogInvoke(ex);
                }
            }
        }

        #endregion
    }
}
