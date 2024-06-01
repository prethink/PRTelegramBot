using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers
{
    public sealed class NextStepUpdateHandler : ExecuteHandler<string>
    {
        public NextStepUpdateHandler(PRBot bot) 
            : base(bot) { }

        public override UpdateType TypeUpdate => UpdateType.Message;

        public bool IgnoreBasicCommand(Update update)
        {
            if(!update.HasStepHandler())
                return false;

            return update!.GetStepHandler().IgnoreBasicCommands;
        }

        public bool LastStepExecuted(Update update)
        {
            if (!update.HasStepHandler())
                return false;

            return update!.GetStepHandler().LastStepExecuted;
        }

        public bool ClearSteps(Update update)
        {
            if (!update.HasStepHandler())
                return false;

            update.ClearStepUserHandler();
            return true;
        }

        public override async Task<ResultUpdate> Handle(Update update)
        {
            try
            {
                if (!update.HasStepHandler())
                    return ResultUpdate.Continue;

                var step = update.GetStepHandler()?.GetExecuteMethod();
                if (step is null)
                    return ResultUpdate.NotFound;

                var resultExecute = await ExecuteMethod(update, new CommandHandler(step));
                if (resultExecute == ResultCommand.Executed)
                    return ResultUpdate.Handled;

                return ResultUpdate.Continue;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return ResultUpdate.Error;
            }
        }

        //public bool IsChangeStep(Update update)
        //{

        //}

        protected override ResultCommand InternalCheck(Update update, CommandHandler handler)
        {
            return ResultCommand.Continue;
        }
    }
}
