namespace PRTelegramBot.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class AccessUtils
    {
        #region Методы

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum ReadFlags<TEnum>(int value)
            where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static int WriteFlags<TEnum>(TEnum flags)
            where TEnum : Enum
        {
            if (!IsFlagsEnum<TEnum>())
                throw new ArgumentException();
            return Convert.ToInt32(flags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static bool IsFlagsEnum<TEnum>()
            where TEnum : Enum
        {
            return Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag<TEnum>(int value, TEnum flag)
            where TEnum : Enum
        {
            var flags = ReadFlags<TEnum>(value);
            return flags.HasFlag(flag);
        }

        #endregion
    }
}
