using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class AccessExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="mask"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag<TEnum>(this int mask, TEnum flag) where TEnum : Enum
        {
            return AccessUtils.HasFlag(mask, flag);
        }
    }
}
