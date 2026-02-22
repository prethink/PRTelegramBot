using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.Builders
{
    internal class MessageBuilderTests
    {
        private StringCustomFormatter formatter;

        [SetUp]
        public void Setup()
        {
            formatter = new StringCustomFormatter();
        }

        [Test]
        public void ShouldReplace_PositionalArguments()
        {
            var result = formatter
                .Message("{0} {1} {2}")
                .AddArgument("Hello")
                .AddArgument("World")
                .AddArgument(123)
                .Build();

            Assert.AreEqual("Hello World 123", result);
        }

        [Test]
        public void ShouldReplace_NamedResolvers_WithStrings()
        {
            var result = formatter
                .Message("{QA} tested {PR}")
                .AddResolver("QA", "Ivan")
                .AddResolver("PR", "PR-456")
                .Build();

            Assert.AreEqual("Ivan tested PR-456", result);
        }

        [Test]
        public void ShouldReplace_Mixed_PositionalAndNamed()
        {
            var result = formatter
                .Message("{QA} approved {0} tasks for {Dev}")
                .AddResolver("QA", "Anna")
                .AddResolver("Dev", "Peter")
                .AddArgument(5)
                .Build();

            Assert.AreEqual("Anna approved 5 tasks for Peter", result);
        }

        [Test]
        public void ShouldKeep_UnresolvedTokensIntact()
        {
            var result = formatter
                .Message("{QA} and {Unknown} did {0}")
                .AddResolver("QA", "Mike")
                .AddArgument("something")
                .Build();

            Assert.AreEqual("Mike and {Unknown} did something", result);
        }

        [Test]
        public void ShouldSupport_LazyResolvers()
        {
            string dynamicName = "Alice";

            var result = formatter
                .Message("{QA} approved the request")
                .AddResolver("QA", () => dynamicName)
                .Build();

            Assert.AreEqual("Alice approved the request", result);

            // поменяли значение и билдим заново
            dynamicName = "Bob";

            result = formatter
                .Message("{QA} approved the request")
                .AddResolver("QA", () => dynamicName)
                .Build();

            Assert.AreEqual("Bob approved the request", result);
        }

        [Test]
        public void ShouldHandle_EmptyTemplate()
        {
            var result = formatter
                .Message("")
                .Build();

            Assert.AreEqual("", result);
        }

        [Test]
        public void ShouldHandle_TemplateWithoutTokens()
        {
            var result = formatter
                .Message("No tokens here")
                .Build();

            Assert.AreEqual("No tokens here", result);
        }
    }
}
