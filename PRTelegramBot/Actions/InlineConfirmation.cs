﻿using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;

namespace PRTelegramBot.Actions
{
    /// <summary>
    /// Базовый обработчик для подтверждения действия.
    /// </summary>
    public static class InlineConfirmation
    {
        #region Методы

        /// <summary>
        /// Обработка подтверждения действия.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.CallbackWithConfirmation)]
        public static async Task ActionWithConfirmation(IBotContext context)
        {
            try
            {
                using (var inlineHandler = new InlineCallback<EntityTCommand<string>>(context))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    if (InlineCallbackWithConfirmation.DataCollection.TryGetValue(command.Data.EntityId, out var inlineCommand))
                    {
                        inlineCommand.YesCallback.ButtonName = inlineCommand.YesButton;
                        var yesButton = inlineCommand.YesCallback;
                        var noButton = inlineCommand.NoCallback;
                        var menu = new List<IInlineContent>() { yesButton, noButton };
                        var testMenu = MenuGenerator.InlineKeyboard(2, menu);
                        var option = new OptionMessage() { MenuInlineKeyboardMarkup = testMenu };
                        var actionLastMessage = command.Data.GetActionWithLastMessage();
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                            await Helpers.Message.Edit(context, inlineCommand.BaseMessage, option);
                        else
                            await Helpers.Message.Send(context, inlineCommand.BaseMessage, option);
                    }
                    else
                    {
                        string msg = "Ошибка при выполнение команды, попробуйте еще раз.";
                        await Helpers.Message.Edit(context, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
            }
        }

        /// <summary>
        /// Базовый обработчик при нажатие на нет.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.CallbackWithConfirmationResultNo)]
        public static async Task ActionWithConfirmationResultNo(IBotContext context)
        {
            try
            {
                await context.BotClient.DeleteMessage(context.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
            }
        }

        #endregion
    }
}
