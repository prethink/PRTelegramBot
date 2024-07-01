using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик сообщений. 
    /// </summary>
    public abstract class MessageCommandUpdateHandler : CommandUpdateHandler<string>
    {
        #region Поля и свойства

        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public abstract MessageType TypeMessage { get; }

        #endregion

        #region Базовый класс

        public override UpdateType TypeUpdate => UpdateType.Message;

        protected override bool CanExecute(string currentCommand, string command, CommandHandler handler)
        {
            if (handler.CommandComparison == CommandComparison.Equals)
            {
                if (handler is StringCommandHandler stringHandler && currentCommand.Equals(command, stringHandler.StringComparison))
                    return true;
            }
            else if (handler.CommandComparison == CommandComparison.Contains)
            {
                if (handler is StringCommandHandler stringHandler && currentCommand.Contains(command, stringHandler.StringComparison))
                    return true;
            }
            else
            {
                throw new NotImplementedException();
            }
            return false;
        }

        public override bool AddCommand(string command, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            try
            {
                commands.Add(command, new CommandHandler(@delegate));
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
        }

        public override bool RemoveCommand(string command)
        {
            try
            {
                commands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        protected MessageCommandUpdateHandler(PRBotBase bot)
            : base(bot) { }

        #endregion

    }
}
