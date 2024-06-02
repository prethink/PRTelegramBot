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

        public override async Task<ResultUpdate> Handle(Update update)
        {
            try
            {
                string command = update.Message.Text;
                if (command.StartsWith('/'))
                {
                    var resultExecute = await StartHasDeepLink(command, update);
                    if (resultExecute == ResultCommand.Executed)
                        return ResultUpdate.Handled;

                    resultExecute = await ExecuteCommand(command, update, commands);
                    if (resultExecute != ResultCommand.Continue)
                        return ResultUpdate.Handled;
                }
                return ResultUpdate.Continue;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return ResultUpdate.Error;
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
                registerService.RegisterMethodFromClass(bot, typeof(SlashHandlerAttribute), methodsInClass, commands, serviceProvider);
            }
        }

        protected override ResultCommand InternalCheck(Update update, CommandHandler handler)
        {
            return ResultCommand.Continue;
        }

        /// <summary>
        /// Проверка является ли команда start с аргументом.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнение команд.</returns>
        private async Task<ResultCommand> StartHasDeepLink(string command, Update update)
        {
            try
            {
                if (!command.ToLower().Contains("start") && command.Contains(" "))
                    return ResultCommand.Continue;

                var spl = command.Split(' ');
                if (spl.Length < 2 || string.IsNullOrEmpty(spl[1]))
                    return ResultCommand.Continue;

                bot.Events.OnUserStartWithArgsInvoke(new StartEventArgs(bot, update, spl[1]));
                return ResultCommand.Executed;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return ResultCommand.Error;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="serviceProvider">Сервис провайдер.</param>
        public SlashMessageUpdateHandler(PRBot bot, IServiceProvider serviceProvider)
            : base(bot, serviceProvider)
        {
            RegisterCommands();
        }

        #endregion
    }
}
