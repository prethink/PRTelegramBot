namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utilities for working with access rights.
    /// </summary>
    public static class AccessUtils
    {
        #region Methods

        /// <summary>
        /// Reads the flags.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="mask">Access mask.</param>
        /// <returns>The access flags enum.</returns>
        public static TEnum ReadFlags<TEnum>(int mask)
            where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), mask);
        }

        /// <summary>
        /// Writes the flags.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="flags">Set of flags.</param>
        /// <returns>Access mask.</returns>
        public static int WriteFlags<TEnum>(TEnum flags)
            where TEnum : Enum
        {
            if (!IsFlagsEnum<TEnum>())
                throw new ArgumentException();
            return Convert.ToInt32(flags);
        }

        /// <summary>
        /// Checks whether the enum is a flags enum.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <returns>True if the enum is a flags enum; false if it is not.</returns>
        public static bool IsFlagsEnum<TEnum>()
            where TEnum : Enum
        {
            return Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute));
        }

        /// <summary>
        /// Checks whether the access mask carries the required flag.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="mask">Access mask</param>
        /// <param name="flag">The flag being checked.</param>
        /// <returns>True if the flag is set; False if it is not.</returns>
        public static bool HasFlag<TEnum>(int mask, TEnum flag)
            where TEnum : Enum
        {
            int flagValue = Convert.ToInt32(flag);
            return (mask & flagValue) != 0;
        }

        #endregion
    }
}
