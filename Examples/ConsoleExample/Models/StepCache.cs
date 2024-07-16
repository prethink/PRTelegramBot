using PRTelegramBot.Interfaces;

namespace ConsoleExample.Models
{
    /// <summary>
    /// Кэш для пошагового выполнения команд
    /// </summary>
    public class StepCache : ITelegramCache
    {
        public string Name { get; set; }
        public string BirthDay { get; set; }
        public bool ClearData()
        {
            this.BirthDay = string.Empty; 
            this.Name = string.Empty;
            return true;
        }
    }
}
