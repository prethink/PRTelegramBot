using System.Security.Cryptography;
using System.Text;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utilities for working with strings.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Hash used for the file name.
        /// </summary>
        /// <param name="input">String.</param>
        /// <param name="limit">String.</param>
        /// <returns>Hash.</returns>
        public static string HashForFileName(string input, int limit = 16)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLowerInvariant().Substring(0, limit);
        }
    }
}
