using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the InlineCallback converter.
    /// </summary>
    public interface IInlineMenuConverter
    {
        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <param name="callbackData">
        /// The data sent to the bot when the button is pressed.
        /// Maximum length: 1–64 bytes.
        /// Typically used to identify the command or to pass arguments.
        /// </param>
        /// <returns>InlineCallback, or null.</returns>
        InlineCallback GetCommandByCallbackOrNull(string callbackData);

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="callbackData">
        /// The data sent to the bot when the button is pressed.
        /// Maximum length: 1–64 bytes.
        /// Typically used to identify the command or to pass arguments.
        /// </param>
        /// <returns>InlineCallback, or null.</returns>
        InlineCallback<T> GetCommandByCallbackOrNull<T>(string callbackData)
            where T : TCommandBase;

        /// <summary>
        /// Generates the callbackData from an InlineCallback.
        /// </summary>
        /// <param name="inlineCallback">The button that handles the data.</param>
        /// <returns>The converted data-handling button.</returns>
        string GenerateCallbackData(InlineCallback inlineCallback);

        /// <summary>
        /// Generates the callbackData from an InlineCallback.
        /// </summary>
        /// <typeparam name="T">Button type.</typeparam>
        /// <param name="inlineCallback">The button that handles the data.</param>
        /// <returns>The converted data-handling button.</returns>
        string GenerateCallbackData<T>(InlineCallback<T> inlineCallback)
            where T : TCommandBase;
    }
}
