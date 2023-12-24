using PRTelegramBot.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
