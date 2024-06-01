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
                if (command.StartsWith("/"))
                {
                    var resultExecute = await ExecuteCommand(command, update, commands);
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
            if (!command.StartsWith("/"))
                command = "/" + command;

            return base.AddCommand(command, @delegate);
        }

        protected override void RegisterCommands()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticSlashCommandHandlers(bot.Options.BotId);
            registerService.RegisterCommand(bot, typeof(SlashHandlerAttribute), methods, commands);

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

        private async Task<ResultUpdate> StartHasDeepLink(string command, Update update)
        {
            try
            {
                if (!command.ToLower().Contains("start") && command.Contains(" "))
                    return ResultUpdate.NotFound;

                var spl = command.Split(' ');
                if (spl.Length < 2 || string.IsNullOrEmpty(spl[1]))
                    return ResultUpdate.Continue;

                bot.Events.OnUserStartWithArgsInvoke(new StartEventArgs(bot, update, spl[1]));
                return ResultUpdate.Handled;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return ResultUpdate.Error;
            }
        }

        #endregion

        #region Конструкторы

        public SlashMessageUpdateHandler(PRBot bot, IServiceProvider serviceProvider)
            : base(bot, serviceProvider)
        {
            RegisterCommands();
        }

        #endregion
    }
}
