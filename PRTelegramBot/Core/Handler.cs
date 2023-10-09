using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using PRTelegramBot.Extensions;
using PRTelegramBot.Configs;
using static PRTelegramBot.Core.Router;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Core
{
    public class Handler : IUpdateHandler
    {
        /// <summary>
        /// Клиент телеграм бота
        /// </summary>
        private PRBot telegram;

        public event Func<ITelegramBotClient, Update, Task>? OnUpdate;

        /// <summary>
        /// Маршрутизатор
        /// </summary>
        public Router Router { get; private set; }
        public TelegramConfig Config { get; init; }

        public Handler(PRBot botClient, TelegramConfig config)
        {
            telegram = botClient;
            Config = config;
            Router = new Router(telegram, Config);
            
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
                if(Config.WhiteListUsers.Count > 0)
                {
                    if(!Config.WhiteListUsers.Contains(update.GetChatId()))
                    {
                        await Router.OnAccessDeniedInvoke(telegram.botClient, update);
                        return;
                    }
                }

                if(update.Type == UpdateType.Message)
                {
                    await HandleMessage(update);
                    return;
                }

                if(update.Type == UpdateType.CallbackQuery)
                {
                    await HandleCallbackQuery(update, cancellationToken);
                    return;
                }


                await OnUpdate?.Invoke(botClient, update);

            }
            catch (Exception ex)
            {
                telegram.InvokeErrorLog(ex);
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
                telegram.InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Обработчик ошибок
        /// </summary>
        /// <param name="botClient">Телеграм бот</param>
        /// <param name="exception">Ошибка</param>
        /// <param name="cancellationToken">Токен</param>
        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
                telegram.InvokeErrorLog(ex);
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
                if (update.Message.Type == MessageType.Text)
                {
                    telegram.InvokeCommonLog($"Пользователь :{update.GetInfoUser()} написал {command}");
                }
                else
                {
                    telegram.InvokeCommonLog($"Пользователь :{update.GetInfoUser()} отправил команду {command}");
                }
                
                
                Router.ExecuteCommandByMessage(command, update);
            }
            catch (Exception ex)
            {
                telegram.InvokeErrorLog(ex);
            }
        }
    }


}
