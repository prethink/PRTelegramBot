using System.ComponentModel;

namespace PRTelegramBot.Extensions
{
    public static class DescriptionExtension
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
        /// <summary>
        /// Позволяет получить описание у Enum
        /// </summary>
        /// <param name="command">enum</param>
        /// <returns>Описание</returns>
        public static string GetDescription(this Enum command)
        {
            return command.GetAttribute<DescriptionAttribute>()?.Description ?? "";
        }
    }
}
