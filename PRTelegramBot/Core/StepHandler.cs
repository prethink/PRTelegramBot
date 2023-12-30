using PRTelegramBot.Interface;
using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Core
{
    internal class StepHandler : IStep
    {
        private ITelegramCache _cache;
        private StepTelegram root;
        private StepTelegram previewStep;
        private StepTelegram currentStep;

        public StepHandler(StepTelegram step)
        {
            this.root = step;
            this.previewStep = step;
            this.currentStep = step;
        }

        public void BackStep()
        {
            this.currentStep = this.previewStep;
        }

        public ITelegramCache GetCacheData()
        {
            throw new NotImplementedException();
        }

        public void NextStep(int step = 0)
        {
            var nextStep = currentStep.Steps.ElementAtOrDefault(step);
            if(nextStep != null)
            {
                this.currentStep = nextStep;
            }
        }

        public ITelegramCache SetCacheData(ITelegramCache data)
        {
            throw new NotImplementedException();
        }
    }
}
