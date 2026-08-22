using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Handles a TCommand in page form.
    /// </summary>
    public class PageTCommand : TCommandBase
    {
        #region Fields and properties

        /// <summary>
        /// Page number.
        /// </summary>
        [JsonPropertyName("1")]
        public int Page { get; set; }

        /// <summary>
        /// Command header.
        /// </summary>
        [JsonPropertyName("2")]
        public int Header { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="enumValueInt">Enum header as an int.</param>
        public PageTCommand(int page, Enum enumValueInt)
            : base(0)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="enumValueInt">Enum header as an int.</param>
        /// <param name="lastCommand"></param>
        public PageTCommand(int page, Enum enumValueInt, int lastCommand)
            : base(lastCommand)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="enumValueInt">Enum header as an int.</param>
        /// <param name="action">Action to perform on the previous message.</param>
        public PageTCommand(int page, Enum enumValueInt, ActionWithLastMessage action)
            : base(action)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="enumValueInt">Enum header as an int.</param>
        /// <param name="lastCommand">Previous command.</param>
        /// <param name="action">Action to perform on the previous message.</param>
        public PageTCommand(int page, Enum enumValueInt, int lastCommand, ActionWithLastMessage action)
            : base(lastCommand, action)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PageTCommand() { }

        #endregion
    }
}
