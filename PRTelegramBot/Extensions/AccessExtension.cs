using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for access checks.
    /// </summary>
    public static class AccessExtension
    {
        #region Methods

        /// <summary>
        /// Checks whether the flag is present in the access mask.
        /// </summary>
        /// <typeparam name="TEnum">The enum type being checked.</typeparam>
        /// <param name="mask">Access mask.</param>
        /// <param name="flag">The flag being checked.</param>
        /// <returns>True if the flag is set; False if it is not.</returns>
        public static bool HasFlag<TEnum>(this int mask, TEnum flag) where TEnum : Enum
        {
            return AccessUtils.HasFlag(mask, flag);
        }

        #endregion
    }
}
