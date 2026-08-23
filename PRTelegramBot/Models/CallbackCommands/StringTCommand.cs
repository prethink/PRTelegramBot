﻿using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// String data for commands.
    /// </summary>
    public class StringTCommand : TCommandBase
    {
        #region Fields and properties

        /// <summary>
        /// Text data.
        /// </summary>
        [JsonPropertyName("1")]
        public string StrData { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Entity identifier.</param>
        public StringTCommand(string data)
            : base(0)
        {
            StrData = data;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Entity identifier.</param>
        /// <param name="lastCommand">Previous command.</param>
        public StringTCommand(string data, int lastCommand)
            : base(lastCommand)
        {
            StrData = data;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Entity identifier.</param>
        /// <param name="action">Action to perform on the previous message.</param>
        public StringTCommand(string data, ActionWithLastMessage action)
            : base(action)
        {
            StrData = data;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Entity identifier.</param>
        /// <param name="lastCommand">Previous command.</param>
        /// <param name="action">Action to perform on the previous message.</param>
        public StringTCommand(string data, int lastCommand, ActionWithLastMessage action)
            : base(lastCommand, action)
        {
            StrData = data;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public StringTCommand() { }

        #endregion
    }
}
