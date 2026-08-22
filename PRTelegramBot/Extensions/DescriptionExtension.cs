using System.ComponentModel;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for descriptions.
    /// </summary>
    public static class DescriptionExtension
    {
        #region Methods

        /// <summary>
        /// Gets the attribute from the enum value.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type.</typeparam>
        /// <param name="enum">A value from the enum.</param>
        /// <returns>Attribute.</returns>
        internal static TAttribute GetAttribute<TAttribute>(this Enum @enum) where TAttribute : Attribute
        {
            var enumType = @enum.GetType();
            var name = Enum.GetName(enumType, @enum);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        /// <summary>
        /// Lets you get the description of an enum value.
        /// </summary>
        /// <param name="enum">A value from the enum.</param>
        /// <returns>Description.</returns>
        public static string GetDescription(this Enum @enum)
        {
            return @enum.GetAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
        }

        #endregion
    }
}
