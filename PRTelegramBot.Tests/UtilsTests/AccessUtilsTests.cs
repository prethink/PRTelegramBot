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
        public void AccessUtils_ReadFlags_HasFlag(int mask, TestAccess exceptedFlag)
        {
            Assert.IsTrue(mask.HasFlag(exceptedFlag));
        }

        [Test]
        [TestCase(WRITE_FLAG, TestAccess.Read)]
        [TestCase(WRITE_FLAG, TestAccess.ReadWrite)]
        [TestCase(READ_FLAG, TestAccess.Write)]
        [TestCase(READ_FLAG, TestAccess.ReadWrite)]
        public void AccessUtils_ReadFlags_NotHasFlag(int mask, TestAccess exceptedFlag)
        {
            Assert.IsTrue(!mask.HasFlag(exceptedFlag));
        }

        [Test]
        [TestCase(TestAccess.ReadWrite, READ_WRITE_FLAG)]
        [TestCase(TestAccess.Read, READ_FLAG)]
        [TestCase(TestAccess.Write, WRITE_FLAG)]
        public void AccessUtils_WriteFlags(TestAccess flag, int exceptedMask)
        {
            var mask = AccessUtils.WriteFlags(flag);
            Assert.IsTrue(mask == exceptedMask);
        }
    }
}
