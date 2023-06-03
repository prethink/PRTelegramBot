using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExample.Models
{
    public enum CustomTHeader
    {
        [Description("Бесплатный ВИП")]
        GetFreeVIP,
        [Description("Вип на 1 день")]
        GetVipOneDay,
        [Description("Вип на 1 неделю")]
        GetVipOneWeek,
        [Description("Вип на 1 месяц")]
        GetVipOneMonth,
        [Description("Вип навсегда")]
        GetVipOneForever,
        [Description("Пример 1")]
        ExampleOne,
        [Description("Пример 2")]
        ExampleTwo,
        [Description("Пример 3")]
        ExampleThree,
    }
}
