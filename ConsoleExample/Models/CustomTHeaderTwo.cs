using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace ConsoleExample.Models
{
    [InlineCommand]
    public enum CustomTHeaderTwo
    {
        [Description("Пример 1")]
        ExampleOne = 600,
        [Description("Пример 2")]
        ExampleTwo,
        [Description("Пример 3")]
        ExampleThree,
        [Description("Пример возращения назад")]
        ExampleBack,
        [Description("Пример страниц")]
        CustomPageHeader,
        [Description("Пример страниц2")]
        CustomPageHeader2,
    }
}
