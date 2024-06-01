using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    public abstract class CommandUpdateHandler<TKey> : ExecuteHandler<TKey>
    {
        #region Поля и свойства

        /// <summary>
        /// 
        /// </summary>
        public long CommandCount => commands.Count;

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<TKey, CommandHandler> commands { get; set; } = new();

        protected RegisterCommandService registerService = new RegisterCommandService();

        #endregion

        #region События

        /// <summary>
        /// Событие когда нужно проверить привилегии перед выполнением команды
        /// </summary>
        public event Func<ITelegramBotClient, Update, Func<ITelegramBotClient, Update, Task>, int?, Task>? OnCheckPrivilege;

        #endregion

        #region Методы


        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="update"></param>
        /// <param name="commandList"></param>
        /// <returns></returns>
        protected async Task<ResultCommand> ExecuteCommand(TKey command, Update update, Dictionary<TKey, CommandHandler> commandList)
        {
            foreach (var commandExecute in commandList.OrderByDescending(x => x.Value.CommandComparison == CommandComparison.Equals))
            {
                if (CanExecute(command, commandExecute.Key, commandExecute.Value))
                    return await ExecuteMethod(update, commandExecute.Value);
            }
            return ResultCommand.Continue;
        }

        /// <summary>
        /// Можно ли выполнить команду.
        /// </summary>
        /// <param name="currentCommand">Текущая команда.</param>
        /// <param name="commandFromCollection">Команда из коллекции.</param>
        /// <param name="handler">Обработчик команды.</param>
        /// <returns>True - можно выполнить команду, False - нельзя выполнить команду.</returns>
        protected abstract bool CanExecute(TKey currentCommand, TKey commandFromCollection, CommandHandler handler);

        /// <summary>
        /// Регистрация команд.
        /// </summary>
        protected abstract void RegisterCommands();

        /// <summary>
        /// Добавить новую команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="delegate">Метод обработки команды.</param
        /// <returns>True - команда добавлена, False - не удалось добавить команду.</returns>
        public abstract bool AddCommand(TKey command, Func<ITelegramBotClient, Update, Task> @delegate);

        /// <summary>
        /// Удалить команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <returns>True - команда удалена, False - не удалось удалить команду.</returns>
        public abstract bool RemoveCommand(TKey command);

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        /// <param name="serviceProvider">Сервис провайдер.</param>
        protected CommandUpdateHandler(PRBot bot, IServiceProvider serviceProvider)
            : base(bot)
        {
            this.serviceProvider = serviceProvider;
        }

        #endregion
    }
}
