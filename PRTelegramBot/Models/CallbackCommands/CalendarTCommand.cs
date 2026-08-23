using System.Globalization;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Command that carries a date from the calendar.
    /// </summary>
    public class CalendarTCommand : TCommandBase
    {
        #region Fields and properties

        /// <summary>
        /// Date.
        /// </summary>
        [JsonPropertyName("1")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Culture.
        /// </summary>
        [JsonPropertyName("2")]
        public string Culture { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="date">Date.</param>
        public CalendarTCommand(DateTime date)
            : this(date, CultureInfo.GetCultureInfo("ru-RU", false), 0)
        {
            Date = date;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="date">Date.</param>
        /// <param name="headerCallbackCommand">Callback command header.</param>
        public CalendarTCommand(DateTime date, int headerCallbackCommand)
            : this(date, CultureInfo.GetCultureInfo("ru-RU", false), headerCallbackCommand)
        {
            Date = date;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="date">Date.</param>
        /// <param name="headerCallbackCommand">Callback command header.</param>
        /// <param name="culture">Calendar language.</param>
        public CalendarTCommand(DateTime date, CultureInfo culture, int headerCallbackCommand)
            : base(headerCallbackCommand, Enums.ActionWithLastMessage.Edit)
        {
            Date = date;
            Culture = culture.Name;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CalendarTCommand() { }

        #endregion
    }
}
