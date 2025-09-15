using PRTelegramBot.Interfaces;
using System.Reflection;
using Telegram.Bot;

namespace PRTelegramBot.Helpers
{
    /// <summary>
    /// Хелпер для работы с файлами
    /// </summary>
    public static class FileWorker
    {
        /// <summary>
        /// Базовая директория
        /// </summary>
        public static string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Скачивание файлов с telegram серверов
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="fileName">Название файла</param>
        /// <returns>Путь до файла</returns>
        public static async Task<string> DownloadFileFromTelegram(IBotContext context, long telegramId, string fileId, string fileName)
        {
            string folder = Path.Combine("Uploads", "Users", telegramId.ToString());
            string fullPath = Path.Combine(BaseDir, folder, fileName);
            string dbpath = Path.Combine(folder, fileName).Replace('\\', '/');

            Directory.CreateDirectory(Path.Combine(BaseDir, folder));

            await using Stream fileStream = File.OpenWrite(fullPath);
            var file = await context.BotClient.GetInfoAndDownloadFile(
                fileId: fileId,
                destination: fileStream);
            return dbpath;
        }

        /// <summary>
        /// Сохранение файлов в папку пользователя
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <param name="stream">Потом</param>
        /// <param name="fileName">Название файла</param>
        /// <returns></returns>
        public static string SaveFileToUser(long telegramId, MemoryStream stream, string fileName)
        {
            string folder = Path.Combine("Uploads", "Users", telegramId.ToString());
            string fullPath = Path.Combine(BaseDir, folder, fileName);

            Directory.CreateDirectory(Path.Combine(BaseDir, folder));

            File.WriteAllBytes(fullPath, stream.ToArray());
            return fullPath;
        }
    }
}
