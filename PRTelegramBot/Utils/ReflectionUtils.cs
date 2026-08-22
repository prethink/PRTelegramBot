using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using System.Reflection;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Automatically finds the methods marked with specific attributes
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
        /// Searches the program for methods that handle reply commands
        /// </summary>
        /// <returns>Array of methods that handle reply commands</returns>
        public static Type[] FindServicesToRegistration()
        {
            return FindClassesWithBotHandlerAttribute();
        }
        /// <summary>
        /// Searches the program for methods that handle reply commands
        /// <param name="botId">Unique bot identifier</param>
        /// </summary>
        /// <returns>Array of methods that handle reply commands</returns>
        public static MethodInfo[] FindStaticReplyCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(ReplyMenuHandlerAttribute), BindingFlags.Public | BindingFlags.Static, botId);
        }

        /// <summary>
        /// Searches the program for methods that handle reply commands
        /// <param name="botId">Unique bot identifier</param>
        /// </summary>
        /// <returns>Array of methods that handle reply commands</returns>
        public static MethodInfo[] FindStaticDynamicReplyCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(ReplyMenuDynamicHandlerAttribute), BindingFlags.Public | BindingFlags.Static, botId);
        }

        /// <summary>
        /// Searches the program for methods that handle inline commands
        /// <param name="botId">Unique bot identifier</param>
        /// </summary>
        /// <returns>Array of methods that handle inline commands</returns>
        public static MethodInfo[] FindStaticInlineCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(InlineCallbackHandlerAttribute<>), BindingFlags.Public | BindingFlags.Static, botId);
        }

        /// <summary>
        /// Searches the program for methods that handle slash commands
        /// <param name="botId">Unique bot identifier</param>
        /// </summary>
        /// <returns>Array of methods that handle slash commands</returns>
        public static MethodInfo[] FindStaticSlashCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(SlashHandlerAttribute), BindingFlags.Public | BindingFlags.Static, botId);
        }

        public static void FindEnumHeaders()
        {
            EnumHeaders enums = EnumHeaders.Instance;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // Iterate over all assemblies
            foreach (Assembly assembly in assemblies)
            {
                // Get all types from the assembly and keep only the enums
                var types = assembly
                    .GetTypes()
                    .Where(type => type.IsEnum && type.GetCustomAttributes(typeof(InlineCommandAttribute), true)
                    .Any())
                    .ToList();

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
                throw new ArgumentException($"{enumType} is not an Enum type.");

            foreach (var value in Enum.GetValues(enumType))
            {
                if (!(Convert.ChangeType(value, enumType.GetEnumUnderlyingType()) is int))
                    throw new ArgumentException($"{enumType}.{value} is not of type int.");
            }
        }

        /// <summary>
        /// Searches for methods that carry the required attribute
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Array of the methods that were found</returns>
        public static MethodInfo[] FindMethods(Type type, BindingFlags flags, long botId = 0)
        {
            var assemblyes = AppDomain.CurrentDomain.GetAssemblies();
            var list = new List<MethodInfo>();
            foreach (var item in assemblyes)
            {
                var tempMethods = item.GetTypes()
                 .SelectMany(t => t.GetMethods(flags))
                 .Where(m => m.GetCustomAttributes()
                     .OfType<IBaseQueryAttribute>()
                     .Any(attr => (attr.BotIds.Contains(botId)  || attr.BotIds.Any(x => x == PRConstants.ALL_BOTS_ID)) && (attr.GetType().IsGenericType 
                     ? attr.GetType().GetGenericTypeDefinition() == type 
                     : attr.GetType() == type))
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
                    .Where(t => t.IsClass && t.GetCustomAttribute(typeof(BotHandlerAttribute)) is not null);

                foreach (var type in types)
                    uniqueTypes.Add(type); 
            }
            return uniqueTypes.ToArray();
        }

        public static bool IsValidMethodForBaseBaseQueryAttribute(PRBotBase bot, MethodInfo method)
        {
            try
            {
                Type expectedReturnType = typeof(Task);
                Type expectedBotContext = typeof(IBotContext);

                ParameterInfo[] parameters = method.GetParameters();

                if (method.ReturnType == expectedReturnType &&
                    parameters.Length == 1 &&
                    parameters[0].ParameterType == expectedBotContext)
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
                bot.GetLogger<ReflectionUtils>().LogErrorInternal(ex);
                return false;
            }
        }
    }
}
