using PRTelegramBot.Builders;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Утилита для работы с кастомным форматированием строк.
    /// Позволяет строить строки с позиционными аргументами и именованными токенами.
    /// </summary>
    public class StringCustomFormatter
    {
        #region Методы

        /// <summary>
        /// Создаёт новый билдера сообщения с указанным шаблоном.
        /// </summary>
        /// <param name="template">Шаблон строки с токенами, например "{QA} протестировал {PR}, {0}"</param>
        /// <returns>Экземпляр <see cref="MessageBuilder"/> для дальнейшего добавления аргументов и резолверов.</returns>
        public MessageBuilder Message(string template) => new MessageBuilder(template);

        #endregion
    }
}
