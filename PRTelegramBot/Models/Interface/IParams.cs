using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Models.Interface
{
    public interface IParams<T>
    {
        T GetParams<T>();
    }
}
    