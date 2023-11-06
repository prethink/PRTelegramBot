using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Models
{
    public abstract class CustomParameters
    {
        public abstract void InitData();
        public abstract void ClearData();
    }
}
