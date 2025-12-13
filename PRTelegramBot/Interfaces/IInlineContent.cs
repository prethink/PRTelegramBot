namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Общий интерфейс для Inline кнопок.
    /// </summary>
    public interface IInlineContent
    {
        /// <summary>
        /// Получает название кнопки.
        /// </summary>
        /// <returns>Название кнопки.</returns>
        public string GetButtonName();

        /// <summary>
        /// Установить новое значение кнопки.
        /// </summary>
        /// <returns>Название кнопки.</returns>
        public string SetButtonName(string name);

        /// <summary>
        /// Получает контент.
        /// </summary>
        /// <returns>Контент кнопки.</returns>
        public object GetContent();
    }
}
