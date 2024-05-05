using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    public interface IExecuteCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public Task ExecuteCommandByMessage(string command, Update update);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public Task ExecuteCommandByCallBack(Update update);
    }
}
