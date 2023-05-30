namespace PRTelegramBot.Core
{
    /// <summary>
    /// Класс для выполнения фоновых работ
    /// </summary>
    public class Tasker
    {
        /// <summary>
        /// Таймаут в секундах выполнения команды
        /// </summary>
        public int TimeOut { get; set; }

        public Tasker(int timeOut)
        {
            TimeOut = timeOut;
        }

        /// <summary>
        /// Запуск фоновых задач
        /// </summary>
        public async Task Start()
        {
            while (true)
            {
                //TODO: Выполнение фоновой задачи
                await Task.Delay(TimeOut * 1000);
            }
        }
    }
}
