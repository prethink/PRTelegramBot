using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot;

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
        public static Type[] FindServicesToRegistration()
        {
            return FindUniqueClassesWithAttribute<TelegramBotHandlerAttribute>();
        }
        /// <summary>
        /// Поиск методов в программе для выполнения reply команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static MethodInfo[] FindStaticMessageMenuHandlers(long botId = 0)
        {
            var methods = FindMethods(typeof(ReplyMenuHandlerAttribute),(BindingFlags.Public | BindingFlags.Static), botId);
            return methods.Where(x => x.GetCustomAttributes(typeof(ReplyMenuDictionaryHandlerAttribute), true).Length == 0).ToArray();
        }

        /// <summary>
        /// Поиск методов в программе для выполнения reply команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для reply команд</returns>
        public static MethodInfo[] FindStaticMessageMenuDictionaryHandlers(long botId = 0)
        {
            return FindMethods(typeof(ReplyMenuDictionaryHandlerAttribute), (BindingFlags.Public | BindingFlags.Static), botId);
        }

        /// <summary>
        /// Поиск методов в программе для выполнения inline команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для inline команд</returns>
        public static MethodInfo[] FindStaticInlineMenuHandlers(long botId = 0)
        {
            return FindMethods(typeof(InlineCallbackHandlerAttribute<>), (BindingFlags.Public | BindingFlags.Static), botId);
        }

        /// <summary>
        /// Поиск методов в программе для выполнения слеш команд
        /// <param name="botId">Уникальный идентификатор бота</param>
        /// </summary>
        /// <returns>Массив методов для слеш команд</returns>
        public static MethodInfo[] FindStaticSlashCommandHandlers(long botId = 0)
        {
            return FindMethods(typeof(SlashHandlerAttribute), (BindingFlags.Public | BindingFlags.Static), botId);
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
                    if (type.IsEnum && type.GetCustomAttributes(typeof(InlineCommandAttribute), false).Any())
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

        public static Type[] FindUniqueClassesWithAttribute<TAttribute>() where TAttribute : TelegramBotHandlerAttribute
        {
            var assemblyes = AppDomain.CurrentDomain.GetAssemblies();

            var uniqueClasses = assemblyes
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(type => type.GetCustomAttributes(typeof(TAttribute), true)
                                       .OfType<TAttribute>()
                                       .Any())
                )
                .Distinct()
                .ToArray();

            return uniqueClasses;
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
                     .OfType<BaseQueryAttribute>()
                     .Any(attr => attr.BotId == botId && (attr.GetType().IsGenericType ? attr.GetType().GetGenericTypeDefinition() == type : attr.GetType() == type))
                     )
                     .ToList();

                list.AddRange(tempMethods);
            }

            return list.ToArray();
        }

        public static bool IsValidMethorForBaseBaseQueryAttribute(MethodInfo method)
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
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
