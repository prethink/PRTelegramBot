using PRTelegramBot.Models;

namespace PRTelegramBot.Utils
{
    public static class MessageUtils
    {
        /// <summary>
        /// Создает опции если option null
        /// </summary>
        /// <param name="option">опции</param>
        /// <returns>Экземпляр класс OptionMessage</returns>
        public static OptionMessage CreateOptionsIfNull(OptionMessage option = null)
        {
            if (option == null)
                option = new OptionMessage();
            return option;
        }
    }
}
