using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик slash команд.
    /// </summary>
    public sealed class SlashMessageUpdateHandler : MessageCommandUpdateHandler
    {
        #region Поля и свойства

        public override MessageType TypeMessage => MessageType.Text;

        #endregion

        #region Методы

        public override async Task<UpdateResult> Handle(Update update)
        {
            try
            {
                string command = update.Message.Text;
                if (command.StartsWith('/'))
                {
                    var resultExecute = StartHasDeepLink(command, update);
                    if (resultExecute == CommandResult.Executed)
                        return UpdateResult.Handled;

                    bot.Events.MessageEvents.OnPreSlashCommandHandleInvoke(new BotEventArgs(bot, update));
                    resultExecute = await ExecuteCommand(command, update, commands);
                    if (resultExecute != CommandResult.Continue)
                    {
                        bot.Events.MessageEvents.OnPostSlashCommandHandleInvoke(new BotEventArgs(bot, update));
                        return UpdateResult.Handled;
                    }
                }
                return UpdateResult.Continue;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
                return UpdateResult.Error;
            }
        }

        public override bool AddCommand(string command, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            if (!command.StartsWith('/'))
                command = "/" + command;

            return base.AddCommand(command, @delegate);
        }

        protected override void RegisterCommands()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticSlashCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(SlashHandlerAttribute), methods, commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(SlashHandlerAttribute), methodsInClass, commands, bot.Options.ServiceProvider);
            }
        }

        /// <summary>
        /// Проверка является ли команда start с аргументом.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнение команд.</returns>
        private CommandResult StartHasDeepLink(string command, Update update)
        {
            try
            {
                if (!command.ToLower().Contains("start") && command.Contains(" "))
                    return CommandResult.Continue;

                var spl = command.Split(' ');
                if (spl.Length < 2 || string.IsNullOrEmpty(spl[1]))
                    return CommandResult.Continue;

                bot.Events.OnUserStartWithArgsInvoke(new StartEventArgs(bot, update, spl[1]));
                return CommandResult.Executed;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
                return CommandResult.Error;
            }
        }

        /// <summary>
        /// Внутрення проверка для <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения проверки.</returns>
        protected override async Task<InternalCheckResult> InternalCheck(Update update, CommandHandler handler)
        {
            var currentCheckers = bot.Options.CommandCheckers.Where(x => x.CommandTypes.Contains(CommandType.Slash));
            if (currentCheckers.Any())
            {
                foreach (var commandChecker in currentCheckers)
                {
                   var result = await commandChecker.Checker.Check(bot, update, handler);
                   if (result != InternalCheckResult.Passed)
                       return result;
                }
            }
            return await base.InternalCheck(update, handler); 
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public SlashMessageUpdateHandler(PRBotBase bot)
            : base(bot)
        {
            RegisterCommands();
        }

        #endregion
    }
}
