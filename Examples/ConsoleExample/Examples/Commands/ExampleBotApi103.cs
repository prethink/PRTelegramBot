using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleExample.Examples.Commands
{
    /// <summary>
    /// What Bot API 10.3 added: buttons that are visible but inert, and messages that only
    /// one person can see.
    /// </summary>
    internal class ExampleBotApi103
    {
        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Disabled" is sent to the chat.
        /// Example of a menu whose shape stays put while some of it is unavailable.
        /// </summary>
        [ReplyMenuHandler("Disabled")]
        public static async Task DisabledButtons(IBotContext context)
        {
            /* Before 10.3 an unavailable option had two bad choices: drop the button, which makes
             * the menu jump around, or keep it live and explain the refusal after the tap.
             * InlineDisabled is the third: Telegram greys the button out and never sends a
             * callback, so the layout holds still and the handler is never bothered.
             */
            var menu = new InlineKeyboardBuilder()
                .AddButton(new InlineCallback("Step 1 — done", BotApi103Header.UnlockedStep))
                .AddRowWithButton(new InlineDisabled("Step 2 — finish step 1 first"))
                .AddRowWithButton(new InlineDisabled("Step 3 — locked"))
                .Build();

            var option = new OptionMessage { MenuInlineKeyboardMarkup = menu };
            await MessageSender.Send(context, "Only the first step is open:", option);
        }

        /// <summary>
        /// Handles a press on the one button that is not disabled.
        /// </summary>
        [InlineCallbackHandler<BotApi103Header>(BotApi103Header.UnlockedStep)]
        public static async Task UnlockedStep(IBotContext context)
        {
            await MessageSender.Send(context, "This one works. The greyed-out buttons send nothing at all.");
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Ephemeral" is sent to the chat.
        /// Example of a reply only one person in the chat can see.
        /// </summary>
        [ReplyMenuHandler("Ephemeral")]
        public static async Task EphemeralMenu(IBotContext context)
        {
            /* An ephemeral message is drawn as an overlay for a single user and never lands in
             * the chat history. It is how a bot answers one person in a group without the
             * other members reading along.
             *
             * Run this in a GROUP, not in a private chat with the bot — ephemeral messages
             * exist to keep a shared timeline uncluttered and are refused anywhere else.
             *
             * The two buttons below are the route that works for any bot: an ephemeral reply
             * within 15 seconds of a callback query needs no special rights.
             */
            var menu = new InlineKeyboardBuilder()
                .AddButton(new InlineCallback("Show me my balance", BotApi103Header.AnswerPrivately))
                .AddRowWithButton(new InlineCallback("Replace this menu", BotApi103Header.ReplaceTheMenu))
                .Build();

            var option = new OptionMessage { MenuInlineKeyboardMarkup = menu };
            await MessageSender.Send(context, "Everyone sees this message. The replies will be private.", option);
        }

        /// <summary>
        /// Answers the person who pressed the button, and only them.
        /// </summary>
        [InlineCallbackHandler<BotApi103Header>(BotApi103Header.AnswerPrivately)]
        public static async Task AnswerPrivately(IBotContext context)
        {
            /* SendEphemeral takes the receiver from the update, and — because this came from a
             * button — the callback query id as well, so Telegram knows which tap to answer.
             */
            await MessageSender.SendEphemeral(context, "Your balance is 42 ⭐. Nobody else in this chat sees this.");
        }

        /// <summary>
        /// Shows the ephemeral message in place of the menu instead of over it.
        /// </summary>
        [InlineCallbackHandler<BotApi103Header>(BotApi103Header.ReplaceTheMenu)]
        public static async Task ReplaceTheMenu(IBotContext context)
        {
            /* replaceCallbackQueryMessage swaps the original message out for this one, for this
             * user only. Everyone else in the chat still sees the menu untouched.
             *
             * It needs a group. In a private chat Telegram refuses it with MESSAGE_ID_INVALID —
             * there is no shared timeline to replace anything on, and a one-to-one message is
             * edited with the edit methods instead. The plain ephemeral reply above works in both,
             * so this is a limit of the replace flag rather than of ephemeral messages.
             */
            if (context.Update.CallbackQuery?.Message?.Chat.Type == ChatType.Private)
            {
                await MessageSender.SendEphemeral(context, "Replacing needs a group — try this example there.");
                return;
            }

            await MessageSender.SendEphemeral(context, "The menu is gone — for you.", replaceCallbackQueryMessage: true);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "EphemeralTo" is sent to the chat.
        /// Example of an ephemeral message aimed at somebody other than the sender.
        /// </summary>
        [ReplyMenuHandler("EphemeralTo")]
        public static async Task EphemeralToSomebodyElse(IBotContext context)
        {
            /* The receiver does not have to be the person who triggered the command. A moderation
             * bot can drop a note to a moderator in the same chat this way.
             *
             * REQUIRES THE BOT TO BE AN ADMINISTRATOR of the chat. There is no callback query
             * to quote here, so this is the third of Telegram's three routes — the only one
             * that can start an ephemeral exchange rather than continue one. Without the rights
             * Telegram answers: Bad Request: BOT_NOT_ADMIN
             *
             * Delivery is not guaranteed either: a user who is offline may simply never see it.
             */
            if (context.Update.Message?.Chat.Type == ChatType.Private)
            {
                await MessageSender.Send(context, "This one needs a group where the bot is an administrator. "
                    + "There is no button press here to answer, and a private chat has no administrators.");
                return;
            }

            /* Whether the bot actually holds the rights is not checked here — GroupUtils could
             * ask Telegram, at the cost of a round trip. In a group without them the call below
             * still fails with BOT_NOT_ADMIN, which is the honest outcome to see.
             */
            var moderatorId = context.Update.GetUserId();
            await MessageSender.SendEphemeral(context, moderatorId, "A quiet word, just for you.");
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "Rich" is sent to the chat.
        /// Example of a rich message — headings, lists and quotations as real structure.
        /// </summary>
        [ReplyMenuHandler("Rich")]
        public static async Task Rich(IBotContext context)
        {
            /* A rich message is not a formatted one. A formatted message is a single run of
             * text with entities laid over it; a rich message is built from blocks — headings,
             * lists, tables, quotations, dividers, embedded media — and Telegram lays them out.
             *
             * The HTML dialect here is the rich message one, not what ParseMode.Html accepts.
             * The framework passes it through as HTML; Telegram does the parsing.
             */
            const string html = """
                <h1>Weekly report</h1>
                <p>Revenue is up <b>12%</b> on last week.</p>
                <ul>
                    <li>New users: 1 204</li>
                    <li>Churn: 0.8%</li>
                </ul>
                <blockquote>Growth held through the weekend.</blockquote>
                """;

            await MessageSender.SendRichMessage(context, html);
        }

        /// <summary>
        /// The command will run for the bot with botId 0.
        /// The command runs when "RichPrivate" is sent to the chat.
        /// Example of a rich message only one person sees.
        /// </summary>
        [ReplyMenuHandler("RichPrivate")]
        public static async Task RichPrivate(IBotContext context)
        {
            /* The report itself goes out from the button handler below, not from here.
             *
             * That is not decoration: an ephemeral message needs either a callback query to
             * answer, an incoming ephemeral message to reply to, or administrator rights in
             * the chat. Sending one straight from this command handler has none of the three,
             * and Telegram refuses it with: Bad Request: BOT_NOT_ADMIN
             *
             * Going through a button gives us the callback query, and then it works for any
             * bot in any group.
             */
            var menu = new InlineKeyboardBuilder()
                .AddButton(new InlineCallback("Show my report", BotApi103Header.RichPrivately))
                .Build();

            var option = new OptionMessage { MenuInlineKeyboardMarkup = menu };
            await MessageSender.Send(context, "Everyone sees this. The report will not be.", option);
        }

        /// <summary>
        /// Sends a rich message that only the person who pressed the button can see.
        /// </summary>
        [InlineCallbackHandler<BotApi103Header>(BotApi103Header.RichPrivately)]
        public static async Task RichPrivately(IBotContext context)
        {
            /* Every OptionMessage setting a rich message can carry is mapped the same way it
             * is for an ordinary message — including the ephemeral parameters, so the two
             * features above combine without any extra work.
             *
             * SendEphemeral fills these in from the update; here we are not going through it,
             * so the callback query id has to be supplied by hand.
             */
            var option = new OptionMessage
            {
                EphemeralMessageParameters = new EphemeralMessageParameters
                {
                    ReceiverUserId = context.Update.GetUserId(),
                    CallbackQueryId = context.Update.CallbackQuery?.Id,
                }
            };

            await MessageSender.SendRichMessage(context, "<h1>Your report</h1><p>Only you can see this.</p>", option);
        }
    }
}
