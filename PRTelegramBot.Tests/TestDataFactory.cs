using Moq;
using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests
{
    public static class TestDataFactory
    {
        private static long messageid = 1234568;

        public static Update CreateUpdate()
        {
            var update = new Update();
            return update;
        }

        public static Update CreateUpdateWithTypeMessage(long chatId = 555555)
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

        public static Update CreateMessageTypeVenue()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Venue = new();
            return update;
        }

        public static Update CreateMessageTypeAnimation()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Animation = new();
            return update;
        }

        public static Update CreateMessageTypeText()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Text = "Example text";
            return update;
        }

        public static Update CreateMessageTypeAudio()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Audio = new();
            return update;
        }

        public static Update CreateMessageTypeDocument()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Document = new();
            return update;
        }

        public static Update CreateMessageTypePhoto()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Photo = new List<PhotoSize> { new PhotoSize() }.ToArray();
            return update;
        }

        public static Update CreateMessageTypeSticker()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Sticker = new();
            return update;
        }

        public static Update CreateMessageTypeStory()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Story = new();
            return update;
        }

        public static Update CreateMessageTypeVideo()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Video = new();
            return update;
        }

        public static Update CreateMessageTypeVideoNote()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.VideoNote = new();
            return update;
        }

        public static Update CreateMessageTypeVoice()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Voice = new();
            return update;
        }

        public static Update CreateMessageTypeContact()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Contact = new();
            return update;
        }

        public static Update CreateMessageTypeDice()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Dice = new();
            return update;
        }

        public static Update CreateMessageTypeGame()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Game = new();
            return update;
        }

        public static Update CreateMessageTypePoll()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Poll = new();
            return update;
        }

        public static Update CreateMessageTypeLocation()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Location = new();
            return update;
        }

        public static Update CreateMessageTypeNewChatMembers()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.NewChatMembers = new List<User> { new User() }.ToArray();
            return update;
        }

        public static Update CreateMessageTypeLeftChatMember()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.LeftChatMember = new();
            return update;
        }

        public static Update CreateMessageTypeNewChatTitle()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.NewChatTitle = "New Chat Title";
            return update;
        }

        public static Update CreateMessageTypeNewChatPhoto()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.NewChatPhoto = new List<PhotoSize> { new PhotoSize() }.ToArray();
            return update;
        }

        public static Update CreateMessageTypeDeleteChatPhoto()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.DeleteChatPhoto = true;
            return update;
        }

        public static Update CreateMessageTypeGroupChatCreated()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.GroupChatCreated = true;
            return update;
        }

        public static Update CreateMessageTypeSupergroupChatCreated()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.SupergroupChatCreated = true;
            return update;
        }

        public static Update CreateMessageTypeChannelChatCreated()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ChannelChatCreated = true;
            return update;
        }

        public static Update CreateMessageTypeMessageAutoDeleteTimerChanged()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.MessageAutoDeleteTimerChanged = new();
            return update;
        }

        public static Update CreateMessageTypeMigrateToChatId()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.MigrateToChatId = 12345;
            return update;
        }

        public static Update CreateMessageTypeMigrateFromChatId()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.MigrateFromChatId = 12345;
            return update;
        }

        public static Update CreateMessageTypePinnedMessage()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.PinnedMessage = new();
            return update;
        }

        public static Update CreateMessageTypeInvoice()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Invoice = new();
            return update;
        }

        public static Update CreateMessageTypeSuccessfulPayment()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.SuccessfulPayment = new();
            return update;
        }

        public static Update CreateMessageTypeUsersShared()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.UsersShared = new();
            return update;
        }

        public static Update CreateMessageTypeChatShared()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ChatShared = new();
            return update;
        }

        public static Update CreateMessageTypeConnectedWebsite()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ConnectedWebsite = "example.com";
            return update;
        }

        public static Update CreateMessageTypeWriteAccessAllowed()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.WriteAccessAllowed = new();
            return update;
        }

        public static Update CreateMessageTypePassportData()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.PassportData = new();
            return update;
        }

        public static Update CreateMessageTypeProximityAlertTriggered()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ProximityAlertTriggered = new();
            return update;
        }

        public static Update CreateMessageTypeBoostAdded()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.BoostAdded = new();
            return update;
        }

        public static Update CreateMessageTypeChatBackgroundSet()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ChatBackgroundSet = new();
            return update;
        }

        public static Update CreateMessageTypeForumTopicCreated()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ForumTopicCreated = new();
            return update;
        }

        public static Update CreateMessageTypeForumTopicEdited()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ForumTopicEdited = new();
            return update;
        }

        public static Update CreateMessageTypeForumTopicClosed()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ForumTopicClosed = new();
            return update;
        }

        public static Update CreateMessageTypeForumTopicReopened()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.ForumTopicReopened = new();
            return update;
        }

        public static Update CreateMessageTypeGeneralForumTopicHidden()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.GeneralForumTopicHidden = new();
            return update;
        }

        public static Update CreateMessageTypeGeneralForumTopicUnhidden()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.GeneralForumTopicUnhidden = new();
            return update;
        }

        public static Update CreateMessageTypeGiveawayCreated()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.GiveawayCreated = new();
            return update;
        }

        public static Update CreateMessageTypeGiveaway()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.Giveaway = new();
            return update;
        }

        public static Update CreateMessageTypeGiveawayWinners()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.GiveawayWinners = new();
            return update;
        }

        public static Update CreateMessageTypeGiveawayCompleted()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.GiveawayCompleted = new();
            return update;
        }

        public static Update CreateMessageTypeVideoChatScheduled()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.VideoChatScheduled = new();
            return update;
        }

        public static Update CreateMessageTypeVideoChatStarted()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.VideoChatStarted = new();
            return update;
        }

        public static Update CreateMessageTypeVideoChatEnded()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.VideoChatEnded = new();
            return update;
        }

        public static Update CreateMessageTypeVideoChatParticipantsInvited()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.VideoChatParticipantsInvited = new();
            return update;
        }

        public static Update CreateMessageTypeWebAppData()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.WebAppData = new();
            return update;
        }

        public static Update CreateMessageTypeWebApp()
        {
            var update = CreateUpdateWithTypeMessage();
            update.Message.WebAppData = new();
            return update;
        }

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

        #region Context

        public static IBotContext CreateBotContext()
        {
            var mockContext = new Mock<IBotContext>();
            mockContext.Setup(c => c.Update).Returns(CreateUpdateWithTypeMessage());
            return mockContext.Object;
        }

        #endregion
    }
}
