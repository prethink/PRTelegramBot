namespace PRTelegramBot.EventBus.Events
{
    public interface IPRTaskRunnerSubscriber : IPRGlobalSubscriber
    {
        void StopEvent(IEnumerable<long> botIds, Guid taskId);

        void StopEvent(Guid taskId);
    }
}
