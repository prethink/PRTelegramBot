using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PRTelegramBot.Interface
{
    public interface IStep
    {
        ITelegramCache SetCacheData(ITelegramCache data);
        ITelegramCache GetCacheData();
        void BackStep();
        void NextStep(int step = 0);
    }
}
