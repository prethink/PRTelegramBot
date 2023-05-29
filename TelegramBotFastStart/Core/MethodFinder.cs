using PRTelegramBot.Attributes;
using System.Reflection;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Позволяет автоматически находить методы который помечены определенными атрибутами
    /// </summary>
    public class MethodFinder
    {
        /// <summary>
        /// Поиск методов в программе для выполнения reply команд
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static MethodInfo[] FindMessageMenuHandlers()
        {
            return FindMethods(typeof(ReplyMenuHandlerAttribute));
        }

        /// <summary>
        /// Поиск методов в программе для выполнения inline команд
        /// </summary>
        /// <returns>Массив методов для inline команд</returns>
        public static MethodInfo[] FindInlineMenuHandlers()
        {
            return FindMethods(typeof(InlineCallbackHandlerAttribute));
        }

        /// <summary>
        /// Поиск методов в программе для выполнения слеш команд
        /// </summary>
        /// <returns>Массив методов для слеш команд</returns>
        public static MethodInfo[] FindSlashCommandHandlers()
        {
            return FindMethods(typeof(SlashHandlerAttribute));
        }

        /// <summary>
        /// Поиск методов которые требуемый атрибут
        /// </summary>
        /// <param name="type">Тип атрибута</param>
        /// <returns>Массив найденных методов</returns>
        public static MethodInfo[] FindMethods(Type type)
        {
            string thisAssemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.ToLower() == thisAssemblyName.ToLower())
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
                .Where(m => m.GetCustomAttributes(type, false).Length > 0)
                .ToArray();

            return result;
        }
    }
}
