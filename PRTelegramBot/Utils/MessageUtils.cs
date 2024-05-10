using PRTelegramBot.Models;

namespace PRTelegramBot.Utils
{
    public static class MessageUtils
    {
        /// <summary>
        /// Создает параместры если option null.
        /// </summary>
        /// <param name="option">Параметры.</param>
        /// <returns>Экземпляр класса OptionMessage.</returns>
        public static OptionMessage CreateOptionsIfNull(OptionMessage option = null)
        {
            if (option == null)
                option = new OptionMessage();
            return option;
        }
    }
}
