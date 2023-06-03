using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers;
using System.Reflection;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Позволяет автоматически находить методы который помечены определенными атрибутами
    /// </summary>
    public class ReflectionFinder
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
            return FindMethods(typeof(InlineCallbackHandlerAttribute<>));
        }

        /// <summary>
        /// Поиск методов в программе для выполнения слеш команд
        /// </summary>
        /// <returns>Массив методов для слеш команд</returns>
        public static MethodInfo[] FindSlashCommandHandlers()
        {
            return FindMethods(typeof(SlashHandlerAttribute));
        }

        public static void FindEnumHeaders()
        {
            EnumHeaders enums = EnumHeaders.Instance;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            int count = 0;
            // Обходим все сборки
            foreach (Assembly assembly in assemblies)
            {
                // Получаем все типы из сборки
                Type[] types = assembly.GetTypes();

                // Ищем только перечисления
                foreach (Type type in types)
                {
                    if (type.IsEnum && type.Name.Contains("THeader"))
                    {
                        Array enumValues = Enum.GetValues(type);
                        foreach (Enum item in enumValues)
                        {
                            enums.Add(count, item);
                            count++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Поиск методов которые требуемый атрибут
        /// </summary>
        /// <param name="type">Тип атрибута</param>
        /// <returns>Массив найденных методов</returns>
        public static MethodInfo[] FindMethods(Type type)
        {
            string thisAssemblyName = Assembly.GetEntryAssembly().GetName().FullName;
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
