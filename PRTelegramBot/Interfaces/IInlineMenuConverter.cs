using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс конвертера InlineCallback.
    /// </summary>
    public interface IInlineMenuConverter
    {
        /// <summary>
        /// Преобразовать данные в команду.
        /// </summary>
        /// <param name="callbackData">
        /// Данные, которые будут отправлены боту при нажатии на кнопку.
        /// Максимальная длина: 1–64 байта.
        /// Обычно используется для идентификации команды или передачи аргументов.
        /// </param>
        /// <returns>InlineCallback или null.</returns>
        InlineCallback GetCommandByCallbackOrNull(string callbackData);

        /// <summary>
        /// Преобразовать данные в команду.
        /// </summary>
        /// <typeparam name="T">Тип команды.</typeparam>
        /// <param name="callbackData">
        /// Данные, которые будут отправлены боту при нажатии на кнопку.
        /// Максимальная длина: 1–64 байта.
        /// Обычно используется для идентификации команды или передачи аргументов.
        /// </param>
        /// <returns>InlineCallback или null.</returns>
        InlineCallback<T> GetCommandByCallbackOrNull<T>(string callbackData)
            where T : TCommandBase;

        /// <summary>
        /// Сгенерировать callbackData из InlineCallback.
        /// </summary>
        /// <param name="inlineCallback">Кнопка обработки данных.</param>
        /// <returns>Сконвертированная кнопка обработки данных.</returns>
        string GenerateCallbackData(InlineCallback inlineCallback);

        /// <summary>
        /// Сгенерировать callbackData из InlineCallback.
        /// </summary>
        /// <typeparam name="T">Тип кнопки.</typeparam>
        /// <param name="inlineCallback">Кнопка обработки данных.</param>
        /// <returns>Сконвертированная кнопка обработки данных.</returns>
        string GenerateCallbackData<T>(InlineCallback<T> inlineCallback)
            where T : TCommandBase;
    }
}
