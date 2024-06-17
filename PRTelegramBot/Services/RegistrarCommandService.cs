using PRTelegramBot.Core;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace PRTelegramBot.Services
{
    public class RegisterCommands
    {
        private Handler handler;

        /// <summary>
        /// Регистрация Slash command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool AddSlashCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            return handler.MessageFacade.SlashHandler.AddCommand(command, method);
        }

        /// <summary>
        /// Регистрация Reply command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool AddReplyCommand(string command, Func<ITelegramBotClient, Update, Task> method)
        {
            return handler.MessageFacade.ReplyHandler.AddCommand(command, method);
        }

        /// <summary>
        /// Регистрация inline command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool AddInlineCommand(Enum command, Func<ITelegramBotClient, Update, Task> method)
        {
            return handler.InlineUpdateHandler.AddCommand(command, method);
        }

        /// <summary>
        /// Удаление Reply команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveReplyCommand(string command)
        {
            return handler.MessageFacade.ReplyHandler.RemoveCommand(command);
        }

        /// <summary>
        /// Удаление slash команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveSlashCommand(string command)
        {
            return handler.MessageFacade.SlashHandler.RemoveCommand(command);
        }

        /// <summary>
        /// Удаление inline команды
        /// </summary>
        /// <param name="command">перечисление команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveInlineCommand(Enum command)
        {
            return handler.InlineUpdateHandler.RemoveCommand(command);
        }

        public RegisterCommands(Handler handler)
        {
            this.handler = handler;
        }
    }
}
