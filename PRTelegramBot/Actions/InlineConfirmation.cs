using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;

namespace PRTelegramBot.Actions
{
    /// <summary>
    /// Base handler for action confirmation.
    /// </summary>
    public class InlineConfirmation
    {
        #region Methods

        /// <summary>
        /// Handles the action confirmation.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.CallbackWithConfirmation)]
        public static async Task ActionWithConfirmation(IBotContext context)
        {
            try
            {
                using (var inlineHandler = new InlineCallback<EntityTCommand<string>>(context))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    if (command?.Data?.EntityId is not null
                        && InlineCallbackWithConfirmation.TryGetPending(command.Data.EntityId, out var inlineCommand)
                        && inlineCommand is not null)
                    {
                        inlineCommand.YesCallback.ButtonName = inlineCommand.YesButton;
                        var yesButton = inlineCommand.YesCallback;
                        var noButton = inlineCommand.NoCallback;
                        var menu = new List<IInlineContent>() { yesButton, noButton };
                        var testMenu = MenuGenerator.InlineKeyboard(2, menu);
                        var option = new OptionMessage() { MenuInlineKeyboardMarkup = testMenu };
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                            await MessageEditor.Edit(context, inlineCommand.BaseMessage, option);
                        else
                            await MessageSender.Send(context, inlineCommand.BaseMessage, option);
                    }
                    else
                    {
                        string msg = "Something went wrong while running the command, please try again.";
                        await MessageEditor.Edit(context, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineConfirmation>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Base handler invoked when "no" is pressed.
        /// </summary>
        [InlineCallbackHandler<PRTelegramBotCommand>(PRConstants.ALL_BOTS_ID, PRTelegramBotCommand.CallbackWithConfirmationResultNo)]
        public static async Task ActionWithConfirmationResultNo(IBotContext context)
        {
            try
            {
                // The confirmation has been answered, so it no longer needs to be remembered.
                using (var inlineHandler = new InlineCallback<EntityTCommand<string>>(context))
                {
                    var command = inlineHandler.GetCommandByCallbackOrNull();
                    if (command?.Data?.EntityId is not null)
                        InlineCallbackWithConfirmation.Complete(command.Data.EntityId);
                }

                await context.BotClient.DeleteMessage(context.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<InlineConfirmation>().LogErrorInternal(ex);
            }
        }

        #endregion
    }
}
