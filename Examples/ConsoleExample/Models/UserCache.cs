using PRTelegramBot.Interfaces;

namespace ConsoleExample.Models
{
    /// <summary>
    /// Cache example.
    /// </summary>
    public class UserCache : ITelegramCache
    {
        public long Id { get; set; }
        /// <summary>
        /// Temporary data
        /// </summary>
        public string Data { get; set; }

        public bool ClearData()
        {
            Data = string.Empty;
            return true;
        }
    }
}
