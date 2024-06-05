using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.Common
{
    public static class UpdateSetUp
    {
        private static long chatId = 123456;
        private static long messageid = 1234568;

        public static Update CreateUpdateWithTypeMessage()
        {
            var update = new Update();
            update.Message = new Message();
            update.Message.Chat = new Chat();
            update.Message.Chat.Id = chatId;
            return update;
        }

        public static Update CreateWithStartDeepLink(string deeplink)
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Text = $"/Start {deeplink}";
            return update;
        }

        public static Update CreateWithTextMessage(string message)
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Text = message;
            return update;
        }
    }
}
