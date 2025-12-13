using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleUpdateEvents
    {
        public static async Task OnUpdateMyChatMember(BotEventArgs args)
        {
            //Обработка информации из myChatHandle
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
                        //Другие персонажи
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
             Для примера можно рассмотреть зарегистрирован ли пользователь или нет.
                Если зарегистрирован
                    return UpdateResult.Continue; - данный результат позволит продолжить обработку.
                Если не зарегистрирован то вызвать метод регистрации
                    RegisterMethod();
                    return UpdateResult.Stop или return UpdateResult.Handled - позволит прервать текущую обработку и отправить пользователя на регистрацию
             */
            return UpdateResult.Continue;
        }

        public async static Task Handler_OnPostUpdate(BotEventArgs e)
        {
            // Пример. Регистрация последней активности пользователя в боте. Допустим дата и время
        }
    }
}
