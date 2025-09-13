﻿using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку встроенной клавиатуры с описанием игры, которая будет запущена при нажатии пользователем на кнопку.<br/><br/><b>ПРИМЕЧАНИЕ:</b> Этот тип кнопки <b>должен</b> всегда быть первой кнопкой в первой строке.
    /// </summary>
    public class InlineCallbackGame : InlineBase, IInlineContent
    {
        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return "";
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithCallbackGame(ButtonName);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        public InlineCallbackGame(string buttonName)
            : base(buttonName) { }

        #endregion
    }
}
