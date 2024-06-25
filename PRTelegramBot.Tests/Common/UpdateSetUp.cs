using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.Common
{
    public static class UpdateSetUp
    {
        private static long chatId = 123456;
        private static long messageid = 1234568;

        public static Update CreateUpdate()
        {
            var update = new Update();
            return update;
        }

        public static Update CreateUpdateWithTypeMessage()
        {
            var update = CreateUpdate();
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

        #region MessageEvents



        #endregion

        #region Update

        public static Update CreateWithTextMessage(string message)
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Text = message;
            return update;
        }

        public static Update CreateUpdateTypeMessage()
        {
            var update = CreateUpdate();
            update.Message = new Message();
            return update;
        }

        public static Update CreateUpdateTypeEditedMessage()
        {
            var update = CreateUpdate();
            update.EditedMessage = new();
            return update;
        }

        public static Update CreateUpdateTypeChannelPost()
        {
            var update = CreateUpdate();
            update.ChannelPost = new();
            return update;
        }

        public static Update CreateUpdateTypeEditedChannelPost()
        {
            var update = CreateUpdate();
            update.EditedChannelPost = new();
            return update;
        }

        public static Update CreateUpdateTypeBusinessConnection()
        {
            var update = CreateUpdate();
            update.BusinessConnection = new();
            return update;
        }

        public static Update CreateUpdateTypeBusinessMessage()
        {
            var update = CreateUpdate();
            update.BusinessMessage = new();
            return update;
        }

        public static Update CreateUpdateTypeEditedBusinessMessage()
        {
            var update = CreateUpdate();
            update.EditedBusinessMessage = new();
            return update;
        }

        public static Update CreateUpdateTypeDeletedBusinessMessages()
        {
            var update = CreateUpdate();
            update.DeletedBusinessMessages = new();
            return update;
        }

        public static Update CreateUpdateTypeMessageReaction()
        {
            var update = CreateUpdate();
            update.MessageReaction = new();
            return update;
        }

        public static Update CreateUpdateTypeMessageReactionCount()
        {
            var update = CreateUpdate();
            update.MessageReactionCount = new();
            return update;
        }

        public static Update CreateUpdateTypeInlineQuery()
        {
            var update = CreateUpdate();
            update.InlineQuery = new();
            return update;
        }

        public static Update CreateUpdateTypeChosenInlineResult()
        {
            var update = CreateUpdate();
            update.ChosenInlineResult = new();
            return update;
        }

        public static Update CreateUpdateTypeCallbackQuery()
        {
            var update = CreateUpdate();
            update.CallbackQuery = new();
            return update;
        }

        public static Update CreateUpdateTypeShippingQuery()
        {
            var update = CreateUpdate();
            update.ShippingQuery = new();
            return update;
        }

        public static Update CreateUpdateTypePreCheckoutQuery()
        {
            var update = CreateUpdate();
            update.PreCheckoutQuery = new();
            return update;
        }

        public static Update CreateUpdateTypePoll()
        {
            var update = CreateUpdate();
            update.Poll = new();
            return update;
        }

        public static Update CreateUpdateTypePollAnswer()
        {
            var update = CreateUpdate();
            update.PollAnswer = new();
            return update;
        }

        public static Update CreateUpdateTypeMyChatMember()
        {
            var update = CreateUpdate();
            update.MyChatMember = new();
            return update;
        }

        public static Update CreateUpdateTypeChatMember()
        {
            var update = CreateUpdate();
            update.ChatMember = new();
            return update;
        }

        public static Update CreateUpdateTypeChatJoinRequest()
        {
            var update = CreateUpdate();
            update.ChatJoinRequest = new();
            return update;
        }

        public static Update CreateUpdateTypeChatBoost()
        {
            var update = CreateUpdate();
            update.ChatBoost = new();
            return update;
        }

        public static Update CreateUpdateTypeRemovedChatBoost()
        {
            var update = CreateUpdate();
            update.RemovedChatBoost = new();
            return update;
        }

        #endregion
    }
}
