using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services
{
    public sealed class RegisterCommandService
    {
        #region Методы

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bot"></param>
        /// <param name="attributetype"></param>
        /// <param name="methods"></param>
        /// <param name="commands"></param>
        /// <param name="serviceProvider"></param>
        public void RegisterMethodFromClass<T>(PRBot bot, Type attributetype, MethodInfo[] methods, Dictionary<T, CommandHandler> commands, IServiceProvider serviceProvider)
        {
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);

                    if (attribute == null || ((IBaseQueryAttribute)attribute).BotId != bot.Options.BotId && ((IBaseQueryAttribute)attribute).BotId != -1)
                        continue;

                    bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                    if (!isValidMethod)
                    {
                        bot.InvokeErrorLog(new Exception($"The method {method.Name} has an invalid signature. " +
                            $"Required return {nameof(Task)} arg1 {nameof(ITelegramBotClient)} arg2 {nameof(Update)}"));
                        continue;
                    }

                    var telegramhandler = HandlerFactory.CreateHandler((IBaseQueryAttribute)attribute, method, serviceProvider);
                    foreach (var command in ((ICommandStore<T>)attribute).Commands)
                        commands.Add(command, telegramhandler);
                }
                catch (Exception ex)
                {
                    bot.InvokeErrorLog(ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bot"></param>
        /// <param name="attributetype"></param>
        /// <param name="methods"></param>
        /// <param name="commands"></param>
        public void RegisterCommand<T>(PRBot bot, Type attributetype, MethodInfo[] methods, Dictionary<T, CommandHandler> commands)
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

                    foreach (var command in ((ICommandStore<T>)attribute).Commands)
                    {
                        bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                        if (!isValidMethod)
                        {
                            bot.InvokeErrorLog(new Exception($"The method {method.Name} has an invalid signature for the {attribute.GetType()} attribute. The method will be ignored."));
                            continue;
                        }

                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), method, false);
                        var telegramCommand = HandlerFactory.CreateHandler((IBaseQueryAttribute)attribute, (Func<ITelegramBotClient, Update, Task>)serverMessageHandler, null);
                        commands.Add(command, telegramCommand);
                    }
                }
                catch (Exception ex)
                {
                    bot.InvokeErrorLog(ex);
                }
            }
        }

        #endregion
    }
}
