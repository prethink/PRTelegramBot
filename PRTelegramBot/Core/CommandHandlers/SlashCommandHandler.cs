using PRTelegramBot.Attributes;
using PRTelegramBot.Core.CommandStores;
using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
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
            string text = context.Update.Message.Text;
            if (text.StartsWith('/'))
            {
                var command = RemoveBotMention(text);

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
                        ((BotContext)context).SetCustomData(spl.Skip(1).ToList());
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

        #region Methods

        /// <summary>
        /// Strips the <c>@botusername</c> suffix Telegram appends to a command in a group chat.
        /// </summary>
        /// <param name="text">Raw message text.</param>
        /// <returns>The command with the mention removed.</returns>
        /// <remarks>
        /// In a group, tapping /get_3 in the command list sends "/get_3@cs2_server_bot". Splitting
        /// that on the argument separator yielded "3@cs2", "server" and "bot" instead of the single
        /// argument the command carries, so the mention has to come off before anything else reads
        /// the text. The suffix only ever sits on the first whitespace-delimited token, and any
        /// arguments that follow a space are left untouched.
        /// </remarks>
        internal static string RemoveBotMention(string text)
        {
            var separator = text.IndexOf(' ');
            var token = separator < 0 ? text : text.Substring(0, separator);

            var at = token.IndexOf('@');
            if (at < 0)
                return text;

            return token.Substring(0, at) + (separator < 0 ? string.Empty : text.Substring(separator));
        }

        /// <summary>
        /// Checks whether the command is start with an argument.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of executing the commands.</returns>
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
                context.Current.GetLogger<SlashCommandHandler>().LogErrorInternal(ex);
                return CommandResult.Error;
            }
        }

        #endregion
    }
}
