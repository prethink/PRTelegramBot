using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;

namespace AspNetWebHook.Services
{
    public class BotHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public BotHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            StartBots();
        }

        private async Task StartBots()
        {
            await Task.Delay(2000);
            var bots = BotCollection.Instance.GetBots();
            foreach (var bot in bots)
            {
                bot.Options.ServiceProvider = serviceProvider;
                bot.ReloadHandlers();
                await bot.Start();

                if (bot.DataRetrieval == DataRetrievalMethod.WebHook)
                {
                    var webHookResult = await((PRBotWebHook)bot)
                        .GetWebHookInfo(bot.Options.CancellationTokenSource.Token);
                    if (!string.IsNullOrEmpty(webHookResult.LastErrorMessage))
                        bot.Events.OnErrorLogInvoke(new Exception(webHookResult.LastErrorMessage));
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            var bots = BotCollection.Instance.GetBots();
            foreach (var bot in bots)
            {
                await bot.Stop();
            }
        }
    }
}
