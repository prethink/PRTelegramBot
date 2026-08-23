using FluentAssertions;
using PRTelegramBot.Converters.Inline;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class FileInlineConverterTests
    {
        /// <summary>
        /// Exposes the folder the converter settled on, which is protected on the converter itself.
        /// </summary>
        private sealed class ProbeConverter : FileInlineConverter
        {
            public ProbeConverter(string path) : base(path) { }

            public ProbeConverter() { }

            public string BasePath => basePath;
        }

        [Test]
        public void TheFolderNameGivenToTheConstructorIsTheOneUsed()
        {
            var converter = new ProbeConverter("MyCallbacks");

            Path.GetFileName(converter.BasePath).Should().Be("MyCallbacks");
        }

        [Test]
        public void TheDefaultFolderIsInlineCallbacks()
        {
            var converter = new ProbeConverter();

            Path.GetFileName(converter.BasePath).Should().Be("InlineCallbacks");
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void AnEmptyFolderNameIsRejected(string? path)
        {
            var act = () => new ProbeConverter(path!);

            act.Should().Throw<ArgumentException>();
        }
    }
}
