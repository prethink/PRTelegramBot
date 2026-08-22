using ConsoleExample.Extension;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleEvents
    {
        public static async Task OnWrongTypeChat(BotEventArgs e)
        {
            string msg = "Invalid chat type";
            await MessageSender.Send(e.Context, msg);
        }

        public static async Task OnMissingCommand(BotEventArgs args)
        {
            string msg = "Command not found";
            await MessageSender.Send(args.Context, msg);
        }

        public static async Task OnErrorCommand(BotEventArgs args)
        {
            string msg = "An error occurred while handling the command";
            await MessageSender.Send(args.Context, msg);
        }

        /// <summary>
        /// Event raised to check the user's privileges
        /// </summary>
        /// <param name="callback">callback invoked on success</param>
        /// <param name="mask">Access mask</param>
        /// Subscribes to the privilege check event <see cref="Program"/>
        public static async Task OnCheckPrivilege(PrivilegeEventArgs e)
        {
            if (!e.Mask.HasValue)
            {
                // No access mask, run the method.
                await e.ExecuteMethod(e.Context);
                return;
            }

            // Get the value of the required access mask.
            var requiredAccess = e.Mask.Value;

            // Get the user's access flags.
            // Here you implement the flag lookup however you like — for example, they can be read from a database.
            var userFlags = e.Context.Update.LoadExampleFlagPrivilege();

            if (requiredAccess.HasFlag(userFlags))
            {
                // Access granted, run the method.
                await e.ExecuteMethod(e.Context);
                return;
            }

            // No access.
            string errorMsg = "You do not have access to this feature";
            await MessageSender.Send(e.Context, errorMsg);
            return;

        }

        public static async Task OnUserStartWithArgs(StartEventArgs args)
        {
            string msg = "The user sent start with an argument";
            await MessageSender.Send(args.Context, msg);
        }
        public static async Task OnWrongTypeMessage(BotEventArgs e)
        {
            string msg = "Invalid message type";
            await MessageSender.Send(e.Context, msg);
        }
    }
}
