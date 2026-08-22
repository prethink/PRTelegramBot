using FluentAssertions;
using PRTelegramBot.EventBus;
using PRTelegramBot.Tests.TestModels;

namespace PRTelegramBot.Tests.EventsTests
{
    public class PREventBusTests
    {
        private readonly List<IPRGlobalSubscriber> created = new();

        private T Track<T>(T subscriber) where T : IPRGlobalSubscriber
        {
            created.Add(subscriber);
            return subscriber;
        }

        [TearDown]
        public void TearDown()
        {
            // The bus keeps its subscribers in static state, so every test cleans up after itself.
            foreach (var subscriber in created)
                PREventBus.Unsubscribe(subscriber);

            created.Clear();
        }

        [Test]
        public void SubscribedSubscriberReceivesTheEvent()
        {
            var subscriber = Track(new TestSubscriber());
            subscriber.Subscribe();

            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            subscriber.PingCount.Should().Be(1);
        }

        [Test]
        public void EverySubscribedSubscriberReceivesTheEvent()
        {
            var first = Track(new TestSubscriber());
            var second = Track(new TestSubscriber());
            first.Subscribe();
            second.Subscribe();

            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            first.PingCount.Should().Be(1);
            second.PingCount.Should().Be(1);
        }

        [Test]
        public void UnsubscribedSubscriberStopsReceivingTheEvent()
        {
            var subscriber = Track(new TestSubscriber());
            subscriber.Subscribe();
            subscriber.Unsubscribe();

            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            subscriber.PingCount.Should().Be(0);
        }

        [Test]
        public void RaisingAnEventWithoutSubscribersDoesNotThrow()
        {
            var act = () => PREventBus.RaiseEvent<IPROtherTestSubscriber>(x => x.Pong());

            act.Should().NotThrow();
        }

        [Test]
        public void EventsAreRoutedByContractType()
        {
            var ping = Track(new TestSubscriber());
            var pong = Track(new OtherTestSubscriber());
            ping.Subscribe();
            pong.Subscribe();

            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            ping.PingCount.Should().Be(1);
            pong.PongCount.Should().Be(0);
        }

        [Test]
        public void AFaultySubscriberDoesNotStopTheBroadcast()
        {
            var faulty = Track(new ThrowingTestSubscriber());
            var healthy = Track(new TestSubscriber());
            faulty.Subscribe();
            healthy.Subscribe();

            var act = () => PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            act.Should().NotThrow();
            faulty.PingCount.Should().Be(1);
            healthy.PingCount.Should().Be(1);
        }

        [Test]
        public void SubscribingTwiceDeliversTheEventTwice()
        {
            var subscriber = Track(new TestSubscriber());
            subscriber.Subscribe();
            subscriber.Subscribe();

            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            subscriber.PingCount.Should().Be(2);
        }

        [Test]
        public void UnsubscribingDuringABroadcastTakesEffectFromTheNextBroadcast()
        {
            var target = Track(new TestSubscriber());
            var unsubscriber = Track(new UnsubscribingTestSubscriber(target));
            unsubscriber.Subscribe();
            target.Subscribe();

            // The bus iterates a snapshot, so the target still receives the event it was
            // removed during; the removal is visible from the next broadcast onwards.
            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());
            var afterFirst = target.PingCount;

            PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());

            afterFirst.Should().Be(1);
            target.PingCount.Should().Be(1, "the subscriber was removed during the first broadcast");
        }

        [Test]
        public void UnsubscribingDuringABroadcastDoesNotThrow()
        {
            var target = Track(new TestSubscriber());
            var unsubscriber = Track(new UnsubscribingTestSubscriber(target));
            unsubscriber.Subscribe();
            target.Subscribe();

            var act = () =>
            {
                PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());
                PREventBus.RaiseEvent<IPRTestSubscriber>(x => x.Ping());
            };

            act.Should().NotThrow();
        }

        [Test]
        public void UnsubscribingSomeoneWhoNeverSubscribedDoesNotThrow()
        {
            var subscriber = new TestSubscriber();

            var act = () => PREventBus.Unsubscribe(subscriber);

            act.Should().NotThrow();
        }
    }
}
