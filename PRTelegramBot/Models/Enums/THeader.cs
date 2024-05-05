using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Идентификаторы для callback команд
    /// </summary>
    [InlineCommand]
    public enum THeader
    {
        [Description(nameof(None))]
        None = 0,
        [Description(nameof(PickMonth))]
        PickMonth = 1,
        [Description(nameof(PickYear))]
        PickYear = 2,
        [Description(nameof(ChangeTo))]
        ChangeTo = 3,
        [Description(nameof(YearMonthPicker))]
        YearMonthPicker = 4,
        [Description(nameof(PickDate))]
        PickDate = 5,
        [Description(nameof(NextPage))]
        NextPage = 6,
        [Description(nameof(CurrentPage))]
        CurrentPage = 7 ,
        [Description(nameof(PreviousPage))]
        PreviousPage = 8,
    }
}
