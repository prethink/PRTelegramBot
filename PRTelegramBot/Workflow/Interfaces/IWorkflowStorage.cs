namespace PRTelegramBot.Workflow.Interfaces
{
    internal interface IWorkflowStorage
    {
        Task SaveStateAsync(long userId, string stateName);
        Task<string?> LoadStateAsync(long userId);
        Task ClearStateAsync(long userId);
    }
}
