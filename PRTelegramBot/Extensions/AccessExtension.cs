using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для проверки доступа.
    /// </summary>
    public static class AccessExtension
    {
        #region Методы

        /// <summary>
        /// Проверяет есть ли флаг в маске доступа.
        /// </summary>
        /// <typeparam name="TEnum">Тип enum который проверяем.</typeparam>
        /// <param name="mask">Маска доступа.</param>
        /// <param name="flag">Проверяемый флаг.</param>
        /// <returns>True - есть флаг, False - нет флага.</returns>
        public static bool HasFlag<TEnum>(this int mask, TEnum flag) where TEnum : Enum
        {
            return AccessUtils.HasFlag(mask, flag);
        }

        #endregion
    }
}
