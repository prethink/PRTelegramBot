using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils
{
    public class InlineUtils
    {
        /// <summary>
        /// Создает inline кнопку.
        /// </summary>
        /// <param name="inlineData">Данные inline кнопки.</param>
        /// <returns>Inline кнопка.</returns>
        public static InlineKeyboardButton GetInlineButton(IInlineContent inlineData)
        {
            return inlineData switch
            {
                InlineCallback inlineCallback => InlineKeyboardButton.WithCallbackData(inlineCallback.GetButtonName(), inlineCallback.GetContent() as string),
                InlinePay inlinePay => InlineKeyboardButton.WithPay(inlinePay.GetButtonName()),
                InlineURL inlineUrl => InlineKeyboardButton.WithUrl(inlineUrl.GetButtonName(), inlineUrl.GetContent() as string),
                InlineWebApp inlineWebApp => InlineKeyboardButton.WithWebApp(inlineWebApp.GetButtonName(), inlineWebApp.GetContent() as WebAppInfo),
                InlineLoginUrl inlineLogin => InlineKeyboardButton.WithLoginUrl(inlineLogin.GetButtonName(), inlineLogin.GetContent() as LoginUrl),
                InlineCallbackGame inlineCallbackGame => InlineKeyboardButton.WithCallbackGame(inlineCallbackGame.GetButtonName()),
                InlineSwitchInlineQuery inlineSwitchInlineQuery => InlineKeyboardButton.WithSwitchInlineQuery(inlineSwitchInlineQuery.GetButtonName(), inlineSwitchInlineQuery.GetContent() as string),
                InlineSwitchInlineQueryCurrentChat inlineSwitchInlineQueryCurrentChat => InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(inlineSwitchInlineQueryCurrentChat.GetButtonName(), inlineSwitchInlineQueryCurrentChat.GetContent() as string),
                InlineSwitchInlineQueryChosenChat inlineSwitchInlineQueryChosenChat => InlineKeyboardButton.WithSwitchInlineQueryChosenChat(inlineSwitchInlineQueryChosenChat.GetButtonName(), inlineSwitchInlineQueryChosenChat.GetContent() as SwitchInlineQueryChosenChat),

                _ => throw new NotImplementedException($"{inlineData.GetType()} is not implemented yet.")
            };
        }

        /// <summary>
        /// Создает одно inline меню из нескольких.
        /// </summary>
        /// <param name="keyboards">Массив меню.</param>
        /// <returns> Inline меню для бота.</returns>
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
