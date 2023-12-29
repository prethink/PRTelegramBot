using System.Text;
using Telegram.Bot;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Генератор разных данных
    /// </summary>
    public static class Generator
    {
        const string Digits = "0123456789";
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        const string Symbols = " ~`@#$%^&*()_+-=[]{};'\\:\"|,./<>?";

        [Flags]
        public enum Chars
        {
            Digits = 0b0001,
            Alphabet = 0b0010,
            Symbols = 0b0100
        }

        /// <summary>
        /// Генериурет случайный набор символов
        /// </summary>
        /// <param name="chars">Указывает какого типа должны быть символы</param>
        /// <param name="length">Длина набора символов</param>
        /// <param name="prefix">Префикс ставится перед сгенерированным набором символов</param>
        /// <returns>Сгенерированный набор символов</returns>
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
        /// Генерирует купон
        /// Можно использовать для разных акций или промо кодов
        /// </summary>
        /// <param name="segmentLength">Длина сегмента кода</param>
        /// <param name="countSplit">Количество разделителей</param>
        /// <param name="symbolSplit">Символ разделителя, по умолчанию - </param>
        /// <returns>Сгенерированный купон</returns>
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

        /// <summary>
        /// Генерирует реферальную ссылку
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <param name="refLink">реферальная ссылка</param>
        /// <returns>Реферальная ссылка пример https://t.me/{bot.Username}?start={refLink}</returns>
        public async static Task<string> GenerateRefLink(this ITelegramBotClient botClient, string refLink)
        {
            if (string.IsNullOrEmpty(refLink))
            {
                throw new ArgumentNullException(nameof(refLink));
            }

            var bot = await botClient.GetMeAsync();
            return $"https://t.me/{bot.Username}?start={refLink}";
        }
    }
}
