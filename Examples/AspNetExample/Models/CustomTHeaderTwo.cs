using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace AspNetExample.Models
{
    [InlineCommand]
    public enum CustomTHeaderTwo
    {
        [Description("Example 1")]
        ExampleOne = 600,
        [Description("Example 2")]
        ExampleTwo,
        [Description("Example 3")]
        ExampleThree,
        [Description("Going back example")]
        ExampleBack,
        [Description("Pagination example")]
        CustomPageHeader,
        [Description("Pagination example 2")]
        CustomPageHeader2,
    }
}
