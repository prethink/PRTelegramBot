using FluentAssertions;
using PRTelegramBot.Configs;

namespace PRTelegramBot.Tests.ConfigsTests
{
    public class BotConfigJsonProviderTests
    {
        private string directory = string.Empty;

        /// <summary>
        /// Shape used to check binding a whole section onto a class.
        /// The provider looks the section up by the type name.
        /// </summary>
        private class SampleOptions
        {
            public string Token { get; set; } = string.Empty;

            public int Timeout { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            directory = Path.Combine(Path.GetTempPath(), "prtelegrambot-config-tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, recursive: true);
        }

        private string WriteConfig(string json, string name = "config.json")
        {
            var path = Path.Combine(directory, name);
            File.WriteAllText(path, json);
            return path;
        }

        #region Flat files

        [Test]
        public void GetKeysAndValuesReadsAFlatFile()
        {
            var path = WriteConfig("""
                {
                  "MAIN_MENU": "Main menu",
                  "BACK": "Back"
                }
                """);

            var provider = new BotConfigJsonProvider(path);

            provider.GetKeysAndValues().Should().BeEquivalentTo(new Dictionary<string, string>
            {
                ["MAIN_MENU"] = "Main menu",
                ["BACK"] = "Back"
            });
        }

        [Test]
        public void GetKeysAndValuesReturnsNothingForAnEmptyObject()
        {
            var provider = new BotConfigJsonProvider(WriteConfig("{}"));

            provider.GetKeysAndValues().Should().BeEmpty();
        }

        [Test]
        public void GetValueReadsASingleKey()
        {
            var provider = new BotConfigJsonProvider(WriteConfig("""{ "MAIN_MENU": "Main menu" }"""));

            provider.GetValue<string>("MAIN_MENU").Should().Be("Main menu");
        }

        [Test]
        public void GetValueReturnsDefaultForAMissingKey()
        {
            var provider = new BotConfigJsonProvider(WriteConfig("""{ "MAIN_MENU": "Main menu" }"""));

            provider.GetValue<string>("NOT_THERE").Should().BeNull();
        }

        [Test]
        public void GetValueConvertsToTheRequestedType()
        {
            var provider = new BotConfigJsonProvider(WriteConfig("""{ "Timeout": "30" }"""));

            provider.GetValue<int>("Timeout").Should().Be(30);
        }

        #endregion

        #region Sections

        [Test]
        public void GetOptionsBindsASectionNamedAfterTheType()
        {
            var path = WriteConfig("""
                {
                  "SampleOptions": {
                    "Token": "abc",
                    "Timeout": 30
                  }
                }
                """);

            var options = new BotConfigJsonProvider(path).GetOptions<SampleOptions>();

            options.Should().NotBeNull();
            options.Token.Should().Be("abc");
            options.Timeout.Should().Be(30);
        }

        [Test]
        public void GetOptionsReturnsNothingWhenTheSectionIsMissing()
        {
            var provider = new BotConfigJsonProvider(WriteConfig("""{ "Other": { "Token": "abc" } }"""));

            provider.GetOptions<SampleOptions>().Should().BeNull();
        }

        [Test]
        public void GetKeysAndValuesByOptionsReturnsTheSectionEntries()
        {
            var path = WriteConfig("""
                {
                  "SampleOptions": {
                    "Token": "abc",
                    "Timeout": "30"
                  }
                }
                """);

            var values = new BotConfigJsonProvider(path).GetKeysAndValuesByOptions<SampleOptions>();

            // Keys keep the section prefix that IConfiguration uses.
            values.Should().ContainValues("abc", "30");
            values.Keys.Should().OnlyContain(k => k.StartsWith("SampleOptions:"));
        }

        #endregion

        #region Paths

        [Test]
        public void SetConfigPathSwitchesToAnotherFile()
        {
            var first = WriteConfig("""{ "KEY": "first" }""", "first.json");
            var second = WriteConfig("""{ "KEY": "second" }""", "second.json");

            var provider = new BotConfigJsonProvider(first);
            provider.GetValue<string>("KEY").Should().Be("first");

            provider.SetConfigPath(second);
            provider.GetValue<string>("KEY").Should().Be("second");
        }

        [Test]
        public void SetConfigPathThrowsForAMissingFile()
        {
            var provider = new BotConfigJsonProvider();
            var missing = Path.Combine(directory, "does-not-exist.json");

            provider.Invoking(x => x.SetConfigPath(missing))
                .Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void SetConfigPathThrowsForMalformedJson()
        {
            var path = WriteConfig("{ this is not json");

            var provider = new BotConfigJsonProvider();

            provider.Invoking(x => x.SetConfigPath(path)).Should().Throw<Exception>();
        }

        #endregion
    }
}
