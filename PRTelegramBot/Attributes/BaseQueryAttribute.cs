using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Attributes
{
    [AttributeUsage(AttributeTargets.Method,Inherited = true)]
    public class BaseQueryAttribute : Attribute
    {
        public long BotId { get; set; }
        public BaseQueryAttribute(long botId = 0)
        {
            BotId = botId;
        }
    }
}
