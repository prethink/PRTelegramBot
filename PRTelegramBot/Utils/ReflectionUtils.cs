using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers;
using PRTelegramBot.Interfaces;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Позволяет автоматически находить методы который помечены определенными атрибутами
    /// </summary>
    public class ReflectionUtils
    {
        public static object CreateInstanceWithNullArguments(Type type)
        {
            var parameters = type
                .GetConstructors()
                .Single()
                .GetParameters()
                .Select(p => (object)null)
                .ToArray();
            return Activator.CreateInstance(type, parameters);
        }
        /// <summary>
        /// Поиск методов в программе для выполнения reply команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static Type[] FindServicesToRegistration()
        {
            return FindClassesWithBotHandlerAttribute();
        }
        /// <summary>
        /// Поиск методов в программе для выполнения reply команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static MethodInfo[] FindStaticMessageMenuHandlers(long botId = 0)
        {
            var methods = FindMethods(typeof(ReplyMenuHandlerAttribute), BindingFlags.Public | BindingFlags.Static, botId);
            return methods.Where(x => x.GetCustomAttributes(typeof(ReplyMenuDynamicHandlerAttribute), true).Length == 0).ToArray();
        }

        /// <summary>
        /// Поиск методов в программе для выполнения reply команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static MethodInfo[] FindStaticMessageMenuDictionaryHandlers(long botId = 0)
        {
            return FindMethods(typeof(ReplyMenuDynamicHandlerAttribute), BindingFlags.Public | BindingFlags.Static, botId);
        }

        /// <summary>
        /// Поиск методов в программе для выполнения inline команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для inline команд</returns>
        public static MethodInfo[] FindStaticInlineMenuHandlers(long botId = 0)
        {
            return FindMethods(typeof(InlineCallbackHandlerAttribute<>), BindingFlags.Public | BindingFlags.Static, botId);
        }

        /// <summary>
        /// Поиск методов в программе для выполнения слеш команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для слеш команд</returns>
        public static MethodInfo[] FindStaticSlashCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(SlashHandlerAttribute), BindingFlags.Public | BindingFlags.Static, botId);
        }

        public static void FindEnumHeaders()
        {
            EnumHeaders enums = EnumHeaders.Instance;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // Обходим все сборки
            foreach (Assembly assembly in assemblies)
            {
                // Получаем все типы из сборки и ищем только перечисления
                var types = assembly.GetTypes().Where(type => type.IsEnum && type.GetCustomAttributes(typeof(InlineCommandAttribute), true).Any()).ToList();

                foreach (Type type in types)
                {
                    ValidateEnumIsInt(type);
                    Array enumValues = Enum.GetValues(type);
                    foreach (Enum item in enumValues)
                    {
                        var valint = Convert.ToInt32(item);
                        enums.Add(valint, item);
                    }
                }
            }
        }

        public static bool AddEnumsHeader(Enum @enum)
        {
            ValidateEnumIsInt(@enum);
            EnumHeaders enums = EnumHeaders.Instance;
            var valint = Convert.ToInt32(@enum);
            if(!enums.ContainsKey(valint, @enum))
            {
                enums.Add(valint, @enum);
                return true;
            }

            return false;
        }

        public static void ValidateEnumIsInt(Enum @enum)
        {
            Type enumType = @enum.GetType();
            ValidateEnumIsInt(enumType);
        }

        public static void ValidateEnumIsInt(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"{enumType} is not an Enum type.");
            }

            foreach (var value in Enum.GetValues(enumType))
            {
                if (!(Convert.ChangeType(value, enumType.GetEnumUnderlyingType()) is int))
                {
                    throw new ArgumentException($"{enumType}.{value} is not of type int.");
                }
            }
        }

        /// <summary>
        /// Поиск методов которые требуемый атрибут
        /// </summary>
        /// <param name="type">Тип атрибута</param>
        /// <returns>Массив найденных методов</returns>
        public static MethodInfo[] FindMethods(Type type, BindingFlags flags, long botId = 0)
        {
            var assemblyes = AppDomain.CurrentDomain.GetAssemblies();
            var list = new List<MethodInfo>();
            foreach (var item in assemblyes)
            {
                var tempMethods = item.GetTypes()
                 .SelectMany(t => t.GetMethods(flags))
                 .Where(m => m.GetCustomAttributes()
                     .OfType<IBotIdentifier>()
                     .Any(attr => attr.BotId == botId && (attr.GetType().IsGenericType ? attr.GetType().GetGenericTypeDefinition() == type : attr.GetType() == type))
                     )
                     .ToList();

                list.AddRange(tempMethods);
            }

            return list.ToArray();
        }

        public static Type[] FindClassesWithBotHandlerAttribute()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var uniqueTypes = new HashSet<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly
                    .GetTypes()
                    .Where(t => t.IsClass && t.GetCustomAttribute(typeof(BotHandlerAttribute)) != null);

                foreach (var type in types)
                    uniqueTypes.Add(type); 
            }
            return uniqueTypes.ToArray();
        }

        public static bool IsValidMethodForBaseBaseQueryAttribute(MethodInfo method)
        {
            try
            {
                Type expectedReturnType = typeof(Task);
                Type expectedBotClientType = typeof(ITelegramBotClient);
                Type expectedUpdateType = typeof(Update);

                ParameterInfo[] parameters = method.GetParameters();

                if (method.ReturnType == expectedReturnType &&
                    parameters.Length == 2 &&
                    parameters[0].ParameterType == expectedBotClientType &&
                    parameters[1].ParameterType == expectedUpdateType)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
