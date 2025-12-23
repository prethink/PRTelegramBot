using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Core.CommandStores
{
    /// <summary>
    /// Хранилище для slash команд.
    /// </summary>
    public sealed class SlashCommandStore : MessageCommandStore
    {
        #region Базовый класс

        /// <summary>
        /// Зарегистрировать команды.
        /// </summary>
        public override void RegisterCommand()
        {
            MethodInfo[] methods = ReflectionUtils.FindStaticSlashCommandHandlers(bot.Options.BotId);
            registerService.RegisterStaticCommand(bot, typeof(SlashHandlerAttribute), methods, Commands);

            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();
            foreach (var serviceType in servicesToRegistration)
            {
                var methodsInClass = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                registerService.RegisterMethodFromClass(bot, typeof(SlashHandlerAttribute), methodsInClass, Commands, bot.Options.ServiceProvider);
            }
        }

        /// <summary>
        /// Добавить новую команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="delegate">Метод обработки команды.</param>
        /// <returns>True - команда добавлена, False - не удалось добавить команду.</returns>
        public override bool AddCommand(string command, Func<IBotContext, Task> @delegate)
        {
            if (!command.StartsWith('/'))
                command = "/" + command;

            return base.AddCommand(command, @delegate);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public SlashCommandStore(PRBotBase bot) : base(bot) { }

        #endregion
    }
}
