using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace ConsoleExample.Models
{
    [InlineCommand]
    public enum CustomTHeader
    {
        [Description("Бесплатный ВИП")]
        GetFreeVIP = 500,
        [Description("Вип на 1 день")]
        GetVipOneDay,
        [Description("Вип на 1 неделю")]
        GetVipOneWeek,
        [Description("Вип на 1 месяц")]
        GetVipOneMonth,
        [Description("Вип навсегда")]
        GetVipOneForever,
        [Description("Шаг из Inline")]
        InlineWithStep,
        [Description("Кастомная кнопка")]
        CustomButton
    }
}
