using System.ComponentModel;

namespace ConsoleExample.Models
{
    public enum AddCustomTHeader
    {
        [Description(nameof(TestAddCommand))]
        TestAddCommand = 700,
        [Description(nameof(TestAddCommandTwo))]
        TestAddCommandTwo,
    }
}
