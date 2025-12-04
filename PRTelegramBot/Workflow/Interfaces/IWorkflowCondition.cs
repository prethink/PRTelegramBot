using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Workflow.Interfaces
{
    public interface IWorkflowCondition : IWorkflowNode
    {
        Task<bool> CheckConditionAsync(IBotContext context);
    }
}

