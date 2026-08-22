using System.Text;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Generator for various kinds of data.
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Digits.
        /// </summary>
        const string Digits = "0123456789";

        /// <summary>
        /// Alphabet.
        /// </summary>
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Characters.
        /// </summary>
        const string Symbols = " ~`@#$%^&*()_+-=[]{};'\\:\"|,./<>?";

        [Flags]
        public enum Chars
        {
            Digits = 0b0001,
            Alphabet = 0b0010,
            Symbols = 0b0100
        }

        /// <summary>
        /// Generates a random character set.
        /// </summary>
        /// <param name="chars">Specifies which kind of characters to use.</param>
        /// <param name="length">Length of the character set.</param>
        /// <param name="prefix">The prefix placed before the generated character set.</param>
        /// <returns>The generated character set.</returns>
        public static string RandomSymbols(Chars chars, int length, string prefix = "")
        {
            var random = new Random();
            var resultPassword = new StringBuilder(length);
            var passwordCharSet = string.Empty;
            resultPassword.Append(prefix);
            if (chars.HasFlag(Chars.Alphabet))
            {
                passwordCharSet += Alphabet + Alphabet.ToUpper();
            }
            if (chars.HasFlag(Chars.Digits))
            {
                passwordCharSet += Digits;
            }
            if (chars.HasFlag(Chars.Symbols))
            {
                passwordCharSet += Symbols;
            }
            for (var i = 0; i < length; i++)
            {
                resultPassword.Append(passwordCharSet[random.Next(0, passwordCharSet.Length)]);
            }
            return resultPassword.ToString();
        }

        /// <summary>
        /// Generates a coupon.
        /// Can be used for various campaigns or promo codes.
        /// </summary>
        /// <param name="segmentLength">Length of the code segment.</param>
        /// <param name="countSplit">Number of separators.</param>
        /// <param name="symbolSplit">Separator character; the default is - .</param>
        /// <returns>The generated coupon.</returns>
        public static string Coupon(int segmentLength = 6, int countSplit = 1, char symbolSplit = '-')
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var couponCharSet = Alphabet.ToUpper() + Digits;
            var result = new StringBuilder();

            for (int i = 0; i < countSplit + 1; i++)
            {
                for (int j = 0; j < segmentLength; j++)
                {
                    result.Append(couponCharSet[random.Next(0, couponCharSet.Length)]);
                }
                if (i < countSplit)
                    result.Append(symbolSplit);
            }

            return result.ToString();
        }
    }
}
