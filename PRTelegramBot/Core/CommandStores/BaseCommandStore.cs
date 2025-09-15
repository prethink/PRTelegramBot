using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Registrars;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Базовый класс хранилища команд.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа для команды.</typeparam>
    public abstract class BaseCommandStore<TKey>
        where TKey : notnull
    {
        #region Поля и свойства

        /// <summary>
        /// Количество команд.
        /// </summary>
        public long CommandCount => Commands.Count;

        /// <summary>
        /// Команды.
        /// </summary>
        public Dictionary<TKey, CommandHandler> Commands { get; private set; } = new();

        /// <summary>
        /// Сервис регистрации команд.
        /// </summary>
        protected MethodRegistrar registerService = new MethodRegistrar();

        /// <summary>
        /// Telegram bot.
        /// </summary>
        protected PRBotBase bot;

        #endregion

        #region Методы

        /// <summary>
        /// Очистить список команд.
        /// </summary>
        public void ClearCommands()
        {
            Commands.Clear();
        }

        /// <summary>
        /// Зарегистрировать команды.
        /// </summary>
        public abstract void RegisterCommand();

        /// <summary>
        /// Добавить новую команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="delegate">Метод обработки команды.</param>
        /// <returns>True - команда добавлена, False - не удалось добавить команду.</returns>
        public abstract bool AddCommand(TKey command, Func<IBotContext, Task> @delegate);

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
        /// <param name="bot">Бот.</param>
        protected BaseCommandStore(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
