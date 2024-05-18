using PRTelegramBot.Models.Enums;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    internal class StringCommandHandler : CommandHandler
    {
        #region Свойства и константы

        public StringComparison StringComparison { get; private set; }

        #endregion

        #region Конструкторы

        public StringCommandHandler(MethodInfo method)
            : this(method, null, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        public StringCommandHandler(MethodInfo method, CommandComparison commandComparison)
            : this(method, null, commandComparison, StringComparison.OrdinalIgnoreCase) { }

        public StringCommandHandler(MethodInfo method, IServiceProvider ServiceProvider)
            : this(method, ServiceProvider, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        public StringCommandHandler(Func<ITelegramBotClient, Update, Task> command)
            : this(command, null, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        public StringCommandHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider)
            : this(command, ServiceProvider, CommandComparison.Equals, StringComparison.OrdinalIgnoreCase) { }

        public StringCommandHandler(Func<ITelegramBotClient, Update, Task> command, CommandComparison commandComparison) 
            : this(command,null, commandComparison, StringComparison.OrdinalIgnoreCase) { }

        public StringCommandHandler(MethodInfo method, IServiceProvider ServiceProvider, CommandComparison commandComparison, StringComparison stringComparison) 
            : base(method, ServiceProvider, commandComparison)
        {
            this.StringComparison = StringComparison;
        }

        public StringCommandHandler(Func<ITelegramBotClient, Update, Task> command, IServiceProvider ServiceProvider, CommandComparison commandComparison, StringComparison stringComparison)
            : base(command, ServiceProvider, commandComparison)
        {
            this.StringComparison = StringComparison;
        }

        #endregion
    }
}
