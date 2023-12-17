using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TelegramBotHandlerAttribute : Attribute
    {
    }
}
