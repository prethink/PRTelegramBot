using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace ConsoleExample.Models.CommandHeaders
{
    [InlineCommand]
    public enum CustomTHeader
    {
        [Description("Free VIP")]
        GetFreeVIP = 500,
        [Description("VIP for 1 day")]
        GetVipOneDay,
        [Description("VIP for 1 week")]
        GetVipOneWeek,
        [Description("VIP for 1 month")]
        GetVipOneMonth,
        [Description("VIP forever")]
        GetVipOneForever,
        [Description("Step from inline")]
        InlineWithStep,
        [Description("Custom button")]
        CustomButton,
        [Description("Calendar callback")]
        CalendarCallback
    }
}
