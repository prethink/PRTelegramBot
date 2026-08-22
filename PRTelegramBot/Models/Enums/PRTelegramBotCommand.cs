using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Identifiers for callback commands
    /// </summary>
    [InlineCommand]
    public enum PRTelegramBotCommand
    {
        /// <summary>
        /// No command.
        /// </summary>
        [Description(nameof(None))]
        None = 0,

        /// <summary>
        /// Picking a month in the calendar.
        /// </summary>
        [Description(nameof(PickMonth))]
        PickMonth = 1,

        /// <summary>
        /// Picking a year in the calendar.
        /// </summary>
        [Description(nameof(PickYear))]
        PickYear = 2,

        /// <summary>
        /// Switching the calendar to another month or year.
        /// </summary>
        [Description(nameof(ChangeTo))]
        ChangeTo = 3,

        /// <summary>
        /// Opening the month and year picker.
        /// </summary>
        [Description(nameof(YearMonthPicker))]
        YearMonthPicker = 4,

        /// <summary>
        /// Picking a specific date in the calendar.
        /// </summary>
        [Description(nameof(PickDate))]
        PickDate = 5,

        /// <summary>
        /// Moving to the next page of a paginated output.
        /// </summary>
        [Description(nameof(NextPage))]
        NextPage = 6,

        /// <summary>
        /// The current page of a paginated output.
        /// </summary>
        [Description(nameof(CurrentPage))]
        CurrentPage = 7 ,

        /// <summary>
        /// Moving to the previous page of a paginated output.
        /// </summary>
        [Description(nameof(PreviousPage))]
        PreviousPage = 8,

        /// <summary>
        /// A callback that asks the user to confirm the action first.
        /// </summary>
        [Description(nameof(CallbackWithConfirmation))]
        CallbackWithConfirmation = 9,

        /// <summary>
        /// The user answered "no" to a confirmation request.
        /// </summary>
        [Description(nameof(CallbackWithConfirmationResultNo))]
        CallbackWithConfirmationResultNo = 10
    }
}
