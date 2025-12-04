using PRTelegramBot.Attributes;
using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using System.Reflection;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class SlashCommandHandler : IMessageCommandHandler
    {
        #region IMessageCommandHandler

        /// <inheritdoc />
        public async Task<UpdateResult> Handle(IBotContext context, Message updateType)
        {
            string command = context.Update.Message.Text;
            if (command.StartsWith('/'))
            {
                var resultExecute = StartHasDeepLink(context, command);

                var executer = new ExecutorSlashCommand(context.Current);
                var currentHandler = context.Current.Handler as Handler;
                if (currentHandler is null)
                    return UpdateResult.Continue;

                var executeMethod = executer.GetExecuteHandlerOrNull(command, context, currentHandler.SlashCommandsStore.Commands);
                if (executeMethod == null)
                    return UpdateResult.NotFound;

                var attr = executeMethod.Method.GetCustomAttribute<SlashHandlerAttribute>();
                if(attr.SplitChar != default)
                {
                    var spl = command.Split(attr.SplitChar);
                    if (spl.Length > 1)
                        context.SetCustomData(spl.Skip(1).ToList());
                }

                context.Current.Events.CommandsEvents.OnPreSlashCommandHandleInvoke(context.CreateBotEventArgs());

                resultExecute = await executer.Execute(context, executeMethod);

                if (resultExecute != CommandResult.Continue)
                {
                    context.Current.Events.CommandsEvents.OnPostSlashCommandHandleInvoke(context.CreateBotEventArgs());
                    return UpdateResult.Handled;
                }
            }
            return UpdateResult.Continue;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверка является ли команда start с аргументом.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="command">Команда.</param>
        /// <returns>Результат выполнение команд.</returns>
        private CommandResult StartHasDeepLink(IBotContext context, string command)
        {
            try
            {
                if (!command.ToLower().Contains("start") && command.Contains(" "))
                    return CommandResult.Continue;

                var spl = command.Split(' ');
                if (spl.Length < 2 || string.IsNullOrEmpty(spl[1]))
                    return CommandResult.Continue;

                context.Current.Events.OnUserStartWithArgsInvoke(new StartEventArgs(context, spl[1]));
                return CommandResult.Executed;
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
                return CommandResult.Error;
            }
        }

        #endregion
    }
}
