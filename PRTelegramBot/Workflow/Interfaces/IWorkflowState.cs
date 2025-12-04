using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Workflow.Interfaces
{
    public interface IWorkflowState : IWorkflowNode
    {
        string Name { get; }
        Task StartState(IBotContext context);
        Task ExitState(IBotContext context);
    }
}
