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
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static MethodInfo[] FindMessageMenuHandlers(long botId = 0)
        {
            return FindMethods(typeof(ReplyMenuHandlerAttribute), botId);
        }

        /// <summary>
        /// Поиск методов в программе для выполнения inline команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для inline команд</returns>
        public static MethodInfo[] FindInlineMenuHandlers(long botId = 0)
        {
            return FindMethods(typeof(InlineCallbackHandlerAttribute<>), botId);
        }

        /// <summary>
        /// Поиск методов в программе для выполнения слеш команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для слеш команд</returns>
        public static MethodInfo[] FindSlashCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(SlashHandlerAttribute), botId);
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
        public static MethodInfo[] FindMethods(Type type, long botId = 0)
        {
            string thisAssemblyName = Assembly.GetEntryAssembly().GetName().FullName;
            var query = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.ToLower() == thisAssemblyName.ToLower())
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
                ;

            if(botId != 0)
            {
                return query
                    .Where(m => m.GetCustomAttributes(type, false)
                    .OfType<BaseQueryAttribute>()
                    .Any(attr => attr.BotId == botId))
                    .ToArray();
            }

            return query.Where(m => m.GetCustomAttributes(type, false).Any()).ToArray();

        }
    }
}
