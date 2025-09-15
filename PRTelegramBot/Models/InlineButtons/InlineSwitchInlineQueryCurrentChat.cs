using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку встроенной клавиатуры. При нажатии на кнопку в поле ввода текущего чата будет вставлено имя пользователя бота и указанный инлайн-запрос. Может быть пустым, в этом случае будет вставлено только имя пользователя бота.<br/><br/>Это предлагает быстрый способ для пользователя открыть вашего бота в инлайн-режиме в том же чате - подходит для выбора чего-либо из нескольких вариантов. Не поддерживается в каналах и для сообщений, отправленных от имени аккаунта Telegram Business.
    /// </summary>
    public class InlineSwitchInlineQueryCurrentChat : InlineBase, IInlineContent
    {
        #region Поля и свойства

        /// <summary>
        /// Если установлено, при нажатии на кнопку в поле ввода текущего чата будет вставлено имя пользователя бота и указанный инлайн-запрос. Может быть пустым, в этом случае будет вставлено только имя пользователя бота.<br/><br/>Это предлагает быстрый способ для пользователя открыть вашего бота в инлайн-режиме в том же чате - подходит для выбора чего-либо из нескольких вариантов. Не поддерживается в каналах и для сообщений, отправленных от имени аккаунта Telegram Business.
        /// </summary>
        public string SwitchInlineQueryCurrentChat { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return SwitchInlineQueryCurrentChat;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(ButtonName, SwitchInlineQueryCurrentChat);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="switchInlineQueryCurrentChat">Если установлено, при нажатии на кнопку в поле ввода текущего чата будет вставлено имя пользователя бота и указанный инлайн-запрос. Может быть пустым, в этом случае будет вставлено только имя пользователя бота.<br/><br/>Это предлагает быстрый способ для пользователя открыть вашего бота в инлайн-режиме в том же чате - подходит для выбора чего-либо из нескольких вариантов. Не поддерживается в каналах и для сообщений, отправленных от имени аккаунта Telegram Business.</param>
        public InlineSwitchInlineQueryCurrentChat(string buttonName, string switchInlineQueryCurrentChat)
            : base(buttonName)
        {
            SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat;
        }

        #endregion
    }
}
