using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    public abstract class MessageCommandUpdateHandler : CommandUpdateHandler<string>
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public abstract MessageType TypeMessage { get; }

        public override UpdateType TypeUpdate => UpdateType.Message;

        #region События

        /// <summary>
        /// Событие когда не найдена команда
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnMissingCommand;

        /// <summary>
        /// Событие когда указан не верный тип сообщения
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnWrongTypeMessage;

        #endregion

        #region Методы

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
                bot.InvokeErrorLog(ex);
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
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        #endregion


        #region Конструкторы

        protected MessageCommandUpdateHandler(PRBot bot, IServiceProvider serviceProvider)
            : base(bot, serviceProvider)
        {
        }

        #endregion

    }
}
