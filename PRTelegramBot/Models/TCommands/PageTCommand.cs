﻿using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Обработка TCommand в формате страницы.
    /// </summary>
    public class PageTCommand : TCommandBase
    {
        #region Поля и свойства

        /// <summary>
        /// Номер страницы.
        /// </summary>
        [JsonPropertyName("1")]
        public int Page { get; set; }

        /// <summary>
        /// Заголовок команды.
        /// </summary>
        [JsonPropertyName("2")]
        public int Header { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="enumValueInt">Заголовок enum в формате int.</param>
        public PageTCommand(int page, Enum enumValueInt)
            : base(0)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="enumValueInt">Заголовок enum в формате int.</param>
        /// <param name="lastCommand"></param>
        public PageTCommand(int page, Enum enumValueInt, int lastCommand)
            : base(lastCommand)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="enumValueInt">Заголовок enum в формате int.</param>
        /// <param name="action">Действие с прошлым сообщением.</param>
        public PageTCommand(int page, Enum enumValueInt, ActionWithLastMessage action)
            : base(action)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="enumValueInt">Заголовок enum в формате int.</param>
        /// <param name="lastCommand">Прошлая команда.</param>
        /// <param name="action">Действие с прошлым сообщением.</param>
        public PageTCommand(int page, Enum enumValueInt, int lastCommand, ActionWithLastMessage action)
            : base(lastCommand, action)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public PageTCommand() { }

        #endregion
    }
}
