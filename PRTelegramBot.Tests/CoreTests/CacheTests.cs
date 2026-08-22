using FluentAssertions;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    public class CacheTests
    {
        private class SampleCache : ITelegramCache
        {
            public string Value { get; set; } = string.Empty;

            public bool ClearData()
            {
                Value = string.Empty;
                return true;
            }
        }

        private static Update CreateUnlinkedUpdate()
        {
            return new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = 555555 },
                    From = new User { Id = 111111 }
                }
            };
        }

        /// <summary>
        /// The cache is keyed by the bot the update belongs to. An update that was never
        /// linked to a bot has no key, and every cache call must say so rather than
        /// silently reading or writing someone else's data.
        /// </summary>
        [Test]
        public void CreateCacheDataThrowsWhenTheUpdateIsNotLinkedToABot()
        {
            var update = CreateUnlinkedUpdate();

            update.Invoking(x => x.CreateCacheData<SampleCache>())
                .Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void GetCacheDataThrowsWhenTheUpdateIsNotLinkedToABot()
        {
            var update = CreateUnlinkedUpdate();

            update.Invoking(x => x.GetCacheData<SampleCache>())
                .Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void HasCacheDataThrowsWhenTheUpdateIsNotLinkedToABot()
        {
            var update = CreateUnlinkedUpdate();

            update.Invoking(x => x.HasCacheData())
                .Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void ClearDataResetsTheCacheInstance()
        {
            var cache = new SampleCache { Value = "something" };

            cache.ClearData().Should().BeTrue();
            cache.Value.Should().BeEmpty();
        }
    }
}
