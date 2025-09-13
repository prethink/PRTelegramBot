using System.ComponentModel;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для описания.
    /// </summary>
    public static class DescriptionExtension
    {
        #region Методы

        /// <summary>
        /// Получить атрибут из перечисления.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <param name="enum">Значение из перечисления.</param>
        /// <returns>Атрибут.</returns>
        internal static TAttribute GetAttribute<TAttribute>(this Enum @enum) where TAttribute : Attribute
        {
            var enumType = @enum.GetType();
            var name = Enum.GetName(enumType, @enum);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        /// <summary>
        /// Позволяет получить описание у Enum.
        /// </summary>
        /// <param name="enum">Значение из перечисления.</param>
        /// <returns>Описание.</returns>
        public static string GetDescription(this Enum @enum)
        {
            return @enum.GetAttribute<DescriptionAttribute>()?.Description ?? "";
        }

        #endregion
    }
}
