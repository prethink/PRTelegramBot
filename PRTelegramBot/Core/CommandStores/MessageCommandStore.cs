using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Базовый класс хранилища для команд типа message.
    /// </summary>
    public abstract class MessageCommandStore : BaseCommandStore<string>
    {
        #region Методы

        /// <summary>
        /// Добавить новую команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="delegate">Метод обработки команды.</param>
        /// <returns>True - команда добавлена, False - не удалось добавить команду.</returns>
        public override bool AddCommand(string command, Func<IBotContext, Task> @delegate)
        {
            try
            {
                Commands.Add(command, new CommandHandler(@delegate, bot));
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, ex));
                return false;
            }
        }

        /// <summary>
        /// Удалить команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <returns>True - команда удалена, False - не удалось удалить команду.</returns>
        public override bool RemoveCommand(string command)
        {
            try
            {
                Commands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, ex));
                return false;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public MessageCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
