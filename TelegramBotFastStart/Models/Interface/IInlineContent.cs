namespace PRTelegramBot.Models.Interface
{
    /// <summary>
    /// Общий интерфейс для Inline кнопок
    /// </summary>
    public interface IInlineContent
    {
        /// <summary>
        /// Получает название кнопки
        /// </summary>
        /// <returns>Название кнопки</returns>
        public string GetTextButton();

        /// <summary>
        /// Получает контент
        /// </summary>
        /// <returns>Контент кнопки</returns>
        public object GetContent();
    }
}
