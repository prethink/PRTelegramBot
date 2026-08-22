using FluentAssertions;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.UtilsTests
{
    public class GeneratorTests
    {
        private const string Digits = "0123456789";
        private const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Symbols = " ~`@#$%^&*()_+-=[]{};'\\:\"|,./<>?";

        #region RandomSymbols

        [TestCase(1)]
        [TestCase(8)]
        [TestCase(64)]
        public void RandomSymbolsProducesTheRequestedLength(int length)
        {
            Generator.RandomSymbols(Generator.Chars.Alphabet, length).Should().HaveLength(length);
        }

        [Test]
        public void RandomSymbolsPrependsThePrefix()
        {
            var result = Generator.RandomSymbols(Generator.Chars.Digits, 5, "ORD-");

            result.Should().StartWith("ORD-");
            result.Should().HaveLength("ORD-".Length + 5);
        }

        [Test]
        public void RandomSymbolsUsesDigitsOnly()
        {
            var result = Generator.RandomSymbols(Generator.Chars.Digits, 200);

            result.ToCharArray().Should().OnlyContain(c => Digits.Contains(c));
        }

        [Test]
        public void RandomSymbolsUsesLettersOnly()
        {
            var result = Generator.RandomSymbols(Generator.Chars.Alphabet, 200);

            result.ToCharArray().Should().OnlyContain(c => Letters.Contains(c));
        }

        [Test]
        public void RandomSymbolsUsesSymbolsOnly()
        {
            var result = Generator.RandomSymbols(Generator.Chars.Symbols, 200);

            result.ToCharArray().Should().OnlyContain(c => Symbols.Contains(c));
        }

        [Test]
        public void RandomSymbolsCombinesTheRequestedCharacterSets()
        {
            var result = Generator.RandomSymbols(Generator.Chars.Digits | Generator.Chars.Alphabet, 500);

            result.ToCharArray().Should().OnlyContain(c => Digits.Contains(c) || Letters.Contains(c));
            result.ToCharArray().Should().Contain(c => Digits.Contains(c));
            result.ToCharArray().Should().Contain(c => Letters.Contains(c));
        }

        [Test]
        public void RandomSymbolsReturnsOnlyThePrefixForZeroLength()
        {
            Generator.RandomSymbols(Generator.Chars.Digits, 0, "ONLY").Should().Be("ONLY");
        }

        [Test]
        public void RandomSymbolsProducesDifferentValues()
        {
            var values = Enumerable.Range(0, 20)
                .Select(_ => Generator.RandomSymbols(Generator.Chars.Alphabet | Generator.Chars.Digits, 24))
                .ToList();

            values.Distinct().Should().HaveCount(values.Count);
        }

        #endregion

        #region Coupon

        [Test]
        public void CouponUsesTheDefaultShape()
        {
            var coupon = Generator.Coupon();

            // Six characters, a separator, six more.
            coupon.Should().HaveLength(13);
            coupon.Split('-').Should().HaveCount(2);
        }

        [TestCase(4, 0, 4)]
        [TestCase(4, 1, 9)]
        [TestCase(5, 2, 17)]
        [TestCase(3, 3, 15)]
        public void CouponLengthFollowsTheSegmentsAndSeparators(int segmentLength, int countSplit, int expectedLength)
        {
            Generator.Coupon(segmentLength, countSplit).Should().HaveLength(expectedLength);
        }

        [Test]
        public void CouponSplitsIntoTheRequestedNumberOfSegments()
        {
            Generator.Coupon(4, 2).Split('-').Should().HaveCount(3);
        }

        [Test]
        public void CouponHonoursACustomSeparator()
        {
            var coupon = Generator.Coupon(4, 1, '_');

            coupon.Should().Contain("_");
            coupon.Should().NotContain("-");
            coupon.Split('_').Should().HaveCount(2);
        }

        [Test]
        public void CouponUsesUppercaseLettersAndDigitsOnly()
        {
            var coupon = Generator.Coupon(20, 0);

            coupon.ToCharArray().Should().OnlyContain(c => char.IsAsciiLetterUpper(c) || char.IsAsciiDigit(c));
        }

        [Test]
        public void CouponWithoutSplitsHasNoSeparator()
        {
            Generator.Coupon(8, 0).Should().NotContain("-");
        }

        #endregion
    }
}
