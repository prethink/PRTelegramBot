using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Registrars
{
    /// <summary>
    /// Регистрация новых команд с помощью кода в процессе работы бота.
    /// </summary>
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
                return handler.SlashCommandsStore.AddCommand(command, method);
            else
                return false;

        }

        public bool AddReplyCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.ReplyCommandsStore.AddCommand(command, method);
            else
                return false;
        }

        public bool AddInlineCommand(Enum command, Func<ITelegramBotClient, Update, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.CallbackQueryCommandsStore.AddCommand(command, method);
            else
                return false;
        }

        public bool RemoveReplyCommand(string command)
        {
            if (bot.Handler is Handler handler)
                return handler.ReplyCommandsStore.RemoveCommand(command);
            else
                return false;
        }

        public bool RemoveSlashCommand(string command)
        {
            if (bot.Handler is Handler handler)
                return handler.SlashCommandsStore.RemoveCommand(command);
            else
                return false;
        }

        public bool RemoveInlineCommand(Enum command)
        {
            if (bot.Handler is Handler handler)
                return handler.CallbackQueryCommandsStore.RemoveCommand(command);
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
