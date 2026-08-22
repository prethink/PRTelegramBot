using FluentAssertions;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.ExtensionsTests
{
    /// <summary>
    /// The cache lives in static state keyed by "{botId}-{chatId}", so every test uses
    /// its own chat and cleans up after itself.
    /// </summary>
    public class CacheExtensionTests
    {
        private static long nextChatId = 700000;
        private static int nextUpdateId = 700000;

        private readonly List<Update> created = new();

        private class SampleCache : ITelegramCache
        {
            public string Value { get; set; } = string.Empty;

            public bool ClearData()
            {
                Value = string.Empty;
                return true;
            }
        }

        private class OtherCache : ITelegramCache
        {
            public bool ClearData() => true;
        }

        private static PRBotBase CreateBot(long botId)
        {
            return new PRBotDummy(opt =>
            {
                opt.Client = new TelegramBotClient("35425:token");
                opt.Token = "35425:token";
                opt.BotId = botId;
            }, null);
        }

        private Update CreateLinkedUpdate(PRBotBase bot, long? chatId = null)
        {
            var update = new Update
            {
                Id = Interlocked.Increment(ref nextUpdateId),
                Message = new Message
                {
                    Id = 1,
                    Chat = new Chat { Id = chatId ?? Interlocked.Increment(ref nextChatId) },
                    From = new User { Id = 111 }
                }
            };

            update.AddTelegramClient(bot);
            created.Add(update);
            return update;
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var update in created)
            {
                update.RemoveCacheData();
                update.ClearTelegramClient();
            }

            created.Clear();
        }

        [Test]
        public void ThereIsNoCacheUntilSomethingIsStored()
        {
            var update = CreateLinkedUpdate(CreateBot(1));

            update.HasCacheData().Should().BeFalse();
        }

        [Test]
        public void CreateCacheDataStoresANewEntry()
        {
            var update = CreateLinkedUpdate(CreateBot(1));

            var cache = update.CreateCacheData<SampleCache>();

            cache.Should().NotBeNull();
            update.HasCacheData().Should().BeTrue();
        }

        [Test]
        public void CreateCacheDataReplacesWhatWasThereBefore()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            var first = update.CreateCacheData<SampleCache>();
            first.Value = "old";

            var second = update.CreateCacheData<SampleCache>();

            second.Should().NotBeSameAs(first);
            second.Value.Should().BeEmpty();
        }

        [Test]
        public void GetOrCreateReturnsTheSameInstanceOnEveryCall()
        {
            var update = CreateLinkedUpdate(CreateBot(1));

            var first = update.GetOrCreate<SampleCache>();
            first.Value = "kept";
            var second = update.GetOrCreate<SampleCache>();

            second.Should().BeSameAs(first);
            second.Value.Should().Be("kept");
        }

        [Test]
        public void GetOrCreateReplacesTheEntryWhenTheTypeDiffers()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            update.GetOrCreate<SampleCache>();

            var other = update.GetOrCreate<OtherCache>();

            other.Should().NotBeNull();
            update.GetCacheData<OtherCache>().Should().BeSameAs(other);
        }

        [Test]
        public void GetCacheDataReturnsWhatWasStored()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            var cache = update.CreateCacheData<SampleCache>();
            cache.Value = "stored";

            update.GetCacheData<SampleCache>().Value.Should().Be("stored");
        }

        [Test]
        public void GetCacheDataCreatesTheEntryWhenThereIsNone()
        {
            var update = CreateLinkedUpdate(CreateBot(1));

            update.GetCacheData<SampleCache>().Should().NotBeNull();
            update.HasCacheData().Should().BeTrue();
        }

        [Test]
        public void ClearCacheDataResetsTheContentButKeepsTheEntry()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            var cache = update.CreateCacheData<SampleCache>();
            cache.Value = "filled";

            update.ClearCacheData();

            cache.Value.Should().BeEmpty();
            update.HasCacheData().Should().BeTrue();
        }

        [Test]
        public void RemoveCacheDataDropsTheEntryCompletely()
        {
            var update = CreateLinkedUpdate(CreateBot(1));
            update.CreateCacheData<SampleCache>();

            update.RemoveCacheData();

            update.HasCacheData().Should().BeFalse();
        }

        /// <summary>
        /// The cache belongs to a user of a bot, not to a single update.
        /// </summary>
        [Test]
        public void TwoUpdatesFromTheSameChatShareTheCache()
        {
            var bot = CreateBot(1);
            var chatId = Interlocked.Increment(ref nextChatId);
            var first = CreateLinkedUpdate(bot, chatId);
            var second = CreateLinkedUpdate(bot, chatId);

            first.CreateCacheData<SampleCache>().Value = "shared";

            second.GetCacheData<SampleCache>().Value.Should().Be("shared");
        }

        [Test]
        public void DifferentChatsHaveTheirOwnCache()
        {
            var bot = CreateBot(1);
            var first = CreateLinkedUpdate(bot);
            var second = CreateLinkedUpdate(bot);

            first.CreateCacheData<SampleCache>().Value = "first";

            second.HasCacheData().Should().BeFalse();
        }

        /// <summary>
        /// Two bots serving the same chat must not see each other's cache.
        /// </summary>
        [Test]
        public void DifferentBotsHaveTheirOwnCacheForTheSameChat()
        {
            var chatId = Interlocked.Increment(ref nextChatId);
            var first = CreateLinkedUpdate(CreateBot(1), chatId);
            var second = CreateLinkedUpdate(CreateBot(2), chatId);

            first.CreateCacheData<SampleCache>().Value = "bot one";

            second.HasCacheData().Should().BeFalse();
        }

        [Test]
        public void CacheCallsThrowWhenTheUpdateIsNotLinkedToABot()
        {
            var update = new Update
            {
                Id = Interlocked.Increment(ref nextUpdateId),
                Message = new Message { Id = 1, Chat = new Chat { Id = 1 }, From = new User { Id = 1 } }
            };

            update.Invoking(x => x.HasCacheData()).Should().Throw<KeyNotFoundException>();
        }
    }
}
