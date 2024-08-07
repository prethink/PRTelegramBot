﻿using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Хранилище callbackquery команд.
    /// </summary>
    public sealed class CallbackQueryCommandStore : BaseCommandStore<Enum>
    {
        #region Базовый класс

        /// <summary>
        /// Зарегистрировать команды.
        /// </summary>
        public override void RegisterCommand()
        {
            ReflectionUtils.FindEnumHeaders();
            MethodInfo[] methods = ReflectionUtils.FindStaticInlineCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(InlineCallbackHandlerAttribute<>), methods, Commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(InlineCallbackHandlerAttribute<>), methodsInClass, Commands, bot.Options.ServiceProvider);
            }
        }

        /// <summary>
        /// Добавить новую команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="delegate">Метод обработки команды.</param
        /// <returns>True - команда добавлена, False - не удалось добавить команду.</returns>
        public override bool AddCommand(Enum command, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            try
            {
                ReflectionUtils.AddEnumsHeader(command);
                Commands.Add(command, new CommandHandler(@delegate));
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Удалить команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <returns>True - команда удалена, False - не удалось удалить команду.</returns>
        public override bool RemoveCommand(Enum command)
        {
            try
            {
                Commands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public CallbackQueryCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
