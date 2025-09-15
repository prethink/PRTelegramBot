using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;

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

        /// <inheritdoc />
        public bool AddSlashCommand(string command, Func<IBotContext, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.SlashCommandsStore.AddCommand(command, method);
            else
                return false;

        }

        /// <inheritdoc />
        public bool AddReplyCommand(string command, Func<IBotContext, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.ReplyCommandsStore.AddCommand(command, method);
            else
                return false;
        }

        /// <inheritdoc />
        public bool AddInlineCommand(Enum command, Func<IBotContext, Task> method)
        {
            if (bot.Handler is Handler handler)
                return handler.CallbackQueryCommandsStore.AddCommand(command, method);
            else
                return false;
        }

        /// <inheritdoc />
        public bool RemoveReplyCommand(string command)
        {
            if (bot.Handler is Handler handler)
                return handler.ReplyCommandsStore.RemoveCommand(command);
            else
                return false;
        }

        /// <inheritdoc />
        public bool RemoveSlashCommand(string command)
        {
            if (bot.Handler is Handler handler)
                return handler.SlashCommandsStore.RemoveCommand(command);
            else
                return false;
        }

        /// <inheritdoc />
        public bool RemoveInlineCommand(Enum command)
        {
            if (bot.Handler is Handler handler)
                return handler.CallbackQueryCommandsStore.RemoveCommand(command);
            else
                return false;
        }

        /// <inheritdoc />
        public void Init(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
