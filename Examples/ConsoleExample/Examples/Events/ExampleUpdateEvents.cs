using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleUpdateEvents
    {
        public static async Task OnUpdateMyChatMember(BotEventArgs args)
        {
            //Handling the information from myChatHandle
            var myChatHandle = args.Context.Update.MyChatMember;
            try
            {
                if (myChatHandle.NewChatMember.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Member)
                {
                    if (myChatHandle.NewChatMember.User.Id == args.Context.BotClient.BotId)
                    {
                        await MessageSender.Send(args.Context, "Hello world");
                    }
                    else
                    {
                        //Other actors
                    }
                }
            }
            catch (Exception ex)
            {
                args.Context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(args.Context, ex));
            }
        }

        public async static Task<UpdateResult> Handler_OnUpdate(BotEventArgs e)
        {
            /*
             As an example, consider whether the user is registered or not.
                If they are registered
                    return UpdateResult.Continue; - this result lets processing continue.
                If they are not registered, call the registration method
                    RegisterMethod();
                    return UpdateResult.Stop or return UpdateResult.Handled - stops the current processing and sends the user to registration
             */
            return UpdateResult.Continue;
        }

        public async static Task Handler_OnPostUpdate(BotEventArgs e)
        {
            // Example. Recording the user's last activity in the bot — a date and time, say
        }
    }
}
