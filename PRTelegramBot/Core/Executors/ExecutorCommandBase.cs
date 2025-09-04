using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Executors
{
    /// <summary>
    /// Базовый исполнитель команд.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа для команд.</typeparam>
    internal abstract class ExecutorCommandBase<TKey>
    {
        #region Поля и свойства

        /// <summary>
        /// Telegram bot.
        /// </summary>
        protected PRBotBase bot;

        /// <summary>
        /// Тип команд.
        /// </summary>
        public abstract CommandType CommandType { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить команду.
        /// </summary>
        /// <param name="command">Команда для выполения.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="commands">Команды.</param>
        /// <returns>Результат выполнения команды.</returns>
        public async Task<CommandResult> Execute(TKey command, Update update, Dictionary<TKey, CommandHandler> commands)
        {
            foreach (var commandExecute in commands.OrderByDescending(x => x.Value.CommandComparison == CommandComparison.Equals))
            {
                if (CanExecute(command, commandExecute.Key, commandExecute.Value))
                    return await ExecuteMethod(bot, update, commandExecute.Value);
            }
            return CommandResult.Continue;
        }

        /// <summary>
        /// Выполнить метод.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения команды.</returns>
        public virtual async Task<CommandResult> ExecuteMethod(PRBotBase bot, Update update, CommandHandler handler)
        {
            var result = await InternalCheck(bot, update, handler);
            if (result != InternalCheckResult.Passed)
                return CommandResult.InternalCheck;

            await handler.ExecuteCommand(bot.BotClient, update);
            return CommandResult.Executed;
        }

        /// <summary>
        /// Внутрення проверка для <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения проверки.</returns>
        protected abstract Task<InternalCheckResult> InternalCheck(PRBotBase bot, Update update, CommandHandler handler);

        /// <summary>
        /// Можно ли выполнить команду.
        /// </summary>
        /// <param name="currentCommand">Текущая команда.</param>
        /// <param name="commandFromCollection">Команда из коллекции.</param>
        /// <param name="handler">Обработчик команды.</param>
        /// <returns>True - можно выполнить команду, False - нельзя выполнить команду.</returns>
        protected abstract bool CanExecute(TKey currentCommand, TKey commandFromCollection, CommandHandler handler);

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public ExecutorCommandBase(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
