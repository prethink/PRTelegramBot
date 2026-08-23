using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utilities for building inline buttons and menus.
    /// </summary>
    public class InlineUtils
    {
        /// <summary>
        /// Creates an inline button.
        /// </summary>
        /// <param name="inlineData">Inline button data.</param>
        /// <returns>Inline button.</returns>
        public static InlineKeyboardButton GetInlineButton(IInlineContent inlineData)
        {
            ArgumentNullException.ThrowIfNull(inlineData);

            // Every button in the library derives from InlineBase and already knows how to turn
            // itself into an InlineKeyboardButton. Dispatching through that method instead of a
            // switch over concrete types means a newly added button kind works here at once,
            // and a subclass that overrides the conversion is honoured.
            if (inlineData is InlineBase inlineBase)
                return inlineBase.GetInlineButton();

            throw new NotImplementedException($"{inlineData.GetType()} is not implemented yet.");
        }

        /// <summary>
        /// Merges several inline menus into one.
        /// </summary>
        /// <param name="keyboards">Array of menus.</param>
        /// <returns> An inline menu for the bot.</returns>
        public static InlineKeyboardMarkup UnitInlineKeyboard(params InlineKeyboardMarkup[] keyboards)
        {
            List<IEnumerable<InlineKeyboardButton>> buttons = new();
            foreach (var keyboard in keyboards)
                buttons.AddRange(keyboard.InlineKeyboard);

            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }
    }
}
