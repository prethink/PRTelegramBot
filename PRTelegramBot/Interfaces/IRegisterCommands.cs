using PRTelegramBot.Core;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс регистратора команд.
    /// </summary>
    public interface IRegisterCommand
    {
        /// <summary>
        /// Регистрация Slash command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool AddSlashCommand(string command, Func<IBotContext, Task> method);

        /// <summary>
        /// Регистрация Reply command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool AddReplyCommand(string command, Func<IBotContext, Task> method);

        /// <summary>
        /// Регистрация inline command
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="method">Метод</param>
        /// <returns>True - метод зарегистрирован, false - ошибка/не зарегистрирован</returns>
        public bool AddInlineCommand(Enum command, Func<IBotContext, Task> method);

        /// <summary>
        /// Удаление Reply команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveReplyCommand(string command);

        /// <summary>
        /// Удаление slash команды
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveSlashCommand(string command);

        /// <summary>
        /// Удаление inline команды
        /// </summary>
        /// <param name="command">перечисление команды</param>
        /// <returns>True - метод удален, false - ошибка</returns>
        public bool RemoveInlineCommand(Enum command);

        /// <summary>
        /// Инициализация.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public void Init(PRBotBase bot);
    }
}
