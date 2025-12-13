using PRTelegramBot.Interfaces;
using PRTelegramBot.Utils;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Builders.Keyboard
{
    /// <summary>
    /// Билдер для удобного построения InlineKeyboardMarkup.
    /// Позволяет задавать параметры клавиатуры и динамически добавлять кнопки и строки.
    /// </summary>
    public class InlineKeyboardBuilder : KeyboardBuilderBase<IInlineContent, InlineKeyboardMarkup, InlineKeyboardBuilder>
    {
        #region Базовый класс

        /// <inheritdoc/>
        public override InlineKeyboardMarkup Build()
        {
            this.ReplaceEmptyButtons();
            var convertedButtons = buttons
                .Select(row => row
                    .Select(button => InlineUtils.GetInlineButton(button))
                    .ToList())
                .ToList();

            return new InlineKeyboardMarkup(convertedButtons);
        }

        /// <inheritdoc/>
        protected override void ReplaceEmptyButtons()
        {
            foreach (var row in buttons)
            {
                foreach (var button in row)
                {
                    if (button.GetButtonName().Equals(KEY_EMPTY_BUTTON_NAME, StringComparison.OrdinalIgnoreCase))
                        button.SetButtonName(emptyButtonName);
                }
            }
        }

        #endregion
    }
}
