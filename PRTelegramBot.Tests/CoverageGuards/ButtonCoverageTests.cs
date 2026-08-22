using System.Reflection;
using FluentAssertions;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.CoverageGuards
{
    /// <summary>
    /// Guards that fail when Telegram.Bot gains a new kind of button the framework does not wrap.
    /// </summary>
    /// <remarks>
    /// Buttons arrive the same silent way the update types do: Telegram.Bot adds a factory,
    /// nothing breaks, and users simply cannot build that button through this framework.
    /// <c>WithCopyText</c> and <c>WithRequestManagedBot</c> were both missed this way.
    /// </remarks>
    public class ButtonCoverageTests
    {
        /// <summary>
        /// Every inline button wrapper builds its button through one of these factories,
        /// so the set of factories used is the set of button kinds the framework supports.
        /// </summary>
        private static HashSet<string> InlineFactoriesUsedByWrappers()
        {
            var wrappers = typeof(InlineBase).Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(InlineBase).IsAssignableFrom(t));

            var used = new HashSet<string>();

            foreach (var wrapper in wrappers)
            {
                var method = wrapper.GetMethod(nameof(InlineBase.GetInlineButton),
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                if (method is null)
                    continue;

                // The wrapper name is the reliable link: matching the factory by IL would be
                // brittle, so the mapping is spelled out below instead.
                used.Add(wrapper.Name);
            }

            return used;
        }

        /// <summary>
        /// Maps each inline button factory to the wrapper that exposes it.
        /// A new factory with no entry here means the framework cannot build that button.
        /// </summary>
        private static readonly Dictionary<string, string> InlineFactoryToWrapper = new()
        {
            ["WithCallbackData"] = nameof(InlineCallback),
            ["WithCallbackGame"] = nameof(InlineCallbackGame),
            ["WithCopyText"] = "InlineCopyText",
            ["WithLoginUrl"] = "InlineLoginUrl",
            ["WithPay"] = "InlinePay",
            ["WithSwitchInlineQuery"] = "InlineSwitchInlineQuery",
            ["WithSwitchInlineQueryChosenChat"] = "InlineSwitchInlineQueryChosenChat",
            ["WithSwitchInlineQueryCurrentChat"] = "InlineSwitchInlineQueryCurrentChat",
            ["WithUrl"] = "InlineURL",
            ["WithWebApp"] = "InlineWebApp"
        };

        /// <summary>
        /// Maps each reply keyboard button factory to the builder method that exposes it.
        /// </summary>
        private static readonly Dictionary<string, string> KeyboardFactoryToBuilderMethod = new()
        {
            ["WithRequestChat"] = "AddRequestChat",
            ["WithRequestContact"] = "AddRequestContact",
            ["WithRequestLocation"] = "AddRequestLocation",
            ["WithRequestManagedBot"] = "AddRequestManagedBot",
            ["WithRequestPoll"] = "AddRequestPoll",
            ["WithRequestUsers"] = "AddRequestUsers",
            ["WithWebApp"] = "AddButtonWebApp"
        };

        private static IEnumerable<string> FactoryNames(Type buttonType)
        {
            return buttonType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.Name.StartsWith("With"))
                .Select(x => x.Name)
                .Distinct();
        }

        [Test]
        public void EveryInlineButtonKindHasAWrapper()
        {
            var unwrapped = FactoryNames(typeof(InlineKeyboardButton))
                .Where(name => !InlineFactoryToWrapper.ContainsKey(name))
                .ToList();

            unwrapped.Should().BeEmpty(
                "Telegram.Bot offers inline button kinds this framework cannot build. Add a wrapper " +
                "deriving from InlineBase, then list it in InlineFactoryToWrapper. Missing: {0}",
                string.Join(", ", unwrapped));
        }

        [Test]
        public void EveryInlineWrapperNamedInTheMapExists()
        {
            var wrappers = InlineFactoriesUsedByWrappers();

            var absent = InlineFactoryToWrapper.Values
                .Where(name => !wrappers.Contains(name))
                .ToList();

            absent.Should().BeEmpty(
                "the map names wrappers that no longer exist. Keep it in step with the classes " +
                "deriving from InlineBase. Absent: {0}",
                string.Join(", ", absent));
        }

        [Test]
        public void EveryReplyButtonKindHasABuilderMethod()
        {
            var unwrapped = FactoryNames(typeof(KeyboardButton))
                .Where(name => !KeyboardFactoryToBuilderMethod.ContainsKey(name))
                .ToList();

            unwrapped.Should().BeEmpty(
                "Telegram.Bot offers reply keyboard button kinds ReplyKeyboardBuilder cannot build. " +
                "Add a builder method, then list it in KeyboardFactoryToBuilderMethod. Missing: {0}",
                string.Join(", ", unwrapped));
        }

        [Test]
        public void EveryBuilderMethodNamedInTheMapExists()
        {
            var methods = typeof(ReplyKeyboardBuilder)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .ToHashSet();

            var absent = KeyboardFactoryToBuilderMethod.Values
                .Where(name => !methods.Contains(name))
                .ToList();

            absent.Should().BeEmpty(
                "the map names builder methods that no longer exist on ReplyKeyboardBuilder. " +
                "Absent: {0}",
                string.Join(", ", absent));
        }
    }
}
