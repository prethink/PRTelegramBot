using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Factories;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;
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
            where Tkey : notnull
        {
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);

                    if (attribute is null || !((IBaseQueryAttribute)attribute).BotIds.Contains(bot.Options.BotId) && !((IBaseQueryAttribute)attribute).BotIds.Contains(PRConstants.ALL_BOTS_ID))
                        continue;

                    bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(bot, method);
                    if (!isValidMethod)
                    {
                        var exception = new InvalidOperationException($"The method {method.Name} has an invalid signature. " +
                            $"Required return {nameof(Task)} arg1 {nameof(ITelegramBotClient)} arg2 {nameof(Update)}");
                        bot.GetLogger<MethodRegistrar>().LogErrorInternal(exception);
                        continue;
                    }

                    var telegramhandler = new HandlerFactory().CreateHandler((IBaseQueryAttribute)attribute, method, bot);
                    foreach (var command in ((ICommandStore<Tkey>)attribute).Commands)
                        commands.Add(command, telegramhandler);
                }
                catch (Exception ex)
                {
                    bot.GetLogger<MethodRegistrar>().LogErrorInternal(ex);
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
            where Tkey : notnull
        {
            foreach (var method in methods)
            {
                try
                {
                    if (!method.IsStatic)
                        continue;

                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);
                    if (attribute is null)
                        continue;

                    foreach (var command in ((ICommandStore<Tkey>)attribute).Commands)
                    {
                        bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(bot, method);
                        if (!isValidMethod)
                        {
                            var exception = new InvalidOperationException($"The method {method.Name} has an invalid signature for the {attribute.GetType()} attribute. The method will be ignored.");
                            bot.GetLogger<PRBackgroundTaskRunner>().LogErrorInternal(exception);
                            continue;
                        }

                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<IBotContext, Task>), method, false);
                        var telegramCommand = new HandlerFactory().CreateHandler((IBaseQueryAttribute)attribute, (Func<IBotContext, Task>)serverMessageHandler, null);
                        commands.Add(command, telegramCommand);
                    }
                }
                catch (Exception ex)
                {
                    bot.GetLogger<MethodRegistrar>().LogErrorInternal(ex);
                }
            }
        }

        #endregion
    }
}
