using System.Security.Cryptography;
using System.Text;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Утилиты для работы со строками.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Хеш для имени файла.
        /// </summary>
        /// <param name="input">Строка.</param>
        /// <param name="limit">Строка.</param>
        /// <returns>Хэш.</returns>
        public static string HashForFileName(string input, int limit = 16)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLowerInvariant().Substring(0, limit);
        }
    }
}
