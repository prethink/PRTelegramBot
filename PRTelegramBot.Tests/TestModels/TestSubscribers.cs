using PRTelegramBot.EventBus;

namespace PRTelegramBot.Tests.TestModels
{
    /// <summary>
    /// Event contract used by the event bus tests.
    /// </summary>
    public interface IPRTestSubscriber : IPRGlobalSubscriber
    {
        void Ping();
    }

    /// <summary>
    /// A second, unrelated event contract, used to check that the bus routes by type.
    /// </summary>
    public interface IPROtherTestSubscriber : IPRGlobalSubscriber
    {
        void Pong();
    }

    /// <summary>
    /// Counts how many times it was invoked.
    /// </summary>
    public class TestSubscriber : IPRTestSubscriber
    {
        public int PingCount { get; private set; }

        public void Ping() => PingCount++;

        public void Subscribe() => PREventBus.Subscribe(this);

        public void Unsubscribe() => PREventBus.Unsubscribe(this);

        public void Dispose() => Unsubscribe();
    }

    /// <summary>
    /// Throws on every invocation, to check that one faulty subscriber
    /// does not stop the broadcast to the rest.
    /// </summary>
    public class ThrowingTestSubscriber : IPRTestSubscriber
    {
        public int PingCount { get; private set; }

        public void Ping()
        {
            PingCount++;
            throw new InvalidOperationException("Subscriber failed on purpose.");
        }

        public void Subscribe() => PREventBus.Subscribe(this);

        public void Unsubscribe() => PREventBus.Unsubscribe(this);

        public void Dispose() => Unsubscribe();
    }

    /// <summary>
    /// Unsubscribes another subscriber while the broadcast is running.
    /// </summary>
    public class UnsubscribingTestSubscriber : IPRTestSubscriber
    {
        private readonly IPRGlobalSubscriber target;

        public int PingCount { get; private set; }

        public UnsubscribingTestSubscriber(IPRGlobalSubscriber target)
        {
            this.target = target;
        }

        public void Ping()
        {
            PingCount++;
            PREventBus.Unsubscribe(target);
        }

        public void Subscribe() => PREventBus.Subscribe(this);

        public void Unsubscribe() => PREventBus.Unsubscribe(this);

        public void Dispose() => Unsubscribe();
    }

    /// <summary>
    /// Subscriber of the unrelated contract.
    /// </summary>
    public class OtherTestSubscriber : IPROtherTestSubscriber
    {
        public int PongCount { get; private set; }

        public void Pong() => PongCount++;

        public void Subscribe() => PREventBus.Subscribe(this);

        public void Unsubscribe() => PREventBus.Unsubscribe(this);

        public void Dispose() => Unsubscribe();
    }
}
