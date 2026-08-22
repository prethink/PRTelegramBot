using PRTelegramBot.Interfaces;
using System.Reflection;
using Telegram.Bot;

namespace PRTelegramBot.Helpers
{
    /// <summary>
    /// Helper for working with files
    /// </summary>
    public static class FileWorker
    {
        /// <summary>
        /// Base directory
        /// </summary>
        public static string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Downloads files from the Telegram servers
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="telegramId">User identifier</param>
        /// <param name="fileId">File identifier</param>
        /// <param name="fileName">File name</param>
        /// <returns>Path to the file</returns>
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
        /// Saves files into the user's folder
        /// </summary>
        /// <param name="telegramId">User identifier</param>
        /// <param name="stream">Stream</param>
        /// <param name="fileName">File name</param>
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
