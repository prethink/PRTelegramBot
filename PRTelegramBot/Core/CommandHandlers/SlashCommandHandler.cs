using PRTelegramBot.Core.Executors;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class SlashCommandHandler : IMessageCommandHandler
    {
        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, Message updateType)
        {
            string command = update.Message.Text;
            if (command.StartsWith('/'))
            {
                var resultExecute = StartHasDeepLink(bot, command, update);
                if (resultExecute == CommandResult.Executed)
                    return UpdateResult.Handled;

                bot.Events.CommandsEvents.OnPreSlashCommandHandleInvoke(new BotEventArgs(bot, update));

                var executer = new ExecutorSlashCommand(bot);
                var currentHandler = bot.Handler as Handler;
                if (currentHandler == null)
                    return UpdateResult.Continue;

                resultExecute = await executer.Execute(command, update, currentHandler.SlashCommandsStore.Commands);

                if (resultExecute != CommandResult.Continue)
                {
                    bot.Events.CommandsEvents.OnPostSlashCommandHandleInvoke(new BotEventArgs(bot, update));
                    return UpdateResult.Handled;
                }
            }
            return UpdateResult.Continue;
        }

        #region Методы

        /// <summary>
        /// Проверка является ли команда start с аргументом.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнение команд.</returns>
        private CommandResult StartHasDeepLink(PRBotBase bot, string command, Update update)
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

        #endregion
    }
}
