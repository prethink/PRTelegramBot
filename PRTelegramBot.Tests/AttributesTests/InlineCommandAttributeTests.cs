using PRTelegramBot.Attributes;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.AttributesTests
{
    internal class InlineCommandAttributeTests
    {
        [InlineCommand]
        public enum CustomTelegramCommand
        {
            One = 100,
            Two,
            Three
        }

        [Test]
        public void FindInlineCommands()
        {
            int exceptedCommandCount = 14;
            ReflectionUtils.FindEnumHeaders();
            var inlineCommands = EnumHeaders.Instance.GetAll();
            Assert.AreEqual(exceptedCommandCount, inlineCommands.Count);
        }
    }
}
