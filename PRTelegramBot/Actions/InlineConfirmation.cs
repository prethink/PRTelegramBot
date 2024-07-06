﻿using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Actions
{
    /// <summary>
    /// Базовый обработчик для подтверждения действия.
    /// </summary>
    public static class InlineConfirmation
    {
        /// <summary>
        /// Обработка подтверждения действия.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.CallbackWithConfirmation)]
        public static async Task ActionWithConfirmation(ITelegramBotClient botClient, Update update)
        {
            try
            {
                var command = InlineCallback<EntityTCommand<string>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    if(InlineCallbackWithConfirmation.DataCollection.TryGetValue(command.Data.EntityId, out var inlineCommand))
                    {
                        inlineCommand.YesCallback.ButtonName = inlineCommand.YesButton;
                        var yesButton = inlineCommand.YesCallback;
                        var noButton = inlineCommand.NoCallback;  
                        var menu = new List<IInlineContent>() { yesButton , noButton };
                        var testMenu = MenuGenerator.InlineKeyboard(2, menu);
                        var option = new OptionMessage() { MenuInlineKeyboardMarkup = testMenu };
                        var actionLastMessage = command.Data.GetActionWithLastMessage();
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                        {
                            await Helpers.Message.Edit(botClient, update, inlineCommand.BaseMessage, option);
                        }
                        else
                        {
                            await Helpers.Message.Send(botClient, update, inlineCommand.BaseMessage, option);
                            if(actionLastMessage == ActionWithLastMessage.Delete)
                            {
                                await botClient.DeleteMessageAsync(update.GetChatIdClass(), update.CallbackQuery.Message.MessageId);
                            }
                        }
                    }
                    else
                    {
                        string msg = "Ошибка при выполнение команды, попробуйте еще раз.";
                        await Helpers.Message.Edit(botClient, update, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                botClient.GetBotDataOrNull().Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Базовый обработчик при нажатие на нет.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(-1, PRTelegramBotCommand.CallbackWithConfirmationResultNo)]
        public static async Task ActionWithConfirmationResultNo(ITelegramBotClient botClient, Update update)
        {
            try
            {
                await botClient.DeleteMessageAsync(update.GetChatIdClass(), update.CallbackQuery.Message.MessageId);
            }
            catch (Exception ex)
            {
                botClient.GetBotDataOrNull().Events.OnErrorLogInvoke(ex);
            }
        }
    }
}
