using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Обработчик выполнения обновление.
    /// </summary>
    public abstract class ExecuteHandler : UpdateHandler
    {
        #region Поля и свойства

        public override UpdateType TypeUpdate => UpdateType.Message;

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить метод.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения команды.</returns>
        protected virtual async Task<CommandResult> ExecuteMethod(Update update, CommandHandler handler)
        {
            var @delegate = handler.Command;

            var result = InternalCheck(update, handler);
            if (result != InternalCheckResult.Passed)
                return CommandResult.InternalCheck;

            await @delegate(bot.botClient, update);
            return CommandResult.Executed;
        }

        /// <summary>
        /// Внутрення проверка для <see cref="ExecuteMethod"/>
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <param name="handler">Обработчик.</param>
        /// <returns>Результат выполнения проверки.</returns>
        protected virtual InternalCheckResult InternalCheck(Update update, CommandHandler handler)
        {
            {
                var method = handler.Command.Method;
                var privilages = method.GetCustomAttribute<AccessAttribute>();
                var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
                var requireUpdate = method.GetCustomAttribute<RequiredTypeChatAttribute>();
                var @delegate = handler.Command;

                if (requireUpdate != null)
                {
                    if (!requireUpdate.TypesChat.Contains(update!.Message!.Chat.Type))
                    {
                        bot.Events.OnWrongTypeChatInvoke(new BotEventArgs(bot, update));
                        return InternalCheckResult.WrongChatType;
                    }
                }

                if (requireDate != null)
                {
                    if (!requireDate.TypeMessages.Contains(update!.Message!.Type))
                    {
                        bot.Events.OnWrongTypeMessageInvoke(new BotEventArgs(bot, update));
                        return InternalCheckResult.WrongMessageType;
                    }
                }

                if (privilages != null)
                {
                    bot.Events.OnCheckPrivilegeInvoke(new PrivilegeEventArgs(bot, update, @delegate, privilages.Mask));
                    return InternalCheckResult.PrivilegeCheck;
                }
                return InternalCheckResult.Passed;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Telegram bot.</param>
        protected ExecuteHandler(PRBotBase bot)
            : base(bot) { }

        #endregion
    }
}
