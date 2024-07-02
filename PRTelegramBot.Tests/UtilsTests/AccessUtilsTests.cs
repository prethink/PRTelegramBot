using FluentAssertions;
using PRTelegramBot.Extensions;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.UtilsTests
{
    public class AccessUtilsTests
    {
        private const int READ_FLAG = 1;
        private const int WRITE_FLAG = 2;
        private const int READ_WRITE_FLAG = 3;

        [Flags]
        public enum TestAccess
        {
            Read = 1,
            Write = 2,
            ReadWrite = Read | Write,
        }

        [Test]
        [TestCase(READ_WRITE_FLAG, TestAccess.ReadWrite)]
        [TestCase(WRITE_FLAG, TestAccess.Write)]
        [TestCase(READ_FLAG, TestAccess.Read)]
        public void ReadFlagsShouldBeTrue(int mask, TestAccess exceptedFlag)
        {
            mask.HasFlag(exceptedFlag).Should().BeTrue();
        }

        [Test]
        [TestCase(WRITE_FLAG, TestAccess.Read)]
        [TestCase(WRITE_FLAG, TestAccess.ReadWrite)]
        [TestCase(READ_FLAG, TestAccess.Write)]
        [TestCase(READ_FLAG, TestAccess.ReadWrite)]
        public void ReadFlagsShouldBeFalse(int mask, TestAccess exceptedFlag)
        {
            mask.HasFlag(exceptedFlag).Should().BeFalse();
        }

        [Test]
        [TestCase(TestAccess.ReadWrite, READ_WRITE_FLAG)]
        [TestCase(TestAccess.Read, READ_FLAG)]
        [TestCase(TestAccess.Write, WRITE_FLAG)]
        public void WriteFlagsExpectedFlags(TestAccess flag, int exceptedMask)
        {
            var mask = AccessUtils.WriteFlags(flag);
            Assert.AreEqual(exceptedMask, mask);
        }
    }
}
