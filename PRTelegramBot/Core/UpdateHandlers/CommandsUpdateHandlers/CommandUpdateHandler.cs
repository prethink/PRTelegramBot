using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    /// <summary>
    /// Обработчик выполнение команд обновления.
    /// </summary>
    /// <typeparam name="TKey">Тип команды.</typeparam>
    public abstract class CommandUpdateHandler<TKey> : ExecuteHandler
    {
        #region Поля и свойства

        /// <summary>
        /// Количество команд.
        /// </summary>
        public long CommandCount => commands.Count;

        /// <summary>
        /// Команды.
        /// </summary>
        protected Dictionary<TKey, CommandHandler> commands { get; set; } = new();

        /// <summary>
        /// Сервис регистрации команд.
        /// </summary>
        protected RegisterCommandService registerService = new RegisterCommandService();

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить команды.
        /// </summary>
        /// <param name="command">Команда для выполения.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="commands">Команды.</param>
        /// <returns>Результат выполнения команды.</returns>
        protected async Task<CommandResult> ExecuteCommand(TKey command, Update update, Dictionary<TKey, CommandHandler> commands)
        {
            foreach (var commandExecute in commands.OrderByDescending(x => x.Value.CommandComparison == CommandComparison.Equals))
            {
                if (CanExecute(command, commandExecute.Key, commandExecute.Value))
                    return await ExecuteMethod(update, commandExecute.Value);
            }
            return CommandResult.Continue;
        }

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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        protected CommandUpdateHandler(PRBot bot)
            : base(bot) {}

        #endregion
    }
}
