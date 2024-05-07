using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Interfaces
{
    internal interface ICommandStore<T>
    {
        List<T> Commands { get; set; }
    }
}
