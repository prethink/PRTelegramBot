using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.Core
{
    public class Handler
    {
        /// <summary>
        /// Клиент телеграм бота
        /// </summary>
        private ITelegramBotClient _botClient;

        /// <summary>
        /// Маршрутизатор
        /// </summary>
        private Router _router;

        public Handler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
            _router = new Router(_botClient);
        }

        /// <summary>
        /// Обработчик обновлений
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="update">Обновление телеграм</param>
        /// <param name="cancellationToken">Токен</param>
        /// <returns></returns>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Unknown:
                        break;
                    case UpdateType.Message:
                        await HandleMessageRoute(update);
                        break;
                    case UpdateType.InlineQuery:
                        break;
                    case UpdateType.ChosenInlineResult:
                        break;
                    case UpdateType.CallbackQuery:
                        await HandleCallbackQuery(update, cancellationToken);
                        break;
                    case UpdateType.EditedMessage:
                        break;
                    case UpdateType.ChannelPost:
                        break;
                    case UpdateType.EditedChannelPost:
                        break;
                    case UpdateType.ShippingQuery:
                        break;
                    case UpdateType.PreCheckoutQuery:
                        break;
                    case UpdateType.Poll:
                        break;
                    case UpdateType.PollAnswer:
                        break;
                    case UpdateType.MyChatMember:
                        break;
                    case UpdateType.ChatMember:
                        break;
                    case UpdateType.ChatJoinRequest:
                        break;
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Обработчик inline callback
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        /// <param name="cancellationToken">Токен</param>
        /// <returns></returns>
        private async Task HandleCallbackQuery(Update update, CancellationToken cancellationToken)
        {
            try
            {
                _router.ExecuteCommandByCallBack(update);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Обработчик ошибок
        /// </summary>
        /// <param name="botClient">Телеграм бот</param>
        /// <param name="exception">Ошибка</param>
        /// <param name="cancellationToken">Токен</param>
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                //TODO Logging exception
                //return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }

        }

        /// <summary>
        /// Маршрутизация сообщений
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        async Task HandleMessageRoute(Update update)
        {
            try
            {
                switch (update.Message.Type)
                {
                    case MessageType.Unknown:
                        break;
                    case MessageType.Text:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Photo:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Audio:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Video:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Voice:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Document:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Sticker:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Location:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Contact:
                        await HandleMessageContact(update);
                        break;
                    case MessageType.Venue:
                        break;
                    case MessageType.Game:
                        break;
                    case MessageType.VideoNote:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Invoice:
                        break;
                    case MessageType.SuccessfulPayment:
                        break;
                    case MessageType.WebsiteConnected:
                        break;
                    case MessageType.ChatMembersAdded:
                        break;
                    case MessageType.ChatMemberLeft:
                        break;
                    case MessageType.ChatTitleChanged:
                        break;
                    case MessageType.ChatPhotoChanged:
                        break;
                    case MessageType.MessagePinned:
                        break;
                    case MessageType.ChatPhotoDeleted:
                        break;
                    case MessageType.GroupCreated:
                        break;
                    case MessageType.SupergroupCreated:
                        break;
                    case MessageType.ChannelCreated:
                        break;
                    case MessageType.MigratedToSupergroup:
                        break;
                    case MessageType.MigratedFromGroup:
                        break;
                    case MessageType.Poll:
                        await HandleMessageText(update);
                        break;
                    case MessageType.Dice:
                        break;
                    case MessageType.MessageAutoDeleteTimerChanged:
                        break;
                    case MessageType.ProximityAlertTriggered:
                        break;
                    case MessageType.WebAppData:
                        break;
                    case MessageType.VideoChatScheduled:
                        break;
                    case MessageType.VideoChatStarted:
                        break;
                    case MessageType.VideoChatEnded:
                        break;
                    case MessageType.VideoChatParticipantsInvited:
                        break;
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }

        }

        /// <summary>
        /// Обработчик текстовый сообщений Reply
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        async Task HandleMessageText(Update update)
        {
            try
            {
                string command = update.Message.Text ?? update.Message.Type.ToString();

                TelegramService.GetInstance().InvokeCommonLog($"Пользователь :{update.GetInfoUser()} написал {command}");
                
                _router.ExecuteCommandByMessage(command, update);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }

        }

        /// <summary>
        /// Обработчик контактов пользователей
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        async Task HandleMessageContact(Update update)
        {
            try
            {
                //TODO: Обратчик контактов
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
    }


}
