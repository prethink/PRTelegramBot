using PRTelegramBot.Core;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Registrars
{
    public class RegisterCommand : IRegisterCommand
    {
        #region Поля и свойства

        /// <summary>
        /// Бот.
        /// </summary>
        private PRBotBase bot;

        #endregion

        #region IRegisterCommand

        public bool AddSlashCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.MessageFacade.SlashHandler.AddCommand(command, method);
            else
                return false;

        }

        public bool AddReplyCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.MessageFacade.ReplyHandler.AddCommand(command, method);
            else
                return false;
        }

        public bool AddInlineCommand(Enum command, Func<ITelegramBotClient, Update, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.InlineUpdateHandler.AddCommand(command, method);
            else
                return false;
        }

        public bool RemoveReplyCommand(string command)
        {
            if (bot.Handler is Handler handler)
                return handler.MessageFacade.ReplyHandler.RemoveCommand(command);
            else
                return false;
        }

        public bool RemoveSlashCommand(string command)
        {
            if (bot.Handler is Handler handler)
                return handler.MessageFacade.SlashHandler.RemoveCommand(command);
            else
                return false;
        }

        public bool RemoveInlineCommand(Enum command)
        {
            if (bot.Handler is Handler handler)
                return handler.InlineUpdateHandler.RemoveCommand(command);
            else
                return false;
        }

        public void Init(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
