using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку встроенной клавиатуры. При нажатии на кнопку пользователю будет предложено выбрать один из своих чатов указанного типа, открыть этот чат и вставить имя пользователя бота и указанный инлайн-запрос в поле ввода. Не поддерживается для сообщений, отправленных от имени аккаунта Telegram Business.
    /// </summary>
    public class InlineSwitchInlineQueryChosenChat : InlineBase, IInlineContent
    {
        #region Поля и свойства

        /// <summary>
        /// Если установлено, при нажатии на кнопку пользователю будет предложено выбрать один из своих чатов указанного типа, открыть этот чат и вставить имя пользователя бота и указанный инлайн-запрос в поле ввода. Не поддерживается для сообщений, отправленных от имени аккаунта Telegram Business.
        /// </summary>
        public SwitchInlineQueryChosenChat SwitchInlineQueryChosenChat { get; set; }

        #endregion

        #region IInlineContent

        public object GetContent()
        {
            return SwitchInlineQueryChosenChat;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="switchInlineQueryChosenChat">Если установлено, при нажатии на кнопку пользователю будет предложено выбрать один из своих чатов указанного типа, открыть этот чат и вставить имя пользователя бота и указанный инлайн-запрос в поле ввода. Не поддерживается для сообщений, отправленных от имени аккаунта Telegram Business.</param>
        public InlineSwitchInlineQueryChosenChat(string buttonName, SwitchInlineQueryChosenChat switchInlineQueryChosenChat)
            : base(buttonName)
        {
            SwitchInlineQueryChosenChat = switchInlineQueryChosenChat;
        }

        #endregion
    }
}
