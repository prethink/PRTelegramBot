using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using PRTelegramBot.Extensions;
using PRTelegramBot.Configs;

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
        public Router Router { get; private set; }

        public Handler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
            Router = new Router(_botClient);
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
                var whitelist = ConfigApp.GetSettingsTelegram<TelegramConfig>().WhiteListUsers;
                if(whitelist?.Count > 0)
                {
                    if(!whitelist.Contains(update.GetChatId()))
                    {
                        await Router.OnAccessDeniedInvoke(_botClient, update);
                        return;
                    }
                }

                switch (update.Type)
                {
                    case UpdateType.Unknown:
                        //TODO: Доделать
                        break;
                    case UpdateType.Message:
                        await HandleMessage(update);
                        break;
                    case UpdateType.InlineQuery:
                        //TODO: Доделать
                        break;
                    case UpdateType.ChosenInlineResult:
                        //TODO: Доделать
                        break;
                    case UpdateType.CallbackQuery:
                        await HandleCallbackQuery(update, cancellationToken);
                        break;
                    case UpdateType.EditedMessage:
                        //TODO: Доделать
                        break;
                    case UpdateType.ChannelPost:
                        //TODO: Доделать
                        break;
                    case UpdateType.EditedChannelPost:
                        //TODO: Доделать
                        break;
                    case UpdateType.ShippingQuery:
                        //TODO: Доделать
                        break;
                    case UpdateType.PreCheckoutQuery:
                        //TODO: Доделать
                        break;
                    case UpdateType.Poll:
                        //TODO: Доделать
                        break;
                    case UpdateType.PollAnswer:
                        //TODO: Доделать
                        break;
                    case UpdateType.MyChatMember:
                        //TODO: Доделать
                        break;
                    case UpdateType.ChatMember:
                        //TODO: Доделать
                        break;
                    case UpdateType.ChatJoinRequest:
                        //TODO: Доделать
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
                Router.ExecuteCommandByCallBack(update);
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
        /// Обработчик текстовый сообщений Reply
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        async Task HandleMessage(Update update)
        {
            try
            {
                string command = update.Message.Text ?? update.Message.Type.ToString();

                TelegramService.GetInstance().InvokeCommonLog($"Пользователь :{update.GetInfoUser()} написал {command}");
                
                Router.ExecuteCommandByMessage(command, update);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
    }


}
